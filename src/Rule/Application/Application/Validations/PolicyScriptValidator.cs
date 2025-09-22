using FluentValidation;
using McsCore.Entities;
using RuleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleApplication.Validations
{
    public class PolicyScriptValidator : AbstractValidator<ScriptModel>
    {
        public PolicyScriptValidator()
        {
            RuleFor(x => x.ScriptName).NotEmpty().WithMessage("Script name is required");
            RuleFor(x => x.Script).NotEmpty().WithMessage("Script content is required");
        }
    }
}