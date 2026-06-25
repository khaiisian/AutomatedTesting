using AutomatedTesting.Db.AppDbContextModels;
using MediatR;

namespace AutomatedTesting.Api.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommand: IRequest<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class CreateTaskHandler: IRequestHandler<CreateTaskCommand, int>
{
    private readonly AppDbContext _appDbContext;

    public CreateTaskHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(CreateTaskCommand reqeust, CancellationToken ct)
    {
        var item = new TaskItem
        {
            Title = reqeust.Title,
            Description = reqeust.Description,
            CreatedAt = DateTime.UtcNow,
            Iscompleted = false,
        };

        await _appDbContext.AddAsync(item);
        int result = await _appDbContext.SaveChangesAsync(ct);
        return result;
    }
}
