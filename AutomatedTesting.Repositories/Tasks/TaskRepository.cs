using AutomatedTesting.Shared.Models.Tasks;
using AutomatedTesting.Db.AppDbContextModels;
using Microsoft.EntityFrameworkCore;
using AutomatedTesting.Shared.Mapper;

namespace AutomatedTesting.Repositories.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _appDbContext;

    public TaskRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<TaskResponseModel>> GetTaskList(CancellationToken ct)
    {
        var lst = await _appDbContext.TaskItems.ToListAsync(ct);
        var response = lst.Select(x => Mapper.Map(x)).ToList();
        return response;
    }

    public async Task<TaskResponseModel?> GetTaskById (int id, CancellationToken ct)
    {
        var task = await _appDbContext.TaskItems.FirstOrDefaultAsync(x => x.Id == id, ct);
        return task is null ? null: Mapper.Map(task);
    }

    public async Task<int> CreateTask(CreateTaskRequestModel request, CancellationToken ct)
    {
        var item = new TaskItem
        {
            Title = request.Title!,
            Description = request.Description,
            Iscompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        await _appDbContext.AddAsync(item);
        var result = await _appDbContext.SaveChangesAsync(ct);
        return result;
    }

    public async Task<int> UpdateTask(int id, UpdateTaskRequestModel request, CancellationToken ct)
    {
        var item = await _appDbContext.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null) return -1;

        if (!string.IsNullOrEmpty(request.Title)) item.Title = request.Title;
        if (!string.IsNullOrEmpty(request.Description)) item.Description = request.Description;
        if (request.IsCompleted.HasValue) item.Iscompleted = request.IsCompleted.Value;

        int result = await _appDbContext.SaveChangesAsync(ct);
        return result;
    }

    public async Task<int> DeleteTask(int id, CancellationToken ct)
    {
        var item = await _appDbContext.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null) return -1;

        _appDbContext.TaskItems.Remove(item);

        int result = await _appDbContext.SaveChangesAsync(ct);
        return result;
    }
}
