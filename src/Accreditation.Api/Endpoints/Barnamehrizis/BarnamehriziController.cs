using Accreditation.Application.Common.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Barnamehrizi
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class BarnamehriziController : ControllerBase
    {

        [HttpGet("list")]
        public async Task<IActionResult> GetEtebarDorehList([FromQuery] PagingParams criteria)
        {
            // Simulated delay to mimic real data fetching
            await Task.Delay(100);

            // Create a fake list of responses
            var fakeData = new List<Barnamehrizi>
        {
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی",Name = "بینا گستر" ,OrgGerayeshTitle = "چشم", Ostan="فارس",Title="شيراز"},
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی",Name ="سپید", OrgGerayeshTitle= "چند تخصصی", Ostan="فارس",Title="شيراز" },
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی", Name ="بصیر",OrgGerayeshTitle = "چشم" , Ostan="فارس",Title="شيراز"},
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی", Name = "صنعت نفت", OrgGerayeshTitle ="جراحی عمومی", Ostan="فارس",Title="شيراز" },
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی" , Name ="قائم(عج)", OrgGerayeshTitle = "چشم", Ostan="فارس",Title="شيراز"},
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی" , Name="آزادی", OrgGerayeshTitle = "چشم", Ostan="فارس",Title="شيراز"},
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی", Name="آفرینش" , OrgGerayeshTitle = "چند تخصصی", Ostan="فارس",Title="شيراز"},
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی",Name ="فروردین", OrgGerayeshTitle = "چند تخصصی", Ostan="فارس",Title="شيراز" },
            new Barnamehrizi { Id = Guid.NewGuid(), OrgTypeTitle = "مرکز جراحی محدود و سرپایی", Name ="نگسو" , OrgGerayeshTitle = "چند تخصصی", Ostan="فارس",Title="شيراز"},
        };

            // Apply pagination (simulated)
            var paginatedData = fakeData
                .Skip((criteria.PageNumber - 1) * criteria.PageSize)
                .Take(criteria.PageSize)
                .ToList();

            return Ok(paginatedData);
        }
    }
}
