

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class TaskConfigGroupMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TaskConfigGroup>
    {
        public TaskConfigGroupMapping()
            : this("dbo")
        {
        }

        public TaskConfigGroupMapping(string schema)
        {
            ToTable("TaskConfigGroups", schema);
            HasKey(x => x.TaskConfigGroupId);

            Property(x => x.TaskConfigGroupId).HasColumnName(@"TaskConfigGroupId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.ParentTaskConfigId).HasColumnName(@"ParentTaskConfigId").HasColumnType("int").IsOptional();
            Property(x => x.ParentCaseStateId).HasColumnName(@"ParentCaseStateId").HasColumnType("int").IsOptional();
            Property(x => x.Depth).HasColumnName(@"Depth").HasColumnType("int").IsRequired();

            HasOptional(a => a.CaseState).WithMany(b => b.TaskConfigGroups).HasForeignKey(c => c.ParentCaseStateId).WillCascadeOnDelete(false);
            HasOptional(a => a.TaskConfiguration).WithMany(b => b.TaskConfigGroups).HasForeignKey(c => c.ParentTaskConfigId).WillCascadeOnDelete(false);
        }
    }

}
