

namespace TreeViewSandbox.Entities
{

    public class CaseFilter
    {
        public int CaseFilterId { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public int Sequence { get; set; }
        public int UserRoleId { get; set; }

        public virtual System.Collections.Generic.ICollection<CaseFilterTaskConfiguration> CaseFilterTaskConfigurations { get; set; }


        public virtual UserRole UserRole { get; set; }

        public CaseFilter()
        {
            Sequence = 0;
            UserRoleId = 0;
            CaseFilterTaskConfigurations = new System.Collections.Generic.List<CaseFilterTaskConfiguration>();
        }
    }

}
