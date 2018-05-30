

namespace TreeViewSandbox.Entities
{

    public class UserRole
    {
        public int UserRoleId { get; set; }
        public string Name { get; set; }

        public virtual System.Collections.Generic.ICollection<CaseFilter> CaseFilters { get; set; }
        public virtual System.Collections.Generic.ICollection<UserActivityUserRoleMapping> UserActivityUserRoleMappings { get; set; }
        public virtual System.Collections.Generic.ICollection<UserRoleGroupPrincipalMapping> UserRoleGroupPrincipalMappings { get; set; }

        public UserRole()
        {
            UserActivityUserRoleMappings = new System.Collections.Generic.List<UserActivityUserRoleMapping>();
            UserRoleGroupPrincipalMappings = new System.Collections.Generic.List<UserRoleGroupPrincipalMapping>();
            CaseFilters = new System.Collections.Generic.List<CaseFilter>();
        }
    }

}
