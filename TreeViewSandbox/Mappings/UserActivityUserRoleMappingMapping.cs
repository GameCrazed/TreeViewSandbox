

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class UserActivityUserRoleMappingMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserActivityUserRoleMapping>
    {
        public UserActivityUserRoleMappingMapping()
            : this("dbo")
        {
        }

        public UserActivityUserRoleMappingMapping(string schema)
        {
            ToTable("UserActivityUserRoleMappings", schema);
            HasKey(x => new { x.UserActivityId, x.UserRoleId });

            Property(x => x.UserActivityId).HasColumnName(@"UserActivityId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.UserRoleId).HasColumnName(@"UserRoleId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            HasRequired(a => a.UserActivity).WithMany(b => b.UserActivityUserRoleMappings).HasForeignKey(c => c.UserActivityId);
            HasRequired(a => a.UserRole).WithMany(b => b.UserActivityUserRoleMappings).HasForeignKey(c => c.UserRoleId);
        }
    }

}
