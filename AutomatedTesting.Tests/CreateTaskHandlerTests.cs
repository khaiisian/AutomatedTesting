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

        // IsAny<CreateTaskRequestModel>() => it doesnt care about what kind of CreateTaskRequestModel was used (in other words, it is to bypass the request validation)

        // Build real handler + fake repo
        var handler = new CreateTaskHandler(repo.Object);
        // Make a command (input request)       
        var command = new CreateTaskCommand { Title = "Test", Description = "test" };

        // Run the handler              
        var result = await handler.Handle(command, CancellationToken.None);

        // CHECK the result
        Assert.True(result.IsSuccess);

        repo.Verify(x => x.CreateTask(It.IsAny<CreateTaskRequestModel>(), It.IsAny<CancellationToken>()), Times.Once);
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

        repo.Verify(x => x.CreateTask(It.IsAny<CreateTaskRequestModel>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Maps_command_fields_to_request()
    {
        CreateTaskRequestModel? captured = null;

        var repo = new Mock<ITaskRepository>();
        
        repo.Setup(repo => repo.CreateTask(It.IsAny<CreateTaskRequestModel>(), It.IsAny<CancellationToken>())).Callback<CreateTaskRequestModel, CancellationToken>((model, _) => captured = model).ReturnsAsync(1);

        var handler = new CreateTaskHandler(repo.Object);
        var command = new CreateTaskCommand { Title = "Buy milk", Description = "2 liters" };

        await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(captured);
        Assert.Equal("Buy milk", captured!.Title);
        Assert.Equal("2 liters", captured!.Description);
    }


    [Fact]
    public async Task Throw_repoitory_failures()
    {
        var repo = new Mock<ITaskRepository>();

        repo.Setup(r => r.CreateTask(It.IsAny<CreateTaskRequestModel>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("db down."));

        var handler = new CreateTaskHandler(repo.Object);
        var commnad = new CreateTaskCommand { Title = "abc", Description = "adc" };

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.Handle(commnad, CancellationToken.None)); 
    }
}
