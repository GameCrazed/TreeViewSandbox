using System.Collections;
using System.Collections.Generic;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Dtos
{
    public class TreeSiblings : IEnumerable
    {
        public TreeSiblings()
        {
            Depth = 0;
        }

        public int Depth { get; set; }
        public virtual TreeParent CaseParent { get; set; }
        public virtual TreeSiblings TaskParent { get; set; }
        public virtual TaskConfigSource Node { get; set; }
        public virtual List<TreeSiblings> Children { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}