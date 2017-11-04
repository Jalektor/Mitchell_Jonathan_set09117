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
        private List<string> playerPieces;

        public List<string> PlayerPieces { get { return playerPieces; } set { playerPieces = value; } }

        private string pieceStart;
        private string pieceEnd;
        private string pieceEndL;
        private string pieceEndR;
        private string destination;

        public string PieceStart { get {return pieceStart; } set {pieceStart = value; } }
        public string PieceEnd { get { return pieceEnd; } set { pieceEnd = value; } }
        public string PieceEndL { get {return pieceEndL; } set {pieceEndL = value; }}
        public string PieceEndR { get { return pieceEndR; } set { pieceEndR = value; } }
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
            PlayerPieces = new List<string>();
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
            bool capturedL = false;
            bool capturedR = false;
            Destination = string.Empty;
            NewDest = string.Empty;

            Console.WriteLine("Computer is moving one of it's pieces");
            Console.ReadLine();
            foreach (string piece in PlayerPieces)
            {
                PieceStart = piece.Split(new string[] { "  " }, StringSplitOptions.None)[0];
                board.Startcoord = PieceStart.ToCharArray();

                PieceEndL = GetLeft();
                PieceEndR = GetRight();
                Console.ReadLine();
                if (PieceEndL.Contains("X"))
                {
                    board.Destination = PieceEndL;
                    board.Endcoord = PieceEndL.ToCharArray();
                    capturedL = captureMarker(PieceEndL);
                    if(capturedL == true)
                    {
                        break;
                    }
                }
                if(capturedL == false && PieceEndR.Contains("X"))
                {
                    board.Destination = PieceEndR;
                    board.Endcoord = PieceEndR.ToCharArray();
                    capturedR = captureMarker(PieceEndR);
                    break;
                }
                if(!PieceEndL.Contains("O") && !PieceEndL.Contains("X"))
                {
                    Console.WriteLine("Computer Moving piece\n" +
                        "From {0}\n" +
                        "To: {1}", PieceStart, PieceEndL);
                    Console.ReadLine();
                    for (int i = 0; i < board.Tiles.Length; i++)
                    {
                        if(board.Tiles[i].Contains(PieceStart))
                        {
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart + "   ");
                        }
                        if(board.Tiles[i].Contains(PieceEndL))
                        {
                            PieceEndL = PieceEndL.Replace("   ", "  O");
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceEndL);
                        }
                    }
                    Console.WriteLine("Computer has moved a piece");
                    Console.ReadLine();

                    for (int i = 0; i < PlayerPieces.Count; i++)
                    {
                        if (PlayerPieces[i].Contains(PieceStart))
                        {
                            PlayerPieces.RemoveAt(i);
                            PlayerPieces.Add(PieceEndL);
                            board.player--;
                            break;
                        }
                    }
                    break;
                }
                if(!PieceEndR.Contains("O") && !PieceEndR.Contains("X"))
                {
                    Console.WriteLine("Computer Moving piece\n" +
                        "From {0}\n" +
                        "To: {1}", PieceStart, PieceEndR);
                    Console.ReadLine();
                    for (int i = 0; i < board.Tiles.Length; i++)
                    {
                        if (board.Tiles[i].Contains(PieceStart))
                        {
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart + "   ");
                        }
                        if (board.Tiles[i].Contains(PieceEndR))
                        {
                            PieceEndR = PieceEndR.Replace("   ", "  O");
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceEndR);
                        }
                    }
                    Console.WriteLine("Computer has moved a piece");
                    Console.ReadLine();

                    for (int i = 0; i < PlayerPieces.Count; i++)
                    {
                        if (PlayerPieces[i].Contains(PieceStart))
                        {
                            PlayerPieces.RemoveAt(i);
                            PlayerPieces.Add(PieceEndR);
                            board.player--;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        #endregion
        #region GetDestinationPosition
        public string GetDestCoord()
        {
            string Left = GetLeft(); ;
            string Right = GetRight();
            foreach (string piece in board.Tiles)
            {
                if (piece.Contains(Left))
                {
                    Destination = piece;
                    break;
                }
                else if (piece.Contains(Right))
                {
                    Destination = piece;
                    break;
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
            foreach(string piece in board.Tiles)
            {
                if (piece.Contains(Left))
                {
                    Left = piece;
                    break;
                }
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
            foreach (string piece in board.Tiles)
            {
                if (piece.Contains(Right))
                {
                    Right = piece;
                    break;
                }
            }
            return Right;
        }
        #endregion
        #region CheckPositionAfterOpponentMarker
        public string CheckNewDest()
        {
            string position = checkEnemyMoveToCapture();
            foreach(string coord in board.Tiles)
            {
                if(coord.Contains(position))
                {
                    position = coord;
                    break;
                }
                else
                {
                    position = string.Empty;
                }
            }
            return position;
        }
        #endregion
        #region CaptureEnemyPiece1
        public bool captureMarker(string FwdDiag)
        {
            bool captured = false;
            NewDest = checkEnemyMoveToCapture();
            Console.WriteLine("StartCoord: " + board.Startcoord[0].ToString() + board.Startcoord[1].ToString());
            Console.WriteLine("Endcoord: " + board.Endcoord[0].ToString() + board.Endcoord[1].ToString());
            Console.WriteLine("Newdest: " + NewDest);
            Console.ReadLine();
            foreach(string piece in board.Tiles)
            {
                if (piece.Contains(NewDest) && !piece.Contains("X") && !piece.Contains("O"))
                {
                    Console.WriteLine("Computer attempting to take player Piece");

                    for (int i = 0; i < board.Tiles.Length; i++)
                    {
                        if(board.Tiles[i].Contains(PieceStart))
                        {
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart + "   ");                            
                        }
                        if(board.Tiles[i].Contains(FwdDiag))
                        {
                            FwdDiag = FwdDiag.Replace("  X", "   ");
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], FwdDiag);
                        }
                        if (board.Tiles[i].Contains(NewDest))
                        {
                            NewDest = NewDest + "  O";
                            board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], NewDest);
                            captured = true;
                        }                      
                    }
                    if(captured == true)
                    {
                        Console.WriteLine("Piece taken");
                        Console.WriteLine("Computer captured player piece\n" +
                        "From {0}\n" +
                        "Moving To: {1}\n" +
                        "Capturing: {2}", PieceStart, NewDest, FwdDiag);
                        Console.ReadLine();
                        for (int i = 0; i < PlayerPieces.Count; i++)
                        {
                            if (PlayerPieces[i].Contains(PieceStart))
                            {
                                PlayerPieces.RemoveAt(i);
                                PlayerPieces.Add(NewDest);
                                board.PlayerAMarkerCount--;
                                board.player--;
                                Console.ReadLine();
                                break;
                            }
                        }
                        Console.ReadLine();
                        break;
                    }
                }
            }
            return captured;
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
        //#region AddToNewDest
        //public void AddToNewDest(string NewDest)
        //{
        //    foreach (string piece in board.Tiles)
        //    {
        //        if (piece.Contains(NewDest))
        //        {
        //            NewDest = piece;
        //        }
        //    }
        //}
        //#endregion
    }
}
