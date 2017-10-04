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
        #region variables
        public string[] tiles = {"0","    ","A2 X","    ","A4 X","    ","A6 X","    ","A8 X",
                                      "B1 X","    ","B3 X","    ","B5 X","    ","B7 X","    ",
                                      "    ","C2 X","    ","C4 X","    ","C6 X","    ","C8 X",
                                      "D1  ","    ","D3 0","    ","D5  ","    ","D7  ","    ",
                                      "    ","E2  ","    ","E4  ","    ","E6  ","    ","E8  ",
                                      "F1  ","    ","F3  ","    ","F5  ","    ","F7  ","    ",
                                      "    ","G2  ","    ","G4  ","    ","G6  ","    ","G8  ",
                                      "H1  ","    ","H3  ","    ","H5  ","    ","H7  ","    ",};
        int count = 2;

        // variables for player interation
        // input is based on readline()
        // choice is the chosen markers current location
        // destination is where it is to go
        // after input has been sent to changed to upperCase
        // newDest to store new destination, based on where marker is moving too &/or an enemy marker is being captured
        string input;
        string choice;
        string destination;
        string newDest;

        // converts above string inputs into char array
        // for checking where movement position is going to be
        char[] startcoord;
        char[] endcoord;

        // char array used to compare the above char arrays
        // so as to prevent backwards movements
        // and possibly make sure only diagonal movement forward
        char[] letter = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        // Char array to check position of number new player marker destination
        // AFTER they attempt to take an enemy marker
        // And Then used with above char array to create a new tile destination for player marker
        char[] number = { '1', '2', '3', '4', '5', '6', '7', '8', };


        // int variables to compare positions of char[]'s above
        // based on position of letter in char[] letter
        // used to prevent backwards movements
        // possibly restrict forward movement to diagonal
        int posC;
        int posD;
        #endregion


        #region Constructor
        // Constructor just to create board
        // At the moment
        public Board()
        { }
        #endregion
        #region startsTheGame
        public void begin()
        {
            do
            {
                Console.Clear();

                Console.WriteLine("Welcome to Draughts!\n");

                Console.WriteLine("Select Marker by Row then column");

                createBoard();

                // takes user input and sets it to upperCase
                // in case user wrote in lower case
                Console.WriteLine("Select a Marker to move\n");
                input = Console.ReadLine();
                choice = input.ToUpper();

                startcoord = choice.ToCharArray();

                #region playerOneMove
                // checks array for chosen marker coord
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].Contains(choice))
                    {
                        Console.WriteLine("Marker exists");

                        Console.WriteLine("Where do you want the marker to go?");
                        input = Console.ReadLine();
                        destination = input.ToUpper();

                        endcoord = destination.ToCharArray();

                        // checks if destination coords are present in array tiles[]
                        for (int x = 0; x < tiles.Length; x++)
                        {
                            if (tiles[x].Contains(destination))
                            {
                                // prevents sideways movement
                                // by checking if the respective char array values are the same.
                                // movement not possible
                                // then checks the potential move is not backwards
                                // then checks if forward movement is diagonal only
                                // then "moves" marker to destination
                                // starting position replace with choice var + a space. Just to keep board uniform
                                // #OCD

                                // prevents sideways movement
                                if (endcoord[0] == startcoord[0])
                                {
                                    Console.WriteLine("That move is not possible");
                                    Console.WriteLine("Markers cannot move Sideways");
                                    Console.WriteLine("The counters can only move forward Diagonally");
                                    Console.ReadLine();
                                    break;
                                }
                                // prevents backwards movement
                                else
                                {
                                    bool back = checkBackMove(startcoord, endcoord);

                                    if (back == true)
                                    {
                                        bool fwd = forwardMove(startcoord, endcoord);

                                        // only allows forward diagonal movement
                                        if (fwd == true)
                                        {
                                            // checks for enemy tile in destination tile
                                            // if present perform checkEnemyMoveToCapture function
                                            // returned result determines if move possible
                                            // if yes, marker moved to tiles diagonal from enemy marker, and capturing it
                                            // else capture aborted
                                            if (tiles[x].Contains("0"))
                                            {
                                                Console.WriteLine("Enemy Marker present in destination\nYou must capture it");

                                                newDest = checkEnemyMoveToCapture();

                                                Console.WriteLine(newDest);

                                                // checks where in array postion newDest is
                                                // ***BUG*** Some reason if statement won't work. Else does though
                                                for (int d = 0; d < tiles.Length; d++)
                                                {
                                                    if (tiles[d].Contains(newDest))
                                                    {
                                                        // enemy marker location changes to destination name with "0" replaced with "  "
                                                        tiles[x] = destination + "  ";

                                                        // new destination of player marker
                                                        tiles[d] = newDest + " X";

                                                        // original poistion of marker has the "X" replaced with "  "
                                                        tiles[i] = choice + "  ";

                                                        Console.WriteLine("Marker moved");
                                                        Console.ReadLine();
                                                        begin();
                                                    }
                                                    else /*if(d == tiles.Length - 1)*/
                                                    {
                                                        Console.WriteLine(newDest);
                                                        Console.WriteLine("Cannot take enemy piece. No tiles to move too after.\nMove aborted");
                                                        Console.ReadLine();
                                                        begin();
                                                    }
                                                    Console.ReadLine();
                                                }
                                            }
                                            else
                                            {
                                                newDest = destination + " X";
                                                tiles[x] = newDest;

                                                tiles[i] = choice + "  ";

                                                Console.WriteLine("Marker moved");
                                                Console.ReadLine();
                                                begin();
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("That move is not possible\n");
                                            Console.WriteLine("The counters can only move forward Diagonally");
                                            Console.ReadLine();
                                            break;
                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("That move is not possible\n");
                                        Console.Write("Markers cannot move Backwards\n");
                                        Console.WriteLine("The counters can only move forward Diagonally");
                                        Console.ReadLine();
                                        break;
                                    }

                                }
                            

                            }
                            // flags up error if the type coord is not in array
                            if (x == tiles.Length - 1)
                            {
                                Console.WriteLine("Marker destination does not exist");
                                Console.ReadLine();
                            }
                        }
                        break;

                    }
                    // flags up error if the type coord is not in array
                    if (i == tiles.Length - 1)
                    {
                        Console.WriteLine("No marker on selected position");
                        Console.ReadLine();
                        break;
                    }

                }

            }
            while (count == 2);
            #endregion
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

        #region backwards move check
        // prevents backwards movement
        // checks element position of char[] choice  with same position of char[] letter 
        // when they match, int posC stores this value
        // same with char[] destination
        // stores same element in int posD
        // if posD is less that posC. This means the destination coord is going backwards
        // bool back is false
        // else coord is going forward. bool back is true
        public bool checkBackMove(char[] choice, char[] destination)
        {
            bool back = false;

            for (int y = 0; y < letter.Length; y++)
            {
                if (choice[0] == letter[y])
                {
                    posC = y;
                }
                if (destination[0] == letter[y])
                {
                    posD = y;
                }
                if (posD < posC)
                {
                    back = false;
                }
                else
                {
                    back = true;
                }
            }

            return back;
        }
        #endregion

        #region forwardMoveCheck1
        public bool forwardMove(char[] choice, char[] destination)
        {
            bool fwd = false;
            bool diag = false;

            for (int i = 0; i < letter.Length; i++)
            {
                if (choice[0] == letter[i])
                {
                    posC = i;
                }
                if (destination[0] == letter[i])
                {
                    posD = i;
                }
                if (posD == posC + 1)
                {
                    diag = fwdDiagCheck(choice, destination);
                    break;
                }
            }
            if (diag == true)
            {
                fwd = true;
            }

            return fwd;
        }
        #endregion
        #region forwardMoveCheck2

        public bool fwdDiagCheck(char[] choice, char[] destination)
        {
            bool diagCheck = false;

            if (destination[1] == choice[1] - 1 || destination[1] == choice[1] + 1)
            {
                diagCheck = true;
            }
            return diagCheck;
        }
        #endregion

        #region capture enemy marker
        // when the letter of end coord is the same as positon of letter[i] - 1
        // stores letter[i] in variable
        // when the number coord is the same as number[i] + 1
        // stores numer[i] in variabkes
        // string returns combined tostring of above variables
        string checkEnemyMoveToCapture()
        {
            // String contents debug stuff
            string newDest = "broke\nGo";
            string newL = "break";
            string newN = "stuff";
            char coordL;
            char coordn;

            if (endcoord[1] < startcoord[1])
            {
                for (int i = 0; i < letter.Length; i++)
                {
                    if (endcoord[0] == letter[i] - 1)
                    {
                        coordL = letter[i];
                        newL = coordL.ToString();
                    }
                    if(destination[1] - 1 == number[i])
                    {
                        coordn = number[i];
                        newN = coordn.ToString();
                        
                    }

                    newDest = newL + newN.ToUpper();
                }
            }
            else
            {
                newDest = "";
            }
            return newDest; 
            
        }
        #endregion
    }
}
