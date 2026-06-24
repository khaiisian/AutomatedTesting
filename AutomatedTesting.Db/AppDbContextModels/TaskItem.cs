using System;
using System.Collections.Generic;

namespace AutomatedTesting.Db.AppDbContextModels;

public partial class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool Iscompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}
