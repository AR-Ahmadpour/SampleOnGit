using FluentValidation;
using System.Globalization;

namespace Accreditation.Application.Tahsilats.Add
{
    internal sealed class AddTahsilatCommandValidator : AbstractValidator<AddTahsilatCommand>
    {
        public AddTahsilatCommandValidator()
        {
            RuleFor(x => x.GraduationDate)
                .NotEmpty()
                .Must(BeValidShamsiDate);

            RuleFor(x => x.UniversityName)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.MadrakGuid)
                .MaximumLength(1000);

            RuleFor(x => x.MaghtaTahsiliGuid)
                .NotEmpty();

            RuleFor(x=>x.ReshtehTahsiliGuid)
                .NotEmpty();
        }

        private bool BeValidShamsiDate(string graduationDate)
        {
            if (string.IsNullOrWhiteSpace(graduationDate))
                return false;

            string[] dateParts = graduationDate.Split('/');
            if (dateParts.Length != 3) return false;

            if (!int.TryParse(dateParts[0], out int year) ||
                !int.TryParse(dateParts[1], out int month) ||
                !int.TryParse(dateParts[2], out int day))
            {
                return false;
            }

            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();

                // Validate the date parts
                int maxDay = persianCalendar.GetDaysInMonth(year, month);
                if (month < 1 || month > 12 || day < 1 || day > maxDay)
                {
                    return false;
                }

                // Get the current Shamsi date
                DateTime now = DateTime.Now;
                int currentYear = persianCalendar.GetYear(now);
                int currentMonth = persianCalendar.GetMonth(now);
                int currentDay = persianCalendar.GetDayOfMonth(now);

                // Compare the entered date with the current date
                if (year > currentYear ||
                    (year == currentYear && month > currentMonth) ||
                    (year == currentYear && month == currentMonth && day > currentDay))
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
