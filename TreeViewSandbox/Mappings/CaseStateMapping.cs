

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class CaseStateMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<CaseState>
    {
        public CaseStateMapping()
            : this("dbo")
        {
        }

        public CaseStateMapping(string schema)
        {
            ToTable("CaseStates", schema);
            HasKey(x => x.CaseStateId);

            Property(x => x.CaseStateId).HasColumnName(@"CaseStateId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(max)").IsOptional();
        }
    }

}
