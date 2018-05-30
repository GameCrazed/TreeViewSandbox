

namespace TreeViewSandbox.Entities
{

    public class TaskConfigurationsArchived
    {
        public int TaskConfigurationId { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTime DeletedOn { get; set; }


        public virtual TaskConfiguration TaskConfiguration { get; set; }
    }

}
