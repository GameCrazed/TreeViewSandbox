using System.Collections.Generic;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Dtos
{
    public class TreeParent
    {
        public TreeParent()
        {
        }

        public virtual CaseState CaseState { get; set; }

        public virtual List<TreeSiblings> Children { get; set; }

    }
}