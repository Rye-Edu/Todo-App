using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TagItems.Commands.CreateTagItem;
public class CreateTagItemCommand:IRequest<int>
{
    public string? TagName { get; set; }
    
}

public class CreateTagItemCommandHandler : IRequestHandler<CreateTagItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTagItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateTagItemCommand request, CancellationToken cancellationToken)
    {
        var tagItem = new TagItem(request.TagName!);
        await _context.TagItems.AddAsync(tagItem);
        await _context.SaveChangesAsync(cancellationToken);
        return tagItem.Id;
    }
}
