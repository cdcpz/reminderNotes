using Ensolvers.Note.Application.Base;
using Ensolvers.Note.Application.TagHandler.AddTag;
using Ensolvers.Note.Application.TagHandler.RemoveTag;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ensolvers.Note.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> _logger;
        private readonly IMediator _mediator;

        public TagController(ILogger<TagController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("Add", Name = "AddTag")]
        public async Task<ActionResult<ApiResponse<object>>> Add(AddTagRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("Delete/{NoteId}/{TagId}", Name = "DeleteTag")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(string NoteId, string TagId)
        {
            var response = await _mediator.Send(new RemoveTagRequest() { 
                NoteId = NoteId, 
                TagId = TagId 
            });
            return Ok(response);
        }
    }
}