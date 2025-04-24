using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peristance.Data.Configrations
{
    public class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.productBrand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId);


            builder.Property(P => P.Price)
                .HasColumnType("decimal(10,2)");

            builder.HasOne(P => P.productType)
                   .WithMany()
                   .HasForeignKey(P => P.TypeId);
        }
    }
}
