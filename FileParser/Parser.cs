using FileParser.Models;
using System.Collections.Generic;
using System.IO;

namespace FileParser
{
    public class Parser
    {
        private Stream stream;
        private List<Line> lines = new List<Line>();

        public Parser(Stream stream)
        {
            this.stream = stream;
        }

        public string Delimiter { get; set; } = " ";

        public List<Line> Parse()
        {
            using (StreamReader sr = new StreamReader(this.stream))
            {
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    var tokens = line.Split(Delimiter);
                    lines.Add(new Line(tokens));
                }
            }
            return lines;
        }
    }
}
