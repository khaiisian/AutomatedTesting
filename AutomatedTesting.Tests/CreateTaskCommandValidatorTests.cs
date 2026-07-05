using AutomatedTesting.Application.Features.Tasks.Commands.CreateTask;
using AutomatedTesting.Repositories.Tasks;
using AutomatedTesting.Shared.Models.Tasks;
using FluentValidation.TestHelper;
using Moq;

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
            Title = "testing 1",
            Description = "testing 1",
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }

    [Theory]
    [InlineData("", false)]         // empty -> invalid
    [InlineData("valid title", true)]        // normal -> valid
    public void Title_validation(string title, bool validation)
    {
        var command = new CreateTaskCommand { Title = title, Description = "abc" };

        var result = _validator.TestValidate(command);

        if (validation)
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        else
            result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    public static IEnumerable<object[]> TestLengthCases =>
        new[]
        {
            new object[] {new string ('a', 200), true},
            new object[] {new string ('a', 201), false}
        };

    [Theory]
    [MemberData(nameof(TestLengthCases))]
    public void Title_length_boundary(string title, bool shoulValid)
    {
        var command = new CreateTaskCommand { Title = title, Description = "abc" };

        var result = _validator.TestValidate(command);

        if (shoulValid)
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        else
            result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
