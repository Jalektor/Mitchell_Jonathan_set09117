﻿using System;
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
        #endregion
        public void move()
        {
            #region Player2Move
            // checks array for chosen marker coord
            for (int i = 0; i < board.Tiles.Length; i++ )
            {
                if(board.Tiles[i].Contains(board.Choice) && board.Tiles[i].Contains("O"))
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
                                bool back = checkBackMove(board.Startcoord, board.Endcoord);
                                if(back == true)
                                {
                                    bool fwd = forwardMove(board.Startcoord, board.Endcoord);
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
                                            Console.WriteLine("Enemy Marker present in destination\nYou must capture it");

                                            board.NewDest = checkEnemyMoveToCapture();

                                            // checks where in array postion newDest is
                                            // ***BUG*** Some reason if statement won't work. Else setion does though
                                            // fixed. Just had to change if/else statement to IF's statment
                                            // as new dest is greater than tiles[0] was returning failed move.
                                            // now only checks if d is at tiles.length - 1
                                            // And if array[d] contains contents of var newDest
                                            for (int d = 0; d < board.Tiles.Length; d++)
                                            {
                                                if (board.Tiles[d].Contains(board.NewDest) && !board.Tiles[d].Contains("X") && !board.Tiles[d].Contains("O"))
                                                {
                                                    // enemy marker location changes to destination name with "0" replaced with "  "
                                                    board.Tiles[x] = board.Destination + "  ";

                                                    // new destination of player marker
                                                    board.Tiles[d] = board.NewDest + " O";

                                                    // original poistion of marker has the "X" replaced with "  "
                                                    board.Tiles[i] = board.Choice + "  ";

                                                    Console.WriteLine("Marker moved");
                                                    board.Player--;
                                                    board.PlayerAMarkerCount--;
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
                                        else
                                        {
                                            board.NewDest = board.Destination + " O";
                                            board.Tiles[x] = board.NewDest;

                                            board.Tiles[i] = board.Choice + "  ";

                                            Console.WriteLine("Marker moved");
                                            board.Player--;
                                            Console.ReadLine();
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
                            Console.WriteLine("Marker destination is illegal or contains a marker belonging to the player");
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
                    Console.WriteLine("Marker destination is illegal or already has a player counter on it");
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
                if (board.PosD > board.PosC)
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
                if (board.PosD == board.PosC - 1)
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

            if (destination[1] == choice[1] + 1 || destination[1] == choice[1] - 1)
            {
                diagCheck = true;
            }
            return diagCheck;
        }
        #endregion
        #region capture enemy marker
        // when the letter of end coord is the same as positon of letter[i] -/+ 1
        // stores letter[i] in variable
        // when the number coord is the same as number[i] +/- 1
        // stores numer[i] in variables
        // string returns combined tostring of above variables
        public string checkEnemyMoveToCapture()
        {
            // String contents debug stuff
            string newDest = "broke\nGo";
            string newL = "break";
            string newN = "stuff";
            char coordL;
            char coordn;

            // checks if enemy marker is left diagonal fwd to start coord
            if (board.Endcoord[1] < board.Startcoord[1])
            {
                for (int i = 0; i < board.Letter.Length; i++)
                {
                    if (board.Endcoord[0] == board.Letter[i] + 1)
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
                for (int i = 0; i < board.Letter.Length; i++)
                {
                    if (board.Endcoord[0] == board.Letter[i] + 1)
                    {
                        coordL = board.Letter[i];
                        newL = coordL.ToString();
                    }
                    if (board.Destination[1] + 1 == board.Number[i])
                    {
                        coordn = board.Number[i];
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
    }
}
