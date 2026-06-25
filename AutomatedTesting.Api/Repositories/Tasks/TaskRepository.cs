using AutomatedTesting.Api.Models.Tasks;
using AutomatedTesting.Db.AppDbContextModels;

namespace AutomatedTesting.Api.Repositories.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _appDbContext;

    public TaskRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> CreateTask(CreateTaskRequestModel request, CancellationToken ct)
    {
        var item = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Iscompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        await _appDbContext.AddAsync(item);
        var result = await _appDbContext.SaveChangesAsync(ct);
        return result;
    }
}
