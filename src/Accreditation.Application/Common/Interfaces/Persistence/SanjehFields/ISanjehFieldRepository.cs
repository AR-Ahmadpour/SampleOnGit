using Accreditation.Application.NotNaSanjehs.GetList;
using Accreditation.Application.SanjehFields.GetById;
using Accreditation.Domain.NotNaSanjehs.Entities;
using Accreditation.Domain.SanjehFields.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.SanjehFields
{
    public interface ISanjehFieldRepository
    {
        void Add(SanjehField sanjehField);
        Task<SanjehField?> FindAsync(Guid snj_GUID, Guid FieldGUID, CancellationToken cancellationToken);
        Task<List<SanjehField>> GetByFieldGuidAsync(Guid fieldGuid , CancellationToken cancellationToken);
        void RemoveRange(IEnumerable<SanjehField> sanjehFields);
        Task<List<GetSanjehsByFieldDto>> GetAllSanjehsByFieldId(Guid fieldGuid,Guid? mehvarid, CancellationToken cancellationToken = default);
    }

}
