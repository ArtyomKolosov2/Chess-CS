﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ChessClasses
{
    public static class DisplayClass
    {
        private static void print_alphas()
        {
            char symb_hor_start = 'A',
                 symb_hor_end = 'I';
            Console.Write(" ");
            for (char i = symb_hor_start; i < symb_hor_end; i++)
            {
                Console.Write(" " + i);
            }
            Console.Write("\n");
        }

        public static void show_file_info(FileInfo [] array)
        {
            foreach (var element in array)
            {
                if (element != null)
                {
                    Console.WriteLine(element.Name);
                }
            }
        }

        public static void show_program_info()
        {
            Console.WriteLine(
                "Welcome to ChessInConsole!\n" +
                "Rules are the same like in classic chess\n" +
                "While playing type \"exit\" to stop current game,\n" +
                "\"save\" to save your game (While Playing),\n" +
                "\"info\" to show main info about program,\n" +
                "Enjoy Game!\n");
        }

        public static void show_gameover_info(ChessClass chess)
        {
            Console.WriteLine(chess.get_info());
        }

        public static void show_table(BoardClass board, Stack<string> message_buf)
        {
            char symb_ver = '8',       
                 space_sybmol_white = '•',
                 space_sybmol_black = '•';
            for (int i = 0; i < message_buf.Count; i++)
            {
                Console.WriteLine(message_buf.Pop());
            }
            print_alphas();
            bool flag;
            for (int i = 0; i < board.Height; i++, symb_ver--)
            {
                Console.Write(symb_ver + " ");
                for (int j = 0; j < board.Width; j++)
                {
                    flag = false;
                    for (int k = 0; k < board.Chesses.Count; k++) {
                        if (board.Chesses[k].GetCords.Equals(new Tuple<int, int>(j, i)))
                        {
                            Console.Write(board.Chesses[k] + " ");
                            flag = true;
                            break;
                        }
              
                    }
                    if (!flag)
                    {
                        if ((i + j) % 2 == 0)
                        {
                            Console.Write(space_sybmol_black + " ");          
                        }
                        else
                        {
                            Console.Write(space_sybmol_white + " ");
                        }                        
                    }
                }
                Console.Write(symb_ver + " ");
                Console.Write('\n');
            }
            print_alphas();

        }
    }
}
