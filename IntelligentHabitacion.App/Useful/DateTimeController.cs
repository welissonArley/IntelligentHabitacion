using System;

namespace IntelligentHabitacion.App.Useful
{
    public class DateTimeController
    {
        public static string DateToStringYearMonthAndDay(DateTime date)
        {
            TimeSpan span = DateTime.Now > date ? DateTime.Now - date : date - DateTime.Now;
            var differenceDate = (new DateTime(1, 1, 1) + span);

            var years = differenceDate.Year - 1;
            var months = differenceDate.Month - 1;
            var days = differenceDate.Day - 1;

            string response = $"{(years > 0 ? $"{years} {(years == 1 ? $"{ResourceText.TITLE_YEAR}" : $"{ResourceText.TITLE_YEARS}")}" : "")}".Trim();
            response = $"{response}{(response.Length == 0 ? "" : ",")} { (months > 0 ? $"{months} {(months == 1 ? $"{ResourceText.TITLE_MONTH}" : $"{ResourceText.TITLE_MONTHS}")}" : "")}".Trim();
            response = $"{response}{(response.Length == 0 ? "" : $" {ResourceText.TITLE_AND}")} { (days > 0 ? $"{days} {(days == 1 ? $"{ResourceText.TITLE_DAY}" : $"{ResourceText.TITLE_DAYS}")}" : "")}".Trim();
            response = $"{(response.Length > 0 ? response : $"1 {ResourceText.TITLE_DAY}")}";
            return response;
        }
    }
}
