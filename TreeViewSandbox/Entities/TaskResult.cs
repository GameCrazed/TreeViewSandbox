

namespace TreeViewSandbox.Entities
{

    public class TaskResult
    {
        public int KfiId { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }


        public virtual Task Task { get; set; }
    }

}
