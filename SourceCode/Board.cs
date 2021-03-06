﻿using System;
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
        private string[] tiles = {"0","     ","A2  X","     ","A4  X","     ","A6  X","     ","A8  X",
                                      "B1  X","     ","B3  X","     ","B5  X","     ","B7  X","     ",
                                      "     ","C2  X","     ","C4  X","     ","C6  X","     ","C8  X",
                                      "D1   ","     ","D3   ","     ","D5   ","     ","D7   ","     ",
                                      "     ","E2   ","     ","E4   ","     ","E6   ","     ","E8   ",
                                      "F1  O","     ","F3  O","     ","F5  O","     ","F7  O","     ",
                                      "     ","G2  O","     ","G4  O","     ","G6  O","     ","G8  O",
                                      "H1  O","     ","H3  O","     ","H5  O","     ","H7  O","     " };

        public string[] Tiles { get { return tiles; } set { tiles = value; } }

        // variables for player interation
        // input is based on readline()
        // choice is the chosen markers current location
        // destination is where it is to go
        // after input has been sent to changed to upperCase
        private string input;
        //private string choice;
        //private string destination;

        public string Input { get { return input; } set { input = value; } }
        //public string Choice { get { return choice; } set { choice = value; } }
        //public string Destination { get { return destination; } set { destination = value; } }

        // takes count of markers left for each player
        // IF either are zero, ends loop
        private int playerAMarkerCount = 12;
        private int playerBMarkerCount = 12;

        public int PlayerAMarkerCount { get { return playerAMarkerCount; } set { playerAMarkerCount = value; } }
        public int PlayerBMarkerCount { get { return playerBMarkerCount; } set { playerBMarkerCount = value; } }

        // variable to determine who's turn it is
        public int player = 1;

        public int Player { get { return player; } set { player = value; } }

        private string opponent;

        public string Opponent { get { return opponent; } set { opponent = value; } }

        // object of classes to call on the functions within them
        public Movement Player1 { get; set; }
        public Movement Player2 { get; set; }
        public Skynet Computer { get; set; }
        public Movement PlayerKing { get; set; }

        // variable to control 
        private int gameState = 1;
        #endregion
        #region Constructor
        // Constructor just to create board
        // At the moment
        public Board()
        {
            Computer = new Skynet(this);
            Player1 = new PlayerA(this);
            Player2 = new PlayerB(this);
            PlayerKing = new PlayerKing(this);
        }
        #endregion
        #region PlayerVPlayer
        public void PvP()
        {
            do
            {
                DisplayData();      
                createBoard();

                if (Player == 1)
                {
                    Opponent = "O";
                    PlayerAStart();
                }
                else
                {
                    Opponent = "X";
                    PlayerBStart();
                }

                gameState = checkWin();
            }
            while (gameState == 1);

            Console.Clear();
            createBoard();

            if (PlayerAMarkerCount == 0)
            {
                Console.WriteLine("Congratulations Player 2. you have won!\n" +
                    "Thanks for playing\n" +
                    "Goodbye/n" +
                    "Returning to Main Menu");
            }
            else if (PlayerBMarkerCount == 0)
            {
                Console.WriteLine("Congratulations Player 1. you have won!\n" +
                    "Thanks for playing\n" +
                    "Goodbye\n" +
                    "Returning to Main Menu");
            }
            Console.ReadLine();
        }
        #endregion
        #region PlayerVComputer
        public void PvC()
        {
            Computer.StorePieces();
            do
            {
                DisplayData();                
                createBoard();
                if(Player == 1)
                {
                    Opponent = "C";
                    PlayerAStart();
                }
                else
                {
                    Opponent = "X-C";
                    ComputerPlays();
                }

                gameState = checkWin();
            }
            while (gameState == 1);

            Console.Clear();
            createBoard();
            if (PlayerAMarkerCount == 0)
            {
                Console.WriteLine("Congratulations The Computer has won!\n" +
                    "Thanks for playing\n" +
                    "Goodbye/n" +
                    "Returning to Main Menu");
            }
            else if (PlayerBMarkerCount == 0)
            {
                Console.WriteLine("Congratulations! You have Beaten the computer\n" +
                    "Thanks for playing\n" +
                    "Goodbye\n" +
                    "Returning to Main Menu");
            }
            Console.ReadLine();
        }
        #endregion
        #region DisplaysBoard
        public void DisplayData()
        {
            Console.Clear();

            Console.WriteLine("Welcome to Draughts!\n");

            Console.WriteLine("Select Marker by Row then column");
            Console.WriteLine(player);
            Console.WriteLine("Player 1 marker count: " + PlayerAMarkerCount);
            Console.WriteLine("Player 2 marker count: " + PlayerBMarkerCount + "\n\n");
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

            Console.WriteLine("      1         2         3         4         5         6         7         8");
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
        #region CheckWin
        public int checkWin()
        {
            if (PlayerAMarkerCount == 0 || PlayerBMarkerCount == 0)
            {
                Console.WriteLine("Game Over");
                Console.ReadLine();
                return 0;
            }
            else
            {
                return 1;
            }
        }
        #endregion
        #region PlayerAStart
        public void PlayerAStart()
        { 
            // takes user input and sets it to upperCase
            // in case user wrote in lower case
            Console.WriteLine("Player 1 select a piece to move\n");
            input = Console.ReadLine();
            Player1.PieceStart = input.ToUpper();

            // checks if choice is empty
            if (Player1.PieceStart == "")
            {
                Console.WriteLine("Please enter a piece position to move");
                Console.ReadLine();
                PvP();
            }

            Console.WriteLine("Where do you want the piece to go?");
            Input = Console.ReadLine();
            Player1.Destination = Input.ToUpper();

            //Check is destination is Empty
            if (Player1.Destination == "")
            {
                Console.WriteLine("Please enter a position for your piece to move too");
                Console.ReadLine();
                PvP();
            }

            Player1.Move(Opponent);       
            
            
        }
        #endregion
        #region PlayerBStart
        public void PlayerBStart()
        {
            // takes user input and sets it to upperCase
            // in case user wrote in lower case
            Console.WriteLine("Player 2 select a piece to move\n");
            input = Console.ReadLine();
            Player2.PieceStart = input.ToUpper();

            // checks if choice is empty
            if (Player2.PieceStart == "")
            {
                Console.WriteLine("Please enter a piece position to move");
                Console.ReadLine();
                PvP();
            }

            Console.WriteLine("Where do you want the piece to go?");
            Input = Console.ReadLine();
            Player2.Destination = Input.ToUpper();

            //Check is destination is Empty
            if (Player2.Destination == "")
            {
                Console.WriteLine("Please enter a position for your piece to move to");
                Console.ReadLine();
                PvP();
            }
            Player2.Move(Opponent);
        }
        #endregion
        #region ComputerAStart
        public void ComputerPlays()
        {
            Computer.Move(Opponent);
        }
        #endregion
    }
}
