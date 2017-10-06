using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    class PlayerB
    {
        Board board;
        #region Constructor
        public PlayerB(Board draughts)
        {
            board = draughts;
        }

        public void move()
        {
            #region Player2Move
            // checks array for chosen marker coord
            for (int i = 0; i < board.Tiles.Length; i++ )
            {
                if(board.Tiles[i].Contains(board.Choice))
                {
                    Console.WriteLine("Marker exists");

                    Console.WriteLine("Where do you want the marker to go?");
                    board.Input = Console.ReadLine();
                    board.Destination = board.Input.ToUpper();

                    board.Endcoord = board.Destination.ToCharArray();
                    #region destinationCoords
                    // checks if destination coords are present in array tiles[]
                    for (int x = 0; x < board.Tiles.Length; x++)
                    {
                        if (board.Tiles[x].Contains(board.Destination))
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
                            if (board.Endcoord[0] == board.Startcoord[0])
                            {
                                Console.WriteLine("That move is not possible");
                                Console.WriteLine("Markers cannot move Sideways");
                                Console.WriteLine("The counters can only move forward Diagonally");
                                Console.ReadLine();
                                break;
                            }
                            #region backwardsMove
                            // prevents backwards movement
                            else
                            {
                                // code this section!!! needs function in class
                                //bool back = ;
                            }
                            #endregion
                            #endregion

                        }
                        // flags up error if the type coord is not in array
                        if (x == board.Tiles.Length - 1)
                        {
                            Console.WriteLine("Marker destination does not exist");
                            Console.ReadLine();
                        }
                    }
                    #endregion
                }
                // flags up error if the type coord is not in array
                if (i == board.Tiles.Length - 1)
                {
                    Console.WriteLine("No marker on selected position");
                    Console.ReadLine();
                    break;
                }
            }
            #endregion
        }
        #endregion
    }
}
