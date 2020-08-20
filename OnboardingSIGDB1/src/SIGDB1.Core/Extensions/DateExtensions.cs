using System;

namespace SIGDB1.Core.Extensions
{
    public static class DateExtensions
    {
        public static DateTime? ToDate(this string date) 
        {
            if (date.IsEmpty())
                return null;

            return Convert.ToDateTime(date).Date;
        }
    }
}
