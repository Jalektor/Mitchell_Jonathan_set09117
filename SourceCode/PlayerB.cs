using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class PlayerB : Movement
    {
        Board board;
        Undo Undo = new Undo();
        Error error = new Error();

        #region Constructor
        public PlayerB(Board draughts) : base(draughts)
        {
            board = draughts;
        }
        #endregion

        public override void Move(string Opponent)
        {
            TilesUndo = new string[board.Tiles.Length];

            #region Player2Move
            // checks array for chosen marker coord
            for (int i = 0; i < board.Tiles.Length; i++ )
            {
                if(board.Tiles[i].Contains(PieceStart) && board.Tiles[i].Contains("O"))
                {
                    if(board.Tiles[i].Contains("K"))
                    {
                        board.PlayerKing.Move(Opponent);
                        Console.WriteLine("Blarg");
                        Console.ReadLine();
                        break;
                    }
                    #region destinationCoords
                    // checks if destination coords are present in array tiles[]
                    for (int x = 0; x < board.Tiles.Length; x++)
                    {
                        if (board.Tiles[x].Contains(Destination) && !board.Tiles[x].Contains("O"))
                        {
                            // prevents sideways movement
                            // by checking if the respective char array values are the same.
                            // movement not possible
                            // then checks the potential move is not backwards
                            // then checks if forward movement is diagonal only
                            // then "moves" marker to destination
                            // starting position replace with choice var + a space. Just to keep board uniform
                            // #OCD
                            #region sidewaysMove
                            // prevents sideways movement
                            if (Destination[0] == PieceStart[0])
                            {
                                error.NoSidewaysMove();
                                break;
                            }
                            #region backwardsMove
                            // prevents backwards movement
                            else
                            {
                                // code this section!!! needs function in class
                                bool back = checkBackMove(PieceStart, Destination);
                                if(back == true)
                                {
                                    bool fwd = ForwardMove(PieceStart, Destination);
                                    #region ForwardMove
                                    // only allows foward diagonal movement
                                    if(fwd == true)
                                    {
                                        #region enemy capture
                                        // checks for enemy tile in destination tile
                                        // if present perform checkEnemyMoveToCapture function
                                        // returned result determines if move possible
                                        // if yes, marker moved to tiles diagonal from enemy marker, and capturing it
                                        // else capture aborted
                                        if(board.Tiles[x].Contains("X"))
                                        {
                                            board.player--;
                                            Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                            Undo.undo.Push(TilesUndo);

                                            captureMarker();

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
                                                for (int a = 0; a < 2; a++)
                                                {
                                                    board.DisplayData();
                                                    board.createBoard();
                                                    Console.WriteLine("Do you want to capture another piece? Y/N");
                                                    Ans = Console.ReadLine().ToUpper();
                                                    if (Ans == "Y")
                                                    {
                                                        // sets new choice position
                                                        // finds the fwd diag coords of new choice location
                                                        // searches for them and if they contain enemy marker performs second piece capture
                                                        PieceStart = NewDest;
                                                        Left = getPositionFWDLeft();
                                                        Right = getPositionFWDRight();
                                                        for (int y = 0; y < board.Tiles.Length; y++)
                                                        {
                                                            if (board.Tiles[y].Contains(Left) && board.Tiles[y].Contains("X"))
                                                            {
                                                                Destination = Left;
                                                                captureMarker();
                                                            }
                                                            else if (board.Tiles[y].Contains(Right) && board.Tiles[y].Contains("X"))
                                                            {
                                                                Destination = Right;
                                                                captureMarker();
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
                                            foreach (string piece in board.Tiles)
                                            {
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
                                                            board.Tiles[z] = Destination + " KO";
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
                                                            board.Tiles[z] = Destination + "  O";
                                                        }
                                                    }
                                                }
                                            }
                                            Console.ReadLine();

                                            Console.WriteLine("Marker moved");
                                            Console.WriteLine("Do you want to undo that move? Y/N");
                                            string ans = Console.ReadLine().ToUpper();
                                            if (ans == "Y")
                                            {
                                                Console.WriteLine("Undoing move");
                                                board.Tiles = Undo.undo.Pop();
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                board.Player--;
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
                        // flags up error if the type coord is not in array
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
        #region backwards move check
        // prevents backwards movement
        // checks element position of char[] choice  with same position of char[] letter 
        // when they match, int posC stores this value
        // same with char[] destination
        // stores same element in int posD
        // if posD is less that posC. This means the destination coord is going backwards
        // bool back is false
        // else coord is going forward. bool back is true
        public bool checkBackMove(string choice, string destination)
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
                if (PosD > PosC)
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
                if (PosD == PosC - 1)
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
        public bool FwdDiagCheck(string choice, string destination)
        {
            bool diagCheck = false;

            if (destination[1] == choice[1] + 1 || destination[1] == choice[1] - 1)
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
        public virtual string checkEnemyMoveToCapture()
        {
            // checks if enemy marker is left diagonal fwd to start coord
            if (Destination[1] < PieceStart[1])
            {
                for (int i = 0; i < Letter.Length; i++)
                {
                    if (Destination[0] == Letter[i] + 1)
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
                for (int i = 0; i < Letter.Length; i++)
                {
                    if (Destination[0] == Letter[i] + 1)
                    {
                        coordL = Letter[i];
                        newL = coordL.ToString();
                    }
                    if (Destination[1] + 1 == Number[i])
                    {
                        coordn = Number[i];
                        newN = coordn.ToString();
                        Console.WriteLine("newN: " + newN);
                        Console.ReadLine();

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
        public virtual void captureMarker()
        {
            NewDest = checkEnemyMoveToCapture();

            // checks where in array postion newDest is
            // and changes the string contents based on what element in tiles is being amended
            foreach (string piece in board.Tiles)
            {
                if (piece.Contains(NewDest) && !piece.Contains("X") && !piece.Contains("O"))
                {
                    board.PlayerAMarkerCount--;
                    Console.WriteLine("Enemy Marker present in destination\nAttempting Capture");
                    if (NewDest.Contains("H"))
                    {
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
                                board.Tiles[i] = NewDest + " KO";
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
                                board.Tiles[i] = NewDest + "  O";
                            }
                        }
                    }
                }
            }
            //Console.WriteLine("Enemy Marker present in destination\nYou must capture it");

            //NewDest = checkEnemyMoveToCapture();

            //// checks where in array postion newDest is
            //// and changes the string contents based on what element in tiles is being amended
            //for (d = 0; d < board.Tiles.Length; d++)
            //{
            //    if (board.Tiles[d].Contains(NewDest) && !board.Tiles[d].Contains("X") && !board.Tiles[d].Contains("O"))
            //    {
            //        // enemy marker location changes to destination name with "0" replaced with "  "
            //        board.Tiles[x] = Destination + "   ";

            //        // new destination of player marker
            //        board.Tiles[d] = NewDest + "  O";

            //        // original poistion of marker has the "O" replaced with "  "
            //        board.Tiles[i] = PieceStart + "   ";
            //        Console.WriteLine("Marker moved");

            //        board.PlayerAMarkerCount--;
            //        Console.ReadLine();
            //        break;
            //    }
            //    if (d == board.Tiles.Length - 1)
            //    {
            //        error.NoCapture();
            //    }
            //}
        }
        #endregion
        #region CheckSecondEnemyMarkerLeft/Right
        // returns left/right positions of fwd diag coords after one enemy piece has been taken
        public override string getPositionFWDLeft()
        {
            int x;
            for (x = 0; x < Letter.Length; x++)
            {
                if (PieceStart[0] == Letter[x] + 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if (PieceStart[1] - 1 == Number[x])
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
            int x;
            for (x = 0; x < Letter.Length; x++)
            {
                if (PieceStart[0] == Letter[x] + 1)
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
