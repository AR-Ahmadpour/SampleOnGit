
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Interfaces.Persistence.NotNaSanjehs;
using Accreditation.Application.Common.Interfaces.Persistence.SanjehFields;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.Fields;
using Accreditation.Application.Sanjehs;
using Accreditation.Domain.NotNaSanjehs.Entities;
using Accreditation.Domain.SanjehFields.Entities;
using SharedKernel;


namespace Accreditation.Application.SanjehFields.Add
{
    internal class AddSanjehFieldCommandHandler
    :ICommandHandler<AddSanjehFieldCommand , Guid>
    {
        private readonly ISanjehFieldRepository _sanjehFieldRepository;
        private readonly ISanjehRepository _sanjehRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICurrentUser _userContext;

        public AddSanjehFieldCommandHandler(ISanjehFieldRepository sanjehFieldRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, ICurrentUser userContext, ISanjehRepository sanjehRepository, IFieldRepository fieldRepository)
        {
            _sanjehFieldRepository = sanjehFieldRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userContext = userContext;
            _sanjehRepository = sanjehRepository;
            _fieldRepository = fieldRepository;
        }

        public async Task<Result<Guid>> Handle(AddSanjehFieldCommand request, CancellationToken cancellationToken)
        {
            if (await _fieldRepository.FindAsync(request.FieldGuid, cancellationToken) is null)
            {
                return Result.Failure<Guid>(FieldsErrors.NotFound);
            }

            foreach(var sanjehGuid in request.SanjehGuids)
            {
                if (await _sanjehRepository.FindAsync(sanjehGuid,cancellationToken) is null)
                {
                    return Result.Failure<Guid>(SanjehErrors.NotFoundSanjeh);
                }
            }


            var existingFieldEntries = await _sanjehFieldRepository.GetByFieldGuidAsync(request.FieldGuid,cancellationToken);

            if (existingFieldEntries.Any())
            {
                _sanjehFieldRepository.RemoveRange(existingFieldEntries);
            }

            Guid lastCreatedGuid = Guid.Empty;

            foreach (var sanjeh in request.SanjehGuids)
            {
                var sanjehFields = SanjehField.Create(sanjeh,request.FieldGuid, Guid.Parse(_userContext.UserId),
                    _dateTimeProvider.Now);

                _sanjehFieldRepository.Add(sanjehFields);
                lastCreatedGuid = sanjehFields.GUID;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(lastCreatedGuid);
        }
    }
}
