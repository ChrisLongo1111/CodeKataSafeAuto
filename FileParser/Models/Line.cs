using System.Collections.Generic;

namespace FileParser.Models
{
    public class Line
    {
        public Line(string[] tokens)
        {
            Tokens = new List<string>(tokens);
        }

        public List<string> Tokens { get; set; } 
    }
}
