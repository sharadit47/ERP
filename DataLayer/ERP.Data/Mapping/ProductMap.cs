using ERP.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERP.Data.Models.Mapping
{
    public class tblProductMap : EntityTypeConfiguration<Product>
    {
        public tblProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductID);

             //Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProductName)
                .HasMaxLength(256);

            //this.Property(t => t.Type)
            //    .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tblProducts");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductTitle");
            //this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
