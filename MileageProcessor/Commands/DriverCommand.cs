using CodeKataSafeAuto;
using Commands;
using FileParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MileageProcessor
{
    public class DriverCommand : ICommand
    {
        private List<Driver> drivers;
        public DriverCommand(List<Driver> drivers)
        {
            this.drivers = drivers;
        }

        public void Execute(Line line)
        {
            if (line.Tokens.Count !=  2)
            {
                throw new Exception($"Invalid driver line");
            }
            var name = line.Tokens.Skip(1).First();
            var driver = this.drivers.FirstOrDefault(x => x.Name == name);
            if (driver == null)
            {
                this.drivers.Add(new Driver { Name = name });
            }
        }
    }
}
