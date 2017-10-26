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

    public class Skynet : Movement
    {
        Board board;
        List<string> PlayerPieces = new List<string>();

        private string pieceStart;
        private string pieceEnd;

        public string PieceStart { get {return pieceStart; } set {pieceStart = value; } }
        public string PieceEnd { get {return pieceEnd; } set {pieceEnd = value; }}

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
            PlayerPieces.ForEach(Console.WriteLine);
            Console.WriteLine("Computer has stored all starting positions for its markers");
            Console.ReadLine();
        }
        #endregion

        #region MoveASinglePiece
        public void MovePiece()
        {
            Console.WriteLine("Computer is moving one of it's pieces");
            Console.ReadLine();
            foreach (string piece in PlayerPieces)
            {
                PieceStart = piece.Split(new string[] { "  "}, StringSplitOptions.None)[0];
                board.Choice = PieceStart;
                board.Startcoord = board.Choice.ToCharArray();
                Console.WriteLine("Start" + PieceStart);
                Console.ReadLine();

                PieceEnd = GetDestCoord();
                board.Destination = PieceEnd.Trim();
                Console.WriteLine("Dest" + PieceEnd);
                Console.ReadLine();
                if(!board.Destination.Contains("blank"))
                {
                    for(int i = 0; i < board.Tiles.Length; i++)
                    {
                        board.Tiles[i] = board.Tiles[i].Replace(piece, PieceStart + "   ");
                    }
                    for (int i = 0; i < board.Tiles.Length; i++)
                    {
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
                        }
                    }
                    PlayerPieces.ForEach(Console.WriteLine);
                    Console.ReadLine();
                    board.DisplayData();
                    board.createBoard();
                    Console.ReadLine();

                    board.player--;
                    break;
                }
            }
        }
        #endregion

        #region GetDestinationPosition
        public string GetDestCoord()
        {
            string destination = "Fail";
            string Left = GetLeft(); ;
            string Right = GetRight();
            Console.WriteLine("Left" + Left + "Right" + Right);
            Console.ReadLine();
            foreach(string piece in board.Tiles)
            {
                if(piece.Contains(Left))
                {
                    PieceEnd = piece;
                    Console.WriteLine("Left Is:" + PieceEnd);
                    Console.ReadLine();
                    if(!PieceEnd.Contains("X") && !PieceEnd.Contains("O"))
                    {
                        destination = piece;
                    break;
                    }                   
                }
                if(piece.Contains(Right))
                {
                    PieceEnd = piece;
                    Console.WriteLine("Right Is:" + PieceEnd);
                    Console.ReadLine();
                    if (!PieceEnd.Contains("X") && !PieceEnd.Contains("O"))
                    {
                        destination = piece;
                        break;
                    }
                }
            }
            return destination;
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
            Console.WriteLine("Got Left" + Left);
            Console.ReadLine();
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
            Console.WriteLine("Got Right" + Right);
            Console.ReadLine();
            return Right;
        }
        #endregion
    }
}
