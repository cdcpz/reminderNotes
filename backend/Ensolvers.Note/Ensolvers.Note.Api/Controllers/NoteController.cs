using Ensolvers.Note.Application.Base;
using Ensolvers.Note.Application.NoteHandler.AddNote;
using Ensolvers.Note.Application.NoteHandler.GetNotes;
using Ensolvers.Note.Application.NoteHandler.RemoveNote;
using Ensolvers.Note.Application.TagHandler.RemoveTag;
using Ensolvers.Note.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ensolvers.Note.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private readonly IMediator _mediator;

        public NoteController(ILogger<NoteController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("Get", Name = "GetNotes")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Domain.Note>>>> GetNotes()
        {
            string track = HttpContext.Request.Query["track"].ToString();
            var response = await _mediator.Send(new GetNotesRequest(track));
            return Ok(response);
        }

        [HttpPost("Add", Name = "AddNote")]
        public async Task<ActionResult<ApiResponse<object>>> AddNote(ManageNoteRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("Delete/{Id}", Name = "DeleteNote")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(string Id)
        {
            var response = await _mediator.Send(new RemoveNoteRequest() { NoteId = Id });
            return Ok(response);
        }
    }
}