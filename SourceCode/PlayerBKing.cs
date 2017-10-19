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
            Undo undo = new Undo();
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
                                Console.WriteLine("That move is not possible");
                                Console.WriteLine("Markers cannot move Sideways");
                                Console.WriteLine("The counters can only move forward Diagonally");
                                Console.ReadLine();
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
                                        captureMarker();

                                        //// Might change this to a function later. Repeated XD
                                        //Console.Clear();

                                        //Console.WriteLine("Welcome to Draughts! test\n");

                                        //Console.WriteLine("Select Marker by Row then column");

                                        //Console.WriteLine("Player 1 marker count: " + board.PlayerAMarkerCount);
                                        //Console.WriteLine("Player 2 marker count: " + board.PlayerBMarkerCount + "\n\n");

                                        //board.createBoard();
                                    }
                                    #endregion
                                    else
                                    {
                                        board.Tiles[x] = board.Destination + " KO";
                                        board.Tiles[i] = board.Choice + "   ";

                                        undo.startCoord.Push(board.Choice);
                                        undo.endCoord.Push(board.Destination);
                                        Console.ReadLine();

                                        Console.WriteLine("Marker moved");

                                        Console.WriteLine("Do you want to undo this move? yar Y/N");
                                        string ans = Console.ReadLine().ToUpper();
                                        if (ans == "Y")
                                        {
                                            board.Choice = undo.startCoord.Pop();
                                            board.Destination = undo.endCoord.Pop();

                                            board.Tiles[i] = board.Choice + " KO";
                                            board.Tiles[x] = board.Destination + "   ";
                                            Console.ReadLine();
                                            board.begin();
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
                                    Console.WriteLine("That move is not possible\n");
                                    Console.WriteLine("Markers can only move Backwards one row and diagonally\n");
                                    Console.WriteLine("Or move forwards one row diagonally");
                                    Console.ReadLine();
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

                    board.Player--;
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
    }
}
