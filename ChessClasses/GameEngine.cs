using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClasses
{
    public class GameEngine
    {
        private Board board;
        private Player PlayerOne;
        private Player PlayerTwo;
        private Dictionary<string, int> codes;
        
        public GameEngine()
        {
            this.board = new Board();
            PlayerOne = new Player(0);
            PlayerTwo = new Player(1);
            codes = new Dictionary<string, int>();
            set_dict();
        }

        public string get_players_info()
        {
            return $"{PlayerOne} {PlayerTwo}";
        }

        private void set_dict()
        {
            int size = 8;
            char symb = 'a';
            char symb_digit = '1';
            for (int i = 1; i < size + 1; i++, symb++, symb_digit++)
            {
                codes.Add(symb.ToString(), i);
                codes.Add(symb_digit.ToString(), i);
            }
        }
        public void show_dict()
        {
            foreach (var key in codes.Keys)
            {
                Console.WriteLine(key + ":" + codes[key]);
            }
        }
        private bool is_attack_possible(ChessClass chess_attack, ChessClass second_chess)
        {
            bool result = false;
            if (second_chess?.ChessCode != chess_attack?.ChessCode) 
            { 
                result = true; 
            }
            return result;
        }

        private ChessClass find_chess_by_coords(Tuple<int, int> cords)
        {
            ChessClass result = null;
            for (int i = 0; i < board.Chesses.Count; i++)
            {
                if (board.Chesses[i].GetCords.Equals(cords))
                {
                    result = board.Chesses[i];
                    break;
                }
            }
            return result;
        }
        public void get_chess()
        {
            Tuple<int, int> user_cord = get_user_cord("Input coords: ");
            for (int i = 0; i < board.Chesses.Count && user_cord != null; i++)
            {                
                if (board.Chesses[i].GetCords.Equals(user_cord))
                {
                    ChessClass chess = board.Chesses[i];
                    Tuple<int, int> new_cord = get_user_cord("Input new coords: ") ?? user_cord;
                    if (chess.check_step(new_cord)) 
                    {
                        ChessClass second_chess = find_chess_by_coords(new_cord);
                        if (is_attack_possible(chess, second_chess) && second_chess != null)
                        {
                            board.Chesses.Remove(second_chess);
                            chess.GetCords = new_cord;
                        }
                        else if (second_chess == null)
                        {
                            chess.GetCords = new_cord;
                        }
                        else
                        {
                            Console.WriteLine("Allied Chess!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("This step cant exist!");
                    }
                    break;
                }
            }
            Display.show_table(board);            
        }

        public void move_chess()
        {

        }

        private Tuple<int, int> get_user_cord(string message)
        {
            Console.WriteLine(message);
            Tuple<int, int> result;
            string[] user_choice = Console.ReadLine().Split();
            try
            {
                int x = codes[user_choice[0].ToLower()] - 1;
                int y = board.Height - codes[user_choice[1].ToLower()];
                result = new Tuple<int, int>(x, y);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("KeyError!");
                result = null;
            }
            
            return result;
        }
        public void initialize_board()
        {
            int i = 0;
            for (i = 0; i < this.board.Width; i++)
            {
                this.board.add_chess(new Pawn(i, 1, 0));
                this.board.add_chess(new Pawn(i, 6, 1));
                if (i == 2 || i == 5)
                {
                    this.board.add_chess(new Elephant(i, 0, 0));
                    this.board.add_chess(new Elephant(i, 7, 1));
                }
                else if (i == 0 || i == 7)
                {
                    this.board.add_chess(new Rook(i, 0, 0));
                    this.board.add_chess(new Rook(i, 7, 1));
                }
                else if (i == 1 || i == 6)
                { 
                    this.board.add_chess(new Horse(i, 0, 0));
                    this.board.add_chess(new Horse(i, 7, 1));
                }
                else if (i == 3)
                {
                    this.board.add_chess(new Queen(i, 0, 0));
                    this.board.add_chess(new Queen(i, 7, 1));
                }
                else if (i == 4)
                {
                    this.board.add_chess(new King(i, 0, 0));
                    this.board.add_chess(new King(i, 7, 1));
                }

            }
        }

        public Board GetBoard 
        {
            get { return this.board; }
        }
    }
}
