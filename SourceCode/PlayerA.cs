using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CheckersGame
{  
    public class PlayerA
    {
        // variables for use in various for loops
        // also used to move markers around bored in various functions
        // NOTE. It works. kinda gonna stick to it
        private int x;
        private int i;
        private int d;
        private int y;
        private int z;

        // String contents debug stuff
        // NOTE newL & newN must have a strign in them else are regarded as null, for soome reason
        public string newDest;
        public string newL = "coord";
        public string newN = "coord";
        public char coordL;
        public char coordn;

        Board board;
        

        #region Constructor
        public PlayerA(Board draughts)
        {
            board = draughts;
        }
        #endregion

        public void move()
        {
            Undo undo = new Undo();

            #region playerOneMove
            // checks array for chosen marker coord
            for (i = 0; i < board.Tiles.Length; i++)
            {
                if (board.Tiles[i].Contains(board.Choice) && board.Tiles[i].Contains("X"))
                {
                    #region destinationcoords
                    // checks if destination coords are present in array tiles[]
                    for (x = 0; x < board.Tiles.Length; x++)
                    {
                        if (board.Tiles[x].Contains(board.Destination) && !board.Tiles[x].Contains("X"))
                        {
                            // prevents sideways movement
                            // by checking if the respective char array values are the same.
                            // movement not possible
                            // then checks the potential move is not backwards
                            // then checks if forward movement is diagonal only
                            // then "moves" marker to destination
                            // starting position replace with choice var + a space. Just to keep board uniform
                            // #OCD

                            #region SidewaysMoveCheck
                            // prevents sideways movement
                            if (board.Endcoord[0] == board.Startcoord[0])
                            {
                                Console.WriteLine("That move is not possible");
                                Console.WriteLine("Markers cannot move Sideways");
                                Console.WriteLine("The counters can only move forward Diagonally");
                                Console.ReadLine();
                                break;
                            }
                            #region backwardsMoveCheck
                            // prevents backwards movement
                            else
                            {
                                bool back = checkBackMove(board.Startcoord, board.Endcoord);

                                if (back == true)
                                {
                                    bool fwd = forwardMove(board.Startcoord, board.Endcoord);
                                    #region forwardMove
                                    // only allows forward diagonal movement
                                    if (fwd == true)
                                    {
                                        #region enemycapture
                                        // checks for enemy marker in destination tile
                                        // if present perform checkEnemyMoveToCapture function
                                        // returned result determines if move possible
                                        // if yes, marker moved to tiles diagonal from enemy marker, and capturing it
                                        // else capture aborted
                                        if (board.Tiles[x].Contains("O"))
                                        {
                                            captureMarker();

                                            // Might change this to a function later. Repeated XD
                                            Console.Clear();

                                            Console.WriteLine("Welcome to Draughts!\n");

                                            Console.WriteLine("Select Marker by Row then column");

                                            Console.WriteLine("Player 1 marker count: " + board.PlayerAMarkerCount);
                                            Console.WriteLine("Player 2 marker count: " + board.PlayerBMarkerCount + "\n\n");

                                            board.createBoard();

                                            // sets new choice position
                                            // finds the fwd diag coords of new choice location
                                            // searches for them and if they contain enemy marker performs second marker takeover function
                                            // May consider doing this three deep?????
                                            board.Choice = board.NewDest;
                                            board.Startcoord = board.Choice.ToCharArray();
                                            board.Left = getPositionFWDLeft();
                                            board.Right = getPositionFWDRight();
                                            for(y = 0; y < board.Tiles.Length; y++)
                                            {
                                                if(board.Tiles[y].Contains(board.Left))
                                                {
                                                    if(board.Tiles[y].Contains("O"))
                                                    {
                                                        board.Destination = board.Left;
                                                        board.Endcoord = board.Destination.ToCharArray();
                                                        captureMarker2();
                                                    }
                                                }
                                                if(board.Tiles[y].Contains(board.Right))
                                                {
                                                    if (board.Tiles[y].Contains("O"))
                                                    {
                                                        board.Destination = board.Right;
                                                        board.Endcoord = board.Destination.ToCharArray();
                                                        captureMarker2();
                                                    }
                                                    
                                                }
                                            }
                                            Console.ReadLine();                                           
                                        }
                                        #endregion
                                        else
                                        {
                                            board.Tiles[x] = board.Destination + " X";
                                            board.Tiles[i] = board.Choice + "  ";

                                            undo.startCoord.Push(board.Choice);
                                            undo.endCoord.Push(board.Destination);
                                            Console.ReadLine();

                                            Console.WriteLine("Marker moved");

                                            Console.WriteLine("Do you want to undo this move? Y/N");
                                            string ans = Console.ReadLine().ToUpper();
                                            if(ans == "Y")
                                            {
                                                board.Choice = undo.startCoord.Pop();
                                                board.Destination = undo.endCoord.Pop();

                                                board.Tiles[i] = board.Choice + " X";
                                                board.Tiles[x] = board.Destination + "  ";
                                                Console.ReadLine();
                                                board.begin();
                                            }
                                            else
                                            {
                                                board.Player++;
                                                Console.ReadLine();
                                                break;
                                            }
                                           
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("That move is not possible\n");
                                        Console.WriteLine("The counters can only move forward Diagonally");
                                        Console.ReadLine();
                                        break;
                                    }
                                    #endregion
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
                            #endregion
                            #endregion
                            break;
                        }
                        // flags up error if the type coord is not in array
                        if (x == board.Tiles.Length - 1)
                        {
                            Console.WriteLine("Marker destination is illegal or already has a player counter on it");
                            Console.ReadLine();
                            break;
                        }
                    }
                    #endregion
                    break;
                }
                // flags up error if the type coord is not in array
                if (i == board.Tiles.Length - 1)
                {
                    Console.WriteLine("No player counter on selected position\n");
                    Console.ReadLine();
                    break;
                }
            }
            #endregion
        }

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

            for (int y = 0; y < board.Letter.Length; y++)
            {
                if (choice[0] == board.Letter[y])
                {
                    board.PosC = y;
                }
                if (destination[0] == board.Letter[y])
                {
                    board.PosD = y;
                }
                if (board.PosD < board.PosC)
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
        // Validates to only allow forward move by one row
        // Checks if coord entered is forward move
        // Done by checking the both [0] of choice and destination char[] arrays. This is the letter of the coord
        // Where these values == value in letter char array[]
        // stored value in variable ints
        // ints are then compared. If PosD == PosC +1 this means the destination coord is a row ahead of startcoord only. Returns true
        // Else assumption the forward move is atleast 2 rows ahead of start coord. Returns false
        public bool forwardMove(char[] choice, char[] destination)
        {
            bool fwd = false;
            bool diag = false;

            for (int i = 0; i < board.Letter.Length; i++)
            {
                if (choice[0] == board.Letter[i])
                {
                    board.PosC = i;
                }
                if (destination[0] == board.Letter[i])
                {
                    board.PosD = i;
                }
                if (board.PosD == board.PosC + 1)
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
        // Validates forward diagonal movement
        // checks the second elements of the choice & destination char[] arrays. These are the number of the coords
        // If the destination number is +/- 1 of the choice number
        // The destination coord is the fwd diag of choice coord. Returns true
        // Anything else assumes it is not and returns false
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
        #region CheckEnemyMarker
        // when the letter of end coord is the same as positon of letter[i] -/+ 1
        // stores letter[i] in variable
        // when the number coord is the same as number[i] +/- 1
        // stores numer[i] in variables
        // string returns combined tostring of above variables
        public string checkEnemyMoveToCapture()
        {
            // checks if enemy marker is left diagonal fwd to start coord
            if (board.Endcoord[1] < board.Startcoord[1])
            {
                for (int i = 0; i < board.Letter.Length; i++)
                {
                    if (board.Endcoord[0] == board.Letter[i] - 1)
                    {
                        coordL = board.Letter[i];
                        newL = coordL.ToString();
                    }
                    if (board.Destination[1] - 1 == board.Number[i])
                    {
                        coordn = board.Number[i];
                        newN = coordn.ToString();

                    }
                    newDest = newL + newN.Trim().ToUpper();
                }
            }
            // checks if enemy is right diagonal fwd to start coord
            else if (board.Endcoord[1] > board.Startcoord[1])
            {
                for (int c = 0; c < board.Letter.Length; c++)
                {
                    if (board.Endcoord[0] == board.Letter[c] - 1)
                    {
                        coordL = board.Letter[c];
                        newL = coordL.ToString();
                    }
                    if (board.Destination[1] + 1 == board.Number[c])
                    {
                        coordn = board.Number[c];
                        newN = coordn.ToString();

                    }

                    newDest = newL + newN.Trim().ToUpper();
                }
            }
            else
            {
                newDest = "";
            }
            return newDest;

        }
        #endregion
        #region captureEnemyMarker1
        public void captureMarker()
        {
            Console.WriteLine("Enemy Marker present in destination\nYou must capture it");

            board.NewDest = checkEnemyMoveToCapture();

            // checks where in array postion newDest is
            // and changes the string contents based on what element in tiles is being amended
            for (d = 0; d < board.Tiles.Length; d++)
            {
                if (board.Tiles[d].Contains(board.NewDest) && !board.Tiles[d].Contains("X") && !board.Tiles[d].Contains("O"))
                {
                    // enemy marker location changes to destination name with "0" replaced with "  "
                    board.Tiles[x] = board.Destination + "  ";

                    // new destination of player marker
                    board.Tiles[d] = board.NewDest + " X";

                    // original poistion of marker has the "X" replaced with "  "
                    board.Tiles[i] = board.Choice + "  ";
                    Console.WriteLine("Marker moved");

                    board.Player++;
                    board.PlayerBMarkerCount--;
                    Console.ReadLine();
                    break;
                }
                if (d == board.Tiles.Length - 1)
                {
                    Console.WriteLine("Cannot take enemy piece. No tiles to move too after.\nOr there is an enemy marker at location\nMove aborted");
                    Console.ReadLine();
                }
            }
        }
            #endregion          
        #region CheckSecondEnemyMarkerLeft/Right
        // returns left/right positions of fwd diag coords after one enemy piece has been taken
        public string getPositionFWDLeft()
        {
            int x;
            for (x = 0; x < board.Letter.Length; x++)
            {
                if(board.Startcoord[0] == board.Letter[x] - 1)
                {
                    coordL = board.Letter[x];
                    newL = coordL.ToString();
                }
                if(board.Startcoord[1] - 1 == board.Number[x])
                {
                    coordn = board.Number[x];
                    newN = coordn.ToString();
                }
                newDest = newL + newN.Trim().ToUpper();
            }
            return newDest;
        }
        public string getPositionFWDRight()
        {
            int x;
            for (x = 0; x < board.Letter.Length; x++)
            {
                if (board.Startcoord[0] == board.Letter[x] - 1)
                {
                    coordL = board.Letter[x];
                    newL = coordL.ToString();
                }
                if (board.Startcoord[1] + 1 == board.Number[x])
                {
                    coordn = board.Number[x];
                    newN = coordn.ToString();
                }
                newDest = newL + newN.Trim().ToUpper();
            }
            return newDest;
        }
        #endregion
        #region captureEnemyMarker2
        // checks where in array postion newDest is
        // and changes the string contents based on what element in tiles is being amended
        // This only f there is a second marker present
        public void captureMarker2()
        {
            Console.WriteLine("Enemy Marker present in destination\nYou must capture it");

            board.NewDest = checkEnemyMoveToCapture();

            for (z = 0; z < board.Tiles.Length; z++)
            {
                if (board.Tiles[z].Contains(board.NewDest) && !board.Tiles[z].Contains("X") && !board.Tiles[z].Contains("O"))
                {
                    // enemy marker location changes to destination name with "0" replaced with "  "
                    board.Tiles[y] = board.Destination + "  ";

                    // new destination of player marker
                    board.Tiles[z] = board.NewDest + " X";

                    // original poistion of marker has the "X" replaced with "  "
                    board.Tiles[d] = board.Choice + "  ";
                    Console.WriteLine("Marker moved");

                    board.Player++;
                    board.PlayerBMarkerCount--;
                    break;
                }
                if (d == board.Tiles.Length - 1)
                {
                    Console.WriteLine("Cannot take enemy piece. No tiles to move too after.\nOr there is an enemy marker at location\nMove aborted");
                    Console.ReadLine();
                }
            }
        }
        #endregion
    }
}


                       
