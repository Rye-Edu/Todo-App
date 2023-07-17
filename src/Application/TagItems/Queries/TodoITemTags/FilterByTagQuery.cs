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
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TagItems.Queries.TodoITemTags;
public class FilterByTagQuery : IRequest<IEnumerable<FilterByTagQuery>>, IMapFrom<TagItem>
{
    public string? TagName { get; set; }
    public int Id { get; set; }

    public IEnumerable<TodoTagItemBriefVM>? TodoItems { get; set; }
}

public class FilterByTagQueryHandler : IRequestHandler<FilterByTagQuery, IEnumerable<FilterByTagQuery>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public FilterByTagQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<IEnumerable<FilterByTagQuery>> Handle(FilterByTagQuery request, CancellationToken cancellationToken)
    {

        return await _context.TagItems.Where(tagName => tagName.TagName == request.TagName)
            .ProjectToListAsync<FilterByTagQuery>(_mapper.ConfigurationProvider);
       
    }
}

