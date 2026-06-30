using AutomatedTesting.Db.AppDbContextModels;
using AutomatedTesting.Shared.Models.Tasks;

namespace AutomatedTesting.Repositories.Tasks
{
    public interface ITaskRepository
    {
        Task<int> CreateTask(CreateTaskRequestModel request, CancellationToken ct);
        Task<int> DeleteTask(int id, CancellationToken ct);
        Task<TaskResponseModel?> GetTaskById(int id, CancellationToken ct);
        Task<List<TaskResponseModel>> GetTaskList(CancellationToken ct);
        Task<int> UpdateTask(int id, UpdateTaskRequestModel request, CancellationToken ct);
    }
}