

namespace TreeViewSandbox.Entities
{

    public class UserRoleGroupPrincipalMapping
    {
        public int UserRoleId { get; set; }
        public string GroupPrincipalGuid { get; set; }


        public virtual UserRole UserRole { get; set; }
    }

}
