using AutomatedTesting.Db.AppDbContextModels;
using AutomatedTesting.Shared.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedTesting.Shared.Mapper;

public static class Mapper
{
    public static TaskResponseModel Map(TaskItem taskItem)
    {
        return new TaskResponseModel
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Iscompleted = taskItem.Iscompleted,
            CreatedAt = taskItem.CreatedAt,
        };
    }
}
