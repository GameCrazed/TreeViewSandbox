

namespace TreeViewSandbox.Entities
{

    public class TaskConfiguration
    {
        public int TaskConfigurationId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int LeadTimeDays { get; set; }
        public int Sequence { get; set; }
        public int CaseStateId { get; set; }
        public bool IsManual { get; set; }

        public virtual System.Collections.Generic.ICollection<CaseFilterTaskConfiguration> CaseFilterTaskConfigurations { get; set; }
        public virtual System.Collections.Generic.ICollection<Task> Tasks { get; set; }
        public virtual System.Collections.Generic.ICollection<TaskConfigGroup> TaskConfigGroups { get; set; }
        public virtual System.Collections.Generic.ICollection<TaskConfigSource> TaskConfigSources { get; set; }
        public virtual TaskConfigurationsArchived TaskConfigurationsArchived { get; set; }


        public virtual CaseState CaseState { get; set; }

        public TaskConfiguration()
        {
            IsManual = false;
            Tasks = new System.Collections.Generic.List<Task>();
            TaskConfigGroups = new System.Collections.Generic.List<TaskConfigGroup>();
            TaskConfigSources = new System.Collections.Generic.List<TaskConfigSource>();
            CaseFilterTaskConfigurations = new System.Collections.Generic.List<CaseFilterTaskConfiguration>();
        }
    }

}
