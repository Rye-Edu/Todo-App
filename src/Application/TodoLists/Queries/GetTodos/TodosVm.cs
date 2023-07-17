﻿using Todo_App.Application.Common.Mappings;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.TodoLists.Queries.GetTodos;

public class TodosVm
{
    public IList<PriorityLevelDto> PriorityLevels { get; set; } = new List<PriorityLevelDto>();

    public IList<TodoListDto> Lists { get; set; } = new List<TodoListDto>();

    public IReadOnlyCollection<ColourDto> Colour { get; set; } = new List<ColourDto>();
}