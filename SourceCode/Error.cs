using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Resources;

namespace CheckersGame
{
    class Error
    {
        // Error Messages depending on which error occurs
        public void NoPlayerCounter()
        {
            Console.WriteLine("No player counter on selected position\n");
            Console.ReadLine();
        }

        public void WrongDestCoord()
        {
            Console.WriteLine("Marker destination is illegal or already has a player counter on it");
            Console.ReadLine();
        }

        public void NoSidewaysMove()
        {
            Console.WriteLine("That move is not possible");
            Console.WriteLine("Markers cannot move Sideways");
            Console.WriteLine("The counters can only move forward Diagonally");
            Console.ReadLine();
        }

        public void NoBackMove()
        {
            Console.WriteLine("That move is not possible\n");
            Console.Write("Markers cannot move Backwards\n");
            Console.WriteLine("The counters can only move forward Diagonally");
            Console.ReadLine();
        }

        public void KingNoBackMove()
        {
            Console.WriteLine("That move is not possible\n");
            Console.WriteLine("Markers can only move Backwards one row and diagonally\n");
            Console.WriteLine("Or move forwards one row diagonally");
            Console.ReadLine();
        }

        public void WrongFwdMove()
        {
            Console.WriteLine("That move is not possible\n");
            Console.WriteLine("The counters can only move forward Diagonally one row");
            Console.ReadLine();
        }

        public void NoCapture()
        {
            Console.WriteLine("Cannot take enemy piece. No tiles to move too after.\nOr there is an enemy marker at location\nMove aborted");
            Console.ReadLine();
        }

    }
}
