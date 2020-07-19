using CodeKataSafeAuto;
using FileParser.Models;
using MileageProcessor;
using System.Collections.Generic;
using System.Linq;

namespace Commands
{
    public class Processor
    {
        private List<Line> lines;
        private List<Driver> drivers = new List<Driver>();

        public Processor(List<Line> lines)
        {
            this.lines = lines;
        }

        public void Process() 
        {
            foreach (var line in lines)
            {
                var commandToken = line.Tokens.FirstOrDefault();
                if (commandToken != null)
                {
                    var command = Create(commandToken, drivers);
                    if (command != null)
                    {
                        command.Execute(line);
                    }
                }
            }
        }

        public List<Summary> Summary()
        {
            var summaries = new List<Summary>();
            foreach (var driver in drivers)
            {
                summaries.Add(driver.Summary());
            }
            return summaries.OrderByDescending(x => x.Miles).ToList();
        }

        private ICommand Create(string command, List<Driver> drivers)
        {
            return command switch
            {
                "Driver" => new DriverCommand(drivers),
                "Trip" => new TripCommand(drivers),
                _ => null,
            };
        }
    }
}
