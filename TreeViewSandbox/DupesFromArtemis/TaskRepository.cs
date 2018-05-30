using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using TreeViewSandbox.Data;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox.DupesFromArtemis
{
    public class TaskRepository : ITaskRepository
    {
        private const string DevDb = "Server=PRSQ02;Database=DEV_pureData;Trusted_Connection=True;";
        private readonly string _pureDataConnectionString;
        private readonly ISystemTime _time;

        public TaskRepository(PureConnectionString pureConnectionString)
        {
            _pureDataConnectionString = pureConnectionString ?? DevDb;
            _time = new SystemTime();
        }

        public IResult HardDelete(TaskConfiguration instance)
        {
            try
            {
                using (var ctx = new PureDataContext(_pureDataConnectionString))
                {
                    ctx.Set<TaskConfiguration>().RemoveRange(
                        from m in ctx.Set<TaskConfiguration>()
                        where m.TaskConfigurationId == instance.TaskConfigurationId
                        select m
                    );
                    ctx.SaveChanges();
                }
                return new ResultFactory().Ok();
            }
            catch (Exception ex)
            {
                return new ResultFactory().Error(ex);
            }
        }

        public IResult SoftDelete(TaskConfiguration instance, string user)
        {
            try
            {
                using (var ctx = new PureDataContext(_pureDataConnectionString))
                {
                    var archive = new TaskConfigurationsArchived
                    {
                        TaskConfigurationId = instance.TaskConfigurationId,
                        DeletedBy = user,
                        IsDeleted = true,
                        DeletedOn = DateTime.Now
                    };

                    ctx.Set<TaskConfigurationsArchived>().Add(archive);

                    instance.TaskConfigurationsArchived = archive;

                    ctx.SaveChanges();
                }
                return new ResultFactory().Ok();
            }
            catch (Exception ex)
            {
                return new ResultFactory().Error(ex);
            }
        }

        public IEnumerable<TaskConfiguration> GetAllActiveConfigurations()
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<TaskConfiguration>()
                    .Include(i => i.CaseState)
                    .Include(i => i.TaskConfigurationsArchived)
                    .Where(x => x.TaskConfigurationsArchived == null)
                    .OrderByDescending(c => c.TaskConfigurationId)
                    .ToList();
            }
        }

        public IEnumerable<TaskConfiguration> GetAllActiveManualConfigurations()
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<TaskConfiguration>()
                    .Include(i => i.CaseState)
                    .Include(i => i.TaskConfigurationsArchived)
                    .Where(x => x.TaskConfigurationsArchived == null
                                && x.IsManual)
                    .OrderByDescending(c => c.TaskConfigurationId)
                    .ToList();
            }
        }

        /// <summary>
        /// GetByTaskId is light EF method as in it does not pull in all of its related 
        /// navigation properties.
        /// </summary>
        /// <param name="id">Id of task you want to find</param>
        /// <returns>Found Entity.</returns>
        public Task GetByTaskId(int id)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<Task>()
                    .Include(x => x.TaskConfiguration)
                    .Include(x => x.TaskResult)
                    .SingleOrDefault(x => x.TaskId == id);
            }
        }

        public int CountNecroMatches(int kfiId, TaskConfiguration configuration, DateTime dueDate)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<Task>().Count(
                    x => x.KfiId == kfiId
                         && x.TaskConfigurationId == configuration.TaskConfigurationId
                         && x.DueDate == dueDate
                         && x.Title == configuration.Title
                         && x.Body == configuration.Body);
            }
        }

        public IEnumerable<TaskConfiguration> GetAllDeletedConfigurations()
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<TaskConfiguration>()
                    .Include(i => i.CaseState)
                    .Include(i => i.TaskConfigurationsArchived)
                    .Where(x => x.TaskConfigurationsArchived != null
                                && x.TaskConfigurationsArchived.IsDeleted)
                    .OrderByDescending(c => c.TaskConfigurationId)
                    .ToList();
            }
        }

        private IEnumerable<Task> GetAllTasksThatAreTenDaysOld(int period)
        {
            var tenDaysFromNow = _time.Now.AddDays(-period);
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                var results = from i in ctx.Set<Task>().Include(x => x.TaskResult)
                              where i.Created <= tenDaysFromNow && i.TaskConfiguration.CaseStateId == 1
                              select i;
                return results.ToList();
            }
        }

        public IEnumerable<Task> GetAllTasksThatAreTenDaysOldBy(bool completedStatus, int period)
        {
            var allDiscoveredTasks = GetAllTasksThatAreTenDaysOld(period);

            return completedStatus
                ? allDiscoveredTasks.Where(x => x.TaskResult != null)
                : allDiscoveredTasks.Where(x => x.TaskResult == null);
        }

        public IEnumerable<Task> GetAllExpiredPendingTasks()
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                var qry =
                    from hh in ctx.Set<CaseStateHistory>()
                    group hh by hh.KfiId into histories
                    let top = histories.OrderByDescending(h => h.Created)
                                       .ThenByDescending(h => h.CaseStateId)
                                       .FirstOrDefault()
                    join i in ctx.Set<Task>() on top.KfiId equals i.KfiId
                    join ic in ctx.Set<TaskConfiguration>() on i.TaskConfigurationId equals
                    ic.TaskConfigurationId
                    join ir in ctx.Set<TaskResult>() on new { i.KfiId, i.TaskId } equals
                    new { ir.KfiId, ir.TaskId }
                    into results
                    from ir in results.DefaultIfEmpty()
                    where ir == null && ic.CaseStateId < top.CaseStateId
                    select i;

                return qry.ToList();
            }
        }

        public void Save(TaskConfiguration instance)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                if (
                    ctx.Set<TaskConfiguration>().Any(
                        c => c.TaskConfigurationId == instance.TaskConfigurationId))
                {
                    ctx.Set<TaskConfiguration>().Attach(instance);
                    ctx.Entry(instance).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                else
                {
                    var taskConfig = new TaskConfiguration
                    {
                        Body = instance.Body,
                        Title = instance.Title,
                        LeadTimeDays = instance.LeadTimeDays,
                        Sequence = instance.Sequence,
                        CaseStateId = instance.CaseStateId,
                        IsManual = instance.IsManual
                    };
                    ctx.Set<TaskConfiguration>().Add(taskConfig);
                    ctx.SaveChanges();
                }
            }
        }

        public IEnumerable<Task> ForCase(int kfiId)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<Task>()
                    .Include(i => i.TaskConfiguration)
                    .Include(i => i.TaskConfiguration.TaskConfigurationsArchived)
                    .Include(i => i.TaskResult)
                    .Where(i => i.KfiId == kfiId && i.TaskConfiguration.TaskConfigurationsArchived == null)
                    .ToList();
            }
        }

        public void Save(TaskResult taskResult)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                var kfiId = taskResult.KfiId;
                var taskId = taskResult.TaskId;

                if (ctx.Set<TaskResult>().Any(ir => ir.KfiId == kfiId && ir.TaskId == taskId))
                {
                    throw new InvalidOperationException("The Task has already been completed.");
                }

                TaskResult instance = new TaskResult()
                {
                    Body = taskResult.Body,
                    Title = taskResult.Title,
                    Created = DateTime.Now,
                    CreatedBy = taskResult.CreatedBy,
                    KfiId = kfiId,
                    TaskId = taskId
                };

                ctx.Set<TaskResult>().Add(instance);
                ctx.SaveChanges();
            }
        }

        public void Save(Task toSet)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                var task = new Task
                {
                    Body = toSet.Body,
                    DueDate = toSet.DueDate,
                    TaskConfigurationId = toSet.TaskConfiguration.TaskConfigurationId,
                    KfiId = toSet.KfiId,
                    Sequence = toSet.Sequence,
                    Title = toSet.Title,
                    HasBeenDeferred = toSet.HasBeenDeferred,
                    Created = DateTime.Now,
                    CreatedBy = WindowsIdentity.GetCurrent().Name
                };
                ctx.Set<Task>().Add(task);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<TaskResult> ForTask(Task task)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<TaskResult>().Where(i => i.TaskId == task.TaskId).ToList();
            }
        }

        public IEnumerable<TaskResult> ResultsForCase(int kfiId)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<TaskResult>().
                    Where(r => r.KfiId == kfiId).
                    ToList();
            }
        }

        public string GetCaseStateName(int caseStateId)
        {
            using (var ctx = new PureDataContext(_pureDataConnectionString))
            {
                return ctx.Set<CaseState>().SingleOrDefault(x => x.CaseStateId == caseStateId)?.Name;
            }
        }
    }
}