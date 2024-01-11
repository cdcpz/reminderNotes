using Ensolvers.Note.Application.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.TagHandler.RemoveTag
{
    public class RemoveTagRequest : IRequest<ApiResponse<object>>
    {
        [Required]
        public string NoteId { get; set; }
        [Required]
        public string TagId { get; set; }
    }
}
