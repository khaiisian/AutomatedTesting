using AutomatedTesting.Application.Features.Tasks.Commands.CreateTask;
using AutomatedTesting.Repositories.Tasks;
using AutomatedTesting.Shared.Models.Tasks;
using Moq;

namespace AutomatedTesting.Tests;

public class CreateTaskHandlerTests
{
    [Fact]
    public async Task Return_success_saving()
    {
        // Fake the repository
        var repo = new Mock<ITaskRepository>();

        // Setup its CreateTask method (like scripting in a way the test wanted)
        repo.Setup(r => r.CreateTask(It.IsAny<CreateTaskRequestModel>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(1);

        // Build real handler + fake repo
        var handler = new CreateTaskHandler(repo.Object);
        // Make a command (input request)       
        var command = new CreateTaskCommand { Title = "Test", Description = "test" };

        // Run the handler              
        var result = await handler.Handle(command, CancellationToken.None);

        // CHECK the result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Return_fail_nosaving()
    {
        var repo = new Mock<ITaskRepository>();
        repo.Setup(r => r.CreateTask(It.IsAny<CreateTaskRequestModel>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(0);

        var hanlder = new CreateTaskHandler(repo.Object);
        var command = new CreateTaskCommand { Title="Test", Description = "test" };

        var result = await hanlder.Handle(command, CancellationToken.None);
        Assert.True(result.IsError);
    }
}
