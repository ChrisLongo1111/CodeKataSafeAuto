using System;

namespace Commands
{
    public class Trip
    {
        public Trip(string start, string end, string mileage)
        {
            Start = TimeSpan.Parse(start);
            End = TimeSpan.Parse(end);
            Mileage = double.Parse(mileage);
            var totalTime = End - Start;
            if (totalTime.TotalHours < 0)
            {
                throw new Exception("End Time before Start Time");
            }
            AverageSpeed = Mileage / totalTime.TotalHours;

        }
        public TimeSpan Start { get; private set; }
        public TimeSpan End { get; private set; }
        public double Mileage { get; private set; }
        public double AverageSpeed { get; private set; }
    }
}