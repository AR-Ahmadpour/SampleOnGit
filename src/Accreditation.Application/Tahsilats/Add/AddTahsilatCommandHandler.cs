using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Tahsilats;
using Accreditation.Domain.Tahsilats.Entities;
using SharedKernel;

namespace Accreditation.Application.Tahsilats.Add
{
    internal sealed class AddTahsilatCommandHandler :
        ICommandHandler<AddTahsilatCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITahsilatRepository _tahsilatRepository;

        public AddTahsilatCommandHandler(IUnitOfWork unitOfWork, ITahsilatRepository tahsilatRepository)
        {
            _unitOfWork = unitOfWork;
            _tahsilatRepository = tahsilatRepository;
        }

        public async Task<Result<Guid>> Handle(AddTahsilatCommand request, CancellationToken cancellationToken)
        {

            var tahsilat = Tahsilat.Create(request.UserGuid, request.ReshtehTahsiliGuid, request.MaghtaTahsiliGuid,
                request.MadrakGuid, request.UniversityName, request.GraduationDate);



            _tahsilatRepository.Add(tahsilat);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tahsilat.GUID;
        }
    }
}
