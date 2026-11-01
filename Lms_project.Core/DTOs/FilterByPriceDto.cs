using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.DTOs
{
    public record FilterByPriceDto(decimal MinPrice = 0, decimal MaxPrice = 300) { }
   
}
