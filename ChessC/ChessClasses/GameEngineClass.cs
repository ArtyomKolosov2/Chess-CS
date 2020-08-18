using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
        private Stack<string> message_buf;
        
        public GameEngineClass()
        {
            board = new BoardClass();
            PlayerOneBlack = new Player(0);
            PlayerTwoWhite = new Player(1);
            codes = new Dictionary<string, int>();
            message_buf = new Stack<string>();
            set_dict();
        }

        
        public void prepare_to_game()
        {
            CurrentPlayer = PlayerTwoWhite;
            GameStarted = true;
            initialize_board();
        }

        public void start_game()
        {
            if (GameStarted)
            {
                message_buf.Push($"Move of {CurrentPlayer.ToString()}");
                DisplayClass.show_table(board, message_buf);
                while (GameStarted)
                {
                    game_part();
                    DisplayClass.show_table(board, message_buf);
                }
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

        private void save_game()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Console.WriteLine("Input game name: ");
            string name = Console.ReadLine();
            name = $"{(name.Length > 0 ? name : "game")}.dat";
            using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, board);
                formatter.Serialize(fs, CurrentPlayer);
                Console.WriteLine("Game Saved!");
            }
        }

        private void find_only_dat_files(FileInfo [] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if(!files[i].Extension.Equals(".dat"))
                {
                    files[i] = null;
                }
            }
        }

        private bool find_file_name(FileInfo [] files, string name)
        {
            bool result = false;
            foreach (FileInfo file in files)
            {
                if (file != null && file.Name.Equals(name))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        public void load_game()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = info.GetFiles();
            
            find_only_dat_files(files);
            Console.WriteLine("Games Saved:");
            DisplayClass.show_file_info(files);
            Console.WriteLine("Input game name: ");
            string name = $"{Console.ReadLine()}.dat";
            if (name.Length > 0 && find_file_name(files, name))
            {
                using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
                {
                    try
                    {
                        board = (BoardClass)formatter.Deserialize(fs);
                        CurrentPlayer = (Player)formatter.Deserialize(fs);
                        GameStarted = true;
                    }
                    catch (SerializationException)
                    {
                        Console.WriteLine("LoadError!");
                    }
                }
            }
            else
            {
                Console.WriteLine("LoadError!!!");
            }
        }

        private void gameover(ChessClass chess)
        {
            GameStarted = false;
            DisplayClass.show_gameover_info(chess);
        }
       
        public void game_part()
        {
            Tuple<int, int> user_cord = get_user_cord("Input coords (or msg): ");
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
                            message_buf.Push("Allied Chess!");
                        }
                    }
                    else
                    {
                        message_buf.Push("This step cant exist!");
                    }
                    break;
                }
            }
            message_buf.Push($"Move of {CurrentPlayer.ToString()}");
        }

        private bool CheckForCommands(string user_line)
        {
            bool is_contains_command = false;
            switch (user_line.ToLower())
            {
                case "exit":
                    message_buf.Push("Game Ended!");
                    GameStarted = false;
                    is_contains_command = true;
                    break;

                case "info":
                    DisplayClass.show_program_info();
                    break;

                case "save":
                    
                    is_contains_command = true;
                    save_game();
                    break;

                case "load":
                    
                    is_contains_command = true;
                    load_game();
                    Console.WriteLine("Game Loaded!");
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
