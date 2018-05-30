

namespace TreeViewSandbox.Entities
{

    public class CaseState
    {
        public int CaseStateId { get; set; }
        public string Name { get; set; }

        public virtual System.Collections.Generic.ICollection<CaseStateHistory> CaseStateHistories { get; set; }
        public virtual System.Collections.Generic.ICollection<TaskConfigGroup> TaskConfigGroups { get; set; }
        public virtual System.Collections.Generic.ICollection<TaskConfiguration> TaskConfigurations { get; set; }

        public CaseState()
        {
            CaseStateHistories = new System.Collections.Generic.List<CaseStateHistory>();
            TaskConfigGroups = new System.Collections.Generic.List<TaskConfigGroup>();
            TaskConfigurations = new System.Collections.Generic.List<TaskConfiguration>();
        }
    }

}
