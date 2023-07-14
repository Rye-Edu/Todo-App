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
using Todo_App.Application.Common.Mappings;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TagItems.Queries.TodoITemTags;
public class AllTaggedItemsQuery : IRequest<IEnumerable<AllTaggedItemsQuery>>, IMapFrom<TagItem>
{
    public int Id { get; set; }
    public string? TagName { get; set; }

    public IList<TodoTagItemBriefVM>? TodoItems { get; set; }


}

public class AllTaggedITemsQueryHandler : IRequestHandler<AllTaggedItemsQuery, IEnumerable<AllTaggedItemsQuery>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public AllTaggedITemsQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<IEnumerable<AllTaggedItemsQuery>> Handle(AllTaggedItemsQuery request, CancellationToken cancellationToken)
    {
      
        return await _context.TagItems.ProjectTo<AllTaggedItemsQuery>(_mapper.ConfigurationProvider).ToListAsync();
       

    }
}