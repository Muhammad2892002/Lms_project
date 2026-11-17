using Lms_project.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int EnrollmentId { get; set; }
        public Enrollment EnrollmentNav { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "USD";

        public string PayPalOrderId { get; set; }

        public PaymentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }


    }
}
