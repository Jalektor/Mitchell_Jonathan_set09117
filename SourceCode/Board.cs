using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckersGame
{
    public class Board
    {
        #region variables
        // array to create board values
        // restricted 
        private string[] tiles = {"0","    ","A2 X","    ","A4 X","    ","A6 X","    ","A8 X",
                                      "B1 X","    ","B3 X","    ","B5 X","    ","B7 X","    ",
                                      "    ","C2 X","    ","C4 X","    ","C6 X","    ","C8 X",
                                      "D1  ","    ","D3  ","    ","D5  ","    ","D7  ","    ",
                                      "    ","E2  ","    ","E4  ","    ","E6  ","    ","E8  ",
                                      "F1 O","    ","F3 O","    ","F5 O","    ","F7 O","    ",
                                      "    ","G2 O","    ","G4 O","    ","G6 O","    ","G8 O",
                                      "H1 O","    ","H3 O","    ","H5 O","    ","H7 O","    ",};
        public string[] Tiles { get { return tiles; } set { tiles = value; } }

        // Keeps do while loop going ATM. Will change later
        protected int count = 2;

        // variables for player interation
        // input is based on readline()
        // choice is the chosen markers current location
        // destination is where it is to go
        // after input has been sent to changed to upperCase
        // newDest to store new destination, based on where marker is moving too &/or an enemy marker is being captured
        private string input;
        private string choice;   
        private string destination;
        private string newDest;
        private string left;
        private string right;

        public string Input { get { return input; } set { input = value; } }
        public string Choice { get { return choice; } set { choice = value; } }
        public string Destination { get { return destination; } set { destination = value; } }
        public string NewDest { get { return newDest; } set { newDest = value; } }
        public string Left { get { return left; } set { left = value; } }
        public string Right { get { return right; } set { right = value; } }


        // converts above string inputs into char array
        // for checking where movement position is going to be
        private char[] startcoord;
        private char[] endcoord;

        public char[] Startcoord { get { return startcoord; } set { startcoord = value; } }
        public char[] Endcoord { get { return endcoord; } set { endcoord = value; } }

        // char array used to compare the above char arrays
        // so as to prevent backwards movements
        // and possibly make sure only diagonal movement forward
        private char[] letter = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public char[] Letter { get { return letter; } set { letter = value; } }

        // Char array to check position of number new player marker destination
        // AFTER they attempt to take an enemy marker
        // And Then used with above char array to create a new tile destination for player marker
        private char[] number = { '1', '2', '3', '4', '5', '6', '7', '8' };

        public char[] Number { get { return number; } set { number = value; } }


        // int variables to compare positions of char[]'s above
        // based on position of letter in char[] letter
        // used to prevent backwards movements
        // possibly restrict forward movement to diagonal
        private int posC;
        private int posD;

        public int PosC { get { return posC; } set { posC = value; } }
        public int PosD { get { return posD; } set { posD = value; } }


        // takes count of markers left for each player
        // IF either are zero, ends loop
        private int playerAMarkerCount = 12;
        private int playerBMarkerCount = 12;

        public int PlayerAMarkerCount { get { return playerAMarkerCount; } set { playerAMarkerCount = value; } }
        public int PlayerBMarkerCount { get { return playerBMarkerCount; } set { playerBMarkerCount = value; } }

        // variable to determine who's turn it is
        public int player = 1;

        public int Player { get { return player; } set { player = value; } }

        // object of classes to call on the functions within them
        PlayerA player1; 
        PlayerB player2;

        // variable to control 
        private int gameState = 1;
        #endregion


        #region Constructor
        // Constructor just to create board
        // At the moment
        public Board()
        {
            player1 = new PlayerA(this);
            player2 = new PlayerB(this);
        }
        #endregion
        #region startsTheGame
        public void begin()
        {           
;
            do
            {
                Console.Clear();

                Console.WriteLine("Welcome to Draughts!\n");

                Console.WriteLine("Select Marker by Row then column");

                Console.WriteLine("Player 1 marker count: " + PlayerAMarkerCount);
                Console.WriteLine("Player 2 marker count: " + PlayerBMarkerCount + "\n\n");

                createBoard();

                if(player == 1)
                {
                    // takes user input and sets it to upperCase
                    // in case user wrote in lower case
                    Console.WriteLine("Player 1 select a counter to move\n");
                    input = Console.ReadLine();
                    choice = input.ToUpper();
                    if(choice == "")
                    {
                        Console.WriteLine("Please enter a marker position");
                        Console.ReadLine();
                        begin();
                    }

                    startcoord = choice.ToCharArray();
                    Console.WriteLine("Marker exists");

                    Console.WriteLine("Where do you want the marker to go?");
                    Input = Console.ReadLine();
                    Destination = Input.ToUpper();

                    if (Destination == "")
                    {
                        Console.WriteLine("Please enter a position for your marker to move to");
                        Console.ReadLine();
                        begin();
                    }

                    player1.move();
                }
                else
                {
                    
                    // takes user input and sets it to upperCase
                    // in case user wrote in lower case
                    Console.WriteLine("Player 2 select a counter to move\n");
                    input = Console.ReadLine();
                    choice = input.ToUpper();
                    if (choice == "")
                    {
                        Console.WriteLine("Please enter a marker position");
                        Console.ReadLine();
                        begin();
                    }

                    startcoord = choice.ToCharArray();
                    Console.WriteLine("Marker exists");

                    Console.WriteLine("Where do you want the marker to go?");
                    Input = Console.ReadLine();
                    Destination = Input.ToUpper();

                    if (Destination == "")
                    {
                        Console.WriteLine("Please enter a position for your marker to move to");
                        Console.ReadLine();
                        begin();
                    }
                    player2.move();
                }

                gameState = checkWin();
            }
            while (gameState == 1);

            Console.Clear();
            createBoard();

            if(PlayerAMarkerCount == 0)
            {
                Console.WriteLine("Congratulations Player 2. you have won!\nThanks for playing\nGoodbye");
            }
            else if(PlayerBMarkerCount == 0)
            {
                Console.WriteLine("Congratulations Player 1. you have won!\nThanks for playing\nGoodbye");
            }
            Console.ReadLine();
        }
        #endregion

        #region createsBoard
        public void createBoard()
        {
            // Creates Board
            // Inserts array (insert name here when complete) elements into each position a checker marker
            // starts/can move too
            // 8 * 8 Grid
            // Initially setup with Grid reference
            // letters for columns & numbers for Rows
            // This may change with time
            // first Element set as 0, just to fill it up, but board array element starts at tiles[1]
            // NOTE: first console.writeline spacing here irregular but sets up nicely when executing app # OCD

            Console.WriteLine("      1       2        3        4        5        6        7        8");
            Console.WriteLine("A [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[1], tiles[2], tiles[3], tiles[4], tiles[5], tiles[6], tiles[7], tiles[8]);
            Console.WriteLine("B [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[9], tiles[10], tiles[11], tiles[12], tiles[13], tiles[14], tiles[15], tiles[16]);
            Console.WriteLine("C [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[17], tiles[18], tiles[19], tiles[20], tiles[21], tiles[22], tiles[23], tiles[24]);
            Console.WriteLine("D [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[25], tiles[26], tiles[27], tiles[28], tiles[29], tiles[30], tiles[31], tiles[32]);
            Console.WriteLine("E [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[33], tiles[34], tiles[35], tiles[36], tiles[37], tiles[38], tiles[39], tiles[40]);
            Console.WriteLine("F [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[41], tiles[42], tiles[43], tiles[44], tiles[45], tiles[46], tiles[47], tiles[48]);
            Console.WriteLine("G [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[49], tiles[50], tiles[51], tiles[52], tiles[53], tiles[54], tiles[55], tiles[56]);
            Console.WriteLine("H [ {0} ] [ {1} ] [ {2} ] [ {3} ] [ {4} ] [ {5} ] [ {6} ] [ {7} ]", tiles[57], tiles[58], tiles[59], tiles[60], tiles[61], tiles[62], tiles[63], tiles[64]);
        }
        #endregion

        public int checkWin()
        {
            if (PlayerAMarkerCount == 0 || PlayerBMarkerCount == 0)
            {
                Console.WriteLine("Game Over");
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
