using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class SignUpViewModelValidator:AbstractValidator<SignUpViewModel>
    {
        public SignUpViewModelValidator()
        {
            RuleFor(it => it.UserName).NotEmpty();
            RuleFor(it => it.UserName).MinimumLength(3);
            RuleFor(it => it.Password).NotEmpty();
            RuleFor(it => it.Password).MinimumLength(8);
            RuleFor(it => it.Email).NotEmpty();
            RuleFor(it => it.Email).EmailAddress();
        }
    }
}
