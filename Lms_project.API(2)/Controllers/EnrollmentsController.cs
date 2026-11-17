using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Service;
using Lms_project.Infra.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lms_project.API_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentsController(IEnrollmentService _service, IPaymentService paymentService) : ControllerBase
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<ActionResult> GetAllEnrollments()
        {
            var allEnrollments = await _service.GetAllEnrollments();


            return Ok(allEnrollments);


        }
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("GetEnrollmentById")]
        public async Task<ActionResult> GetEnrollmentById([FromQuery] int Id)
        {
            var enrollment = await _service.GetEnrollmentById(Id);
            return Ok(enrollment);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult> AddNewEnrollment(CreateEnrollment enrollmentDto)
        {
            var newEnrollment = await _service.AddNewEnrollment(enrollmentDto);


            return CreatedAtAction(nameof(GetEnrollmentById), new { Id = newEnrollment.Id }, newEnrollment);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        public async Task<ActionResult> UpdateEnrollment([FromQuery] int Id, [FromQuery] int courseId)
        {
            var updatingResult = await _service.UpdateEnrollment(Id, courseId);
            return Ok(new { message = updatingResult });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete]
        public async Task<ActionResult> DeleteEnrollment([FromQuery] int Id)
        {
            var deletingResult = await _service.DeleteEnrollment(Id);
            return NoContent();
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("{enrollmentId}/pay")]
        public async Task<ActionResult<PayResponseDto>> Pay(int enrollmentId)
            => Ok(await paymentService.Pay(enrollmentId));

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("{enrollmentId}/capture")]
        public async Task<ActionResult<ShowEnrollmentDto>> Capture(int enrollmentId)
            => Ok(await paymentService.Capture(enrollmentId));
    }
}
