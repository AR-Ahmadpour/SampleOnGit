﻿using Accreditation.Domain.AccInstanceStandardEnvironmentStandardsResults.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.accInstanceStandardEnvironmentStandardsResults;

public interface IAccInstanceStandardEnvironmentStandardsResultRepository
{
    void Add(AccInstanceStandardEnvironmentStandardsResult accInstanceStandardResult);
    Task<List<AccInstanceStandardEnvironmentStandardsResult>> GetListByAccInstanceStandardAsync(Guid accInstanceStandardGuid);
    void Delete(AccInstanceStandardEnvironmentStandardsResult accInstanceStandardResult);
    Task<AccInstanceStandardEnvironmentStandardsResult> FindByAccStandardGuidAsync(Guid accInsStandardGuid);
}
