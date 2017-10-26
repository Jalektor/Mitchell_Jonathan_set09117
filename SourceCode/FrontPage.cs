using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    class FrontPage
    {
        
        
        public void Menu()
        {
            Board board = new Board();
            Information info = new Information();
            while(true)
            {
                Console.Clear();

                Console.WriteLine("Welcome to Draughts!\n" +
                "Select one of the following options:\n" +
                "1. Rules\n" +
                "2. Player vs Player\n" +
                "3. Player vs Computer");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        info.Rules();
                        break;

                    case "2":
                        board.begin();
                        break;

                    case "3":
                        board.ComputerPlays();
                        break;

                    default:

                        Console.WriteLine("Please Select One of the above options!");
                        Console.ReadLine();
                        break;
                }
            }                   
                
            

            
        }
    }
}
