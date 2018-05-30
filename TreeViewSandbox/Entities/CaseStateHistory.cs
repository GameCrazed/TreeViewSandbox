

namespace TreeViewSandbox.Entities
{

    public class CaseStateHistory
    {
        public int KfiId { get; set; }
        public int CaseStateId { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }


        public virtual CaseState CaseState { get; set; }
    }

}
