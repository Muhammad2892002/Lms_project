using FluentValidation;
using Lms_project.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Validators.EnrollmentValidator
{
    public class CreateEnrollmentValidator :AbstractValidator<CreateEnrollment>
    {
        public CreateEnrollmentValidator() {
            RuleFor(obj => obj.StudentId).GreaterThan(0).WithMessage("Student Id must be greater than zero");
            RuleFor(obj => obj.CourseId).GreaterThan(0).WithMessage("Course Id must be greater than zero");

        }

    }
}
