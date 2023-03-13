using Hedaya.Application.PrayerTimes.Models;
using MediatR;
using PrayerTimes;

namespace Hedaya.Application.PrayerTimes.Queries
{
    public class GetPrayerTimesQuery : IRequest<PrayerTimesResult>
    {
        public double Latitude { get; }
        public double Longitude { get; }
       
        public GetPrayerTimesQuery(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
           
        }
    }
    public class GetPrayerTimesQueryHandler : IRequestHandler<GetPrayerTimesQuery, PrayerTimesResult>
    {
        public Task<PrayerTimesResult> Handle(GetPrayerTimesQuery request, CancellationToken cancellationToken)
        {
            var prayerTimes = new PrayerTimesCalculator(request.Latitude, request.Longitude);
            prayerTimes.CalculationMethod = CalculationMethods.Egypt;
     
            var timeZoneOffset = TimeSpan.FromHours(3);
            var times = prayerTimes.GetPrayerTimes(new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, timeZoneOffset));

            string GetNextPrayer()
            {              
                // Determine the name of the next prayer
                switch (DateTime.Now.TimeOfDay)
                {
                    case var t when t < times.Fajr:
                        return "Fajr";
                    case var t when t < times.Sunrise:
                        return "Sunrise";
                    case var t when t < times.Dhuhr:
                        return "Dhuhr";
                    case var t when t < times.Asr:
                        return "Asr";
                    case var t when t < times.Maghrib:
                        return "Maghrib";
                    case var t when t < times.Isha:
                        return "Isha";
                    default:
                        return string.Empty;
                }
            }

            string nextPrayer = GetNextPrayer();


            var result = new PrayerTimesResult
            {
                
                Fajr = times.Fajr,
                Sunrise = times.Sunrise,
                Dhuhr = times.Dhuhr,
                Asr = times.Asr,
                Maghrib = times.Maghrib,
                Isha = times.Isha,
                NextPrayer = nextPrayer,
            };


           

            return Task.FromResult(result);
        }

    
    }




}
