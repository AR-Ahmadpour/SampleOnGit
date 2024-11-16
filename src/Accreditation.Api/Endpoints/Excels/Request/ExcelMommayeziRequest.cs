using Accreditation.Application.Body.GetByAccreditationInstanceGuid;
using Accreditation.Application.Headers.GetList;

namespace Accreditation.Api.Endpoints.Excels.Request
{
    public class ExcelMommayeziRequest
    {
        public GetHeaderDto Header { get; set; }
        public GetBodyByAccreditationInstanceGuidMommayeziDto Body { get; set; }
    }
}
