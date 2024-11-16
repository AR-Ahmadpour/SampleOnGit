using Accreditation.Application.Body.GetByAccreditationInstanceGuid;
using Accreditation.Application.Headers.GetList;

namespace Accreditation.Api.Endpoints.Excels.Request
{
    public sealed class ExcelRequest
    {
        public GetHeaderDto Header { get; set; }
        public GetBodyByAccreditationInstanceGuidDto Body { get; set; }
    }
}
