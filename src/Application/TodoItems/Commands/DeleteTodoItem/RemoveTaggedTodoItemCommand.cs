using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;

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
        var tagItems =  await _context.TagItems.Include(todo => todo.TodoItems).SingleOrDefaultAsync(id => id.Id == request.TagItemID);
        var todos = tagItems!.TodoItems!.SingleOrDefault(todoId => todoId.Id == request.TodoItemID);
        if (todos is null || tagItems is null) {
            var error = $"{nameof(TagItem)} {request.TagItemID} {nameof(TodoItem)} {request.TodoItemID} ";
            throw new NotFoundException( error);
        }
        tagItems.TodoItems!.Remove(todos);

       await _context.SaveChangesAsync(cancellationToken);
        return Task.FromResult(Unit.Value).Result;
    }
}
