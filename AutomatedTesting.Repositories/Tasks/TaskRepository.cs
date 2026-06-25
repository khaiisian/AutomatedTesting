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

    public async Task<List<TaskResponseModel>> GetItemList(CancellationToken ct)
    {
        var lst = await _appDbContext.TaskItems.ToListAsync(ct);
        var response = lst.Select(x => Mapper.Map(x)).ToList();
        return response;
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
