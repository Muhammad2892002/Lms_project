
using Lms_project.Core.DTOs;
using Lms_project.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lms_project.API_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController(ICurrencyService currencyService) : ControllerBase
    {
        [HttpPost("Convert")]
        public async Task<ActionResult<ConvertPriceResponseDto>> Convert(ConvertPriceRequestDto request)
        {
            var response = await currencyService.ConvertPriceAsync(request);
            return Ok(response);
        }
    }
}
