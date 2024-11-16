using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Universityes.GetAll
{

    public sealed record GetAllUniversityQuery(bool Run) : IQuery<List<GetAllUniversityDto>>;

}
