using System;

using Microsoft.EntityFrameworkCore;

using SampleMVCWithCQS2Core.DataAccess;
using SampleMVCWithCQS2Core.Domain;
namespace SampleMVCWithCQS2Core.DataAccess.EntityConfiguration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> productConfiguration)
        {
            productConfiguration.ToTable("products");
            productConfiguration.HasKey(o => o.Id);
            productConfiguration.Property(o => o.Id)
                .UseHiLo("productseq");
            productConfiguration.Property<string>("Name")
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName("Name")
                .IsRequired();
            productConfiguration.Property<string>("Category")
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName("Category")
                .IsRequired();
            productConfiguration.Property<decimal>("Price")
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName("Price")
                .IsRequired();
            // productConfiguration.Property<Enum>("Color")
            //     .UsePropertyAccessMode(PropertyAccessMode.Property)
            //     .HasColumnName("Color")
            //     .IsRequired();
                            productConfiguration.Property(b => b.Color).IsRequired();

            productConfiguration.Property<bool>("InStock")
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName("InStock")
                .IsRequired();
        }
    }
}