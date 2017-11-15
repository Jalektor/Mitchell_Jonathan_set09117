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

        private bool capturedL;
        private bool capturedR;

        public bool CapturedL { get { return capturedL; } set { capturedL = value; } }
        public bool CapturedR { get { return capturedR; } set { capturedR = value; } }
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
        #region MovePiece
        public override void Move(string Opponent)
        {
            Console.WriteLine("Computer is moving one of it's pieces");
            Console.ReadLine();
            foreach (string piece in PlayerPieces)
            {
                PieceStart = piece;
                if(PieceStart.Contains("K"))
                {
                    PieceStart.Replace("  O", "   ");
                    board.PlayerKing.Move(Opponent);
                    break;
                }
                Left = getPositionFWDLeft();
                Right = getPositionFWDRight();
                if (Left.Contains("X"))
                {
                    BeginNxtCaptureL();

                    board.DisplayData();
                    board.createBoard();

                    BeginNxtDetect();
                    if(Left.Contains("X"))
                    {
                        BeginNxtCaptureL();
                    }
                    if(CapturedL == false && Right.Contains("X"))
                    {
                        BeginNxtCaptureR();
                        break;
                    }
                    if(CapturedL == true)
                    {
                        break;
                    }
                }
                if(CapturedL == false && Right.Contains("X"))
                {
                    BeginNxtCaptureR();

                    board.DisplayData();
                    board.createBoard();
                    board.player--;

                    BeginNxtDetect();
                    if (Left.Contains("X"))
                    {
                        BeginNxtCaptureL();
                    }
                    if (CapturedL == false && Right.Contains("X"))
                    {
                        BeginNxtCaptureR();
                        break;
                    }
                }
                if(!Left.Contains("O") && !Left.Contains("X"))
                {
                    Destination = Left;
                    Console.WriteLine("Computer Moving piece\n" +
                        "From {0}\n" +
                        "To: {1}", PieceStart, Left);
                    if(Destination.Contains("A"))
                    {
                        for (int i = 0; i < board.Tiles.Length; i++)
                        {
                            if (board.Tiles[i].Contains(PieceStart))
                            {
                                PieceStart = PieceStart.Replace("  O", "   ");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart);
                            }
                            if (board.Tiles[i].Contains(Destination))
                            {
                                Destination = Destination.Replace("   ", " KO");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], Destination);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < board.Tiles.Length; i++)
                        {
                            if (board.Tiles[i].Contains(PieceStart))
                            {
                                PieceStart = PieceStart.Replace("  O", "   ");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart);
                            }
                            if (board.Tiles[i].Contains(Destination))
                            {
                                Destination = Destination.Replace("   ", "  O");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], Destination);
                            }
                        }
                    }                   
                    Console.WriteLine("Computer has moved a piece");
                    Console.ReadLine();
                    for (int i = 0; i < PlayerPieces.Count; i++)
                    {
                        if (PlayerPieces[i].Contains(PieceStart.Trim()))
                        {
                            PlayerPieces.RemoveAt(i);
                            PlayerPieces.Add(Left);
                            board.player--;
                            break;
                        }
                    }
                    break;
                }
                if(!Right.Contains("O") && !Right.Contains("X"))
                {
                    Destination = Right;
                    Console.WriteLine("Computer Moving piece\n" +
                        "From {0}\n" +
                        "To: {1}", PieceStart, Right);
                    if (Destination.Contains("A"))
                    {
                        for (int i = 0; i < board.Tiles.Length; i++)
                        {
                            if (board.Tiles[i].Contains(PieceStart))
                            {
                                PieceStart = PieceStart.Replace("  O", "   ");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart);
                            }
                            if (board.Tiles[i].Contains(Destination))
                            {
                                Destination = Destination.Replace("   ", " KO");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], Destination);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < board.Tiles.Length; i++)
                        {
                            if (board.Tiles[i].Contains(PieceStart))
                            {
                                PieceStart = PieceStart.Replace("  O", "   ");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart);
                            }
                            if (board.Tiles[i].Contains(Destination))
                            {
                                Destination = Destination.Replace("   ", "  O");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], Destination);
                            }
                        }
                    }                   
                    Console.WriteLine("Computer has moved a piece");
                    Console.ReadLine();

                    for (int i = 0; i < PlayerPieces.Count; i++)
                    {
                        if (PlayerPieces[i].Contains(PieceStart.Trim()))
                        {
                            PlayerPieces.RemoveAt(i);
                            PlayerPieces.Add(Right);
                            board.player--;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        #endregion
        #region PossibleLeftDest
        public override string getPositionFWDLeft()
        {
            string Left = "Fail Left";
            for (int i = 0; i < Letter.Length; i++)
            {
                if(PieceStart[0] == Letter[i] + 1)
                {
                    coordL = Letter[i];
                    newL = coordL.ToString();
                }
                if(PieceStart[1] - 1 == Number[i])
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
        public override string getPositionFWDRight()
        {
            string Right = "Fail Right";
            for (int i = 0; i < Letter.Length; i++)
            {
                if (PieceStart[0] == Letter[i] + 1)
                {
                    coordL = Letter[i];
                    newL = coordL.ToString();
                }
                if (PieceStart[1] + 1 == Number[i])
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
            string position = CheckOpponentPiece();
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
        #region CaptureEnemyPiece(s)
        public bool captureMarker(string FwdDiag)
        {
            bool captured = false;
            NewDest = CheckOpponentPiece();
            foreach(string piece in board.Tiles)
            {
                if (piece.Contains(NewDest) && !piece.Contains("X") && !piece.Contains("O"))
                {
                    Console.WriteLine("Computer attempting to take player Piece");

                    if(NewDest.Contains("A"))
                    {
                        for (int i = 0; i < board.Tiles.Length; i++)
                        {
                            if (board.Tiles[i].Contains(PieceStart))
                            {
                                PieceStart = PieceStart.Replace("  O", "   ");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart);
                            }
                            if (board.Tiles[i].Contains(FwdDiag))
                            {
                                FwdDiag = FwdDiag.Replace("  X", "   ");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], FwdDiag);
                            }
                            if (board.Tiles[i].Contains(NewDest))
                            {
                                NewDest = NewDest + " KO";
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], NewDest);
                                captured = true;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < board.Tiles.Length; i++)
                        {
                            if (board.Tiles[i].Contains(PieceStart))
                            {
                                PieceStart = PieceStart.Replace("  O", "   ");
                                board.Tiles[i] = board.Tiles[i].Replace(board.Tiles[i], PieceStart);
                            }
                            if (board.Tiles[i].Contains(FwdDiag))
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
                    }                   
                    if(captured == true)
                    {
                        Console.WriteLine("Piece taken");
                        Console.WriteLine("Computer captured player piece\n" +
                        "From {0}\n" +
                        "Moving To: {1}\n" +
                        "Capturing: {2}", PieceStart, NewDest, FwdDiag);
                        for (int i = 0; i < PlayerPieces.Count; i++)
                        {
                            if (PlayerPieces[i].Contains(PieceStart.Trim()))
                            {
                                PlayerPieces.RemoveAt(i);
                                PlayerPieces.Add(NewDest);
                                board.PlayerAMarkerCount--;
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
        #region CheckPositionAfterOpponentMarker
        public string CheckOpponentPiece()
        {
            // checks if enemy marker is left diagonal fwd to start coord
            if (Destination.ToCharArray()[1] < PieceStart[1])
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
            else if (Destination.ToCharArray()[1] > PieceStart[1])
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
        #region BeginNextOpponentMarkerDetect
        public void BeginNxtDetect()
        {
            PieceStart = NewDest;
            Left = getPositionFWDLeft();
            Right = getPositionFWDRight();
        }
        #endregion
        #region BeginNextCaptureLeftFwd
        public void BeginNxtCaptureL()
        {
            Destination = Left;
            CapturedL = captureMarker(Left);
        }
        #endregion
        #region BeginNextCaptureRightFwd
        public void BeginNxtCaptureR()
        {
            Destination = Right;
            CapturedR = captureMarker(Right);
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
    }
}
