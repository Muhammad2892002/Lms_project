using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Enums;
using Lms_project.Core.Exceptions;
using Lms_project.Core.Repository;
using Lms_project.Core.Service;
using Lms_project.Infra.Repository;
using Microsoft.Extensions.Configuration;


namespace Lms_project.Infra.Service
{
    public class PaymentService(
        IEnrollmentRepository enrollmentRepo,
        IPaymentRepository paymentRepo,
        IPaymentGateway gateway,
        IConfiguration config) : IPaymentService
    {
        public async Task<PayResponseDto> Pay(int enrollmentId)
        {
            var enrollment = await enrollmentRepo.GetEnrollmentById(enrollmentId)
                ?? throw new NotFoundException($"There is no enrollment with id {enrollmentId}");

            if (enrollment.EnrollmentStatus == EnrollmentStatus.Paid)
                throw new ConflictException("This enrollment is already paid.");   // your 409 exception

            var rateAstext =  config["PayPal:JodToUsdRate"];
            decimal rate;
            if (!decimal.TryParse(rateAstext, out rate))
                throw new InvalidOperationException("Invalid or missing PayPal JOD to USD exchange rate.");

            var amount = Math.Round(enrollment.CourseNav.Price * rate, 2);

            var order = await gateway.CreateOrder(amount);

            var payment = new Payment
            {
                EnrollmentId = enrollmentId,
                Amount = amount,
                Currency = "USD",
                PayPalOrderId = order.PayPalOrderId,
                Status = PaymentStatus.Created,
                CreatedAt = DateTime.UtcNow
            };
            await paymentRepo.Add(payment);

            return new PayResponseDto(payment.Id, order.PayPalOrderId, amount, "USD", order.approvalLinks);
        }

        public async Task<ShowEnrollmentDto> Capture(int enrollmentId)
        {
            var enrollment = await enrollmentRepo.GetEnrollmentById(enrollmentId)
                ?? throw new NotFoundException($"There is no enrollment with id {enrollmentId}");
           

            if (enrollment.EnrollmentStatus == EnrollmentStatus.Paid)
                throw new ConflictException("This enrollment is already paid.");

            var payment = await paymentRepo.GetLatestCreated(enrollmentId)
                ?? throw new BadRequestException("Call /pay first.");
            if (DateTime.UtcNow - payment.CreatedAt > TimeSpan.FromHours(3))
            {
                payment.Status = PaymentStatus.Failed;
                await paymentRepo.UpdatePayment(payment);

                throw new PaymentFailedException("Payment  has expired. Call /pay again.");


            }// your 400 exception

            var status = await gateway.CaptureOrder(payment.PayPalOrderId);

            if (status != PaymentStatus.Completed )
            {
                payment.Status = PaymentStatus.Failed;
                await paymentRepo.UpdatePayment(payment);

                throw new PaymentFailedException("Payment was not approved or has expired. Call /pay again.");
            }
           

            payment.Status = PaymentStatus.Completed;
            payment.PaidAt = DateTime.UtcNow;
            enrollment.EnrollmentStatus = EnrollmentStatus.Paid;
            await enrollmentRepo.ChangeEnrollmentStatus(enrollment);
            await paymentRepo.UpdatePayment(payment);

            return  new ShowEnrollmentDto(enrollment.Id,enrollment.CourseNav.Title,enrollment.StudentNav.FullName,enrollment.EnrolledAt,
                enrollment.EnrollmentStatus.ToString(),enrollment.CourseNav.Price) /* map enrollment to your enrollment response DTO */;
        }
    }
}
