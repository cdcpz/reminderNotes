using Ensolvers.Note.Application.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.NoteHandler.RemoveNote
{
    public class RemoveNoteRequest : IRequest<ApiResponse<object>>
    {
        [Required]
        public string NoteId { get; set; }
    }
}
