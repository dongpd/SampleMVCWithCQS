using System;

using Microsoft.EntityFrameworkCore;

using SampleMVCWithCQSCore.Domain;
namespace SampleMVCWithCQSCore.DataAccess.EntityConfiguration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> productConfiguration)
        {
            productConfiguration.ToTable("products");
            productConfiguration.HasKey(o => o.Id);
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
            productConfiguration.Property(b => b.Color).IsRequired();

            productConfiguration.Property<bool>("InStock")
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName("InStock")
                .IsRequired();
        }
    }
}