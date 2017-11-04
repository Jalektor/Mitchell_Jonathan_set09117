using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class PlayerAKing : PlayerA
    {
        Board board;
        UndoRedo Undo = new UndoRedo();
        Error error = new Error();

        private bool fwd;
        private bool back;
            
        #region Constructor
        public PlayerAKing(Board draughts) : base(draughts)
        {
            board = draughts;          
        }
        
        #endregion
        public override void move(string Opponent)
        {
            TilesUndo = new string[board.Tiles.Length];
            PlayerB playerbFunction = new PlayerB(board);

            for (i = 0; i < board.Tiles.Length; i++)
            {
                if(board.Tiles[i].Contains(board.Choice) && board.Tiles[i].Contains("X") && board.Tiles[i].Contains("K"))
                {
                    #region destinationCoords
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
                                error.NoSidewaysMove();
                                break;
                            }
                            #region backwardsMoveCheck
                            // Allows backwards movement
                            // but only IF the coord are diagonal to starting coords AND only 1 row behind
                            else
                            {
                                fwd = forwardMove(board.Startcoord, board.Endcoord);
                                back = playerbFunction.forwardMove(board.Startcoord, board.Endcoord);

                                // checks forward Movwment is legal
                                if (back == true || fwd == true)
                                {
                                    #region enemyCapture
                                    if (board.Tiles[x].Contains("O"))
                                    {
                                        Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                        Undo.undo.Push(TilesUndo);

                                        captureMarker(Opponent);

                                        board.DisplayData();
                                        board.createBoard();

                                        Console.WriteLine("Do you want to undo this move? PlayerA Y/N");
                                        string ans = Console.ReadLine().ToUpper();
                                        if (ans == "Y")
                                        {
                                            Console.WriteLine("Undoing move");
                                            board.Tiles = Undo.undo.Pop();
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            board.Player++;
                                            // sets new choice position
                                            // finds the fwd diag coords of new choice location
                                            // searches for them and if they contain enemy marker performs second marker takeover function
                                            // May consider doing this three deep?????
                                            board.Choice = NewDest;
                                            board.Startcoord = board.Choice.ToCharArray();
                                            if (fwd == true)
                                            {
                                                Left = getPositionFWDLeft();
                                                Right = getPositionFWDRight();
                                                for (y = 0; y < board.Tiles.Length; y++)
                                                {
                                                    if (board.Tiles[y].Contains(Left))
                                                    {
                                                        if (board.Tiles[y].Contains("O"))
                                                        {
                                                            board.Destination = Left;
                                                            board.Endcoord = board.Destination.ToCharArray();
                                                            captureMarker2(Opponent);
                                                        }
                                                    }
                                                    if (board.Tiles[y].Contains(Right))
                                                    {
                                                        if (board.Tiles[y].Contains("O"))
                                                        {
                                                            board.Destination = Right;
                                                            board.Endcoord = board.Destination.ToCharArray();
                                                            captureMarker2(Opponent);
                                                        }

                                                    }
                                                }
                                                Console.ReadLine();
                                            }
                                            if (back == true)
                                            {
                                                Left = playerbFunction.getPositionFWDLeft();
                                                Right = playerbFunction.getPositionFWDRight();
                                                for (y = 0; y < board.Tiles.Length; y++)
                                                {
                                                    if (board.Tiles[y].Contains(Left))
                                                    {
                                                        if (board.Tiles[y].Contains("O"))
                                                        {
                                                            board.Destination = Left;
                                                            board.Endcoord = board.Destination.ToCharArray();
                                                            captureMarker2(Opponent);
                                                        }
                                                    }
                                                    if (board.Tiles[y].Contains(Right))
                                                    {
                                                        if (board.Tiles[y].Contains("O"))
                                                        {
                                                            board.Destination = Right;
                                                            board.Endcoord = board.Destination.ToCharArray();
                                                            captureMarker2(Opponent);
                                                        }

                                                    }
                                                }
                                                Console.ReadLine();
                                            }
                                        }                                       
                                    }
                                    #endregion
                                    else
                                    {
                                        Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                        Undo.undo.Push(TilesUndo);

                                        Console.ReadLine();

                                        board.DisplayData();
                                        board.createBoard();

                                        Console.WriteLine("Marker moved");
                                        Console.WriteLine("Do you want to undo this move? yar Y/N");
                                        string ans = Console.ReadLine().ToUpper();
                                        if (ans == "Y")
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
                                    error.KingNoBackMove();
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
        } 
        #region captureEnemyMarker1
        public override void captureMarker(string Opponent)
        {
            Skynet Comp = board.computer;
            PlayerB playerbFunction = new PlayerB(board);

            Console.WriteLine("Enemy Marker present in destination\nAttempting capture");
            if(fwd == true)
            {
                NewDest = checkEnemyMoveToCapture();
            }
            if(back == true)
            {
                NewDest = playerbFunction.checkEnemyMoveToCapture();
            }
            //NewDest = checkEnemyMoveToCapture1();
            Console.WriteLine(NewDest);
            Console.ReadLine();

            // checks where in array postion newDest is
            // and changes the string contents based on what element in tiles is being amended
            for (d = 0; d < board.Tiles.Length; d++)
            {
                if (board.Tiles[d].Contains(NewDest) && !board.Tiles[d].Contains("X") && !board.Tiles[d].Contains("O"))
                {
                    // enemy marker location changes to destination name with "0" replaced with "  "
                    board.Tiles[x] = board.Destination + "   ";

                    // new destination of player marker
                    board.Tiles[d] = NewDest + " KX";

                    // original poistion of marker has the "X" replaced with "  "
                    board.Tiles[i] = board.Choice + "   ";
                    if (Opponent == "C")
                    {
                        Comp.RemoveTakenPiece(board.Destination);
                    }
                    board.PlayerBMarkerCount--;
                    Console.ReadLine();
                    break;
                }
                if (d == board.Tiles.Length - 1)
                {
                    error.NoCapture();
                }
            }
        }
        #endregion
        #region CaptureSecondMarker
        public override void captureMarker2(string Opponent)
        {
            Skynet Comp = board.computer;
            PlayerB playerbFunction = new PlayerB(board);

            Console.WriteLine("Enemy Marker present in destination\nAttempting capture");
            if (fwd == true)
            {
                NewDest = checkEnemyMoveToCapture();
            }
            if (back == true)
            {
                NewDest = playerbFunction.checkEnemyMoveToCapture();
            }
            Console.WriteLine(NewDest);
            Console.ReadLine();

            for (z = 0; z < board.Tiles.Length; z++)
            {
                if (board.Tiles[z].Contains(NewDest) && !board.Tiles[z].Contains("X") && !board.Tiles[z].Contains("O"))
                { 
                    // enemy marker location changes to destination name with "0" replaced with "  "
                    board.Tiles[y] = board.Destination + "   ";

                    // new destination of player marker
                    board.Tiles[z] = NewDest + " KX";

                    // original poistion of marker has the "X" replaced with "  "
                    board.Tiles[d] = board.Choice + "   ";

                    if (Opponent == "C")
                    {
                        Comp.RemoveTakenPiece(board.Destination);
                    }
                    Console.ReadLine();
                    board.PlayerBMarkerCount--;
                    break;
                }
                if (z == board.Tiles.Length - 1)
                {
                    error.NoCapture();
                }
            }
        }
        #endregion
    }

}
