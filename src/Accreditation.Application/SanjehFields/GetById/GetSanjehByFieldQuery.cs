using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Sanjehs.GetById;

namespace Accreditation.Application.SanjehFields.GetById
{
    public sealed record GetSanjehByFieldQuery(Guid fieldId,Guid? mehvarId) :
IQuery<List<GetSanjehsByFieldDto>>;
}
