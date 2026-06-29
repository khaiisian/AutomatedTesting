using AutomatedTesting.Repositories.Tasks;
using AutomatedTesting.Shared;
using AutomatedTesting.Shared.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedTesting.Application.Features.Tasks.Commands.UpdateTask;

public class UpdateTaskCommand: IRequest<Result<UpdateTaskResponseModel>>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
}

public class UpdateTaskHandler: IRequestHandler<UpdateTaskCommand, Result<UpdateTaskResponseModel>>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<UpdateTaskResponseModel>> Handle(UpdateTaskCommand request, CancellationToken ct)
    {
        var item = new UpdateTaskRequestModel
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = request.IsCompleted,
        };

        var res = await _taskRepository.UpdateTask(request.Id, item, ct);
        if (res == -1) return Result<UpdateTaskResponseModel>.NotFoundError("No task item is found.");

        return res > 0 ? Result<UpdateTaskResponseModel>.Success("Update is successful") : Result<UpdateTaskResponseModel>.Error("Update is failed.");
    }
}
