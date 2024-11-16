namespace Accreditation.Application.AccreditationalInstanceAnswers.GetListAnswerMehvar;

public sealed class AccreditationalInstanceAnswersGetListAnswerMehvarDto
{
    public Guid Guid { get; set; }
    public Guid EtebarDorehGuid { get; set; }
    public string EtebarDorehName { get; set; }
    public int SanjehLevelId{ get; set; }
    public string? SanjehLevelName { get; set; }
    public string? NameMehvar { get; set; }
    public Guid? MehvarGuid { get; set; }
    public List<MehvarDto> SanjeDtos = new List<MehvarDto>();
 }