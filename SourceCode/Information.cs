using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace CheckersGame
{
    class Information
    {
        FrontPage start = new FrontPage();
        public void Rules()
        {
            Console.Clear();

            Console.WriteAscii("        RULES", Color.DarkViolet);
            Console.WriteLineFormatted("\n\n           1. Movement for a single non-king piece is only possible by forward Diagonal Movement of ONE square\n" +
                "           2. Movement for King Pieces is either forward or backward Diagonal movement of ONE square\n" +
                "           3. To change a player piece into a king piece. You must get a marker to the opposite end of the board.\n            This will end the current players turn\n" +
                "           4. Undo feature only available for movement and taking a SINGLE opponents movement\n" +
                "           5. Capturing an opponents marker is automatic. There is no option\n" +
                "           6. Upto 3 enemy pieces can be captured in a single turn for a regular player piece\n"+
                "           7. A king piece can capture upto 6 opponent pieces\n" +
                "           8. To Win, you must capture all your opponents pieces\n", Color.DarkOrange);

            Console.Write("Press any key to return to the Main Menu");
            Console.ReadLine();

            start.Menu();
        }
    }
}
