namespace Accreditation.Api.Endpoints.SanjehFields.Add
{
    public sealed record AddSanjehFieldRequest( 
          Guid FieldGuid,
          List<Guid> SanjehGuids);
}
