

namespace TreeViewSandbox.Entities
{

    public class UserActivity
    {
        public int UserActivityId { get; set; }
        public string Name { get; set; }

        public virtual System.Collections.Generic.ICollection<UserActivityUserRoleMapping> UserActivityUserRoleMappings { get; set; }

        public UserActivity()
        {
            UserActivityUserRoleMappings = new System.Collections.Generic.List<UserActivityUserRoleMapping>();
        }
    }

}
