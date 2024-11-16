using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Accreditation.Application.Common;

public static class PersianDateHelper
{
    public static string ToPersianDateString(DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();
        return string.Format("{0}/{1}/{2}",
            persianCalendar.GetYear(dateTime),
            persianCalendar.GetMonth(dateTime).ToString("00"),
            persianCalendar.GetDayOfMonth(dateTime).ToString("00"));
    }
    public static string ToPersianDateString(DateOnly? date)
    {
        if (!date.HasValue) return null;

        var persianCalendar = new PersianCalendar();
        var dateTime = date.Value.ToDateTime(new TimeOnly(0, 0));
        return string.Format("{0}/{1}/{2}",
           PersianNumberConvertor.ToPersianNumber(persianCalendar.GetYear(dateTime).ToString()),
            PersianNumberConvertor.ToPersianNumber(persianCalendar.GetMonth(dateTime).ToString("00")),
             PersianNumberConvertor.ToPersianNumber(persianCalendar.GetDayOfMonth(dateTime).ToString("00")));
    }

    public static DateOnly? ToPersianDateOnly(DateOnly? date)
    {
        if (!date.HasValue) return null; // Handle null case

        var persianCalendar = new PersianCalendar();
        var dateTime = date.Value.ToDateTime(new TimeOnly(0, 0)); // Convert DateOnly to DateTime for PersianCalendar
        var persianYear = persianCalendar.GetYear(dateTime);
        var persianMonth = persianCalendar.GetMonth(dateTime);
        var persianDay = persianCalendar.GetDayOfMonth(dateTime);

        return new DateOnly(persianYear, persianMonth, persianDay); // Return as DateOnly
    }
    public static string ToPersianDateTimeString(DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();
        return string.Format("{0}/{1}/{2} -{3}",
            persianCalendar.GetYear(dateTime),
            persianCalendar.GetMonth(dateTime).ToString("00"),
            persianCalendar.GetDayOfMonth(dateTime).ToString("00"),
            dateTime.ToString("HH:mm:ss")
            );
    }

}
