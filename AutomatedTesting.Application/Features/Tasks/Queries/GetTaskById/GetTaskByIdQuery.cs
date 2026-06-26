using AutomatedTesting.Repositories.Tasks;
using AutomatedTesting.Shared;
using AutomatedTesting.Shared.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedTesting.Application.Features.Tasks.Queries.GetTaskById;

public class GetTaskByIdQuery : IRequest<Result<TaskResponseModel>>
{
    public int id { get; set; }
}

public class GetTaskByIdHandler: IRequestHandler<GetTaskByIdQuery, Result<TaskResponseModel>>
{
    public readonly ITaskRepository _taskRepository;

    public GetTaskByIdHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<TaskResponseModel>> Handle(GetTaskByIdQuery request, CancellationToken ct)
    {
        var res = await _taskRepository.GetTaskById(request.id, ct);
        return res is null ? Result<TaskResponseModel>.NotFoundError("No task data is found"): Result<TaskResponseModel>.Success(res);
    }
}