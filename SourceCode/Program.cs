using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();

            board.begin();


            // Prevents screen closing
            // only inserted for beggining of development ONLY
            Console.ReadLine();
        }
    }
}
