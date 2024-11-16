﻿using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetRoleHospital
{
    public sealed record GetRoleHospitalQuery(bool IsHospital): IQuery< GetRoleHospitalDto>;
}