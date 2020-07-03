using System;
using System.Collections.Generic;

namespace ChessClasses
{
    public class GameEngineClass
    {
        private BoardClass board;
        private Player PlayerOneBlack;
        private Player PlayerTwoWhite;

        public bool GameStarted { get; private set; } = false;
        public Player CurrentPlayer { get; private set; } = null;
        private Dictionary<string, int> codes;
        
        public GameEngineClass()
        {
            board = new BoardClass();
            PlayerOneBlack = new Player(0);
            PlayerTwoWhite = new Player(1);
            codes = new Dictionary<string, int>();
            set_dict();
        }

        
        public void prepare_to_game()
        {
            CurrentPlayer = PlayerTwoWhite;
            GameStarted = true;
            initialize_board();
            DisplayClass.show_table(GetBoard);
            while (GameStarted)
            {
                game_part();
            }
        }

        public string get_players_info()
        {
            return $"{PlayerOneBlack} {PlayerTwoWhite}";
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

        private void gameover(ChessClass chess)
        {
            GameStarted = false;
            DisplayClass.show_gameover_info(chess);
        }
       
        public void game_part()
        {
            Tuple<int, int> user_cord = get_user_cord("Input coords: ");
            string message = null;
            for (int i = 0; i < board.Chesses.Count && user_cord != null; i++)
            {                
                if (board.Chesses[i].GetCords.Equals(user_cord))
                {
                    ChessClass chess = board.Chesses[i];
                    Tuple<int, int> new_cord = get_user_cord("Input new coords: ") ?? user_cord;
                    bool step = chess.check_step(new_cord, board) && chess.ChessCode == CurrentPlayer.ChessCode;
                    if (step) 
                    {
                        ChessClass second_chess = board.find_chess_by_coords(new_cord);
                        bool possibe = is_attack_possible(chess, second_chess);
                        if (possibe && second_chess != null)
                        {
                            board.Chesses.Remove(second_chess);
                            if (second_chess is King)
                            {
                                gameover(second_chess);
                                break;
                            }
                            chess.GetCords = new_cord;
                            if (chess is Pawn pawn)
                            {
                                pawn.IsStarted = true;
                            }
                            CurrentPlayer = CurrentPlayer.ChessCode != 0 ? PlayerOneBlack : PlayerTwoWhite;
                        }
                        else if (second_chess == null)
                        {
                            chess.GetCords = new_cord;
                            if (chess is Pawn pawn)
                            {
                                pawn.IsStarted = true;
                            }
                            CurrentPlayer = CurrentPlayer.ChessCode != 0 ? PlayerOneBlack : PlayerTwoWhite;
                        }
                        else
                        {
                            message = "Allied Chess!";
                        }
                    }
                    else
                    {
                        message = "This step cant exist!";
                    }
                    break;
                }
            }
            DisplayClass.show_table(board, message);
        }

        private bool CheckForCommands(string user_line)
        {
            bool is_contains_command = false;
            switch (user_line.ToLower())
            {
                case "exit":
                    Console.WriteLine("Game Ended!");
                    GameStarted = false;
                    is_contains_command = true;
                    break;

                case "info":
                    DisplayClass.show_program_info();
                    break;

                case "save":
                    Console.WriteLine("Game Saved!");
                    is_contains_command = true;
                    // ToDo
                    break;

                
            }
            return is_contains_command;
        }

        private Tuple<int, int> get_user_cord(string message)
        {
            Console.WriteLine(message);
            string user_line = Console.ReadLine();
            bool is_command = CheckForCommands(user_line);
            Tuple<int, int> result = null;
            string[] user_choice = user_line.Split();
            if (!is_command)
            {
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

                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("IndexError!");
                    result = null;
                }
            }
            return result;
        }
        public void initialize_board()
        {
            int i = 0;
            for (i = 0; i < board.Width; i++)
            {
                board.add_chess(new Pawn(i, 1, 0));
                board.add_chess(new Pawn(i, 6, 1));
                if (i == 2 || i == 5)
                {
                    board.add_chess(new Elephant(i, 0, 0));
                    board.add_chess(new Elephant(i, 7, 1));
                }
                else if (i == 0 || i == 7)
                {
                    board.add_chess(new Rook(i, 0, 0));
                    board.add_chess(new Rook(i, 7, 1));
                }
                else if (i == 1 || i == 6)
                { 
                    board.add_chess(new Horse(i, 0, 0));
                    board.add_chess(new Horse(i, 7, 1));
                }
                else if (i == 3)
                {
                    board.add_chess(new Queen(i, 0, 0));
                    board.add_chess(new Queen(i, 7, 1));
                }
                else if (i == 4)
                {
                    board.add_chess(new King(i, 0, 0));
                    board.add_chess(new King(i, 7, 1));
                }

            }
        }

        public BoardClass GetBoard 
        {
            get { return board; }
        }
    }
}
