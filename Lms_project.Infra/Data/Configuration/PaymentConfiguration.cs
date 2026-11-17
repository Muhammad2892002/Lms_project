using Lms_project.Core.Entities;
using Lms_project.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Data.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(c => c.EnrollmentNav).WithMany(d => d.Payments).HasForeignKey(p=>p.EnrollmentId);
            builder.Property(p => p.Amount).HasPrecision(10,2);
            builder.Property(p => p.Currency).HasDefaultValue("USD");
            builder.HasIndex(p => p.PayPalOrderId).IsUnique();
            builder.Property(p=>p.Status).HasConversion<string>();
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
            


        }
    }
}
