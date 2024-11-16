using Accreditation.Domain.Common.Enums;

namespace Accreditation.Domain.EvaluationArzyabs.Dtos;

public sealed class GetAllEvaluationArzyabsDto
{
    public Guid Guid { get; set; }
    public Guid ArzyabGuid { get; set; }
    public string? ArzyabName { get; set; }
    public string NationalCode { get; set; }
    public string MobileNo { get; set; }
    public string RoleName { get; set; }
    public int RoleId { get; set; }
    public List<Guid> FieldIds { get; set; }
    public List<FieldDto> FieldDtos { get; set; }
}
public sealed class FieldDto
{
    public Guid Guid { get; set; }
    public string? Name { get; set; }
}
