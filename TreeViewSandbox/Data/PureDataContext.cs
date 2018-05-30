using System.Data.Entity;
using TreeViewSandbox.Mappings;

namespace TreeViewSandbox.Data
{
    public class PureDataContext : DbContext
    {
        static PureDataContext()
        {
            Database.SetInitializer<PureDataContext>(null);
        }

        public PureDataContext() : base("Name=MyDbContext")
        {
        }

        public PureDataContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CaseFilterMapping());
            modelBuilder.Configurations.Add(new CaseFilterTaskConfigurationMapping());
            modelBuilder.Configurations.Add(new CaseStateMapping());
            modelBuilder.Configurations.Add(new CaseStateHistoryMapping());
            modelBuilder.Configurations.Add(new TaskMapping());
            modelBuilder.Configurations.Add(new TaskConfigGroupMapping());
            modelBuilder.Configurations.Add(new TaskConfigSourceMapping());
            modelBuilder.Configurations.Add(new TaskConfigurationMapping());
            modelBuilder.Configurations.Add(new TaskConfigurationsArchivedMapping());
            modelBuilder.Configurations.Add(new TaskResultMapping());
            modelBuilder.Configurations.Add(new UserActivityMapping());
            modelBuilder.Configurations.Add(new UserActivityUserRoleMappingMapping());
            modelBuilder.Configurations.Add(new UserRoleMapping());
            modelBuilder.Configurations.Add(new UserRoleGroupPrincipalMappingMapping());
        }
    }
}
