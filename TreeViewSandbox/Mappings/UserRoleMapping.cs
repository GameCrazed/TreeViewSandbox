

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class UserRoleMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserRole>
    {
        public UserRoleMapping()
            : this("dbo")
        {
        }

        public UserRoleMapping(string schema)
        {
            ToTable("UserRoles", schema);
            HasKey(x => x.UserRoleId);

            Property(x => x.UserRoleId).HasColumnName(@"UserRoleId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(max)").IsOptional();
        }
    }

}
