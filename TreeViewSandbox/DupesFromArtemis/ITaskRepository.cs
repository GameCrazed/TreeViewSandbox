using System;
using System.Collections.Generic;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox.DupesFromArtemis
{
    public interface ITaskRepository
    {
        IEnumerable<TaskConfiguration> GetAllActiveConfigurations();
        IEnumerable<Task> ForCase(int kfiId);
        IEnumerable<TaskResult> ForTask(Task task);
        void Save(TaskConfiguration instance);
        void Save(TaskResult taskResult);
        void Save(Task toSet);
        IEnumerable<TaskResult> ResultsForCase(int kfiId);
        IResult SoftDelete(TaskConfiguration instance, string user);
        IResult HardDelete(TaskConfiguration instance);
        IEnumerable<TaskConfiguration> GetAllDeletedConfigurations();
        IEnumerable<Task> GetAllTasksThatAreTenDaysOldBy(bool completedStatus, int period);
        IEnumerable<Task> GetAllExpiredPendingTasks();
        IEnumerable<TaskConfiguration> GetAllActiveManualConfigurations();
        Task GetByTaskId(int id);
        int CountNecroMatches(int kfiId, TaskConfiguration configuration, DateTime dueDate);
        string GetCaseStateName(int caseStateId);
    }
}
