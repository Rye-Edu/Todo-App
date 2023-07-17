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

namespace Todo_App.Application.TagItems.Commands.UpdateTagItem;
public class UpdateTagItemListCommand: IRequest
{
    public int Id { get; set; }
    public int TodoId { get; set; }


}

public class UpdateTagItemListCommandHandler : IRequestHandler<UpdateTagItemListCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTagItemListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpdateTagItemListCommand request, CancellationToken cancellationToken)
    {
        var tagId = await _context.TagItems
          .FindAsync(new object[] { request.Id }, cancellationToken);
        var todoId = await _context.TagItems
          .FindAsync(new object[] { request.TodoId }, cancellationToken);
        if (tagId == null || todoId == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

      //  ICollection<TagItem> tagItems = new List<TagItem> { _context.TagItems!.FirstOrDefault( i => i.Id == tagId.Id) };
        //_context.TodoItems.Add(new TodoItem { TagItems = new TagItem { Id = tagId.Id });
        return Unit.Value;
    }
}
