
using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lms_project.API_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(ICourseService service) : ControllerBase
    {
        //private readonly ICourseService _courseService;
        //public CoursesController(ICourseService service) {

        //    _courseService = service;
        //}
       
       
         
        [HttpGet]
        //[Route("GetAllCourses")]
        public async Task<ActionResult<List<CourseResponseDto>>> GetAllCourses() {
           
            return Ok(await service.GetAllCoursesAsync());
        
        }
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<CourseResponseDto>> GetCourseById(int Id)
        {
            // var course = _courses.FirstOrDefault(obj=>obj.Id==Id);
            //return course is null ?NotFound(): Ok(course);
            return await service.GetCourseByIdAsync(Id);



        }

        [HttpPost]
       
        public async Task<ActionResult<CourseResponseDto>> CreateCourse(CreateCourseDto course)
        {
            //course.Id = _courses.Count() == 0 ? 1 : _courses.Max(c=>c.Id)+1;
            //_courses.Add(course);
            var creatingResult = await service.CreateCourseAsync(course);
            return creatingResult != null ? Ok(creatingResult) : BadRequest("Course its not valid");

        }
        [HttpPut("Id:int")]
        public async Task<ActionResult<Course>> EditCourse(int Id,Course course) {

            var courseFindingResult =await service.EditCourseAsync(Id, course);
            if (courseFindingResult != null)
            {
             
                return NoContent();
            }
            else {

                return BadRequest();
            
            }
            

          
        
        
        
        }
        [HttpDelete]
        public async Task<ActionResult<Course>> DeleteCourse(int Id) {
            var searchForCourse =await  service.DeleteCourseAsync(Id);
            if (searchForCourse != null)
            {
               
                return NotFound(searchForCourse);

            }
            else {
                return BadRequest(searchForCourse);
            
            
            }
        
        
        }
    }
}
