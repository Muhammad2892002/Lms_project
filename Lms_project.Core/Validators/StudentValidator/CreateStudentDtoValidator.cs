using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Core.Validators.StudentValidator
{
    public class CreateStudentDtoValidator :AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator() {
            RuleFor(obj => obj.Email)
                .NotEmpty().WithMessage("Email must not empty")
                .EmailAddress().WithMessage("Email format is invalid")
                .MaximumLength(100).WithMessage("Email length must not exceed 100 characters");
            RuleFor(obj => obj.FullName).NotEmpty().WithMessage("Full name is Required")
                .Must(FullName => !Regex.IsMatch(FullName, @"^\d+$")).WithMessage("please enter character");
          
            RuleFor(obj => obj.Phone).NotEmpty();
              

                
        
        
        }
    }
}
