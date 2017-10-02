using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckersGame
{
    class Board
    {
        public string[] tiles = {"0","    ","A1 X","    ","A2 X","    ","A3 X","    ","A4 X",
                                      "B1 X","    ","B3 X","    ","B5 X","    ","B7 X","    ",
                                      "    ","C2 X","    ","C4 X","    ","C6 X","    ","C8 X",
                                      "D1  ","    ","D3  ","    ","D5  ","    ","D7  ","    ",
                                      "    ","E2  ","    ","E4  ","    ","E6  ","    ","E8  ",
                                      "F1  ","    ","F3  ","    ","F5  ","    ","F7  ","    ",
                                      "    ","G2  ","    ","G4  ","    ","G6  ","    ","G8  ",
                                      "H1  ","    ","H3  ","    ","H5  ","    ","H7  ","    ",};
        int count = 2;

        // variables for player interation
        // choice is the chosen markers current location
        // destination is where it is to go
        string input;
        string destination;
        

#region Constructor
        // Constructor just to create board
        // At the moment
        public Board()
        { }

        public void begin()
        {
            do
            {
                Console.Clear();

                Console.WriteLine("Welcome to Draughts!\n");

                createBoard();

                // takes user input and sets it to upper
                // in case user wrote in lower case
                Console.WriteLine("Select a Marker to move\n");
                input = Console.ReadLine();
                string choice = input.ToUpper();


                // checks array for chosen marker coord
                for (int i = 0; i < tiles.Length; i++)
                {
                    if(tiles[i].Contains(choice))
                    {
                        Console.WriteLine("Marker exists");

                        Console.WriteLine("Where do you want the marker to go?");
                        input = Console.ReadLine();
                        destination = input.ToUpper();

                        for(int x = 0; x < tiles.Length; x++)
                        {
                            if(tiles[x].Contains(destination))
                            {
                                string newdest = destination + " " + "X";
                                tiles[x] = newdest;

                                tiles[i] = choice + "  ";

                                Console.WriteLine("Marker moved");
                                break;
                            }
                            if(x == tiles.Length - 1)
                            {
                                Console.WriteLine("Marker destination does not exist");
                            }
                        }
                        break;

                    }
                    if(i == tiles.Length - 1)
                    {
                        Console.WriteLine("No marker on selected position");
                        Console.ReadLine();
                        break;
                    }

                }

            }
            while (count == 2);
            
        }
#endregion
        public void createBoard()
        {
            // Creates Board#
            // Inserts array (insert name here when complete) elements into each position a checker marker
            // starts/can move too
            // 8 * 8 Grid
            // Initially setup with Grid reference
            // letters for columns & numbers for Rows
            // This may change with time
            // first Element set as 0, just to fill it up, but board array element starts at tiles[1]

            Console.WriteLine("    1      2      3      4      5      6      7      8");
            Console.WriteLine("A [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[1], tiles[2], tiles[3], tiles[4], tiles[5], tiles[6], tiles[7], tiles[8]);
            Console.WriteLine("B [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[9], tiles[10], tiles[11], tiles[12], tiles[13], tiles[14], tiles[15], tiles[16]);
            Console.WriteLine("C [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[17], tiles[18], tiles[19], tiles[20], tiles[21], tiles[22], tiles[23], tiles[24]);
            Console.WriteLine("D [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[25], tiles[26], tiles[27], tiles[28], tiles[29], tiles[30], tiles[31], tiles[32]);
            Console.WriteLine("E [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[33], tiles[34], tiles[35], tiles[36], tiles[37], tiles[38], tiles[39], tiles[40]);
            Console.WriteLine("F [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[41], tiles[42], tiles[43], tiles[44], tiles[45], tiles[46], tiles[47], tiles[48]);
            Console.WriteLine("G [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[49], tiles[50], tiles[51], tiles[52], tiles[53], tiles[54], tiles[55], tiles[56]);
            Console.WriteLine("H [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[57], tiles[58], tiles[59], tiles[60], tiles[61], tiles[62], tiles[63], tiles[64]);
        }
    }
}
