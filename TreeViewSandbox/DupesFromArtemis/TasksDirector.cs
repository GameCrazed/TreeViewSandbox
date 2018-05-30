using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox.DupesFromArtemis
{
    public class TasksDirector : ITasksDirector
    {
        private readonly TaskRepository _repository;

        public TasksDirector(TaskRepository repo)
        {
            _repository = repo;
        }

        public IEnumerable<TaskConfiguration> All()
        {
            return _repository.GetAllActiveConfigurations().ForEach(x => x.MapToTaskConfiguration());
        }

        public IEnumerable<TaskConfiguration> AllDeleted()
        {
            return _repository.GetAllDeletedConfigurations().ForEach(x => x.MapToTaskConfiguration());
        }

        public void Save(TaskConfiguration instance)
        {
            _repository.Save(instance.MapToTaskConfigEntity());
        }

        public IResult MarkAsDeleted(TaskConfiguration instance, string user)
        {
            return _repository.SoftDelete(instance.MapToTaskConfigEntity(), user);
        }

        public void CompleteTask(Task task, string title, string body)
        {
            _repository.Save(CreateTaskResult(task, title, body).MapToTaskResultEntity());
        }

        private TaskResult CreateTaskResult(Task task, string title, string body)
        {
            return new TaskResult
            {
                Body = body,
                Created = DateTime.Now,
                KfiId = task.KfiId,
                TaskId = task.TaskId,
                Title = title,
                CreatedBy = WindowsIdentity.GetCurrent().Name
            };
        }

        public Task CreateTask(Case Case, TaskConfiguration taskConfiguration)
        {
            var task = new Task
            {
                KfiId = Case.KfiId,
                TaskConfiguration = taskConfiguration,
                Title = taskConfiguration.Title,
                Body = taskConfiguration.Body,
                Sequence = taskConfiguration.Sequence,
                DueDate = DateTime.Now.AddDays(taskConfiguration.LeadTimeDays)
            };
            _repository.Save(task.MapToTaskEntity());
            return task;
        }

        public Task CreateTask(int kfiId, TaskConfiguration taskConfiguration)
        {
            var task = new Task
            {
                KfiId = kfiId,
                TaskConfiguration = taskConfiguration,
                Title = taskConfiguration.Title,
                Body = taskConfiguration.Body,
                Sequence = taskConfiguration.Sequence,
                DueDate = DateTime.Now.AddDays(taskConfiguration.LeadTimeDays)
            };
            _repository.Save(task.MapToTaskEntity());
            return task;
        }

        public void DeferTask(Task task, string title, string body, DateTime dueDate)
        {
            var config = OverrideBodyWithDeferredText(task, body);

            _repository.Save(CreateTaskResult(task, title, body).MapToTaskResultEntity());

            CreateTask(task.KfiId, config, dueDate, true);
        }

        private static TaskConfiguration OverrideBodyWithDeferredText(Task task, string body)
        {
            var config = task.TaskConfiguration;
            if (!string.IsNullOrWhiteSpace(body))
            {
                config.Body = body;
            }

            return config;
        }

        public void CreateTask(int kfiId, TaskConfiguration configuration, DateTime dueDate, bool deferred)
        {
            var task = new Task
            {
                KfiId = kfiId,
                HasBeenDeferred = deferred,
                TaskConfiguration = configuration,
                Title = configuration.Title,
                Body = configuration.Body,
                Sequence = configuration.Sequence,
                Created = DateTime.Now,
                CreatedBy = WindowsIdentity.GetCurrent().Name,
                DueDate = dueDate,
            };
            _repository.Save(task.MapToTaskEntity());
        }

        public void CreateTask(int kfiId, TaskConfiguration configuration, DateTime dueDate)
        {
            var task = new Task
            {
                KfiId = kfiId,
                TaskConfiguration = configuration,
                Title = configuration.Title,
                Body = configuration.Body,
                Sequence = configuration.Sequence,
                Created = DateTime.Now,
                CreatedBy = WindowsIdentity.GetCurrent().Name,
                DueDate = dueDate,
            };
            _repository.Save(task.MapToTaskEntity());
        }

        public Task CreateTask(Case Case, TaskConfiguration taskConfiguration, int leadTime)
        {
            var task = new Task
            {
                KfiId = Case.KfiId,
                TaskConfiguration = taskConfiguration,
                Title = taskConfiguration.Title,
                Body = taskConfiguration.Body,
                Sequence = taskConfiguration.Sequence,
                DueDate = DateTime.Now.AddDays(leadTime)
            };
            _repository.Save(task.MapToTaskEntity());
            return task;
        }

        public void DeferTask(Case Case, Task task, string title, string body)
        {
            _repository.Save(CreateTaskResult(task, title, body).MapToTaskResultEntity());
            CreateTask(Case, task.TaskConfiguration);
        }

        public IEnumerable<Task> ForCase(int kfiId)
        {
            return _repository.ForCase(kfiId).ForEach(x => x.MapToTask());
        }

        public IEnumerable<TaskConfiguration> GetConfigurations()
        {
            return _repository.GetAllActiveConfigurations().ForEach(x => x.MapToTaskConfiguration());
        }

        public IEnumerable<TaskConfiguration> GetConfigurations(int caseStateId)
        {
            return _repository.GetAllActiveConfigurations().Where(c => c.CaseStateId == caseStateId)
                .ForEach(x => x.MapToTaskConfiguration());
        }

        public IEnumerable<TaskConfiguration> GetManualConfigurations()
        {
            return _repository.GetAllActiveManualConfigurations().ForEach(x => x.MapToTaskConfiguration());
        }

        public IEnumerable<TaskConfiguration> GetManualConfigurations(int caseStateId)
        {
            return _repository.GetAllActiveManualConfigurations()
                .Where(c => c.CaseStateId == caseStateId).ForEach(x => x.MapToTaskConfiguration());
        }

        public Task GetByTaskId(int historyItemTaskIdToNecro)
        {
            return _repository.GetByTaskId(historyItemTaskIdToNecro).MapToTask();
        }

        public IEnumerable<Task> Pending(int kfiId, List<TaskConfiguration> caseFilterTaskConfigurations,
            bool hasBeenDeferred = false)
        {
            // deferred items to today's date will now show up on the Outstanding list tab on the GUI.
            return ForCase(kfiId).Where(c => c.DueDate <= DateTime.Now && c.TaskResult == null)
                .ToList();
        }

        public IEnumerable<Task> Pending(int kfiId, bool hasBeenDeferred = false)
        {
            // deferred items to today's date will now show up on the Outstanding list tab on the GUI.
            return ForCase(kfiId).Where(c => c.DueDate.Date <= DateTime.Now && c.TaskResult == null)
                .ToList();
        }

        public IEnumerable<Task> GetAllDeferredTasks(int kfiId)
        {
            return ForCase(kfiId).Where(c => c.TaskResult == null
                                             && c.HasBeenDeferred && c.DueDate.Date > DateTime.Now)
                .ToList();
        }

        public int GetAllDeferredTasksCount(int kfiId)
        {
            return GetAllDeferredTasks(kfiId).Count();
        }

        public string GetCaseStateName(int caseStateId)
        {
            return _repository.GetCaseStateName(caseStateId);
        }

        public IEnumerable<TaskResult> Results(int kfiId)
        {
            return _repository.ResultsForCase(kfiId).ForEach(x => x.MapToTaskResult());
        }

        public IEnumerable<Task> Pending(int kfiId, IEnumerable<TaskConfiguration> configurations)
        {
            return Pending(kfiId).Where(task =>
                configurations.Any(c => c.TaskConfigurationId == task.TaskConfiguration.TaskConfigurationId));
        }

        public IEnumerable<Task> GetAllIncompleteTasksThatAreTenDaysOld(int period)
        {
            return _repository.GetAllTasksThatAreTenDaysOldBy(false, period).ForEach(x => x.MapToTask());
        }

        public IEnumerable<Task> GetAllExpiredPendingTasks()
        {
            return _repository.GetAllExpiredPendingTasks().ForEach(x => x.MapToTask());
        }
    }
}