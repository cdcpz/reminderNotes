using Ensolvers.Note.Application.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.NoteHandler.GetNotes
{
    public class GetNotesRequest : IRequest<ApiResponse<IEnumerable<Domain.Note>>> {
        public string Track { get; set; }
        public GetNotesRequest() { }
        public GetNotesRequest(string track)
        {
            Track = track;
        }
    }
}
