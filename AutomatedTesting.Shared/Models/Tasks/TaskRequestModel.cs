namespace AutomatedTesting.Shared.Models.Tasks;

public class TaskRequestModel
{

}

public class CreateTaskRequestModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}

public class UpdateTaskRequestModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
}