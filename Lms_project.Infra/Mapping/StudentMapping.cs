using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;


namespace Lms_project.Infra.Mapping
{
    public static class StudentMapping
    {
        public static StudentResponseDto ToResponseDto(this Student std) {

            return new StudentResponseDto(std.Id,std.FullName,std.Email,std.Phone);
        
        
        }
        public static Student FromCreateDtoToEntity( this CreateStudent stdDto) {
            return new Student() { FullName=stdDto.FullName,Email=stdDto.Email,Phone=stdDto.Phone };
        
        }

        public static Student FromEditDtoToEntity(this EditStudent stdDto)
        {
            return new Student() {  Email = stdDto.Email, Phone = stdDto.Phone };

        }
    }
}
