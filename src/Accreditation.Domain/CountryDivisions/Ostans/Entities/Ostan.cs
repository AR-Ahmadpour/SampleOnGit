using SharedKernel;

namespace Accreditation.Domain.CountryDivisions.Ostans.Entities;
public sealed class Ostan //: Entity
{
    public int Id { get; private set; }
    public string IntCode { get; private set; } 
    public string Title { get; private set; } 
    public bool IsDeleted { get; private set; }
    public int SortOrder { get; private set; }

    private Ostan() { }

    //private Ostan(Guid id) => GUID = id;

    private Ostan(  string intCode, string title, bool isDeleted, int orderId)
    //: base(gUID)
    {
        IntCode = intCode;
        Title = title;
        IsDeleted = isDeleted;
        SortOrder = orderId;
    }
    public static Ostan Create
       (string intCode, string title, bool isDeleted, int orderId)
   => new Ostan(  intCode, title, isDeleted, orderId);

}