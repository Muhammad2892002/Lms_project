using FluentValidation;
using Lms_project.Core.DTOs;
using Lms_project.Core.Exceptions;
using Lms_project.Core.Repository;
using Lms_project.Core.Service;
using Lms_project.Infra.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lms_project.Infra.Service
{
    public class EnrollmentService(IEnrollmentRepository _repo,IValidator<CreateEnrollment> _validator,ICourseRepository _courseRepo,IStudentRepository _studentRepo): IEnrollmentService
    {
        public async Task<ShowEnrollmentDto> AddNewEnrollment(CreateEnrollment enrollmentDto)
        {
            string msg = "";
            await _validator.ValidateAndThrowAsync(enrollmentDto);
            var checkCourse = await _courseRepo.GetCourseByIdAsync(enrollmentDto.CourseId);
            var checkStudent = await _studentRepo.GetStdByIdAsync(enrollmentDto.StudentId);
            if (checkStudent==null) {
               
            msg += "Student Not Found\n";
            }
            if(checkCourse == null)
            {
                msg += "Course Not Found\n";
            }
            if (await _repo.CheckIfStudentEnrolled(enrollmentDto.StudentId, enrollmentDto.CourseId) == true) {
                throw new ConflictException("This user already exist");
            }
            if (!string.IsNullOrEmpty(msg))
            {
                throw new NotFoundException(msg);
            }
            var enrollmentAsEntity=enrollmentDto.ToEntity();
           var addingResult= await _repo.AddNewEnrollment(enrollmentAsEntity);
           return addingResult.ToDto();
        }

        public async Task<string> DeleteEnrollment(int Id)
        {
            if (Id > 10000 || Id < 0) {
                throw new FluentValidation.ValidationException("Enter valid Id from 1 to 10000");
            }
            var deletingResult = await _repo.DeleteEnrollment(Id);
            if (string.IsNullOrEmpty(deletingResult)) {
                throw new NotFoundException("The enrollment may be deleted or does not exist");
            }
            return deletingResult;
        }

        public async Task<List<ShowEnrollmentDto>> GetAllEnrollments()
        {
            return await _repo.GetAllEnrollments();
        }

        public async Task<ShowEnrollmentDto> GetEnrollmentById(int Id)
        {
            if (Id > 10000 || Id < 0)
            {
                throw new FluentValidation.ValidationException("Enter valid Id from 1 to 10000");
            }
            var enrollment = await _repo.GetEnrollmentById(Id);
            return enrollment.ToDto();
        }

        public async Task<string> UpdateEnrollment(int Id, int courseId)
        {
            string  msg= ""; 
            var bringEnrollment = await _repo.GetEnrollmentById(Id);
            var checkCourse = await _courseRepo.GetCourseByIdAsync(courseId);
            if (bringEnrollment == null)
            {
                msg+="Enrollment not found.   ";
            }
            if (checkCourse == null)
            {
                msg+="   Course not found.";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                throw new NotFoundException(msg);
            }
            if(await _repo.CheckIfStudentEnrolledInCourse(Id, bringEnrollment.StudentId, courseId)==true)
            {
                throw new ConflictException("This student already enrolled in this course");

            }
            var updatingResult = await _repo.UpdateEnrollment(Id, courseId);
            return updatingResult;

           
        }

     
    }
}
