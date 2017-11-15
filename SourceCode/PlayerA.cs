using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CheckersGame
{  
    public class PlayerA : Movement
    {
        Board board;
        Skynet Comp;
        Undo Undo = new Undo();
        Error error = new Error();

        #region Constructor
        public PlayerA(Board draughts) : base(draughts)
        {
            board = draughts;
            Comp = board.Computer;
        }
        #endregion
        #region Movement
        public override void Move(string Opponent)
        {
            TilesUndo = new string[board.Tiles.Length];
            #region playerOneMove
            // checks array for chosen marker coord
            for (int i = 0; i < board.Tiles.Length; i++)
            {
                if (board.Tiles[i].Contains(PieceStart) && board.Tiles[i].Contains("X"))
                {
                    if (board.Tiles[i].Contains("K"))
                    {
                        board.PlayerKing.Move(Opponent);
                        break;
                    }
                    #region destinationcoords
                    // checks if destination coords are present in array tiles[]
                    for (int x = 0; x < board.Tiles.Length; x++)
                    {
                        if (board.Tiles[x].Contains(Destination) && !board.Tiles[x].Contains("X"))
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
                            if (PieceStart[0] == Destination[0])
                            {
                                error.NoSidewaysMove();
                                break;
                            }
                            #region backwardsMoveCheck
                            // prevents backwards movement
                            else
                            {
                                bool back = checkBackMove(PieceStart, Destination);

                                if (back == true)
                                {
                                    bool fwd = ForwardMove(PieceStart, Destination);
                                    #region forwardMove
                                    // only allows forward diagonal movement
                                    if (fwd == true)
                                    {
                                        #region enemyCapture
                                        // checks for enemy marker in destination tile
                                        // if present perform checkEnemyMoveToCapture function
                                        // returned result determines if move possible
                                        // if yes, marker moved to tiles diagonal from enemy marker, and capturing it
                                        // else capture aborted
                                        // capped at 3 captures a turn
                                        if (board.Tiles[x].Contains("O"))
                                        {
                                            board.Player++;
                                            Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                            Undo.undo.Push(TilesUndo);

                                            captureMarker(Opponent);

                                            board.DisplayData();
                                            board.createBoard();

                                            Console.WriteLine("Do you want to undo this move? PlayerA Y/N");
                                            Ans = Console.ReadLine().ToUpper();
                                            if (Ans == "Y")
                                            {
                                                Console.WriteLine("Undoing move");
                                                board.Tiles = Undo.undo.Pop();
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                // can capture upto 2 other pieces
                                                for(int a = 0; a < 2; a++)
                                                {
                                                    board.DisplayData();
                                                    board.createBoard();
                                                    Console.WriteLine("Do you want to capture another piece? Y/N");
                                                    Ans = Console.ReadLine().ToUpper();    
                                                    if(Ans == "Y")
                                                    {
                                                        // sets new choice position
                                                        // finds the fwd diag coords of new choice location
                                                        // searches for them and if they contain enemy marker performs another piece capture
                                                        PieceStart = NewDest;
                                                        Left = getPositionFWDLeft();
                                                        Right = getPositionFWDRight();
                                                        for (int y = 0; y < board.Tiles.Length; y++)
                                                        {
                                                            if (board.Tiles[y].Contains(Left) && board.Tiles[y].Contains("O"))
                                                            {                                                                                                                      
                                                                Destination = Left;
                                                                captureMarker(Opponent);                                                           
                                                            }
                                                            else if (board.Tiles[y].Contains(Right) && board.Tiles[y].Contains("O"))
                                                            {
                                                                Destination = Right;
                                                                captureMarker(Opponent);
                                                            }
                                                        }
                                                        Console.ReadLine();
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }                                                  
                                                }                                                
                                            }                                                                                     
                                        }
                                        #endregion
                                        else
                                        {
                                            Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                            Undo.undo.Push(TilesUndo);
                                            if (Destination.Contains("H"))
                                            {
                                                for (int z = 0; z < board.Tiles.Length; z++)
                                                {
                                                    if (board.Tiles[z].Contains(PieceStart))
                                                    {
                                                        board.Tiles[z] = PieceStart + "   ";
                                                    }
                                                    if (board.Tiles[z].Contains(Destination))
                                                    {
                                                        board.Tiles[z] = Destination + " KX";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                for (int z = 0; z < board.Tiles.Length; z++)
                                                {
                                                    if (board.Tiles[z].Contains(PieceStart))
                                                    {
                                                         board.Tiles[z] = PieceStart + "   ";
                                                    }
                                                    if (board.Tiles[z].Contains(Destination))
                                                    {
                                                         board.Tiles[z] = Destination + "  X";
                                                    }
                                                }
                                            }
                                            Console.ReadLine();

                                            board.DisplayData();
                                            board.createBoard();

                                            Console.WriteLine("Marker moved");

                                            Console.WriteLine("Do you want to undo this move? PlayerA Y/N");
                                            string ans = Console.ReadLine().ToUpper();
                                            if(ans == "Y")
                                            {
                                                Console.WriteLine("Undoing move");
                                                board.Tiles = Undo.undo.Pop();
                                                Console.ReadLine();
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
                                        error.WrongFwdMove();
                                        break;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    error.NoBackMove();
                                    break;
                                }
                            }
                            #endregion
                            #endregion
                            break;
                        }
                        // flags up error if the destination coord is not in array
                        if (x == board.Tiles.Length - 1)
                        {
                            error.WrongDestCoord();
                            break;
                        }
                    }
                    #endregion
                    break;
                }
                // flags up error if the type coord is not in array
                if (i == board.Tiles.Length - 1)
                {
                    error.NoPlayerCounter();
                    break;
                }
            }
            #endregion
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
        public virtual bool checkBackMove(string choice, string destination)
        {
            bool back = false;

            for (int y = 0; y < Letter.Length; y++)
            {
                if (choice[0] == Letter[y])
                {
                    PosC = y;
                }
                if (destination[0] == Letter[y])
                {
                    PosD = y;
                }
                if (PosD < PosC)
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
        public bool ForwardMove(string choice, string destination)
        {
            bool fwd = false;
            bool diag = false;

            for (int i = 0; i < Letter.Length; i++)
            {
                if (choice[0] == Letter[i])
                {
                    PosC = i;
                }
                if (destination[0] == Letter[i])
                {
                    PosD = i;
                }
                if (PosD == PosC + 1)
                {
                    diag = FwdDiagCheck(choice, destination);
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
        public bool FwdDiagCheck(string choice, string destination)
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
            if (Destination[1] < PieceStart[1])
            {
                for (int i = 0; i < Letter.Length; i++)
                {
                    if (Destination[0] == Letter[i] - 1)
                    {
                        coordL = Letter[i];
                        newL = coordL.ToString();
                    }
                    if (Destination[1] - 1 == Number[i])
                    {
                        coordn = Number[i];
                        newN = coordn.ToString();

                    }
                    NewDest = newL + newN.Trim().ToUpper();
                }
            }
            // checks if enemy is right diagonal fwd to start coord
            else if (Destination[1] > PieceStart[1])
            {
                for (int c = 0; c < Letter.Length; c++)
                {
                    if (Destination[0] == Letter[c] - 1)
                    {
                        coordL = Letter[c];
                        newL = coordL.ToString();
                    }
                    if (Destination[1] + 1 == Number[c])
                    {
                        coordn = Number[c];
                        newN = coordn.ToString();

                    }

                    NewDest = newL + newN.Trim().ToUpper();
                }
            }
            else
            {
                NewDest = "";
            }
            return NewDest;

        }
        #endregion
        #region captureEnemyMarker(s)
        public virtual void captureMarker(string Opponent)
        {

            NewDest = checkEnemyMoveToCapture();

            // checks where in array postion newDest is
            // and changes the string contents based on what element in tiles is being amended
            foreach(string piece in board.Tiles)
            {
                if(piece.Contains(NewDest) && !piece.Contains("X") && !piece.Contains("O"))
                {
                    board.PlayerBMarkerCount--;
                    Console.WriteLine("Enemy Marker present in destination\nAttempting Capture");
                    if (NewDest.Contains("H"))
                    {
                        for(int i = 0; i < board.Tiles.Length; i++)
                        {
                            if (board.Tiles[i].Contains(PieceStart))
                            {
                                board.Tiles[i] = PieceStart + "   ";
                            }
                            if (board.Tiles[i].Contains(Destination))
                            {
                                board.Tiles[i] = Destination + "   ";
                            }
                            if (board.Tiles[i].Contains(NewDest))
                            {
                                board.Tiles[i] = NewDest + " KX";
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enemy Marker present in destination\nAttempting Capture");
                        for (int i = 0; i < board.Tiles.Length; i++)
                        {
                            if (board.Tiles[i].Contains(PieceStart))
                            {
                                board.Tiles[i] = PieceStart + "   ";
                            }
                            if (board.Tiles[i].Contains(Destination))
                            {
                                board.Tiles[i] = Destination + "   ";
                            }
                            if (board.Tiles[i].Contains(NewDest))
                            {
                                board.Tiles[i] = NewDest + "  X";
                            }
                        }
                    }
                }
            }
            if (Opponent == "C")
            {
                Comp.RemoveTakenPiece(Destination);
            }
        }
            #endregion          
        #region CheckSecondEnemyMarkerLeft/Right
        // returns left/right positions of fwd diag coords after one enemy piece has been taken
        public override string getPositionFWDLeft()
        {
            for (int x = 0; x < Letter.Length; x++)
            {
                if(PieceStart[0] == Letter[x] - 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if(PieceStart[1] - 1 == Number[x])
                {
                    coordn = Number[x];
                    newN = coordn.ToString();
                }
                NewDest = newL + newN.Trim().ToUpper();
            }
            return NewDest;
        }
        public override string getPositionFWDRight()
        {
            for ( int x = 0; x < Letter.Length; x++)
            {
                if (PieceStart[0] == Letter[x] - 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if (PieceStart[1] + 1 == Number[x])
                {
                    coordn = Number[x];
                    newN = coordn.ToString();
                }
                NewDest = newL + newN.Trim().ToUpper();
            }
            return NewDest;
        }
        #endregion       
    }
}


                       
