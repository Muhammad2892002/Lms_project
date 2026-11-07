using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.Auth;

namespace Lms_project.Core.Validators.RegisterValidator
{
    public class RegisterStdDtoValidator :AbstractValidator<RegisterStdDto>
    {
        public RegisterStdDtoValidator() { 
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");

        }

    }
}
