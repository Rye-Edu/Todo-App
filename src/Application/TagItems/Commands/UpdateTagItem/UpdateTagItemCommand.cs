using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.Common.Mappings;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TagItems.Commands.UpdateTagItem;
public class UpdateTagItemCommand: IRequest, IMapFrom<TagItem>
{
    public int Id { get; set; }
    public string? TagName { get; set; }

}

public class UpdateTagItemCommandHandler : IRequestHandler<UpdateTagItemCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTagItemCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateTagItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TagItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }
     
        entity.TagName = request.TagName;
        await _context.SaveChangesAsync(cancellationToken);


        return Unit.Value;
    }
}
