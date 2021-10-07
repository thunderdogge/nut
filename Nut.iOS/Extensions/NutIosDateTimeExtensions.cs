using System;
using Foundation;

namespace Nut.iOS.Extensions
{
    public static class NutIosDateTimeExtensions
    {
        private static readonly DateTime referenceNsDateTime = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static NSDate ToNsDate(this DateTime date)
        {
            return NSDate.FromTimeIntervalSinceReferenceDate((date - referenceNsDateTime).TotalSeconds);
        }

        public static DateTime ToDateTimeUtc(this NSDate date)
        {
            return referenceNsDateTime.AddSeconds(date.SecondsSinceReferenceDate);
        }
    }
}