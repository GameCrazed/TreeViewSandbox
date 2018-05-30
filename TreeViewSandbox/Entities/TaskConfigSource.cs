

using System.Diagnostics;

namespace TreeViewSandbox.Entities
{
    [DebuggerDisplay("TaskCfgGrp: {TaskConfigGroupId} ==> {TaskConfigurationId}")]
    public class TaskConfigSource
    {
        public int TaskConfigGroupId { get; set; }
        public int TaskConfigurationId { get; set; }


        public virtual TaskConfigGroup TaskConfigGroup { get; set; }

        public virtual TaskConfiguration TaskConfiguration { get; set; }
    }

}
