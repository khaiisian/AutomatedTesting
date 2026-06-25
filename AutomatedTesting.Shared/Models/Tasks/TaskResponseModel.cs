using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedTesting.Shared.Models.Tasks;

public class TaskResponseModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool Iscompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class CreateTaskResponseModel
{

}