﻿using Ensolvers.Note.Application.Base;
using Ensolvers.Note.Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.NoteHandler.RemoveNote
{
    public class RemoveNoteCommand : IRequestHandler<RemoveNoteRequest, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Domain.Note> _noteRepository;

        public RemoveNoteCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _noteRepository = unitOfWork.GenericRepository<Domain.Note>();
        }

        public async Task<ApiResponse<object>> Handle(RemoveNoteRequest request, CancellationToken cancellationToken)
        {
            ApiResponse<object> response;
            if (request is null)
            {
                response = new ApiResponse<object>(
                    System.Net.HttpStatusCode.BadRequest,
                    Resource.ErrorMessagesES.BAD_REQUEST
                );
                return response;
            }

            await _unitOfWork.BeginTransactionAsync();
            Domain.Note? note;
            try
            {
                note = await _noteRepository.Find(request.NoteId);
                if (note is null)
                {
                    response = new ApiResponse<object>(
                        System.Net.HttpStatusCode.NotFound,
                        Resource.ErrorMessagesES.NOT_FOUND
                    );
                    return response;
                }

                _noteRepository.Delete(note);
                await _unitOfWork.CommitAsync();

                response = new ApiResponse<object>(
                    System.Net.HttpStatusCode.OK,
                    string.Format(Resource.SuccessfulMessagesES.RESOURCE_DELETED, new object[] { note.Id })
                );
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                note = default;

                response = new ApiResponse<object>(
                    System.Net.HttpStatusCode.InternalServerError,
                    Resource.ErrorMessagesES.INTERNAL_SERVER_ERROR
                );
            }

            return response;
        }
    }
}
