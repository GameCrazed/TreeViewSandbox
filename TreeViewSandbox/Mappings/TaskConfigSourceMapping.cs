

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class TaskConfigSourceMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TaskConfigSource>
    {
        public TaskConfigSourceMapping()
            : this("dbo")
        {
        }

        public TaskConfigSourceMapping(string schema)
        {
            ToTable("TaskConfigSources", schema);
            HasKey(x => new { x.TaskConfigGroupId, x.TaskConfigurationId });

            Property(x => x.TaskConfigGroupId).HasColumnName(@"TaskConfigGroupId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.TaskConfigurationId).HasColumnName(@"TaskConfigurationId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            HasRequired(a => a.TaskConfigGroup).WithMany(b => b.TaskConfigSources).HasForeignKey(c => c.TaskConfigGroupId).WillCascadeOnDelete(false);
            HasRequired(a => a.TaskConfiguration).WithMany(b => b.TaskConfigSources).HasForeignKey(c => c.TaskConfigurationId).WillCascadeOnDelete(false);
        }
    }

}
