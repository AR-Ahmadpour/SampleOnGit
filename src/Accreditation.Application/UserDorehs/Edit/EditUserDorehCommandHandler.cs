using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.DorehAmoozeshis;
using Accreditation.Application.Common.Interfaces.Persistence.UserDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.UserInfos;
using Accreditation.Application.UserInfos;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.UserDorehs.Edit
{
    internal sealed class EditUserDorehCommandHandler :
        ICommandHandler<EditUserDorehCommand, Guid>
    {
        private readonly IDorehAmoozeshiRepository _dorehAmoozeshiRepository;
        private readonly IUserDorehRepository _userDorehRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditUserDorehCommandHandler(IDorehAmoozeshiRepository dorehAmoozeshiRepository, IUserDorehRepository userDorehRepository, IUnitOfWork unitOfWork)
        {
            _dorehAmoozeshiRepository = dorehAmoozeshiRepository;
            _userDorehRepository = userDorehRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(EditUserDorehCommand request, CancellationToken cancellationToken)
        {
            if (await _dorehAmoozeshiRepository.FindAsync(request.DorehAmoozeshiGuid, cancellationToken) is null)
            {
                return Result.Failure<Guid>(UserDorehErrors.DorehAmoozeshiNotFound);
            }

            var userDoreh = await _userDorehRepository.FindEditAsync(request.UserDorehId, cancellationToken);

            if (userDoreh is null)
            {
                return Result.Failure<Guid>(UserDorehErrors.UserDorehFound);
            }

            userDoreh.Edit(
                request.DorehAmoozeshiGuid,
                request.DorehTitle,
                request.BarGozarKonandeh,
                request.DorehHours,
                request.DorehRole);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userDoreh.GUID;
        }
    }
}
