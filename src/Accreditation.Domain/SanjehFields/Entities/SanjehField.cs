using Accreditation.Domain.Fields.Entities;
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Domain.SanjehFields.Entities
{
    public sealed class SanjehField : Entity
    {
        public SanjehField() { }
        public Guid SanjehGUID { get; private set; }
        public Sanjeh Sanjeh { get; private set; }
        public Guid FieldGUID { get; private set; }
        public Field Field { get; private set; }
        public Guid UserGUID { get; private set; }
        public User User { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreationDate { get; private set; }

        private SanjehField(Guid SanjehGuid, Guid FieldGuid,
            Guid UserGuid, DateTime creationDate)
        {
            SanjehGUID = SanjehGuid;
            CreationDate = creationDate;
            FieldGUID = FieldGuid;
            UserGUID = UserGuid;
        }

        public static SanjehField Create(Guid SanjehGuid, Guid FieldGuid,
            Guid UserGuid, DateTime CreationDate)
        {
            return new SanjehField(SanjehGuid, FieldGuid, UserGuid, CreationDate);
        }


    }
}
