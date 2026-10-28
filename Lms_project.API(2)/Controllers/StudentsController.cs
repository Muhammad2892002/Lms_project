using Lms_project.Core.Entities;
using Lms_project.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.API_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(IStudentService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDto>>> GetAllStudents() {
            try
            {

                var allStds = await _service.GetAllStudentsAsync();
                return Ok(allStds);
            }
            catch (Exception ex) {
                return NotFound(ex.Message);
            
            }
        
        
        
        
        
        }
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<StudentResponseDto>> GetStdById(int Id) {
            var result=await _service.GetStdByIdAsync(Id);
            return Ok(result);
            
        
        
        }
        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> CreateStd(CreateStudent studentDto) {
            var std = await _service.CreateStdAsync(studentDto);
            if (std != null)
            {
                return CreatedAtAction(nameof(GetStdById), new {Id=std.Id},std);
            }
            else {

                return BadRequest("Failed to create");
            
            }
        
        
        
        }
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult<StudentResponseDto>>EditStd(int Id, EditStudent std){
            var editStd = await _service.UpdateStdAsync(Id,std);
            if (editStd != null)
            {
                return Ok(editStd);

            }
            else {
                return BadRequest();
            }
        
        }
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult> DeleteStd(int Id) {
            var DeletingStdResult = await _service.DeleteStdAsync(Id);
            if (DeletingStdResult)
            {
                return NoContent();
            }
            else {

                return NotFound();
            
            }
        
        }

    }
}
