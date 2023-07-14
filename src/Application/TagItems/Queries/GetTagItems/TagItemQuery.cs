using System;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.Common.Mappings;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TagItems.Queries.GetTagItems;
public class TagItemQuery:IRequest<IEnumerable<TagItemQuery>>, IMapFrom<TagItem>
{
    public int Id { get; set; }
    public string? TagName { get; set; }
}

public class TagItemQueryHandler : IRequestHandler<TagItemQuery, IEnumerable<TagItemQuery>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public TagItemQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<IEnumerable<TagItemQuery>> Handle(TagItemQuery request, CancellationToken cancellationToken)
    {
        return await _context.TagItems.ProjectToListAsync<TagItemQuery>(_mapper.ConfigurationProvider);
    }
}
