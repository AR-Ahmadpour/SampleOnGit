using Accreditation.Application.Abstractions.Messaging;

using SharedKernel;


namespace Accreditation.Application.Users.GetRoleHospital
{
    internal sealed class GetRoleHospitalHandler
    : IQueryHandler<GetRoleHospitalQuery, GetRoleHospitalDto>
    {
        public GetRoleHospitalHandler(){}

        public Task<Result<GetRoleHospitalDto>> Handle(GetRoleHospitalQuery request, CancellationToken cancellationToken)
        {
            var dto = new GetRoleHospitalDto(11, "OrgAgent", "کاربر موسسه : ثبت اطلاعات");
            var result = Result.Success(dto); 

            // Return the Result as a Task
            return Task.FromResult(result);
        }
    }
}
