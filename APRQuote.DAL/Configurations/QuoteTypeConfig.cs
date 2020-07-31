using System;
using APRQuote.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APRQuote.DAL.Configurations
{
    internal class QuoteTypeConfig : IEntityTypeConfiguration<QuoteType>
    {
        public void Configure(EntityTypeBuilder<QuoteType> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).UseIdentityColumn();
            builder.Property(m => m.Type).IsRequired().HasMaxLength(20);

            builder.ToTable("TblQuoteType");
        }
    }
}
