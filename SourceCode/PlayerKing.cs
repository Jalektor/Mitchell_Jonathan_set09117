using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class PlayerKing : Movement
    {
        Board board;
        Undo Undo = new Undo();
        Error error = new Error();
        Skynet Comp;
        public string Opponent { get; set; }
        public string FwdLeft { get; set; }
        public string FwdRight { get; set; }      
        public string BckLeft { get; set; }
        public string BckRight { get; set; }

        private bool fwd = false;
        private bool bck = false;

        public string PlayerPiece { get; set; }
        public List<string> PlayerPieces { get; set; }
        public int PiecesTaken { get; set; }
#region Constructor
        public PlayerKing(Board draughts) : base(draughts)
        {
            board = draughts;
            TilesUndo = new string[board.Tiles.Length];
            Comp = board.Computer;
        }
        #endregion
#region Movement      
        public void KingMove(string playerPiece)
        {
            PlayerPiece = playerPiece;
#region PlayerAKing
            if(playerPiece == "X")
            {
                PieceStart = board.Player1.PieceStart;
                Destination = board.Player1.Destination;
                for(int i = 0; i < board.Tiles.Length; i++)
                {
                    if (board.Tiles[i].Contains(Destination) && !board.Tiles[i].Contains("X"))
                    {
                        if(Destination[0] == PieceStart[0])
                        {
                            error.NoSidewaysMove();
                            break;
                        }
                        // Allows backwards movement
                        // but only IF the coord are diagonal to starting coords AND only 1 row behind
                        else
                        {
                            fwd = ForwardMoveA(PieceStart, Destination);
                            bck = ForwardMoveB(PieceStart, Destination);
                            Console.WriteLine();
                            if (bck == true || fwd == true)
                            {
                                if(board.Tiles[i].Contains("O"))
                                {
                                    board.Player++;
                                    Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                    Undo.undo.Push(TilesUndo);

                                    CaptureMarkerA();

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
                                        // can capture upto 6 other pieces
                                        for (int a = 0; a < 6; a++)
                                        {
                                            board.DisplayData();
                                            board.createBoard();
                                            Console.WriteLine("Do you want to capture another piece? Y/N");
                                            Ans = Console.ReadLine().ToUpper();
                                            if (Ans == "Y")
                                            {
                                                Captured = false;
                                                PieceStart = NewDest;
                                                FwdLeft = getPositionFWDLeft();
                                                FwdRight = getPositionFWDRight();
                                                BckLeft = getPositionFWDLeftB();
                                                BckRight = getPositionFWDRightB();
                                                foreach (string piece in board.Tiles)
                                                {
                                                    if (piece.Contains(FwdLeft) && piece.Contains("O"))
                                                    {
                                                        Destination = FwdLeft;
                                                        fwd = true;
                                                        CaptureMarkerA();
                                                    }
                                                    if (piece.Contains(FwdRight) && piece.Contains("O"))
                                                    {
                                                        Destination = FwdRight;
                                                        fwd = true;
                                                        CaptureMarkerA();
                                                    }
                                                    if (piece.Contains(BckLeft) && piece.Contains("O"))
                                                    {
                                                        Destination = BckLeft;
                                                        bck = true;
                                                        CaptureMarkerA();
                                                    }
                                                    if (piece.Contains(BckRight) && piece.Contains("O"))
                                                    {
                                                        Destination = BckRight;
                                                        bck = true;
                                                        CaptureMarkerA();
                                                    }
                                                    if (Captured == true)
                                                    {
                                                        break;
                                                    }
                                                }
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }                                                                           
                                    }
                                }
                                else
                                {
                                    Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                    Undo.undo.Push(TilesUndo);

                                    for(int x = 0; x < board.Tiles.Length; x++)
                                    {
                                        if(board.Tiles[x].Contains(PieceStart))
                                        {
                                            board.Tiles[x] = PieceStart + "   ";
                                        }
                                        if (board.Tiles[x].Contains(Destination))
                                        {
                                            board.Tiles[x] = Destination + " KX";
                                        }                                      
                                    }
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
                        break;
                    }
                    if (i == board.Tiles.Length - 1)
                    {
                        error.WrongDestCoord();
                        break;
                    }
                }
            }
#endregion
#region PlayerB
            if (PlayerPiece == "O")
            {
                PieceStart = board.Player2.PieceStart;
                Destination = board.Player2.Destination;                
                for (int i = 0; i < board.Tiles.Length; i++)
                {
                    #region DestinationCheck
                    if (board.Tiles[i].Contains(Destination) && !board.Tiles[i].Contains("O"))
                    {
                        #region SidewayCheck
                        if (Destination[0] == PieceStart[0])
                        {
                            error.NoSidewaysMove();
                            break;
                        }
                        #region BackwardsMoveCheck
                        // Allows backwards movement
                        // but only IF the coord are diagonal to starting coords AND only 1 row behind
                        else
                        {
                            fwd = ForwardMoveB(PieceStart, Destination);
                            bck = ForwardMoveA(PieceStart, Destination);

                            if (bck == true || fwd == true)
                            {
                                #region OpponentPiece Capture
                                if (board.Tiles[i].Contains("X"))
                                {
                                    board.Player--;
                                    Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                    Undo.undo.Push(TilesUndo);
                                    CaptureMarkerB();

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
                                        // can capture upto 6 other pieces
                                        for(int a = 0; a < 6; a++)
                                        {
                                            board.DisplayData();
                                            board.createBoard();
                                            Console.WriteLine("Do you want to capture another piece? Y/N");
                                            Ans = Console.ReadLine().ToUpper();
                                            if (Ans == "Y")
                                            {
                                                Captured = false;
                                                PieceStart = NewDest;
                                                FwdLeft = getPositionFWDLeftB();
                                                FwdRight = getPositionFWDRightB();
                                                BckLeft = getPositionFWDLeft();
                                                BckRight = getPositionFWDRight();
                                                foreach(string piece in board.Tiles)
                                                {
                                                    if (piece.Contains(FwdLeft) && piece.Contains("X"))
                                                    {
                                                        Destination = FwdLeft;
                                                        fwd = true;
                                                        CaptureMarkerB();
                                                    }
                                                    if (piece.Contains(FwdRight) && piece.Contains("X"))
                                                    {
                                                        Destination = FwdRight;
                                                        fwd = true;
                                                        CaptureMarkerB();
                                                    }
                                                    if (piece.Contains(BckLeft) && piece.Contains("X"))
                                                    {
                                                        Destination = BckLeft;
                                                        bck = true;
                                                        CaptureMarkerB();
                                                    }
                                                    if (piece.Contains(BckRight) && piece.Contains("X"))
                                                    {
                                                        Destination = BckRight;
                                                        bck = true;
                                                        CaptureMarkerB();
                                                    }
                                                    if (Captured == true)
                                                    {
                                                        break;
                                                    }
                                                }
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }                                       
                                    }
                                }
                                #endregion
                                #region MovePlayerPiece
                                else
                                {
                                    Array.Copy(board.Tiles, TilesUndo, board.Tiles.Length);
                                    Undo.undo.Push(TilesUndo);

                                    for (int x = 0; x < board.Tiles.Length; x++)
                                    {
                                        if (board.Tiles[x].Contains(PieceStart))
                                        {
                                            board.Tiles[x] = PieceStart + "   ";
                                        }
                                        if (board.Tiles[x].Contains(Destination))
                                        {
                                            board.Tiles[x] = Destination + " KO";
                                        }
                                    }
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
                                #endregion
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
                    if (i == board.Tiles.Length - 1)
                    {
                        error.WrongDestCoord();
                        break;
                    }
                    #endregion
                }
            }
#endregion
#region CompMove
            if (PlayerPiece == "C")
            {
                board.player--;
                Console.WriteLine("Pieces taken: {0}", PiecesTaken);
                PieceStart = board.Computer.PieceStart;
                PieceStart = PieceStart.Remove(2);
                FwdLeft = getPositionFWDLeftB();
                FwdRight = getPositionFWDRightB();
                BckLeft = getPositionFWDLeft();
                BckRight = getPositionFWDRight();
                foreach ( string piece in board.Tiles)
                {
                    if (piece.Contains(FwdLeft) && !Captured == true)
                    {
                        Destination = FwdLeft;
                        fwd = true;
                        bck = false;
                        CompKingMove();
                        if(Captured == true)
                        {
                            break;
                        }
                    }
                    if (piece.Contains(FwdRight) && !Captured == true)
                    {
                        Destination = FwdRight;
                        fwd = true;
                        bck = false;
                        CompKingMove();
                        if (Captured == true)
                        {
                            break;
                        }
                    }
                    if (piece.Contains(BckLeft) && !Captured == true)
                    {
                        Destination = BckLeft;
                        bck = true;
                        fwd = false;
                        CompKingMove();
                        if (Captured == true)
                        {
                            break;
                        }
                    }
                    if (piece.Contains(BckRight) && !Captured == true)
                    {
                        Destination = BckRight;
                        bck = true;
                        fwd = false;
                        CompKingMove();
                        if (Captured == true)
                        {
                            break;
                        }
                    }
                }
            }
#endregion
            
        }
#endregion
#region PlayerAFwdMove
        // forward movement check if PlayerAKing piece
        // Backward movement check if PLayerBKing/Computer piece.
        public bool ForwardMoveA(string choice, string destination)
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
                    diag = FwdADiagCheck(choice, destination);
                    break;
                }
            }
            if (diag == true)
            {
                fwd = true;
            }

            return fwd;
        }
        public bool FwdADiagCheck(string choice, string destination)
        {
            bool diagCheck = false;

            if (destination[1] == choice[1] - 1 || destination[1] == choice[1] + 1)
            {
                diagCheck = true;
            }
            return diagCheck;
        }
        #endregion
#region PlayerBFwdMove
        // Forward movement check if PlayerBKing/Computer piece
        // Backward movement check if PLayerAKing piece.
        public bool ForwardMoveB(string choice, string destination)
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
                if (PosD == PosC - 1)
                {
                    diag = FwdBDiagCheck(choice, destination);
                    break;
                }
            }
            if (diag == true)
            {
                fwd = true;
            }

            return fwd;
        }
        public bool FwdBDiagCheck(string choice, string destination)
        {
            bool diagCheck = false;

            if (destination[1] == choice[1] + 1 || destination[1] == choice[1] - 1)
            {
                diagCheck = true;
            }
            return diagCheck;
        }
#endregion
#region CaptureMarkerA
        public void CaptureMarkerA()
        {
            bool choice = false;
            bool dest = false;
            bool nDest = false;
            Captured = false;
            Console.WriteLine("Enemy Marker present in destination\nAttempting capture");
            if(fwd == true)
            {
                NewDest = CheckOpponentPieceA();
            }
            if(bck == true)
            {
                NewDest = CheckOpponentPieceB();
            }
            // checks where in array postion newDest is
            // and changes the string contents based on what element in tiles is being amended
            foreach(string piece in board.Tiles)
            {
                if (piece.Contains(NewDest) && !piece.Contains("X") && !piece.Contains("O"))
                {
                    for (int i = 0; i < board.Tiles.Length; i++)
                    {
                        if (board.Tiles[i].Contains(PieceStart))
                        {
                            board.Tiles[i] = PieceStart + "   ";
                            choice = true;
                        }
                        if (board.Tiles[i].Contains(Destination))
                        {
                            board.Tiles[i] = Destination + "   ";
                            dest = true;
                        }
                        if (board.Tiles[i].Contains(NewDest))
                        {
                            board.Tiles[i] = NewDest + " KX";
                            nDest = true;
                        }
                        if (Opponent == "C")
                        {
                            Comp.RemoveTakenPiece(Destination);
                        }
                        if(choice == true && dest == true && nDest == true)
                        {
                            Captured = true;
                        }
                        if(Captured == true)
                        {
                            break;
                        }
                    }
                    if(Captured == false)
                    {
                        error.NoCapture();
                    }
                    else
                    {
                        Console.WriteLine("Piece captured");
                        board.PlayerBMarkerCount--;
                        Console.ReadLine();
                    }
                    break;
                }
            }
            if (Opponent == "C")
            {
                Comp.RemoveTakenPiece(Destination);
            }
        }
        #endregion
#region CaptureMarkerB
        public void CaptureMarkerB()
        {
            PiecesTaken++;
            bool choice = false;
            bool dest = false;
            bool nDest = false;
            bool captured = false;
            if (fwd == true)
            {
                NewDest = CheckOpponentPieceB();
            }
            if (bck == true)
            {
                NewDest = CheckOpponentPieceA();
            }

            // checks where in array postion newDest is
            // and changes the string contents based on what element in tiles is being amended
            foreach (string piece in board.Tiles)
            {
                if (piece.Contains(NewDest) && !piece.Contains("X") && !piece.Contains("O"))
                {
                    for (int i = 0; i < board.Tiles.Length; i++)
                    {
                        if (board.Tiles[i].Contains(PieceStart))
                        {
                            board.Tiles[i] = PieceStart + "   ";
                            choice = true;
                        }
                        if (board.Tiles[i].Contains(Destination))
                        {
                            board.Tiles[i] = Destination + "   ";
                            dest = true;
                        }
                        if (board.Tiles[i].Contains(NewDest))
                        {
                            board.Tiles[i] = NewDest + " KO";
                            nDest = true;
                        }
                    }
                    if (Opponent == "C")
                    {
                        Comp.RemoveTakenPiece(Destination);
                    }
                    if (choice == true && dest == true && nDest == true)
                    {
                        captured = true;
                    }                  
                    if (captured == false)
                    {
                        error.NoCapture();
                    }
                    else
                    {
                        Console.WriteLine("Enemy Marker present in destination\nAttempting capture");
                        Console.ReadLine();
                        Captured = captured;
                        Console.WriteLine("Piece captured");
                        board.PlayerAMarkerCount--;
                        Console.ReadLine();
                    }
                    break;
                }
            }
        }
        #endregion
#region OpponentMarkerCheck1
        public string CheckOpponentPieceA()
        {
            // checks if enemy marker is left diagonal fwd to start coord
            if (Destination[1] < PieceStart[1])
            {
                for (int i = 0; i < Letter.Length; i++)
                {
                    if (Destination[0] == Letter[i] - 1)
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
            else if (Destination[1] > PieceStart[1])
            {
                for (int c = 0; c < Letter.Length; c++)
                {
                    if (Destination[0] == Letter[c] - 1)
                    {
                        coordL = Letter[c];
                        newL = coordL.ToString();
                    }
                    if (Destination[1] + 1 == Number[c])
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
#region OpponentMarkerCheck2
        public string CheckOpponentPieceB()
        {
            // checks if enemy marker is left diagonal fwd to start coord
            if (Destination[1] < PieceStart[1])
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
            else if (Destination[1] > PieceStart[1])
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
                        Console.ReadLine();

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
#region PlayerANewDest2
        // returns left/right positions of fwd diag coords after one enemy piece has been taken
        // PlayerAKing FWD move
        // PlayerB/CompKing Bck Move
        public override string getPositionFWDLeft()
        {
            int x;
            for (x = 0; x < Letter.Length; x++)
            {
                if (PieceStart[0] == Letter[x] - 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if (PieceStart[1] - 1 == Number[x])
                {
                    coordn = Number[x];
                    newN = coordn.ToString();
                }
                NewDest = newL + newN.Trim().ToUpper();
            }
            return NewDest;
        }
        public override string getPositionFWDRight()
        {
            int x;
            for (x = 0; x < Letter.Length; x++)
            {
                if (PieceStart[0] == Letter[x] - 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if (PieceStart[1] + 1 == Number[x])
                {
                    coordn = Number[x];
                    newN = coordn.ToString();
                }
                NewDest = newL + newN.Trim().ToUpper();
            }
            return NewDest;
        }
#endregion
#region PlayerBNewDest2
        // returns left/right positions of fwd diag coords after one enemy piece has been taken
        public string getPositionFWDLeftB()
        {
            int x;
            for (x = 0; x < Letter.Length; x++)
            {
                if (PieceStart[0] == Letter[x] + 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if (PieceStart[1] - 1 == Number[x])
                {
                    coordn = Number[x];
                    newN = coordn.ToString();
                }
                NewDest = newL + newN.Trim().ToUpper();
            }
            return NewDest;
        }
        public string getPositionFWDRightB()
        {
            int x;
            for (x = 0; x < Letter.Length; x++)
            {
                if (PieceStart[0] == Letter[x] + 1)
                {
                    coordL = Letter[x];
                    newL = coordL.ToString();
                }
                if (PieceStart[1] + 1 == Number[x])
                {
                    coordn = Number[x];
                    newN = coordn.ToString();
                }
                NewDest = newL + newN.Trim().ToUpper();
            }
            return NewDest;
        }
        #endregion
#region CheckMovePlayer
        public override void Move(string opponent)
        {
            if(opponent == "O")
            {
                Opponent = opponent;
                PlayerPiece = "X";
                KingMove(PlayerPiece);
            }
            if(opponent == "C")
            {
                Opponent = opponent;
                PlayerPiece = "X";
                KingMove(PlayerPiece);
            }
            if (opponent == "X-C")
            {
                Opponent = opponent;
                PlayerPiece = "C";
                KingMove(PlayerPiece);
            }
            if (opponent == "X")
            {
                Opponent = opponent;
                PlayerPiece = "O";
                KingMove(PlayerPiece);
            }
        }
        #endregion
#region CompMove/capture
        public void CompKingMove()
        {
            foreach(string piece in board.Tiles)
            {
                if(piece.Contains(Destination) && !piece.Contains("X") && !piece.Contains("O") && PiecesTaken == 0)
                {
                    for (int x = 0; x < board.Tiles.Length; x++)
                    {
                        if (board.Tiles[x].Contains(PieceStart))
                        {
                            PieceStart = PieceStart.Replace(" KO", "   ");
                            board.Tiles[x] = PieceStart;
                        }
                        if (board.Tiles[x].Contains(Destination))
                        {
                            board.Tiles[x] = Destination + " KO";
                        }
                    }
                    MovedInfo();
                    Console.ReadLine();
                    break;
                }
            }
            foreach (string piece in board.Tiles)
            {               
                if (piece.Contains(Destination) && piece.Contains("X"))
                {
                    CaptureMarkerB();
                    if(Captured == true)
                    {
                        CapturedInfo();
                        for (int x = 0; x < 5; x++)
                        {
                            board.DisplayData();
                            board.createBoard();
                            PieceStart = NewDest;
                            Captured = false;
                            FwdLeft = getPositionFWDLeftB();
                            FwdRight = getPositionFWDRightB();
                            BckLeft = getPositionFWDLeft();
                            BckRight = getPositionFWDRight();
                            foreach (string tile in board.Tiles)
                            {
                                if (tile.Contains(FwdLeft) && tile.Contains("X"))
                                {
                                    Destination = FwdLeft;
                                    fwd = true;
                                    bck = false;
                                    CaptureMarkerB();
                                }
                                if (tile.Contains(FwdRight) && tile.Contains("X"))
                                {
                                    Destination = FwdRight;
                                    fwd = true;
                                    bck = false;
                                    CaptureMarkerB();
                                }
                                if (tile.Contains(BckLeft) && tile.Contains("X"))
                                {
                                    Destination = BckLeft;
                                    bck = true;
                                    fwd = false;
                                    CaptureMarkerB();
                                }
                                if (tile.Contains(BckRight) && tile.Contains("X"))
                                {
                                    Destination = BckRight;
                                    bck = true;
                                    fwd = false;
                                    CaptureMarkerB();
                                }
                                if (Captured == true)
                                {
                                    CapturedInfo();
                                    break;
                                }
                            }
                        }
                    }
                }               
            }
        }
#endregion
        #region DisplayInfo
        public void CapturedInfo()
        {
            Console.WriteLine("Computer captured player piece\n" +
            "From {0}\n" +
            "Moving To: {1}\n" +
            "Capturing: {2}", PieceStart, NewDest, Destination);
            for (int i = 0; i < Comp.PlayerPieces.Count; i++)
            {
                if (Comp.PlayerPieces[i].Contains(PieceStart.Trim()))
                {
                    Comp.PlayerPieces.RemoveAt(i);
                    Comp.PlayerPieces.Add(NewDest);
                    break;
                }
            }
            Console.ReadLine();
        }
        public void MovedInfo()
        {
            Console.WriteLine("Computer has moved a piece\n" +
                "From {0}\n" +
                        "To: {1}", PieceStart, Destination);
            Console.ReadLine();
            for (int i = 0; i < Comp.PlayerPieces.Count; i++)
            {
                if (Comp.PlayerPieces[i].Contains(PieceStart.Trim()))
                {
                    Comp.PlayerPieces.RemoveAt(i);
                    Comp.PlayerPieces.Add(Left);
                    break;
                }
            }
        }
        #endregion
    }
}
