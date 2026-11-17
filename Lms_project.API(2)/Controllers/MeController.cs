

using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Lms_project.Core.DTOs.StudentDto;
using System.Security.Claims;

namespace Lms_project.API_2_.Controllers
{
    [Route("api/me")]
    [ApiController]
    [Authorize]
    public class MeController(IStudentService _service) : ControllerBase
    {

        [HttpGet]

        public async Task<ActionResult<StudentResponseDto>> GetStdById()
        {
            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);


            var result = await _service.GetStdByUserIdAsync(userId);
            return Ok(result);



        }

        [HttpPut]

        public async Task<ActionResult<StudentResponseDto>> EditStd(EditStudentDto std)
        {
            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            var editStd = await _service.UpdateStdAsync(userId, std);
            if (editStd != null)
            {
                return Ok(editStd);

            }
            else
            {
                return BadRequest();
            }

        }
        [HttpDelete]

        public async Task<ActionResult> DeleteStd()
        {
            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            var DeletingStdResult = await _service.DeleteStdAsync(userId);
            if (DeletingStdResult)
            {
                return NoContent();
            }
            else
            {

                return NotFound();

            }

        }
        [HttpGet("GetAllEnrolledCoursesForStd")]

        public async Task<ActionResult<List<GetAllEnrolledCourses>>> GetAllEnrolledCoursesForStd()
        {
            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            var allEnrolledCourses = await _service.TaskGetAllEnrolledCourseByUserId(userId);
            return Ok(allEnrolledCourses);
        }

    }
}
