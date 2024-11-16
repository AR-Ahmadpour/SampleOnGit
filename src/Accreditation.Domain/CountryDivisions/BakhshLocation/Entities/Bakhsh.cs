using SharedKernel;

namespace Accreditation.Domain.CountryDivisions.BakhshLocation.Entities;

public sealed class Bakhsh /*: Entity*/
{
    public int Id { get; private set; }
    public string IntCode { get; private set; }
    public string Title { get; private set; }
    public int ShahrestanId { get; private set; }
    public Accreditation.Domain.CountryDivisions.Shahrestan.Entities.Shahrestan Shahrestan { get; private set; } = null!;
    public bool IsDeleted { get; private set; }
    public int SortOrder { get; private set; }
    private Bakhsh() { }

   // private Bakhsh(Guid id) => GUID = id;

   // public Bakhsh(Guid gUID, string intCode, string title, bool isDeleted, int orderId)
   // : base(gUID)
   // {
   //     IntCode = intCode;
   //     Title = title;
   //     IsDeleted = isDeleted;
   //     OrderId = orderId;
   // }
   // public static Bakhsh Create
   //    (string intCode, string title, bool isDeleted, int orderId)
   //=> new Bakhsh(Guid.NewGuid(), intCode, title, isDeleted, orderId);
}
