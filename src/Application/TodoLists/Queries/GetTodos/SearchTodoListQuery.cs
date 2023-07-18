using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Enums;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.TodoLists.Queries.GetTodos;
public class SearchTodoListQuery : IRequest<TodosVm> {

    public string? Title { get; set; }

}

public class SearchTodoListQueryHandler : IRequestHandler<SearchTodoListQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchTodoListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<TodosVm> Handle(SearchTodoListQuery request, CancellationToken cancellationToken)
    {
       

        if (string.IsNullOrEmpty(request.Title))
        {
          throw new ArgumentNullException(nameof(request.Title));
        }

        return  new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
               .Cast<PriorityLevel>()
               .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
               .ToList(),

            Lists = await _context.TodoLists
               .AsNoTracking().Where(todoList => todoList.Title!.Contains(request.Title!))
               .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
               .OrderBy(t => t.Title)
               .ToListAsync(cancellationToken),
            Colour = Colour.GetSupportedColors().Select(c => new ColourDto { Colour = c.Code }).ToList()

        };
      
    }
}

