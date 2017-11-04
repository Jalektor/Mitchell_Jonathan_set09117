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
        UndoRedo Undo = new UndoRedo();
        Error error = new Error();

        #region Constructor
        public PlayerA(Board draughts) : base(draughts)
        {
            board = draughts;
            Comp = board.computer;
        }
        #endregion
        public override void move(string Opponent)
        {
            TilesUndo = new string[board.Tiles.Length];
            //PlayerAKing king = new PlayerAKing(board);
            #region playerOneMove
            // checks array for chosen marker coord
            for (i = 0; i < board.Tiles.Length; i++)
            {
                if (board.Tiles[i].Contains(board.Choice) && board.Tiles[i].Contains("X"))
                {
                    if (board.Tiles[i].Contains("K"))
                    {
                        board.playerAKing.move(Opponent);
                        break;
                    }
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
                                error.NoSidewaysMove();
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
                                        #region enemyCapture
                                        // checks for enemy marker in destination tile
                                        // if present perform checkEnemyMoveToCapture function
                                        // returned result determines if move possible
                                        // if yes, marker moved to tiles diagonal from enemy marker, and capturing it
                                        // else capture aborted
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
                                                Left = getPositionFWDLeft();
                                                Right = getPositionFWDRight();
                                                for (y = 0; y < board.Tiles.Length; y++)
                                                {
                                                    if (board.Tiles[y].Contains(Left) && board.Tiles[y].Contains("O"))
                                                    {
                                                        board.Destination = Left;
                                                        board.Endcoord = board.Destination.ToCharArray();
                                                        captureMarker2(Opponent);                                            
                                                    }
                                                    if (board.Tiles[y].Contains(Right) && board.Tiles[y].Contains("O"))
                                                    {
                                                        board.Destination = Right;
                                                        board.Endcoord = board.Destination.ToCharArray();
                                                        captureMarker2(Opponent);                                                       
                                                    }
                                                }
                                                Console.ReadLine();
                                            }                                                                                     
                                        }
                                        #endregion
                                        else
                                        {
                                            Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                            Undo.undo.Push(TilesUndo);
                                            if(board.Destination.Contains("H"))
                                            {
                                                board.Tiles[x] = board.Destination + " KX";
                                                board.Tiles[i] = board.Choice + "   ";
                                            }
                                            else
                                            {
                                                board.Tiles[x] = board.Destination + "  X";
                                                board.Tiles[i] = board.Choice + "   ";
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

        #region backwards move check
        // prevents backwards movement
        // checks element position of char[] choice  with same position of char[] letter 
        // when they match, int posC stores this value
        // same with char[] destination
        // stores same element in int posD
        // if posD is less that posC. This means the destination coord is going backwards
        // bool back is false
        // else coord is going forward. bool back is true
        public virtual bool checkBackMove(char[] choice, char[] destination)
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
        public bool forwardMove(char[] choice, char[] destination)
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
                for (int i = 0; i < Letter.Length; i++)
                {
                    if (board.Endcoord[0] == Letter[i] - 1)
                    {
                        coordL = Letter[i];
                        newL = coordL.ToString();
                    }
                    if (board.Destination[1] - 1 == Number[i])
                    {
                        coordn = Number[i];
                        newN = coordn.ToString();

                    }
                    NewDest = newL + newN.Trim().ToUpper();
                }
            }
            // checks if enemy is right diagonal fwd to start coord
            else if (board.Endcoord[1] > board.Startcoord[1])
            {
                for (int c = 0; c < Letter.Length; c++)
                {
                    if (board.Endcoord[0] == Letter[c] - 1)
                    {
                        coordL = Letter[c];
                        newL = coordL.ToString();
                    }
                    if (board.Destination[1] + 1 == Number[c])
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
        #region captureEnemyMarker1
        public virtual void captureMarker(string Opponent)
        {
            Console.WriteLine("Enemy Marker present in destination\nYou must capture it");

            NewDest = checkEnemyMoveToCapture();

            // checks where in array postion newDest is
            // and changes the string contents based on what element in tiles is being amended
            for (d = 0; d < board.Tiles.Length; d++)
            {
                if (board.Tiles[d].Contains(NewDest) && !board.Tiles[d].Contains("X") && !board.Tiles[d].Contains("O"))
                {
                    if(NewDest.Contains("H"))
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
                    }
                    else
                    {
                        // enemy marker location changes to destination name with "0" replaced with "  "
                        board.Tiles[x] = board.Destination + "   ";

                        // new destination of player marker
                        board.Tiles[d] = NewDest + "  X";

                        // original poistion of marker has the "X" replaced with "  "
                        board.Tiles[i] = board.Choice + "   ";

                        if (Opponent == "C")
                        {
                            string PieceToRemove = board.Destination;
                            Comp.RemoveTakenPiece(PieceToRemove);
                        }
                    }       
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
        #region CheckSecondEnemyMarkerLeft/Right
        // returns left/right positions of fwd diag coords after one enemy piece has been taken
        public string getPositionFWDLeft()
        {
            int x;
            for (x = 0; x < Letter.Length; x++)
            {
                if(board.Startcoord[0] == Letter[x] - 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if(board.Startcoord[1] - 1 == Number[x])
                {
                    coordn = Number[x];
                    newN = coordn.ToString();
                }
                NewDest = newL + newN.Trim().ToUpper();
            }
            return NewDest;
        }
        public string getPositionFWDRight()
        {
            int x;
            for (x = 0; x < Letter.Length; x++)
            {
                if (board.Startcoord[0] == Letter[x] - 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if (board.Startcoord[1] + 1 == Number[x])
                {
                    coordn = Number[x];
                    newN = coordn.ToString();
                }
                NewDest = newL + newN.Trim().ToUpper();
            }
            return NewDest;
        }
        #endregion
        #region captureEnemyMarker2
        // checks where in array postion newDest is
        // and changes the string contents based on what element in tiles is being amended
        // This only f there is a second marker present
        public virtual void captureMarker2(string Opponent)
        {
            Skynet Comp = board.computer;
            Console.WriteLine("Enemy piece present in next space\nAttempting capture");

            NewDest = checkEnemyMoveToCapture();

            for (z = 0; z < board.Tiles.Length; z++)
            {
                if (board.Tiles[z].Contains(NewDest) && !board.Tiles[z].Contains("X") && !board.Tiles[z].Contains("O"))
                {
                    if(NewDest.Contains("H"))
                    {
                        // enemy marker location changes to destination name with "0" replaced with "  "
                        board.Tiles[y] = board.Destination + "   ";

                        // new destination of player marker
                        board.Tiles[z] = NewDest + " KX";

                        // original poistion of marker has the "X" replaced with "  "
                        board.Tiles[d] = board.Choice + "   ";
                        if (Opponent == "C")
                        {
                            string PieceToRemove = board.Destination;
                            Comp.RemoveTakenPiece(PieceToRemove);
                        }
                    }
                    else
                    {
                        // enemy marker location changes to destination name with "0" replaced with "  "
                        board.Tiles[y] = board.Destination + "   ";

                        // new destination of player marker
                        board.Tiles[z] = NewDest + "  X";

                        // original poistion of marker has the "X" replaced with "  "
                        board.Tiles[d] = board.Choice + "   ";
                        if (Opponent == "C")
                        {
                            string PieceToRemove = board.Destination;
                            Comp.RemoveTakenPiece(PieceToRemove);
                        }
                    }
                    
                    Console.WriteLine("Marker moved");
                    Console.ReadLine();
                    board.PlayerBMarkerCount--;
                    break;
                }
                if (d == board.Tiles.Length - 1)
                {
                    error.NoCapture();
                }
            }
        }
        #endregion
    }
}


                       
