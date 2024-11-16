using SharedKernel;

namespace Accreditation.Application.EvaluationArzyabs;
public static class EvaluationArzyabErrors
{
    public static Error ArzyabNotFound => Error.NotFound("EvaluationArzyab.NotFound", "ارزیاب یافت نشد");
    public static Error AccreditationNotFound => Error.NotFound("Evaluation.NotFound", "برنامه ارزیابی یافت نشد");
    public static Error SarparastMustExist => Error.NotFound("Evaluation.SarparastMustExist", "برنامه ارزیابی بدون سرپرست نباید باشد.");
    public static Error AlreadySarparastExists => Error.NotFound("Evaluation.AlreadySarparastExists", " ارزیاب سرپرست در تیم ارزیابی وجود دارد");
}
