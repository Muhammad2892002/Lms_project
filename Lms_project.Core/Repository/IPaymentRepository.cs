using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Repository
{
    public interface IPaymentRepository
    {
        public Task Add(Payment payment);
        public Task<Payment?> GetLatestCreated(int enrollmentId);

        public Task<Payment> UpdatePayment(Payment payment);

    }
}
