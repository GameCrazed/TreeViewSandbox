

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class UserActivityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserActivity>
    {
        public UserActivityMapping()
            : this("dbo")
        {
        }

        public UserActivityMapping(string schema)
        {
            ToTable("UserActivities", schema);
            HasKey(x => x.UserActivityId);

            Property(x => x.UserActivityId).HasColumnName(@"UserActivityId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(max)").IsOptional();
        }
    }

}
