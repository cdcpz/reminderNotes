using Ensolvers.Note.Application.Base;
using Ensolvers.Note.Domain;
using Ensolvers.Note.Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.TagHandler.RemoveTag
{
    public class RemoveTagCommand : IRequestHandler<RemoveTagRequest, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Domain.Tag> _tagRepository;
        private readonly IGenericRepository<Domain.Note> _noteRepository;
        public RemoveTagCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _noteRepository = unitOfWork.GenericRepository<Domain.Note>();
            _tagRepository = unitOfWork.GenericRepository<Tag>();
        }

        public async Task<ApiResponse<object>> Handle(RemoveTagRequest request, CancellationToken cancellationToken)
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
                var tag = await _tagRepository.Find(request.TagId);
                if (tag is null)
                {
                    response = new ApiResponse<object>(
                        System.Net.HttpStatusCode.NotFound,
                        Resource.ErrorMessagesES.NOT_FOUND
                    );
                    return response;
                }

                if(tag.NoteId != request.NoteId)
                {
                    response = new ApiResponse<object>(
                        System.Net.HttpStatusCode.BadRequest,
                        Resource.ErrorMessagesES.BAD_REQUEST
                    );
                    return response;
                }

                note = await _noteRepository.Find(request.NoteId);
                if (note is null)
                {
                    response = new ApiResponse<object>(
                        System.Net.HttpStatusCode.NotFound,
                        Resource.ErrorMessagesES.NOT_FOUND
                    );
                    return response;
                }

                note.RemoveTag(tag);

                _noteRepository.Update(note);
                await _unitOfWork.CommitAsync();

                response = new ApiResponse<object>(
                    System.Net.HttpStatusCode.OK,
                    string.Format(Resource.SuccessfulMessagesES.RESOURCE_DELETED, new object[] { tag.Id })
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
