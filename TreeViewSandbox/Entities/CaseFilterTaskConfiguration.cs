

namespace TreeViewSandbox.Entities
{

    public class CaseFilterTaskConfiguration
    {
        public int TaskConfigurationId { get; set; }
        public int CaseFilterId { get; set; }


        public virtual CaseFilter CaseFilter { get; set; }

        public virtual TaskConfiguration TaskConfiguration { get; set; }
    }

}
