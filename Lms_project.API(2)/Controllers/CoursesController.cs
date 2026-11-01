
using FluentValidation;
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
        [HttpGet("CountAllCourses")]
        public async Task<ActionResult<int>> Count() {
            int count = await service.Count();

            return Ok(new { message=$"The total number of courses is {count}"});
        }
        [HttpGet("Paged")]
        //[Route("GetAllCourses")]
        public async Task<ActionResult<PageResult<CourseResponseDto>>> GetAll([FromQuery] FilterByPriceDto filterByPrice, [FromQuery]int page=1,[FromQuery] int pageSize = 10)
        {
            page=Math.Max(page,1);
            pageSize=Math.Clamp(pageSize,1,50);

            return Ok(await service.GetpageAsync(filterByPrice,page, pageSize));
        }
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<CourseResponseDto>> GetCourseById(int Id)
        {
          
            return await service.GetCourseByIdAsync(Id);



        }

        [HttpPost]
       
        public async Task<ActionResult<CourseResponseDto>> CreateCourse(CreateCourseDto course)
        {
            try
            {
         
                var creatingResult = await service.CreateCourseAsync(course);
                return creatingResult != null ? Ok(creatingResult) : BadRequest("Course its not valid");
            }
            catch (ArgumentException ex) {

                return BadRequest(ex.Message);

            }
            catch (ValidationException ex) {

                return BadRequest(ex.Errors.Select(ex => new {ex.PropertyName,ex.ErrorMessage }));
            }

        }
        [HttpPut("Id:int")]
        public async Task<ActionResult<Course>> EditCourse(int Id, UpdateCourseDto course) {
            try
            {

                var courseFindingResult = await service.EditCourseAsync(Id, course);
                if (courseFindingResult != null)
                {

                    return NoContent();
                }
                else
                {

                    return BadRequest();

                }
            }
            catch (ValidationException ex) {
                return BadRequest(ex.Errors.Select(x => new {x.PropertyName,x.ErrorMessage }));

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
