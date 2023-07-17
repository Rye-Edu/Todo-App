using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.TagItems.Commands.CreateTagItem;
using Todo_App.Application.TagItems.Commands.UpdateTagItem;
using Todo_App.Application.TagItems.Queries.GetTagItems;
using Todo_App.Application.TagItems.Queries.TodoITemTags;

namespace Todo_App.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaggingController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public TaggingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("tag-items")]
    public async Task<IEnumerable<TagItemQuery>> GetTagItems()
    {

        return await _mediator.Send(new TagItemQuery());
    }

    [HttpGet("tagged-items/{id}")]
    public async Task<IEnumerable<TaggedItemsQuery>> GetTaggedItems(int id) {

        return await _mediator.Send(new TaggedItemsQuery { Id = id});
    }

    [HttpGet("tagged-items/all")]
    public async Task<IEnumerable<AllTaggedItemsQuery>> GetAllTaggedItems()
    {

        return await _mediator.Send(new AllTaggedItemsQuery());
    }

    [HttpGet("{filter}")]
    public async Task<IEnumerable<FilterByTagQuery>> FilterByTagName(string filter) {
        return await _mediator.Send(new FilterByTagQuery { TagName = filter});
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<int>> CreateTagItem(CreateTagItemCommand createTagItemCommand) {

        return await _mediator.Send(createTagItemCommand);
    
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateTagItem(UpdateTagItemCommand updateTagItemCommand) {

        await _mediator.Send(updateTagItemCommand);
        return NoContent();
    }
}
