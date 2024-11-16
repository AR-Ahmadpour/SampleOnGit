namespace Accreditation.Api.Endpoints.Excels.EditRequest
{
    public sealed class EditExcelRequest
    {
        public Guid UserGuid { get; set; }

        public Guid AccInstanceGuid { get; set; }

        public Guid FieldGuid { get; set;}
    }
}
