using AutomatedTesting.Api.Models.Tasks;

namespace AutomatedTesting.Api.Repositories.Tasks
{
    public interface ITaskRepository
    {
        Task<int> CreateTask(CreateTaskRequestModel request, CancellationToken ct);
    }
}