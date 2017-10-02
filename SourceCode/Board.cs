﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    class Board
    {
        public string[] tiles;

#region Constructor
        // Constructor just to create board
        // At the moment
        public Board()
        { }
#endregion
        public void CreateBoard()
        {
            // Creates Board#
            // Inserts array (insert name here when complete) elements into each position a checker marker
            // starts/can move too
            // 8 * 8 Grid
            // Initially setup with Grid reference
            // letters for columns & numbers for Rows
            // This may change with time
            // first Element set as 0, just to fill it up, but board array element starts at tiles[1]

            tiles = new string[] {"0","A1","A2","A3","A4","A5","A6","A7","A8",
                                   "B1","B2","B3","B4","B5","B6","B7","B8",
                                   "C1","C2","C3","C4","C5","C6","C7","C8",
                                   "D1","D2","D3","D4","D5","D6","D7","D8",
                                    "E1","E2","E3","E4","E5","E6","E7","E8",
                                    "F1","F2","F3","F4","F5","F6","F7","F8",
                                    "G1","G2","G3","G4","G5","G6","G7","G8",
                                    "H1","H2","H3","H4","H5","H6","H7","H8",};

            Console.WriteLine("     1       2       3       4       5       6       7       8");
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
