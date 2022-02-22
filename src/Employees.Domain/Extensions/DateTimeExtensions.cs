using Employees.Domain.Constants;

namespace Employees.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertToLocalFromUTC(this DateTime d, string timezoneId = TimeZoneConstants.INDIA_TIMEZONE_ID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(d, info);
        }
    }
}
