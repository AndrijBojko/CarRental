using System;


namespace CarRental.UI.Code
{
    public static class DateTimeConverter
    {
        public static DateTime ConvertFromString(string date, string hours, string minutes)
        {
            DateTime convertedDateTime = DateTime.Parse($"{date} {hours}:{minutes}:00");

            return convertedDateTime;
        }
    }
}
