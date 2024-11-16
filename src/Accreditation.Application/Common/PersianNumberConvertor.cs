using System.Text;

namespace Accreditation.Application.Common;


public class PersianNumberConvertor
{
    public static string ToPersianNumber(string input)
    {
        StringBuilder persianNumber = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                persianNumber.Append((char)(c + 1728)); // Convert Arabic digits (0-9) to Persian digits (۰-۹)
            }
            else
            {
                persianNumber.Append(c);
            }
        }

        return persianNumber.ToString();
    }
}
