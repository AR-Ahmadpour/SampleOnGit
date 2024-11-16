using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Domain.Fields.Entities;
using SharedKernel;

namespace Accreditation.Application.Fields.Add
{
    internal sealed class AddFieldCommandHandler :
        ICommandHandler<AddFieldCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEtebarDorehRepository _etebarDorehRepository;
        private readonly IFieldRepository _fieldRepository;

        public AddFieldCommandHandler(IUnitOfWork unitOfWork, IEtebarDorehRepository etebarDorehRepository, IFieldRepository fieldRepository)
        {
            _unitOfWork = unitOfWork;
            _etebarDorehRepository = etebarDorehRepository;
            _fieldRepository = fieldRepository;
        }

        public async Task<Result<Guid>> Handle(AddFieldCommand request, CancellationToken cancellationToken)
        {
            if (await _etebarDorehRepository.FindAsync(request.EtebarDorehGuid, cancellationToken) is null)
            {
                return Result.Failure<Guid>(EtebarDorehErrors.NotFound);
            }


            var instanceTypeIdsString = string.Join(",", request.InstanceTypeIds);


            var fieldAdded = Field.Create(
                request.EtebarDorehGuid,
                request.Title,
                request.TitleCode,
                instanceTypeIdsString
            );

            _fieldRepository.Add(fieldAdded);

            await _unitOfWork.SaveChangesAsync();

            return fieldAdded.GUID;
        }
    }
}
