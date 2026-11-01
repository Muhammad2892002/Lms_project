using FluentValidation;
using Lms_project.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Core.Validators.StudentValidator
{
    public class EditStudentDtoValidator :AbstractValidator<EditStudentDto>
    {
        public EditStudentDtoValidator() {
            RuleFor(obj => obj.Email)
            .NotEmpty().WithMessage("Email must not empty")
            .EmailAddress().WithMessage("Email format is invalid")
            .MaximumLength(100).WithMessage("Email length must not exceed 100 characters");
            RuleFor(obj => obj.Phone).NotEmpty();


        }
    }
}
