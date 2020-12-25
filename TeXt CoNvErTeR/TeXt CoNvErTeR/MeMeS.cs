using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TeXt_CoNvErTeR
{
    class MeMeS
    {
        #region
        const int SWP_NOSIZE = 0x0001;
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        private static IntPtr MyConsole = GetConsoleWindow();

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        private static void CoNsOlElOoKs()
        {
            int xpos = 550;
            int ypos = 300;
            SetWindowPos(MyConsole, 0, xpos, ypos, 0, 0, SWP_NOSIZE);
            Random r = new Random();
            Console.ForegroundColor = (ConsoleColor)r.Next(0, 16);
            Console.BackgroundColor = (ConsoleColor)r.Next(0, 16);
            Console.SetWindowSize(100, 20);
            Console.SetBufferSize(100, 20);
            Console.Title = "DaNk HaXs | TeXt CoNvErTeR | By NeBuLa";
        }
        #endregion
        static void Main(string[] args)
        {
            CoNsOlElOoKs();
            while (true)
                TrAnSlAtEr();
        }
        private static void TrAnSlAtEr()
        {
            string TeXt = Console.ReadLine();
            char[] characters = TeXt.ToCharArray();
            bool up = true;
            foreach (char test in characters)
            {
                if (char.IsLetter(test))
                    if (up)
                    {
                        char Upper = char.ToUpper(test);
                        Console.Write(Upper);
                        up = false;
                    }
                    else
                    {
                        char Lower = char.ToLower(test);
                        Console.Write(Lower);
                        up = true;
                    }
                else
                    Console.Write(test);
            }
            Console.WriteLine();
            return;
        }
    }
}
