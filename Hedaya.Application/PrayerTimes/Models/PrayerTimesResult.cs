using Hedaya.Domain.Enums;

namespace Hedaya.Application.PrayerTimes.Models
{
    public class PrayerTimesResult
    {
     
        public TimeSpan Fajr { get; set; }
        public TimeSpan Sunrise { get; set; }
        public TimeSpan Dhuhr { get; set; }
        public TimeSpan Asr { get; set; }
        public TimeSpan Maghrib { get; set; }
        public TimeSpan Isha { get; set; }
        public PrayerTimeType NextPrayer { get; set; }
    }

}
