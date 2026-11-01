using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.DTOs
{
    public record PageResult<T>(List<T>Items,int Page,int PageSize,int TotalCount)
    {
        public int TotalPages=>(int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
