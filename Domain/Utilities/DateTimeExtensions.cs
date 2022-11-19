using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Utilities;

public static class DateTimeExtensions
{
    private static PersianCalendar PersianDate = new PersianCalendar();

    public static string ToPersian(this DateTime dateTime, bool full = false)
    {
        if (full)
        {
            return PersianDate.GetHour(dateTime).ToString() + ":" +
            PersianDate.GetMinute(dateTime).ToString() + " " +
            PersianDate.GetYear(dateTime).ToString() + "/" +
            PersianDate.GetMonth(dateTime).ToString() + "/" +
            PersianDate.GetDayOfMonth(dateTime).ToString();
        }
        else
        {
            return PersianDate.GetYear(dateTime).ToString() + "/" +
               PersianDate.GetMonth(dateTime).ToString() + "/" +
               PersianDate.GetDayOfMonth(dateTime).ToString();
        }
    }

    public static string ToPersianNullAble(this DateTime? dateTime, bool full = false)
    {
        if (dateTime != null)
            return ((DateTime)dateTime).ToPersian(full);

        return null;
    }

    public static string TimeString(this DateTime? dateTime)
    {
        if (dateTime != null)
            return PersianDate.GetHour((DateTime)dateTime).ToString() + ":" +
                PersianDate.GetMinute((DateTime)dateTime).ToString();

        return null;
    }

    public static DateTime ToMiladi(this string dateTime)
    {
        dateTime = dateTime.Fa2En();
        string[] splitDate = dateTime?.Split('/');
        return new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]), PersianDate);
    }

    public static int GetPersianNumberOfMonth(this DateTime dateTime)
    {
        return PersianDate.GetMonth(dateTime);
    }

    public static string GetPersianMonth(this DateTime dateTime)
    {
        var PersianMonth = new List<string> { "فروردین", "اردیبهشت", "خرداد", "تبر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        var NumberOfMonth = dateTime.GetPersianNumberOfMonth();
        NumberOfMonth = NumberOfMonth - 1;
        return PersianMonth[NumberOfMonth];
    }
}
