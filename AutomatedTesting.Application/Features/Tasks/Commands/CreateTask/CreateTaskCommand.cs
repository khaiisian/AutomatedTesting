using AutomatedTesting.Repositories.Tasks;
using AutomatedTesting.Shared;
using AutomatedTesting.Shared.Models.Tasks;
using MediatR;

namespace AutomatedTesting.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommand : IRequest<Result<CreateTaskResponseModel>>
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Result<CreateTaskResponseModel>>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<CreateTaskResponseModel>> Handle(CreateTaskCommand reqeust, CancellationToken ct)
    {
        var item = new CreateTaskRequestModel
        {
            Title = reqeust.Title,
            Description = reqeust.Description
        };

        var result = await _taskRepository.CreateTask(item, ct);
        if(result > 0)
        {
            return Result<CreateTaskResponseModel>.Success("Item is created successfully.");
        }
        return Result<CreateTaskResponseModel>.Error("Item was not created successfully.");
    }
}
