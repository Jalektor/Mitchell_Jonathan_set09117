using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class Undo
    {
        public Stack<string> startCoord = new Stack<string>();
        public Stack<string> endCoord = new Stack<string>();

        public void displayContents()
        {
            foreach(string coord in startCoord)
            {
                Console.WriteLine(coord);
            }
            foreach (string coord in endCoord)
            {
                Console.WriteLine(coord);
            }
        }
    }
}
