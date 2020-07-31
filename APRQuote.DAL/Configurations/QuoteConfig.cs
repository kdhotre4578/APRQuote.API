using System;
using APRQuote.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APRQuote.DAL.Configurations
{
    internal class QuoteConfig : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).UseIdentityColumn();
            builder.Property(m => m.VehicleId).IsRequired();
            builder.Property(m => m.QuoteTypeId).IsRequired();
            builder.Property(m => m.APRPercentRangeId).IsRequired();

            builder.HasOne(m => m.vehicle).WithMany().HasForeignKey(k => k.VehicleId);
            builder.HasOne(m => m.quoteType).WithMany().HasForeignKey(k => k.QuoteTypeId);
            builder.HasOne(m => m.aprPercentRange).WithMany().HasForeignKey(k => k.APRPercentRangeId);

            builder.ToTable("TblQuotes");
        }
    }
}
