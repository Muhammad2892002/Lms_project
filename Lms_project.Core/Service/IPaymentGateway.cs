using Lms_project.Core.DTOs;
using Lms_project.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Service
{
    public interface IPaymentGateway
    {
        Task<PayPalPayment> CreateOrder(decimal amountUsd);
        Task<PaymentStatus> CaptureOrder(string orderId);
    }
}
