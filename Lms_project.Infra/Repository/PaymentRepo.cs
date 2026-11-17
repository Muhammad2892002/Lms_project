using Lms_project.Core.Entities;
using Lms_project.Core.Enums;
using Lms_project.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Repository
{
    // Infra
    public class PaymentRepository(AppDbContext _context) : IPaymentRepository
    {
        public async Task Add(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment?> GetLatestCreated(int enrollmentId) =>
            await _context.Payments
                .Where(p => p.EnrollmentId == enrollmentId && p.Status == PaymentStatus.Created)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync();

        public async Task<Payment> UpdatePayment(Payment payment) {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        
        }
    }
}
