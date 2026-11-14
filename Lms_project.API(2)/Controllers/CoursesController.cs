
using FluentValidation;
using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Service;
using Lms_project.Core.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lms_project.API_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController(ICourseService service, IImageService imageService) : ControllerBase
    {
        //private readonly ICourseService _courseService;
        //public CoursesController(ICourseService service) {

        //    _courseService = service;
        //}
       
       
         
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        //[Route("GetAllCourses")]
        public async Task<ActionResult<List<CourseResponseDto>>> GetAllCourses() {
           
            return Ok(await service.GetAllCoursesAsync());
        
        }
        [HttpGet("CountAllCourses")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<int>> Count() {
            int count = await service.Count();

            return Ok(new { message=$"The total number of courses is {count}"});
        }
        [HttpGet("Paged")]
        [Authorize(Roles = Roles.Admin)]
        //[Route("GetAllCourses")]
        public async Task<ActionResult<PageResult<CourseResponseDto>>> GetAll([FromQuery] FilterByPriceDto filterByPrice, [FromQuery]int page=1,[FromQuery] int pageSize = 10)
        {
            page=Math.Max(page,1);
            pageSize=Math.Clamp(pageSize,1,50);

            return Ok(await service.GetpageAsync(filterByPrice,page, pageSize));
        }
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [Route("{Id:int}")]
        public async Task<ActionResult<CourseResponseDto>> GetCourseById(int Id)
        {
          
            return await service.GetCourseByIdAsync(Id);



        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]

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
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
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
        [HttpPost("UploadImg")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UploadImg(IFormFile file) { 
            await using var stream = file.OpenReadStream();
            var url=await imageService.SaveCourseImageAsync(stream, file.FileName, file.Length);
            return Ok(new { imgUrl = url });


        }
        [HttpDelete("DeleteImg")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> DeleteImg(string imgUrl)
        {
            var isImgUsed = await service.CheckIfImgUsed(imgUrl);
            imageService.DeleteCourseImage(imgUrl);
            return Ok(new { message = "Image deleted successfully" });


        }

    }
}
