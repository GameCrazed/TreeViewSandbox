using System;
using System.Collections.Generic;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox.DupesFromArtemis
{
    /// <summary>
    /// Creates and Queries Tasks for CaseDetailsDecoratedRepository.
    /// </summary>
    public interface ITasksDirector
    {
        IEnumerable<TaskConfiguration> All();
        IEnumerable<TaskConfiguration> AllDeleted();
        void Save(TaskConfiguration instance);
        IResult MarkAsDeleted(TaskConfiguration instance, string user);
        void CompleteTask(Task task, string title, string body);
        Task CreateTask(Case Case, TaskConfiguration taskConfiguration);
        Task CreateTask(int kfiId, TaskConfiguration taskConfiguration);
        void DeferTask(Task task, string title, string body, DateTime dueDate);
        Task CreateTask(Case Case, TaskConfiguration taskConfiguration, int leadTime);
        void DeferTask(Case Case, Task task, string title, string body);
        IEnumerable<Task> ForCase(int kfiId);
        IEnumerable<TaskConfiguration> GetConfigurations();
        IEnumerable<TaskConfiguration> GetConfigurations(int caseStateId);
        IEnumerable<Task> Pending(int kfiId, List<TaskConfiguration> caseFilterTaskConfigurations, bool hasBeenDeferred = false);
        IEnumerable<TaskResult> Results(int kfiId);
        IEnumerable<Task> Pending(int kfiId, IEnumerable<TaskConfiguration> configurations);
        IEnumerable<Task> GetAllIncompleteTasksThatAreTenDaysOld(int period);
        IEnumerable<Task> GetAllExpiredPendingTasks();
        IEnumerable<TaskConfiguration> GetManualConfigurations();
        IEnumerable<TaskConfiguration> GetManualConfigurations(int caseStateId);
        Task GetByTaskId(int historyItemTaskIdToNecro);
        void CreateTask(int kfiId, TaskConfiguration configuration, DateTime dueDate);
        IEnumerable<Task> GetAllDeferredTasks(int kfiId);
        void CreateTask(int kfiId, TaskConfiguration configuration, DateTime dueDate, bool deferred);
        int GetAllDeferredTasksCount(int kfiId);
        string GetCaseStateName(int caseStateId);
    }
}