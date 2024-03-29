﻿using Ensolvers.Note.Application.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.NoteHandler.AddNote
{
    public class ManageNoteRequest : IRequest<ApiResponse<object>>
    {
        public string? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
