

namespace TreeViewSandbox.Entities
{

    public class Task
    {
        public int KfiId { get; set; }
        public int TaskId { get; set; }
        public string Body { get; set; }
        public int TaskConfigurationId { get; set; }
        public System.DateTime DueDate { get; set; }
        public int Sequence { get; set; }
        public string Title { get; set; }
        public int State { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool HasBeenDeferred { get; set; }

        public virtual TaskResult TaskResult { get; set; }


        public virtual TaskConfiguration TaskConfiguration { get; set; }

        public Task()
        {
            HasBeenDeferred = false;
        }
    }

}
