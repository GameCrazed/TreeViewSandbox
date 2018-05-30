

namespace TreeViewSandbox.Entities
{

    public class UserActivityUserRoleMapping
    {
        public int UserActivityId { get; set; }
        public int UserRoleId { get; set; }


        public virtual UserActivity UserActivity { get; set; }

        public virtual UserRole UserRole { get; set; }
    }

}
