using SharedKernel;
namespace Accreditation.Domain.CountryDivisions.Shahrestan.Entities;

public sealed class Shahrestan : Entity
{
    public int Id { get; private set; }
    public string IntCode { get; private set; }
    public string Title { get; private set; }
    public int OstanId { get; private set; }
    public Accreditation.Domain.CountryDivisions.Ostans.Entities.Ostan Ostan { get; private set; } = null!;
    public bool IsDeleted { get; private set; }
    public int OrderId { get; private set; }
    private Shahrestan() { }

    private Shahrestan(Guid id) => GUID = id;

    public Shahrestan(Guid gUID, string intCode, string title, bool isDeleted, int orderId)
    : base(gUID)
    {
        IntCode = intCode;
        Title = title;
        IsDeleted = isDeleted;
        OrderId = orderId;
    }
    public static Shahrestan Create
       (string intCode, string title, bool isDeleted, int orderId)
   => new Shahrestan(Guid.NewGuid(), intCode, title, isDeleted, orderId);
}