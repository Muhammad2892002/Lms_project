using FluentValidation;
using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Exceptions;
using Lms_project.Core.Repository;
using Lms_project.Core.Service;
using Lms_project.Infra.Mapping;
using Lms_project.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Infra.Service
{
    public class StudentService(IStudentRepository _stdRepo,IValidator<CreateStudentDto>_createValidator,
        IValidator<EditStudentDto>_editValidator, IEnrollmentRepository _enrollmentRepo) : IStudentService
    {
        public async Task<StudentResponseDto> CreateStdAsync(CreateStudentDto stdDto)
        {
            await _createValidator.ValidateAndThrowAsync(stdDto);
            var stdAsEntity = stdDto.FromCreateDtoToEntity();
            if (await _stdRepo.CheckIfEmailReapeted(0,stdAsEntity)==true) {
                throw new ConflictException("The email or phone  you entered is used enter another one");


            }
            var addingStdResult = await _stdRepo.CreateStdAsync(stdAsEntity);
            return addingStdResult.ToResponseDto();
        }

        public async Task<bool> DeleteStdAsync(int Id)
        {
            var checkStudentExists = await _stdRepo.GetStdByIdAsync(Id);
            if (checkStudentExists == null) {
                throw new NotFoundException($"student who has this id:{Id} doesnot exist");
            
            }

            var deleteingResult = await _stdRepo.DeleteStdAsync(Id);
            return (deleteingResult);
        }

        public async Task<PageResult<StudentResponseDto>> GetAllStudentsAsync(int page, int pageSize)
        {
            var allStdAsDto= await _stdRepo.GetAllStudentsAsync(page, pageSize);
            return allStdAsDto;
        }

        public async Task<StudentResponseDto> GetStdByIdAsync(int Id)
        {
            var stdAsDto=await _stdRepo.GetStdByIdAsync(Id);
            if (stdAsDto == null) {
                throw new NotFoundException($"student who has this id:{Id} doesnot exist");

            }
            return stdAsDto.ToResponseDto();
        }

        public async Task<StudentResponseDto> UpdateStdAsync(int Id, EditStudentDto std)
        {
            await _editValidator.ValidateAndThrowAsync(std);
            var stdAsEntity = std.FromEditDtoToEntity();
            if (await _stdRepo.CheckIfEmailReapeted(Id, stdAsEntity) == true) {
                 throw new ConflictException("The email or phone  you entered is used enter another one");

            }
            var updatedStdResult = await _stdRepo.UpdateStdAsync(Id, stdAsEntity);
            return updatedStdResult.ToResponseDto();
        }

        public async Task<List<GetAllEnrolledCourses>> GetAllEnrolledCoursesForStd(int stdId)
        {
            var checkStudentExists = await _stdRepo.GetStdByIdAsync(stdId);
            if (checkStudentExists != null)
            {
                var allEnrolledCourses = await _enrollmentRepo.GetAllEnrolledCoursesForStd(stdId);
                return allEnrolledCourses;



            }
            else
            {
                throw new NotFoundException($"student who has this id:{stdId} doesnot exist");
            }


        }

    }
}
