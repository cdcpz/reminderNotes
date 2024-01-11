using Ensolvers.Note.Application.Base;
using Ensolvers.Note.Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.NoteHandler.AddNote
{
    public class ManageNoteCommand : IRequestHandler<ManageNoteRequest, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Domain.Note> _noteRepository;

        public ManageNoteCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _noteRepository = unitOfWork.GenericRepository<Domain.Note>();
        }

        public async Task<ApiResponse<object>> Handle(ManageNoteRequest request, CancellationToken cancellationToken)
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
                if(string.IsNullOrEmpty(request.Id))
                {
                    note = new Domain.Note(request.Title, request.Body);
                    await _noteRepository.AddAsync(note);

                    response = new ApiResponse<object>(
                        System.Net.HttpStatusCode.OK,
                        string.Format(Resource.SuccessfulMessagesES.RESOURCE_CREATED, new object[] { note.Id })
                    );
                }
                else
                {
                    note = await _noteRepository.Find(request.Id);
                    if(note is null)
                    {
                        response = new ApiResponse<object>(
                            System.Net.HttpStatusCode.NotFound,
                            Resource.ErrorMessagesES.NOT_FOUND
                        );
                        return response;
                    }

                    note.Update(request.Title, request.Body);
                    _noteRepository.Update(note);

                    response = new ApiResponse<object>(
                        System.Net.HttpStatusCode.OK,
                        string.Format(Resource.SuccessfulMessagesES.RESOURCE_MODIFIED, new object[] { note.Id })
                    );
                }
                await _unitOfWork.CommitAsync();
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
