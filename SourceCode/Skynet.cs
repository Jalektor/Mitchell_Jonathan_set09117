using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    // This AI will be the ruler of ALL draughts AI!!!
    // MUWAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHA!!!!
    // Skynet will rule all
    // Hasta La Vista

    public class Skynet : PlayerB
    {
        #region Variables/Objects
        Board board;
        private List<string> playerPieces = new List<string>();

        public List<string> PlayerPieces { get { return playerPieces; } set { playerPieces = value; } }

        private string pieceStart;
        private string pieceEnd;
        private string destination;

        public string PieceStart { get {return pieceStart; } set {pieceStart = value; } }
        public string PieceEnd { get {return pieceEnd; } set {pieceEnd = value; }}
        public string Destination { get { return destination; } set { destination = value; } }
        #endregion
        #region Constructor
        public Skynet(Board draughts) : base(draughts)
        {
            board = draughts;
        }
        #endregion
        #region StoreCompPlayerPieces
        public void StorePieces()
        {
            Console.WriteLine("Computer is storing pieces");
            Console.ReadLine();
            for(int i = 0; i < board.Tiles.Length; i++)
            {
                if (board.Tiles[i].Contains("O"))
                {
                    PlayerPieces.Add(board.Tiles[i]);
                }
            }
            Console.WriteLine("Computer has stored all starting positions for its markers\n");
            Console.ReadLine();
        }
        #endregion
        #region MoveASinglePiece
        public void MovePiece()
        {
            playerPieces.ForEach(Console.WriteLine);
            Destination = string.Empty;
            NewDest = string.Empty;

            Console.WriteLine("Computer is moving one of it's pieces");
            Console.ReadLine();
            foreach (string piece in PlayerPieces)
            {
                PieceStart = piece.Split(new string[] { "  "}, StringSplitOptions.None)[0];
                board.Choice = PieceStart;
                board.Startcoord = board.Choice.ToCharArray();

                PieceEnd = GetDestCoord();
                board.Destination = PieceEnd.Trim();
                if(NewDest.Length != 0)
                {
                    Console.WriteLine("Computer captured player piece\n" +
                        "From {0}\n" +
                        "To: {1}\n" +
                        "Capturing: {2}", PieceStart, NewDest, PieceEnd);
                    Console.ReadLine();
                    for (int i = 0; i < PlayerPieces.Count; i++)
                    {
                        if(PlayerPieces[i].Contains(PieceStart))
                        {
                            PlayerPieces.RemoveAt(i);
                            PlayerPieces.Add(NewDest);
                            board.PlayerAMarkerCount--;
                            board.player--;
                            break;
                        }
                    }
                    break;                   
                }
                else if(!PieceEnd.Contains("O") && !PieceEnd.Contains("X"))
                {
                    Console.WriteLine("Computer Moving piece\n" +
                        "From {0}\n" +
                        "To: {1}", PieceStart, PieceEnd);
                    Console.ReadLine();
                    for(int i = 0; i < board.Tiles.Length; i++)
                    {
                        board.Tiles[i] = board.Tiles[i].Replace(piece, PieceStart + "   ");
                        board.Tiles[i] = board.Tiles[i].Replace(PieceEnd, board.Destination + "  O");
                    }
                        
                    Console.WriteLine("Computer has moved a piece");
                    Console.ReadLine();

                    for(int i = 0; i < PlayerPieces.Count; i++)
                    {
                        if(PlayerPieces[i].Contains(PieceStart))
                        {
                            PlayerPieces.RemoveAt(i);
                            PlayerPieces.Add(PieceEnd);
                            board.player--;
                            break;
                        }
                    }
                    break;
                }
            }
            Console.ReadLine();
        }
        #endregion
        #region GetDestinationPosition
        public string GetDestCoord()
        {
            string Left = GetLeft(); ;
            string Right = GetRight();
            foreach(string piece in board.Tiles)
            {
                if(piece.Contains(Left))
                {
                    // New issue Here
                    // Need to get NEW left/right for place AFTER opponent marker
                    // And check that position
                    // Similar to playerA/B second marker take
                    PieceEnd = piece;
                    if(!PieceEnd.Contains("X") && !PieceEnd.Contains("O"))
                    {
                        Destination = piece;
                        break;
                    }        
                    if(PieceEnd.Contains("X"))
                    {
                        Destination = piece;
                        board.Destination = Destination;
                        board.Endcoord = piece.ToCharArray();
                        NewDest = checkEnemyMoveToCapture();
                        if(!NewDest.Contains("coord"))
                        {
                            AddToNewDest(NewDest);
                            if (!NewDest.Contains("X") && !NewDest.Contains("O"))
                            {
                                captureMarker();
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if(piece.Contains(Right))
                {
                    PieceEnd = piece;
                    if (!PieceEnd.Contains("X") && !PieceEnd.Contains("O"))
                    {
                        Destination = piece;
                        break;
                    }
                    if (PieceEnd.Contains("X"))
                    {
                        Destination = PieceEnd;
                        board.Destination = Destination;
                        board.Endcoord = piece.ToCharArray();
                        NewDest = checkEnemyMoveToCapture();
                        if (!NewDest.Contains("coord"))
                        {
                            AddToNewDest(NewDest);
                            if (!NewDest.Contains("X") && !NewDest.Contains("O"))
                            {
                                captureMarker();
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return Destination;
        }
        #endregion
        #region PossibleLeftDest
        public string GetLeft()
        {
            string Left = "Fail Left";
            for (int i = 0; i < Letter.Length; i++)
            {
                if(board.Startcoord[0] == Letter[i] + 1)
                {
                    coordL = Letter[i];
                    newL = coordL.ToString();
                }
                if(board.Startcoord[1] - 1 == Number[i])
                {
                    coordn = Number[i];
                    newN = coordn.ToString();
                }
                Left = newL + newN.Trim().ToUpper();                  
            }
            return Left;        
        }
        #endregion
        #region PossibleRightDest
        public string GetRight()
        {
            string Right = "Fail Right";
            for (int i = 0; i < Letter.Length; i++)
            {
                if (board.Startcoord[0] == Letter[i] + 1)
                {
                    coordL = Letter[i];
                    newL = coordL.ToString();
                }
                if (board.Startcoord[1] + 1 == Number[i])
                {
                    coordn = Number[i];
                    newN = coordn.ToString();
                }
                Right = newL + newN.Trim().ToUpper();
            }
            return Right;
        }
        #endregion
        #region CaptureEnemyPiece1
        public override void captureMarker()
        {
            Console.WriteLine("Computer attempting to take player Piece");
            Console.ReadLine();
            foreach (string piece in board.Tiles)
            {
                if(piece.Contains(NewDest) && !piece.Contains("X") && !piece.Contains("O"))
                {
                    for(int i = 0; i < board.Tiles.Length; i++)
                    {
                        if(board.Tiles[i].Contains(PieceStart))
                        {
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart + "   ");
                        }
                        if(board.Tiles[i].Contains(PieceEnd))
                        {
                            Destination = Destination.Replace("  X", "");
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], Destination + "   ");
                        }
                        if(board.Tiles[i].Contains(NewDest))
                        {
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], NewDest + "  O");
                        }
                    }
                    Console.ReadLine();
                    break;
                }
            }
        }
        #endregion
        #region RemovePiece
        public void RemoveTakenPiece(string PieceToRemove)
        {
            PieceStart = PieceToRemove;
            Console.ReadLine();
            for (int i = 0; i < PlayerPieces.Count; i++)
            {
                if (PlayerPieces[i].Contains(PieceStart))
                {
                    PlayerPieces.RemoveAt(i);
                    break;
                }
            }
        }
        #endregion
        #region AddToNewDest
        public void AddToNewDest(string NewDest)
        {
            foreach(string piece in board.Tiles)
            {
                if(piece.Contains(NewDest))
                {
                    NewDest = piece;
                }
            }
        }
        #endregion
    }
}
