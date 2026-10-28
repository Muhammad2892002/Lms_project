using Lms_project.Core.Entities;
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
    public class StudentService(IStudentRepository _stdRepo) : IStudentService
    {
        public async Task<StudentResponseDto> CreateStdAsync(CreateStudent stdDto)
        {
            var stdAsEntity = stdDto.FromCreateDtoToEntity();
            var addingStdResult = await _stdRepo.CreateStdAsync(stdAsEntity);
            return addingStdResult.ToResponseDto();
        }

        public async Task<bool> DeleteStdAsync(int Id)
        {
            var deleteingResult = await _stdRepo.DeleteStdAsync(Id);
            return (deleteingResult);
        }

        public async Task<List<StudentResponseDto>> GetAllStudentsAsync()
        {
            var allStdAsDto= await _stdRepo.GetAllStudentsAsync();
            return allStdAsDto.Select(obj => obj.ToResponseDto()).ToList();
        }

        public async Task<StudentResponseDto> GetStdByIdAsync(int Id)
        {
            var stdAsDto=await _stdRepo.GetStdByIdAsync(Id);
            return stdAsDto.ToResponseDto();
        }

        public async Task<StudentResponseDto> UpdateStdAsync(int Id, EditStudent std)
        {
            var stdAsEntity = std.FromEditDtoToEntity();
            var updatedStdResult = await _stdRepo.UpdateStdAsync(Id, stdAsEntity);
            return updatedStdResult.ToResponseDto();
        }
    }
}
