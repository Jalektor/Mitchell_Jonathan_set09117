using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    class Information
    {
        FrontPage start = new FrontPage();
        public void Rules()
        {
            Console.Clear();

            Console.WriteLine("The Rules for this game of draughts:\n" +
                "1. Movement for a single non-king piece is only possible by forward Diagonal Movement of ONE square\n" +
                "2. Movement for King Pieces is either forward or backward DIagonal movement of ONE square\n" +
                "3. To change a player piece into a king piece. You must get a marker to the opposite end of the board\n" +
                "4. Undo feature only available for movement and taking a SINGLE opponents movement\n" +
                "5. Capturing an opponents marker is automatic. There is no option\n" +
                "6. Upto 2 enemy pieces can be captured in a single turn"+
                "7. To Win, you must capture all your opponents pieces\n");

            Console.ReadLine();

            start.Menu();
        }
    }
}
