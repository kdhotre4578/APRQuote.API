using System;
using APRQuote.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APRQuote.DAL.Configurations
{
    internal class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).UseIdentityColumn();
            builder.Property(m => m.Make).IsRequired().HasMaxLength(30);
            builder.Property(m => m.VehicleType).IsRequired().HasMaxLength(20);

            builder.ToTable("TblVehicle");
        }
    }
}
