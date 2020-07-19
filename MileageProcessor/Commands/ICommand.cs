using FileParser.Models;
using System.Collections.Generic;

namespace CodeKataSafeAuto
{
    public interface ICommand
    {
        public void Execute(Line line);
    }
}
