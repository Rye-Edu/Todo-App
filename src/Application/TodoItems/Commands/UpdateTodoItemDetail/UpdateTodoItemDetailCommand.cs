using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Enums;
using Todo_App.Domain.Events;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }
    public int ListId { get; init; }
    public int? TagItemId { get; set; }
    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }

    public string? Colour { get; set; }

   // public ICollection<TagItem>? TagItems { get; set; }
   
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);
        var tagItem = await _context.TagItems.Include(todos => todos.TodoItems).SingleOrDefaultAsync(id=> id.Id== request.TagItemId);
        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }
        if (tagItem != null) {
          
            entity.AddDomainEvent(new TaggedTodoItemAddedEvent(entity!, tagItem!));
        }
       
      

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;
        entity.Colour = (Colour)request.Colour!;

        
 
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
