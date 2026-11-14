using Lms_project.Core.DTOs;
using Lms_project.Core.Service;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Lms_project.Infra.Service
{
    public class CurrencyService(HttpClient http): ICurrencyService
    {
        private record PairResponse([property: JsonPropertyName("conversion_rate")]decimal ConversionRate) ;
        public async Task<ConvertPriceResponseDto> ConvertPriceAsync(ConvertPriceRequestDto request)
        {
            var data = await http.GetFromJsonAsync<PairResponse>($"pair/{request.FromCurrency}/{request.ToCurrency}")
                ?? throw new HttpRequestException("Failed to retrieve currency conversion data");
            var converted = Math.Round(request.Amount * data.ConversionRate, 2);
            return new ConvertPriceResponseDto(request.Amount, request.FromCurrency, converted, data.ConversionRate);

        }

    }
}
