namespace SharedKernel;

public sealed record SelectListResponse(Guid Guid, string title);
public sealed record SelectListCountryDevisionResponse(int id, string title);
public sealed record GetListByEtebarDorehDto(Guid GUID, string Title, bool IsDeleted);

