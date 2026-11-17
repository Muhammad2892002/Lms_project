using Lms_project.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Service
{
    public interface IPaymentService
    {
        public Task<PayResponseDto> Pay(int enrollmentId);
        public Task<ShowEnrollmentDto> Capture(int enrollmentId);
    }
}
