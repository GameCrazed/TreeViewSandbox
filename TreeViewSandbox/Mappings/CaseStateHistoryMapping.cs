

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class CaseStateHistoryMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<CaseStateHistory>
    {
        public CaseStateHistoryMapping()
            : this("dbo")
        {
        }

        public CaseStateHistoryMapping(string schema)
        {
            ToTable("CaseStateHistories", schema);
            HasKey(x => new { x.KfiId, x.CaseStateId });

            Property(x => x.KfiId).HasColumnName(@"KfiId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.CaseStateId).HasColumnName(@"CaseStateId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Created).HasColumnName(@"Created").HasColumnType("datetime").IsRequired();
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("nvarchar(max)").IsOptional();

            HasRequired(a => a.CaseState).WithMany(b => b.CaseStateHistories).HasForeignKey(c => c.CaseStateId);
        }
    }

}
