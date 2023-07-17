using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;

namespace Todo_App.Application.TodoItems.Commands.DeleteTodoItem;
public class RemoveTaggedTodoItemCommand:IRequest
{
    public int TodoItemID { get; set; }
    public int TagItemID { get; set; }
}


public class RemoveTaggedTodoItemCommandHandler : IRequestHandler<RemoveTaggedTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveTaggedTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(RemoveTaggedTodoItemCommand request, CancellationToken cancellationToken)
    {
        var id =  _context.TagItems.Include(todo => todo.TodoItems).Single(id => id.Id == request.TagItemID);
        var items = _context.TodoItems.Single(todo => todo.Id == request.TodoItemID);
        id.TodoItems!.Remove(items);

       await _context.SaveChangesAsync(cancellationToken);
        return Task.FromResult(Unit.Value).Result;
    }
}
