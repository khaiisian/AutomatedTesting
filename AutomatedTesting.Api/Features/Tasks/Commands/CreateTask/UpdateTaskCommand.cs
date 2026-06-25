//using AutomatedTesting.Db.AppDbContextModels;
//using MediatR;

//namespace AutomatedTesting.Api.Features.Tasks.Commands.CreateTask;

//public class UpdateTaskCommand: IRequest<int>
//{
//    public int Id { get; set; }
//    public string Title { get; set; }
//    public string Description { get; set; }
//    public bool IsCompleted { get; set; }
//}

//public class UpdateTaskHandler: IRequestHandler<UpdateTaskCommand, int>
//{
//    private readonly AppDbContext _appDbContext;

//    public UpdateTaskHandler(AppDbContext appDbContext)
//    {
//        _appDbContext = appDbContext;
//    }

//    public async Task<int> Handle (UpdateTaskCommand request, CancellationToken ct)
//    {

//    }
//}
