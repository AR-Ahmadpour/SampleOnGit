using Accreditation.Domain.AccreditationInstances.Entities;
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Domain.AccreditationInstanceAnswers.Entities;

public sealed class AccreditationalInstanceAnswer : Entity
{
    public Guid SanjehGUID { get; private set; }
    public Guid? UserGUID { get; private set; }
    public User User { get; private set; }
    public Guid? AuditGUID { get; private set; }
    public Guid AccreditationInstanceGUID { get; private set; }
    public decimal? Result { get; private set; }
    public decimal? AuditResult { get; private set; }
    public string? AnswerText { get; private set; }
    public DateTime? DateTime { get; set; }
    public DateTime? AuditDateTime { get; set; }
    public bool? Universityopinion { get; set; }
    public string? UniversityopinionText { get; set; }
    public Sanjeh Sanjeh { get; private set; } = null!;
    public AccreditationInstance AccreditationalInstance { get; private set; } = null!;
    private AccreditationalInstanceAnswer()
    {

    }
    private AccreditationalInstanceAnswer(Guid accreditationInstanceGUID,
                                          Guid sanjehGUID)
                                : base(Guid.NewGuid())
    {
        SanjehGUID = sanjehGUID;
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
    public static AccreditationalInstanceAnswer Create(Guid accreditationInstanceGUID,
                                                       Guid sanjehGUID) =>
        new AccreditationalInstanceAnswer(accreditationInstanceGUID,
                                          sanjehGUID);
    public void EditResult(string? answerText,decimal? result,Guid UserId, DateTime DateTimeNow)
    {
        AnswerText = answerText;
        Result = result;
        UserGUID=UserId;
        DateTime = DateTimeNow;
    }


    public void EditResult(decimal? result)
    {
        Result = result;
    }


    public void EditAuditResult(decimal? Auditresult)
    {
        AuditResult = Auditresult;
    }


}

