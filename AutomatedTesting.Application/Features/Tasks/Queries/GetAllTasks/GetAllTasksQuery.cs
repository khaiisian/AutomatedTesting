using AutomatedTesting.Repositories.Tasks;
using AutomatedTesting.Shared;
using AutomatedTesting.Shared.Mapper;
using AutomatedTesting.Shared.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedTesting.Application.Features.Tasks.Queries.GetAllTasks;

public class GetAllTasksQuery : IRequest<Result<List<TaskResponseModel>>>;

public class GetAllTaskHandler: IRequestHandler<GetAllTasksQuery, Result<List<TaskResponseModel>>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<List<TaskResponseModel>>> Handle(GetAllTasksQuery query, CancellationToken ct)
    {
        var lst = await _taskRepository.GetTaskList(ct);
        if(lst.Count > 0)
        {
            return Result<List<TaskResponseModel>>.Success(lst, "Tasks are retrieved successfully.");
        }

        return Result<List<TaskResponseModel>>.Error("There is not task data to retrieve.");
    }
}