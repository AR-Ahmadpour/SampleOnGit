
namespace Accrediation.Application.Common.FluentValidationCustomLanguageManager;

internal sealed class FluentValidationCustomLanguage:FluentValidation.Resources.LanguageManager
{
    public FluentValidationCustomLanguage()
    {
        Culture=new System.Globalization.CultureInfo("fa-IR");
    }
}
