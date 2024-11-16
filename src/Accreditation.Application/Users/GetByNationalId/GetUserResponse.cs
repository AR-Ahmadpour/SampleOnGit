

using Accreditation.Domain.Users;

namespace Accreditation.Application.Users.GetById;

public sealed record GetUserResponse
{
    public GetUserResponse()
    { Roles = new List<Role>(); }
    public Guid GUID { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? FatherName { get; init; }
    public string? BirthCertNo { get; init; }
    public string? BirthCertSerial { get; init; }
    public string? BirthPlace { get; init; }
    public string? Mobile { get; init; }
    public string? Tel { get; init; }
    public string? Email { get; init; }
    public byte?  GenderId { get; init; }
    public DateOnly? DeathDate { get; init; }
    public bool? IsAlive { get; init; }
    public bool? SabteAhvalApproved { get; init; } = true;
    public bool? IsDeleted { get; init; }
    public long? ImageId { get; init; }
    public string? NationalCode { get; init; }
    public DateOnly? BirthDate { get; init; }
    public bool? AtbaKhareji { get; init; }
    public Guid? SSOUserId { get; init; }
    public DateTime? CreatedDate { get; init; }
    public DateTime? UpdatedDate { get; init; }
    public Guid? CreatedByGUID { get; init; }
    public Guid? UpdatedByGUID { get; init; }
    public string Password { get; init; }
    public bool? MobileConfirmed { get; init; }
    public ICollection<Role> Roles { get; init; }





}
