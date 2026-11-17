using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.DTOs
{
    public record ConvertPriceRequestDto(decimal Amount, string FromCurrency, string ToCurrency);
    public record ConvertPriceResponseDto(decimal OrginalAmount, string FromCurrency, decimal ToCurrency, decimal ExchangedAmount);
}
