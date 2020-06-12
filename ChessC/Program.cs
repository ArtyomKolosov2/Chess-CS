using System;
using System.Text;
using ChessClasses;

namespace ChessC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Board board = new Board();
            for (int i = 0; i < 10; i++)
            {
                board.add_chess(new ChessClasses.Pawn(0, i, 1));
            }
            Display.show_table(board);
            Console.Write('\n');
            Console.ReadKey();
        }
    }
}
