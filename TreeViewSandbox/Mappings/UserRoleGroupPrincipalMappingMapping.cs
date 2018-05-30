

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class UserRoleGroupPrincipalMappingMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserRoleGroupPrincipalMapping>
    {
        public UserRoleGroupPrincipalMappingMapping()
            : this("dbo")
        {
        }

        public UserRoleGroupPrincipalMappingMapping(string schema)
        {
            ToTable("UserRoleGroupPrincipalMappings", schema);
            HasKey(x => new { x.UserRoleId, x.GroupPrincipalGuid });

            Property(x => x.UserRoleId).HasColumnName(@"UserRoleId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.GroupPrincipalGuid).HasColumnName(@"GroupPrincipalGuid").HasColumnType("nvarchar").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            HasRequired(a => a.UserRole).WithMany(b => b.UserRoleGroupPrincipalMappings).HasForeignKey(c => c.UserRoleId);
        }
    }

}
