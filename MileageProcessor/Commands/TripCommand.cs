using CodeKataSafeAuto;
using Commands;
using FileParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MileageProcessor
{
    public class TripCommand : ICommand
    {
        private List<Driver> drivers;

        public TripCommand(List<Driver> drivers)
        {
            this.drivers = drivers;
        }

        public void Execute(Line line)
        {
            if (line.Tokens.Count != 5)
            {
                throw new Exception($"Invalid Trip for Driver");
            }
            var name = line.Tokens.Skip(1).First();
            var driver = drivers.FirstOrDefault(x => x.Name == name);
            if (driver != null)
            {
                var start = line.Tokens.Skip(2).First();
                var end = line.Tokens.Skip(3).First();
                var mileage = line.Tokens.Skip(4).First();
                driver.Trips.Add(new Trip(start, end, mileage));
            }
            else
            {
                throw new Exception($"Driver {name} does not exist");
            }
        }
    }
}
