using AutomatedTesting.Repositories.Tasks;
using AutomatedTesting.Shared;
using AutomatedTesting.Shared.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedTesting.Application.Features.Tasks.Commands.DeleteTask;

public class DeleteTaskCommand : IRequest<Result<DeleteTaskResponseModel>>
{
    public int Id { get; set; }
};

public class DeleteTaskHandler: IRequestHandler<DeleteTaskCommand, Result<DeleteTaskResponseModel>>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<DeleteTaskResponseModel>> Handle (DeleteTaskCommand request, CancellationToken ct)
    {
        var res = await _taskRepository.DeleteTask(request.Id, ct);
        if (res == -1) return Result<DeleteTaskResponseModel>.NotFoundError("No task item is found.");

        return res > 0 ? Result<DeleteTaskResponseModel>.Success("Task item is deleted.") : Result<DeleteTaskResponseModel>.Error("Task item is not deleted.");
    }
}