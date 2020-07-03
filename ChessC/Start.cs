using System;
using System.Runtime.InteropServices;
using System.Text;
using ChessClasses;

namespace ChessC
{
    class Start
    {
        static void Main(string[] args)
        {
            set_console_settings(size:50);
            GameEngineClass gameEngine = new GameEngineClass();
            string Command = null;
            bool is_started = true;
            while (is_started)
            {
                Console.Write("Type comand:\n" +
                    "Start - Start Game\n" +
                    "Info = Show Main info about program\n" +
                    "Exit - Exit From Game\n");
                Command = Console.ReadLine();
                switch (Command.ToLower())
                {
                    case "start":
                        gameEngine.prepare_to_game();
                        break;

                    case "exit":
                        is_started = false;
                        break;

                    case "info":
                        DisplayClass.show_program_info();
                        break;

                    default:
                        Console.WriteLine("Command Error!");
                        break;
                }
            }
        }

        static void set_console_settings(
            int sizeX=40, 
            int sizeY=12, 
            string font = "NSimSun",
            short size = 72)
        {
            Console.SetWindowSize(sizeX, sizeY);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.Title = "Chesses In Console Alpha";
            SetConsoleFont(font, size);
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal unsafe struct CONSOLE_FONT_INFO_EX
        {
            internal uint cbSize;
            internal uint nFont;
            internal COORD dwFontSize;
            internal int FontFamily;
            internal int FontWeight;
            internal fixed char FaceName[LF_FACESIZE];
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            internal short X;
            internal short Y;

            internal COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }
        private const int STD_OUTPUT_HANDLE = -11;
        private const int TMPF_TRUETYPE = 4;
        private const int LF_FACESIZE = 32;
        private static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int dwType);


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int SetConsoleFont(IntPtr hOut, uint dwFontNum);
        public static void SetConsoleFont(string fontName, short size)
        {
            unsafe
            {
                IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
                if (hnd != INVALID_HANDLE_VALUE)
                {
                    CONSOLE_FONT_INFO_EX info = new CONSOLE_FONT_INFO_EX();
                    info.cbSize = (uint)Marshal.SizeOf(info);
                    CONSOLE_FONT_INFO_EX newInfo = new CONSOLE_FONT_INFO_EX();
                    newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
                    newInfo.FontFamily = TMPF_TRUETYPE;
                    IntPtr ptr = new IntPtr(newInfo.FaceName);
                    Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);                   
                    newInfo.dwFontSize = new COORD(size, size);
                    newInfo.FontWeight = info.FontWeight;
                    SetCurrentConsoleFontEx(hnd, false, ref newInfo);
                }
            }
        }
        
    }
}
