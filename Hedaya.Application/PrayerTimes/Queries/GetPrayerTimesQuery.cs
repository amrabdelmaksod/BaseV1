using Hedaya.Application.PrayerTimes.Models;
using Hedaya.Domain.Enums;
using MediatR;
using PrayerTimes;

namespace Hedaya.Application.PrayerTimes.Queries
{
    public class GetPrayerTimesQuery : IRequest<object>
    {
        public double Latitude { get; }
        public double Longitude { get; }
       
        public GetPrayerTimesQuery(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
           
        }
    }
    public class GetPrayerTimesQueryHandler : IRequestHandler<GetPrayerTimesQuery, object>
    {
        public async Task<object> Handle(GetPrayerTimesQuery request, CancellationToken cancellationToken)
        {
            var prayerTimes = new PrayerTimesCalculator(request.Latitude, request.Longitude);
            prayerTimes.CalculationMethod = CalculationMethods.Egypt;


     
            var timeZoneOffset = TimeSpan.FromHours(3);
            var times = prayerTimes.GetPrayerTimes(new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, timeZoneOffset));

            PrayerTimeType GetNextPrayer()
            {

                var egyptTime = DateTime.Now.AddHours(-1);
                // Determine the name of the next prayer
                switch (egyptTime.TimeOfDay)
                {
                    case var t when t < times.Fajr:
                        return PrayerTimeType.Fajr;
                    case var t when t < times.Sunrise:
                        return PrayerTimeType.Sunrise;
                    case var t when t < times.Dhuhr:
                        return PrayerTimeType.Dhuhr;
                    case var t when t < times.Asr:
                        return PrayerTimeType.Asr;
                    case var t when t < times.Maghrib:
                        return PrayerTimeType.Maghrib;
                    case var t when t < times.Isha:
                        return PrayerTimeType.Isha;
                    default:
                        return PrayerTimeType.None;
                }
            }

            var nextPrayer = GetNextPrayer();


            var prayerTimesResult = new PrayerTimesResult
            {
                
                Fajr = times.Fajr,
                Sunrise = times.Sunrise,
                Dhuhr = times.Dhuhr,
                Asr = times.Asr,
                Maghrib = times.Maghrib,
                Isha = times.Isha,
                NextPrayer = nextPrayer,
            };


           

            return new {result = prayerTimesResult};
        }

    
    }




}
