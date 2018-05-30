

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class CaseFilterTaskConfigurationMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<CaseFilterTaskConfiguration>
    {
        public CaseFilterTaskConfigurationMapping()
            : this("dbo")
        {
        }

        public CaseFilterTaskConfigurationMapping(string schema)
        {
            ToTable("CaseFilterTaskConfigurations", schema);
            HasKey(x => new { x.CaseFilterId, x.TaskConfigurationId });

            Property(x => x.TaskConfigurationId).HasColumnName(@"TaskConfigurationId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.CaseFilterId).HasColumnName(@"CaseFilterId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            HasRequired(a => a.CaseFilter).WithMany(b => b.CaseFilterTaskConfigurations).HasForeignKey(c => c.CaseFilterId);
            HasRequired(a => a.TaskConfiguration).WithMany(b => b.CaseFilterTaskConfigurations).HasForeignKey(c => c.TaskConfigurationId);
        }
    }

}
