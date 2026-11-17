using Lms_project.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Service
{
    public interface ICurrencyService
    {
        public Task<ConvertPriceResponseDto> ConvertPriceAsync(ConvertPriceRequestDto request);
    }
}
