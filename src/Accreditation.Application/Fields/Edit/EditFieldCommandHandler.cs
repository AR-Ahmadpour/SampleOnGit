using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using SharedKernel;


namespace Accreditation.Application.Fields.Edit
{
    internal sealed class EditFieldCommandHandler :
        ICommandHandler<EditFieldCommand, Guid>
    {

        private readonly IFieldRepository _fieldRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditFieldCommandHandler(IFieldRepository fieldRepository, IUnitOfWork unitOfWork)
        {
            _fieldRepository = fieldRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(EditFieldCommand request, CancellationToken cancellationToken)
        {

            var field = await _fieldRepository.FindAsync(request.GUID, cancellationToken);

            if (field is null)
            {
                return Result.Failure<Guid>(FieldsErrors.NotFound);
            }

            if (field.IsDeleted)
            {
                return Result.Failure<Guid>(FieldsErrors.NotActive);
            }

            var instanceTypeIdsString = string.Join(",", request.InstanceTypeIds);

            field.Edit(request.Title,request.TitleCode,instanceTypeIdsString);

            await _unitOfWork.SaveChangesAsync();

            return field.GUID;

        }
    }
}
