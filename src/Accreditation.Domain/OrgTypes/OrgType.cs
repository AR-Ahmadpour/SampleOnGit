
using SharedKernel;

namespace Accreditation.Domain.OrgTypes;
public sealed class OrgType : Entity
{
    public OrgType( Guid gUID, string intCode, string intTitle, string title, bool isDeleted, int sortOrder)
        : base( gUID)
    {
        IntCode = intCode;
        IntTitle = intTitle;
        Title = title;
        IsDeleted = isDeleted;
        SortOrder = sortOrder;
    }

    private OrgType()
    {
    }

    public string? IntCode { get; private set; }
    public string? IntTitle { get; private set; }
    public string Title { get; private set; }
    public bool IsDeleted { get; private set; }
    public int SortOrder { get; private set; }

    public static OrgType Create
        (string intCode, string intTitle, string title, bool isDeleted, int sortOrder)
    => new OrgType( Guid.NewGuid(), intCode, intTitle, title, isDeleted, sortOrder);
}