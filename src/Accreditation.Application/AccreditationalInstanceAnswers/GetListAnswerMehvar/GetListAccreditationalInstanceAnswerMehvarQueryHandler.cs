using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationalInstanceAnswers;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using SharedKernel;

namespace Accreditation.Application.AccreditationalInstanceAnswers.GetListAnswerMehvar;

internal sealed class GetListAccreditationalInstanceAnswerMehvarQueryHandler
    : IQueryHandler<GetListAccreditationalInstanceAnswerMehvarQuery, AccreditationalInstanceAnswersGetListAnswerMehvarDto>
{
    private readonly IAccreditationInstanceAnswerRepository _accreditationInstanceAnswerRepository;
    private readonly IMehvarRepository _mehvarRepository;
    private readonly IZirMehvarRepository _zirMehvarRepository;
    private readonly IStandardRepository _standardRepository;
    private readonly ISanjehRepository _sanjehRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;

    public GetListAccreditationalInstanceAnswerMehvarQueryHandler(IAccreditationInstanceAnswerRepository accreditationInstanceAnswerRepository,
                                                            IMehvarRepository mehvarRepository,
                                                            IZirMehvarRepository zirMehvarRepository,
                                                            IStandardRepository standardRepository,
                                                            ISanjehRepository sanjehRepository,
                                                            IAccreditationInstanceRepository accreditationInstanceRepository)
    {
        _accreditationInstanceAnswerRepository = accreditationInstanceAnswerRepository;
        _mehvarRepository = mehvarRepository;
        _zirMehvarRepository = zirMehvarRepository;
        _standardRepository = standardRepository;
        _sanjehRepository = sanjehRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
    }

    public async Task<Result<AccreditationalInstanceAnswersGetListAnswerMehvarDto>> Handle(
        GetListAccreditationalInstanceAnswerMehvarQuery request,
        CancellationToken cancellationToken)
    {
        //TODO
        var accInstanceAnswers = await _accreditationInstanceAnswerRepository.GetListAccInstanceAnswersAsync(request.AccreditationalInstaneGuid);
        var etebarDoreh = await _accreditationInstanceRepository.FindAsync(accInstanceAnswers.First().AccreditationInstanceGUID);
        var mehvars = await _mehvarRepository.GetAllByEtebarDorehIdMehvarsAsync(etebarDoreh.GUID, cancellationToken);
        var zirMehvars = await _zirMehvarRepository.GetSelectListAsync(etebarDoreh.GUID);
        var standards = _standardRepository.GetSelectListAsync(etebarDoreh.GUID);
        var sanjehs = _sanjehRepository.GetByEtebarDorehGuidAsync(etebarDoreh.GUID);

         return  new AccreditationalInstanceAnswersGetListAnswerMehvarDto();

    }
}

