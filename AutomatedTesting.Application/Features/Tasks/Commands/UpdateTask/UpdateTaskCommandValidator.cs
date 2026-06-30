using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedTesting.Application.Features.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        When(x => x.Title is not null, () =>
        {
            RuleFor(x => x.Title)
            .NotEmpty().MaximumLength(200);
        });

        When(x => x.Description is not null, () =>
        {
            RuleFor(x => x.Description)
            .NotEmpty().MaximumLength(1000);
        });
    }
}
