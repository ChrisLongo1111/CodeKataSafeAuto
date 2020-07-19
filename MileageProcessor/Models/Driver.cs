using MileageProcessor;
using System;
using System.Collections.Generic;

namespace Commands
{
    public class Driver
    {
        public string Name { get; set; }

        public List<Trip> Trips { get; set; } = new List<Trip>();

        public Summary Summary()
        {
            var mileage = 0.0;
            var timeSpan = new TimeSpan();
            foreach (var trip in Trips)
            {
                if (trip.AverageSpeed < 5 || trip.AverageSpeed > 100)
                {
                    continue;
                }
                mileage += trip.Mileage;
                timeSpan += trip.End - trip.Start;
            }

            var summary = new Summary
            {
                Name = Name,
                Miles = (int)Math.Round(mileage),
                Mph = timeSpan.TotalHours == 0 || mileage == 0 ? 0 : (int)Math.Round(mileage / timeSpan.TotalHours)
            };

            return summary;
        }
    }
}
