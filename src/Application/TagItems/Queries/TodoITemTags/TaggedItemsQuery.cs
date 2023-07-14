using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.Common.Mappings;
using Todo_App.Application.TagItems.Queries.GetTagItems;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TagItems.Queries.TodoITemTags;
public class TaggedItemsQuery:IRequest<IEnumerable<TaggedItemsQuery>>, IMapFrom<TagItem>
{
    public int Id { get; set; }
    public string? TagName { get; set; }

    public IList<TodoTagItemBriefVM>? TodoItems { get; set; }

}

public class TaggedITemsQueryHandler : IRequestHandler<TaggedItemsQuery, IEnumerable<TaggedItemsQuery>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public TaggedITemsQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<IEnumerable<TaggedItemsQuery>> Handle(TaggedItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.TagItems.Where(id => id.Id == request.Id)
            .ProjectToListAsync<TaggedItemsQuery>(_mapper.ConfigurationProvider);
        
    }
}