using Ensolvers.Note.Application.Base;
using Ensolvers.Note.Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.NoteHandler.GetNotes
{
    internal class GetNotesQuery : IRequestHandler<GetNotesRequest, ApiResponse<IEnumerable<Domain.Note>>>
    {
        private readonly IGenericRepository<Domain.Note> _noteRepository;

        public GetNotesQuery(IUnitOfWork unitOfWork)
        {
            _noteRepository = unitOfWork.GenericRepository<Domain.Note>();
        }

        public async Task<ApiResponse<IEnumerable<Domain.Note>>> Handle(GetNotesRequest request, CancellationToken cancellationToken)
        {
            ApiResponse<IEnumerable<Domain.Note>> response;
            if(request is null)
            {
                response = new ApiResponse<IEnumerable<Domain.Note>>(
                    System.Net.HttpStatusCode.BadRequest, 
                    Resource.ErrorMessagesES.BAD_REQUEST
                );
                return response;
            }

            IEnumerable<Domain.Note> notes;

            Func<Domain.Note, bool> filter = (note) =>
            {
                return string.IsNullOrEmpty(request.Track) ? true : note.Compare(request.Track);
            };

            notes = await _noteRepository.Where(filter);
            response = new ApiResponse<IEnumerable<Domain.Note>>(
                Resource.SuccessfulMessagesES.SUCCESSFUL_PROCCESS, 
                notes
            );
            return response;
        }
    }
}
