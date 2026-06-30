using AutomatedTesting.Application.Features.Tasks.Commands.CreateTask;
using FluentValidation.TestHelper;

namespace AutomatedTesting.Tests;

public class CreateTaskCommandValidatorTests
{
    private readonly CreateTaskCommandValidator _validator = new();

    [Fact]
    public void Empty_title_should_fail()
    {
        var command = new CreateTaskCommand { Title = "", Description = "abc" };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Valid_title_should_pass()
    {
        var command = new CreateTaskCommand
        {
            Title = "testing",
            Description = "testing",
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }
}
