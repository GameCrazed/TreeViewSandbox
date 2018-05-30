

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class TaskConfigurationMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TaskConfiguration>
    {
        public TaskConfigurationMapping()
            : this("dbo")
        {
        }

        public TaskConfigurationMapping(string schema)
        {
            ToTable("TaskConfigurations", schema);
            HasKey(x => x.TaskConfigurationId);

            Property(x => x.TaskConfigurationId).HasColumnName(@"TaskConfigurationId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.Body).HasColumnName(@"Body").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.LeadTimeDays).HasColumnName(@"LeadTimeDays").HasColumnType("int").IsRequired();
            Property(x => x.Sequence).HasColumnName(@"Sequence").HasColumnType("int").IsRequired();
            Property(x => x.CaseStateId).HasColumnName(@"CaseStateId").HasColumnType("int").IsRequired();
            Property(x => x.IsManual).HasColumnName(@"IsManual").HasColumnType("bit").IsRequired();

            HasRequired(a => a.CaseState).WithMany(b => b.TaskConfigurations).HasForeignKey(c => c.CaseStateId);

        }
    }

}
