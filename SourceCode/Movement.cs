using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public abstract class Movement
    {
        Board board;
        #region Variables

        // newDest to store new destination, based on where marker is moving too &/or an enemy marker is being captured
        // Left & right used for second enemy marker take - to get location of new coord for player piece
        private string pieceStart;
        private string destination;
        private string newDest;
        private string left;
        private string right;

        public string Destination { get { return destination; } set { destination = value; } }
        public string PieceStart { get { return pieceStart; } set { pieceStart = value; } }
        public string NewDest { get { return newDest; } set { newDest = value; } }
        public string Left { get { return left; } set { left = value; } }
        public string Right { get { return right; } set { right = value; } }

        // char array used to compare the above char arrays
        // so as to prevent backwards movements
        // and possibly make sure only diagonal movement forward
        private char[] letter = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public char[] Letter { get { return letter; } set { letter = value; } }

        // Char array to check position of number new player marker destination
        // AFTER they attempt to take an enemy marker
        // And Then used with above char array to create a new tile destination for player marker
        private char[] number = { '1', '2', '3', '4', '5', '6', '7', '8' };

        public char[] Number { get { return number; } set { number = value; } }

        // int variables to compare positions of char[]'s above
        // based on position of letter in char[] letter
        // used to prevent backwards movements
        // possibly restrict forward movement to diagonal
        private int posC;
        private int posD;

        public int PosC { get { return posC; } set { posC = value; } }
        public int PosD { get { return posD; } set { posD = value; } }

        // String contents debug stuff
        // NOTE newL & newN must have a strign in them else are regarded as null, for some reason that escapes me :D
        public string newL = "coord";
        public string newN = "coord";
        public char coordL;
        public char coordn;

        public string[] TilesUndo { get; set; }
        public string Ans { get; set; }
        public bool Captured { get; set; }
        #endregion
        #region Constructor
        // needed?
        public Movement(Board draughts)
        {
            board = draughts;
        }
        #endregion
        public abstract void Move(string Opponent);
        public abstract string getPositionFWDLeft();
        public abstract string getPositionFWDRight();

    }
}
