using System.Collections.Generic;
using System.Diagnostics;

namespace TreeViewSandbox.Entities
{
    [DebuggerDisplay("STATE: {CaseState?.Name} ==> ParentID: {ParentTaskConfigId} / TaskGroupId:{TaskConfigGroupId}")]
    public sealed class TaskConfigGroup
    {
        public int TaskConfigGroupId { get; set; }
        public int? ParentTaskConfigId { get; set; }
        public int? ParentCaseStateId { get; set; }
        public int Depth { get; set; }

        public ICollection<TaskConfigSource> TaskConfigSources { get; set; }


        public CaseState CaseState { get; set; }

        public TaskConfiguration TaskConfiguration { get; set; }

        public TaskConfigGroup()
        {
            TaskConfigSources = new List<TaskConfigSource>();
        }
    }

}
