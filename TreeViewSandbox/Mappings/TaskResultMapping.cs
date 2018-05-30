

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class TaskResultMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TaskResult>
    {
        public TaskResultMapping()
            : this("dbo")
        {
        }

        public TaskResultMapping(string schema)
        {
            ToTable("TaskResults", schema);
            HasKey(x => new { x.KfiId, x.TaskId });

            Property(x => x.KfiId).HasColumnName(@"KfiId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.TaskId).HasColumnName(@"TaskId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.Body).HasColumnName(@"Body").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.Created).HasColumnName(@"Created").HasColumnType("datetime").IsRequired();
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("nvarchar(max)").IsOptional();

            HasRequired(a => a.Task).WithOptional(b => b.TaskResult).WillCascadeOnDelete(false);
        }
    }

}
