using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClasses
{
    static public class Display
    {
        public static void show_table(Board board)
        {
            int index_of_chess = 0;
            char symb_ver = '8',
                 symb_hor_start = 'A',
                 symb_hor_end = 'I';
            Console.Write(" ");
            for (char i = symb_hor_start; i < symb_hor_end; i++)
            {
                Console.Write(" " + i);
            }
            Console.Write("\n");

            for (int i = 0; i < board.Height; i++, symb_ver--)
            {
                Console.Write(symb_ver + " ");
                for (int j = 0; j < board.Width; j++)
                {
                    
                    if (board.Chesses[index_of_chess].GetCords.Equals(new Tuple<int, int>(j, i)))
                    {
                        Console.Write(board.Chesses[index_of_chess++] + " ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.Write('\n');
            }
        }
    }
}
