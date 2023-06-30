using System.Globalization;

namespace ServiceStationApi
{
    public class HandlerDate
    {
        public static DateTime ConvertStrToDate(string date)
        {
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime convertedDate))
            {
                return convertedDate;
            }

            return DateTime.UtcNow.Date;
        }
    }
}
