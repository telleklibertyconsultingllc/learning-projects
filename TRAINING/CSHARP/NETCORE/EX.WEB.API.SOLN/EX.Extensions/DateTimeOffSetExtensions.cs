using System;

namespace EX.Extensions
{
    public static class DateTimeOffSetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            return DateTime.Now.Year - dateTimeOffset.Year;
        }
    }
}
