

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class TaskMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Task>
    {
        public TaskMapping()
            : this("dbo")
        {
        }

        public TaskMapping(string schema)
        {
            ToTable("Tasks", schema);
            HasKey(x => new { x.KfiId, x.TaskId });

            Property(x => x.KfiId).HasColumnName(@"KfiId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.TaskId).HasColumnName(@"TaskId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Body).HasColumnName(@"Body").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.TaskConfigurationId).HasColumnName(@"TaskConfigurationId").HasColumnType("int").IsRequired();
            Property(x => x.DueDate).HasColumnName(@"DueDate").HasColumnType("datetime").IsRequired();
            Property(x => x.Sequence).HasColumnName(@"Sequence").HasColumnType("int").IsRequired();
            Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.State).HasColumnName(@"State").HasColumnType("int").IsRequired();
            Property(x => x.Created).HasColumnName(@"Created").HasColumnType("datetime").IsRequired();
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.HasBeenDeferred).HasColumnName(@"HasBeenDeferred").HasColumnType("bit").IsRequired();

            HasRequired(a => a.TaskConfiguration).WithMany(b => b.Tasks).HasForeignKey(c => c.TaskConfigurationId);
        }
    }

}
