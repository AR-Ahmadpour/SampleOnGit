using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Tahsilats;
using Accreditation.Domain.Sanjehs.Entities;
using SharedKernel;

namespace Accreditation.Application.Tahsilats.Edit
{
    internal sealed class EditTahsilatCommandHandler :
        ICommandHandler<EditTahsilatCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITahsilatRepository _tahsilatRepository;

        public EditTahsilatCommandHandler(IUnitOfWork unitOfWork, ITahsilatRepository tahsilatRepository)
        {
            _unitOfWork = unitOfWork;
            _tahsilatRepository = tahsilatRepository;
        }

        public async Task<Result<Guid>> Handle(EditTahsilatCommand request, CancellationToken cancellationToken)
        {
            var tahsilat = await _tahsilatRepository.FindAsyncEdit(request.TahsilatGuid, cancellationToken);

            if (tahsilat is null)
            {
                return Result.Failure<Guid>(TahsilatError.TahsilatNotFound);
            }

            tahsilat.Edit(request.MaghtaTahsiliGuid,
                request.ReshtehTahsiliGuid,
                request.UniversityName,
                request.GraduationDate,
                request.MadrakGuid);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tahsilat.GUID;
        }
    }
}
