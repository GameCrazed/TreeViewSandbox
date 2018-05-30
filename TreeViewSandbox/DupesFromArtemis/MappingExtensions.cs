using TreeViewSandbox.Entities;

namespace TreeViewSandbox.DupesFromArtemis
{
    public static class MappingExtensions
    {
        public static TaskConfiguration MapToTaskConfiguration(this TaskConfiguration configEntity)
        {
            return new TaskConfiguration
            {
                Sequence = configEntity.Sequence,
                Title = configEntity.Title,
                Body = configEntity.Body,
                CaseState = configEntity.CaseState,
                CaseStateId = configEntity.CaseStateId,
                IsManual = configEntity.IsManual,
                LeadTimeDays = configEntity.LeadTimeDays,
                TaskConfigurationId = configEntity.TaskConfigurationId,
                TaskConfigurationsArchived = configEntity.TaskConfigurationsArchived
            };
        }

        public static TaskConfiguration MapToTaskConfigEntity(this TaskConfiguration config)
        {
            return new TaskConfiguration
            {
                Title = config.Title,
                Body = config.Body,
                Sequence = config.Sequence,
                TaskConfigurationId = config.TaskConfigurationId,
                LeadTimeDays = config.LeadTimeDays,
                CaseState = config.CaseState,
                IsManual = config.IsManual,
                CaseStateId = config.CaseStateId,
                TaskConfigurationsArchived = config.TaskConfigurationsArchived,
            };
        }

        public static TaskResult MapToTaskResultEntity(this TaskResult task)
        {
            return new TaskResult
            {
                Body = task.Body,
                Title = task.Title,
                TaskId = task.TaskId,
                CreatedBy = task.CreatedBy,
                Created = task.Created,
                KfiId = task.KfiId,
            };
        }

        public static Task MapToTaskEntity(this Task task)
        {
            return new Task
            {
                TaskConfiguration = MapToTaskConfigEntity(task.TaskConfiguration),
                Title = task.Title,
                Body = task.Body,
                KfiId = task.KfiId,
                CreatedBy = task.CreatedBy,
                TaskId = task.TaskId,
                Created = task.Created,
                Sequence = task.Sequence,
                DueDate = task.DueDate,
                HasBeenDeferred = task.HasBeenDeferred
            };
        }

        public static Task MapToTask(this Task taskEntity)
        {
            return new Task
            {
                KfiId = taskEntity.KfiId,
                Body = taskEntity.Body,
                TaskConfiguration = MapToTaskConfiguration(taskEntity.TaskConfiguration),
                Created = taskEntity.Created,
                CreatedBy = taskEntity.CreatedBy,
                DueDate = taskEntity.DueDate,
                HasBeenDeferred = taskEntity.HasBeenDeferred,
                Sequence = taskEntity.Sequence,
                TaskId = taskEntity.TaskId,
                Title = taskEntity.Title,
                TaskResult = taskEntity.TaskResult?.MapToTaskResult()
            };
        }

        public static TaskResult MapToTaskResult(this TaskResult task)
        {
            return new TaskResult
            {
                KfiId = task.KfiId,
                Body = task.Body,
                Title = task.Title,
                CreatedBy = task.CreatedBy,
                TaskId = task.TaskId,
                Created = task.Created
            };
        }
    }
}
