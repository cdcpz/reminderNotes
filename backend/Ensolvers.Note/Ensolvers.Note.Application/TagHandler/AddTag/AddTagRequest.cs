using Ensolvers.Note.Application.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.TagHandler.AddTag
{
    public class AddTagRequest : IRequest<ApiResponse<object>>
    {
        public string NoteId { get; set; }
        public string Tag { get; set; }
        public TagColorRequest Color { get; set; }
    }

    public class TagColorRequest
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}
