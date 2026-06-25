using AutomatedTesting.Db.AppDbContextModels;
using AutomatedTesting.Shared.Models.Tasks;

namespace AutomatedTesting.Repositories.Tasks
{
    public interface ITaskRepository
    {
        Task<int> CreateTask(CreateTaskRequestModel request, CancellationToken ct);
        Task<List<TaskResponseModel>> GetItemList(CancellationToken ct);
    }
}