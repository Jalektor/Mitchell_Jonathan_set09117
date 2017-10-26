using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class PlayerBKing : PlayerB
    {
        Board board;
        UndoRedo Undo = new UndoRedo();
        Error error = new Error();

        private bool fwd;
        private bool back;

        #region Constructor
        public PlayerBKing(Board draughts) : base(draughts)
        {
            board = draughts;
        }

        #endregion
        public void Move()
        {
            PlayerA playeraFunction = new PlayerA(board);

            for (i = 0; i < board.Tiles.Length; i++)
            {
                if (board.Tiles[i].Contains(board.Choice) && board.Tiles[i].Contains("O") && board.Tiles[i].Contains("K"))
                {
                    #region destinationCoords
                    // checks if destination coords are present in array tiles[]
                    for (x = 0; x < board.Tiles.Length; x++)
                    {
                        if (board.Tiles[x].Contains(board.Destination) && !board.Tiles[x].Contains("O"))
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
                                back = playeraFunction.forwardMove(board.Startcoord, board.Endcoord);

                                // checks forward Movwment is legal
                                if (back == true || fwd == true)
                                {
                                    #region enemyCapture
                                    if (board.Tiles[x].Contains("X"))
                                    {
                                        Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                        Undo.undo.Push(TilesUndo);

                                        captureMarker();

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
                                            board.player--;
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
                                                        if (board.Tiles[y].Contains("X"))
                                                        {
                                                            board.Destination = Left;
                                                            board.Endcoord = board.Destination.ToCharArray();
                                                            captureMarker2();
                                                        }
                                                    }
                                                    if (board.Tiles[y].Contains(Right))
                                                    {
                                                        if (board.Tiles[y].Contains("X"))
                                                        {
                                                            board.Destination = Right;
                                                            board.Endcoord = board.Destination.ToCharArray();
                                                            captureMarker2();
                                                        }

                                                    }
                                                }
                                                Console.ReadLine();
                                            }
                                            if (back == true)
                                            {
                                                Left = playeraFunction.getPositionFWDLeft();
                                                Right = playeraFunction.getPositionFWDRight();
                                                for (y = 0; y < board.Tiles.Length; y++)
                                                {
                                                    if (board.Tiles[y].Contains(Left))
                                                    {
                                                        if (board.Tiles[y].Contains("X"))
                                                        {
                                                            board.Destination = Left;
                                                            board.Endcoord = board.Destination.ToCharArray();
                                                            captureMarker2();
                                                        }
                                                    }
                                                    if (board.Tiles[y].Contains(Right))
                                                    {
                                                        if (board.Tiles[y].Contains("X"))
                                                        {
                                                            board.Destination = Right;
                                                            board.Endcoord = board.Destination.ToCharArray();
                                                            captureMarker2();
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
                                            board.Player--;
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
        public override void captureMarker()
        {
            PlayerA playeraFunction = new PlayerA(board);

            Console.WriteLine("Enemy Marker present in destination\nYou must capture it");
            if (fwd == true)
            {
                NewDest = checkEnemyMoveToCapture();
            }
            if (back == true)
            {
                NewDest = playeraFunction.checkEnemyMoveToCapture();
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
                    board.Tiles[d] = NewDest + " KO";

                    // original poistion of marker has the "X" replaced with "  "
                    board.Tiles[i] = board.Choice + "   ";

                    Console.WriteLine("Marker moved");

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
        #region captureSecondMarker
        public override void captureMarker2()
        {
            PlayerA playeraFunction = new PlayerA(board);

            Console.WriteLine("Enemy Marker present in destination\nYou must capture it");
            if (fwd == true)
            {
                NewDest = checkEnemyMoveToCapture();
            }
            if (back == true)
            {
                NewDest = playeraFunction.checkEnemyMoveToCapture();
            }
            //NewDest = checkEnemyMoveToCapture1();
            Console.WriteLine(NewDest);
            Console.ReadLine();

            // checks where in array postion newDest is
            // and changes the string contents based on what element in tiles is being amended
            for (z = 0; z < board.Tiles.Length; z++)
            {
                if (board.Tiles[z].Contains(NewDest) && !board.Tiles[z].Contains("X") && !board.Tiles[z].Contains("O"))
                {
                    // enemy marker location changes to destination name with "0" replaced with "  "
                    board.Tiles[y] = board.Destination + "   ";

                    // new destination of player marker
                    board.Tiles[z] = NewDest + " KO";

                    // original poistion of marker has the "X" replaced with "  "
                    board.Tiles[d] = board.Choice + "   ";

                    Console.WriteLine("Marker moved");
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
