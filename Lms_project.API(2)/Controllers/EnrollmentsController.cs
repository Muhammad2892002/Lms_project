using Lms_project.Core.DTOs;
using Lms_project.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lms_project.API_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController(IEnrollmentService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllEnrollments() { 
            var allEnrollments = await _service.GetAllEnrollments();

            return Ok(allEnrollments);


        }
        [HttpGet("GetEnrollmentById")]
        public async Task<ActionResult> GetEnrollmentById([FromQuery] int Id)
        {
            var enrollment = await _service.GetEnrollmentById(Id);
            return Ok(enrollment);
        }
        [HttpPost]
        public async Task<ActionResult> AddNewEnrollment(CreateEnrollment enrollmentDto) {
            var newEnrollment = await _service.AddNewEnrollment(enrollmentDto);

         
            return CreatedAtAction(nameof(GetEnrollmentById), new { Id = newEnrollment.Id }, newEnrollment);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateEnrollment([FromQuery]int Id, [FromQuery]int courseId) {
            var updatingResult = await _service.UpdateEnrollment(Id, courseId);
            return Ok(new { message=updatingResult });
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteEnrollment([FromQuery]int Id) {
            var deletingResult = await _service.DeleteEnrollment(Id);
            return NoContent();
        }
    }
}
