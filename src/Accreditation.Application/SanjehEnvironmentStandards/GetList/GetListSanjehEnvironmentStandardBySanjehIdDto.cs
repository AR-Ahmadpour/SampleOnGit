namespace Accreditation.Application.SanjehEnvironmentStandards.GetList
{
    public sealed record GetListSanjehEnvironmentStandardBySanjehIdDto
    {
        public string title { get; set; }
        public Guid Guid { get; set; }
    }
}
