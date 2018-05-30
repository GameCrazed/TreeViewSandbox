

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class TaskConfigurationsArchivedMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TaskConfigurationsArchived>
    {
        public TaskConfigurationsArchivedMapping()
            : this("dbo")
        {
        }

        public TaskConfigurationsArchivedMapping(string schema)
        {
            ToTable("TaskConfigurationsArchiveds", schema);
            HasKey(x => x.TaskConfigurationId);

            Property(x => x.TaskConfigurationId).HasColumnName(@"TaskConfigurationId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            Property(x => x.DeletedBy).HasColumnName(@"DeletedBy").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.DeletedOn).HasColumnName(@"DeletedOn").HasColumnType("datetime").IsRequired();

            HasRequired(a => a.TaskConfiguration).WithOptional(b => b.TaskConfigurationsArchived).WillCascadeOnDelete(false);
        }
    }

}
