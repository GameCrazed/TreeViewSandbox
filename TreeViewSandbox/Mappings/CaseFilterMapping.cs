

using TreeViewSandbox.Entities;

namespace TreeViewSandbox.Mappings
{

    public class CaseFilterMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<CaseFilter>
    {
        public CaseFilterMapping()
            : this("dbo")
        {
        }

        public CaseFilterMapping(string schema)
        {
            ToTable("CaseFilters", schema);
            HasKey(x => x.CaseFilterId);

            Property(x => x.CaseFilterId).HasColumnName(@"CaseFilterId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.Created).HasColumnName(@"Created").HasColumnType("datetime").IsRequired();
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.Sequence).HasColumnName(@"Sequence").HasColumnType("int").IsRequired();
            Property(x => x.UserRoleId).HasColumnName(@"UserRoleId").HasColumnType("int").IsRequired();

            HasRequired(a => a.UserRole).WithMany(b => b.CaseFilters).HasForeignKey(c => c.UserRoleId).WillCascadeOnDelete(false);
        }
    }

}
