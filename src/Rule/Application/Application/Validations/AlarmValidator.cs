using Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleApplication.Validations
{
    public class AlarmValidator : AbstractValidator<AlarmModel>
    {
        public AlarmValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Alarm name is required.");
            RuleFor(x => x.DeviceId).NotEmpty().WithMessage("Device ID is required.");
            RuleFor(x => x.ParameterId).NotEmpty().WithMessage("Parameter ID is required.");
            RuleFor(x => x.Threshold).NotEmpty().WithMessage("Threshold is required.");
            RuleFor(x => x.Condition).NotEmpty().WithMessage("Condition is required.");
            RuleFor(x => x.Severity).NotEmpty().WithMessage("Invalid severity level.");
        }
    }
}
