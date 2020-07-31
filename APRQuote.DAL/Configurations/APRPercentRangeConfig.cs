using System;
using APRQuote.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APRQuote.DAL.Configurations
{
    internal class APRPercentRangeConfig : IEntityTypeConfiguration<APRPercentRange>
    {
        public void Configure(EntityTypeBuilder<APRPercentRange> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).UseIdentityColumn();
            builder.Property(m => m.ZeroThreeMonths).IsRequired();
            builder.Property(m => m.ThreeSixMonths).IsRequired();
            builder.Property(m => m.SixTwelveMonths).IsRequired();
            builder.Property(m => m.TwelvePlusMonths).IsRequired();

            builder.ToTable("TblAPRPercentRange");
        }
    }
}
