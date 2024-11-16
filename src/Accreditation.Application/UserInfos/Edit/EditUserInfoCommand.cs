﻿using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.UserInfos.Edit
{
    public sealed record EditUserInfoCommand( Guid UserInfoId,string Address, bool MaritalStatus, int ChildCount,
    int BirthOstandId, int BirthShahrId, int AddressOstanId, int AddressShahrId, string PersonalPicGUID, string KartMeliGUID,
    string ShenasnamehGUID)
  : ICommand<Guid>;
}