using Commands;
using FileParser;
using FileParser.Models;
using MileageProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeKataSafeAuto
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any() || !File.Exists(args.First()))
            {
                throw new Exception("the argument must be a valid file name");
            }
            List<Line> lines = null;
            using (var fileStream = new FileStream(args.First(), FileMode.Open))
            {
                var fileParser = new Parser(fileStream);
                lines = fileParser.Parse();
            }
            if (lines != null && lines.Any())
            {
                var processor = new Processor(lines);
                processor.Process();
                var summaries = processor.Summary();
                SummaryReport(summaries);
            }
        }
        
        private static void SummaryReport(List<Summary> summaries)
        {
            foreach (var summary in summaries)
            {
                if (summary.Mph != 0)
                {
                    Console.WriteLine($"{summary.Name}: {summary.Miles} miles @ {summary.Mph} mph");
                }
                else
                {
                    Console.WriteLine($"{summary.Name}: {summary.Miles} miles");
                }
            }
        }
    }
}
