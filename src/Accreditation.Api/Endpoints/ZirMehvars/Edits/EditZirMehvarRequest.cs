namespace Accreditation.Api.Endpoints.ZirMehvars.Edits
{
    
        public sealed record EditZirMehvarRequest(
                string title,
                int weightedCoefficient,
                int sortOrder);
    
}
