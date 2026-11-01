using FluentValidation;
using Lms_project.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Lms_project.Core.Validators.CourseValidator
{
    public class EditCourseDtoValidator :AbstractValidator<UpdateCourseDto>
    {
        public EditCourseDtoValidator() {
      

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be equal or greater than zero ");


            RuleFor(obj => obj.Title).Must(title => !Regex.IsMatch(title, @"^\d+$")).WithMessage("Title must not be only numbers.");

            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.InstructorId).GreaterThan(0);

        }
    }
}
