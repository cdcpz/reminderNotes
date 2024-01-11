using Ensolvers.Note.Application.Base;
using Ensolvers.Note.Domain;
using Ensolvers.Note.Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.TagHandler.AddTag
{
    public class AddTagCommand : IRequestHandler<AddTagRequest, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Domain.Note> _noteRepository;

        public AddTagCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _noteRepository = unitOfWork.GenericRepository<Domain.Note>();
        }

        public async Task<ApiResponse<object>> Handle(AddTagRequest request, CancellationToken cancellationToken)
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
                        Resource.ErrorMessagesES.INTERNAL_SERVER_ERROR
                    );
                    return response;
                }

                var tag = note.AddTag(request.Tag, request.Color.Red, request.Color.Green, request.Color.Blue);
                if(tag is null)
                {
                    response = new ApiResponse<object>(
                        System.Net.HttpStatusCode.BadRequest,
                        Resource.ErrorMessagesES.INVALID_TAG
                    );
                    return response;
                }

                _noteRepository.Update(note);
                await _unitOfWork.CommitAsync();

                response = new ApiResponse<object>(
                    System.Net.HttpStatusCode.OK,
                    string.Format(Resource.SuccessfulMessagesES.RESOURCE_CREATED, new object[] { tag.Id })
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
