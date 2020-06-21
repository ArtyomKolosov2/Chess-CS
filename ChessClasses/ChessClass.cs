using System;
using System.Collections.Generic;

namespace ChessClasses
{
    public class ChessClass
    {
        protected int chess_code = 0;
        protected string chess_repr = null;
        protected Tuple<int, int> chess_cord;
        protected List<Tuple<int, int>> chess_steps;

        public override string ToString()
        {
            return chess_repr ?? "";
        }

        public string get_info()
        {
            string info;
            info = string.Format("{0} {1}",chess_code != 0 ? "White":"Black", base.ToString());
            return info;
        }

        public static bool operator true(ChessClass cls)
        {
            return cls.chess_repr != null;
        }
        public static bool operator false(ChessClass cls)
        {
            return cls.chess_repr == null;
        }

        protected Tuple<int, int> sum_tuples(Tuple<int, int> first, Tuple<int, int> second)
        {
            Tuple<int, int> new_tuple = new Tuple<int, int>
                (
                first.Item1 + second.Item1,
                first.Item2 + second.Item2
                );
            return new_tuple;
        }

        public virtual bool check_step(Tuple<int, int> destination, BoardClass board)
        {
            bool result = false;
            foreach (var step in chess_steps)
            {
                Tuple<int, int> new_cord = sum_tuples(step, chess_cord);
                if (new_cord.Equals(destination))
                {
                    result = true;
                    break;
                }
          
            }
            return result;         
        }

        public int ChessCode
        {
            get { return chess_code; }
        }

        public List<Tuple<int, int>> ChessSteps
        {
            get { return chess_steps; }
        }

        public string ChessRepr
        {
            get { return chess_repr; }
        }

        public Tuple<int, int> GetCords
        {
            get { return chess_cord; }
            set { chess_cord = value; }
        }
        
    }

    public class Pawn : ChessClass
    {
        private bool is_started = false;
        private List<Tuple<int, int>> attack_steps;
        public Pawn(int x, int y, int code)
        {
            chess_cord = new Tuple<int, int>(x, y);
            chess_code = code;
            if (code != 0)
            {
                chess_repr = "♙";
            }
            else
            {
                chess_repr = "♟";
            }
            Tuple<int, int> step;
            if (code != 0)
            {
                attack_steps = new List<Tuple<int, int>>
                {
                new Tuple<int, int>(1, -1),
                new Tuple<int, int>(-1, -1)
                };
                step = new Tuple<int, int>(0, -1);
            }
            else
            {
                attack_steps = new List<Tuple<int, int>>
                {
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(1, 1)
                };
                step = new Tuple<int, int>(0, 1);
            }
            chess_steps = new List<Tuple<int, int>>
            {
                step
            };
            
        }

        public override bool check_step(Tuple<int, int> destination, BoardClass board)
        {
            int repeat = is_started ? 1 : 2;
            bool result = false;
            Tuple<int, int> step_one = sum_tuples(chess_cord, attack_steps[0]);
            Tuple<int, int> step_two = sum_tuples(chess_cord, attack_steps[1]);
            bool one = (step_one.Equals(destination) && board.find_chess_by_coords(step_one) != null);
            bool two = (step_two.Equals(destination) && board.find_chess_by_coords(step_two) != null);
            if (one || two)
            {
                result = true;
            }
            foreach (var step in chess_steps)
            {
                if (result)
                {
                    break;
                }
                Tuple<int, int> new_cord = chess_cord;
                for (int i = 0; i < repeat; i++)
                {
                    new_cord = sum_tuples(step, new_cord);
                    if (board.find_chess_by_coords(new_cord) != null)
                    {
                        break;
                    }
                    if (new_cord.Equals(destination))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool IsStarted
        {
            get { return is_started; }
            set { is_started = value; }
        }
    }

    public class Horse : ChessClass
    {
        public Horse(int x, int y, int code)
        {
            this.chess_cord = new Tuple<int, int>(x, y);
            this.chess_code = code;
            if (code != 0)
            {
                this.chess_repr = "♘";
            }
            else
            {
                this.chess_repr = "♞";
            }
            this.chess_steps = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(2, -1),
                new Tuple<int, int>(-2, -1),
                new Tuple<int, int>(1, -2),
                new Tuple<int, int>(-1, -2),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(-2, 1),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(-1, 2),
            };
        }
    }

    public class Rook : ChessClass
    {
        public Rook(int x, int y, int code)
        {
            this.chess_cord = new Tuple<int, int>(x, y);
            this.chess_code = code;
            if (code != 0)
            {
                this.chess_repr = "♖";
            }
            else
            {
                this.chess_repr = "♜";
            }
            this.chess_steps = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(-1, 0),

            };
        }
        public override bool check_step(Tuple<int, int> destination, BoardClass board)
        {
            int start = -1;
            bool result = false;
            foreach (var step in chess_steps)
            {
                Tuple<int, int> new_cord = chess_cord;

                while (new_cord.Item1 < board.Height && new_cord.Item2 < board.Width && new_cord.Item1 > start && new_cord.Item2 > start)
                {
                    new_cord = sum_tuples(step, new_cord);
                    if (new_cord.Equals(destination))
                    {
                        result = true;
                        break;
                    }
                    if (board.find_chess_by_coords(new_cord) != null)
                    {
                        break;
                    }
                }

                if (result)
                {
                    break;
                }
            }
            return result;

        }
    }

    public class Elephant : ChessClass
    {
        public Elephant(int x, int y, int code)
        {
            this.chess_cord = new Tuple<int, int>(x, y);
            this.chess_code = code;
            if (code != 0)
            {
                this.chess_repr = "♗";
            }
            else
            {
                this.chess_repr = "♝";
            }
            this.chess_steps = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(1, -1),
                new Tuple<int, int>(-1, 1),

            };


        }

        public override bool check_step(Tuple<int, int> destination, BoardClass board)
        {
            int start = -1;
            bool result = false;
            foreach (var step in chess_steps)
            {
                Tuple<int, int> new_cord = chess_cord;

                while (new_cord.Item1 < board.Height && new_cord.Item2 < board.Width && new_cord.Item1 > start && new_cord.Item2 > start)
                {
                    new_cord = sum_tuples(step, new_cord);
                    if (new_cord.Equals(destination))
                    {
                        result = true;
                        break;
                    }
                    if (board.find_chess_by_coords(new_cord) != null)
                    {
                        break;
                    }
                }

                if (result)
                {
                    break;
                }
            }
            return result;

        }
    }

    public class Queen : ChessClass
    {
        public Queen(int x, int y, int code)
        {
            this.chess_cord = new Tuple<int, int>(x, y);
            this.chess_code = code;
            if (code != 0)
            {
                this.chess_repr = "♕";
            }
            else
            {
                this.chess_repr = "♛";
            }
            this.chess_steps = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(1, -1),
            };
        }
        public override bool check_step(Tuple<int, int> destination, BoardClass board)
        {
            int start = -1;
            bool result = false;
            foreach (var step in chess_steps)
            {
                Tuple<int, int> new_cord = chess_cord;

                while (new_cord.Item1 < board.Height && new_cord.Item2 < board.Width && new_cord.Item1 > start && new_cord.Item2 > start)
                {
                    new_cord = sum_tuples(step, new_cord);
                    if (new_cord.Equals(destination))
                    {
                        result = true;
                        break;
                    }
                    if (board.find_chess_by_coords(new_cord) != null)
                    {
                        break;
                    }
                }
                
                if (result)
                {
                    break;
                }
            }
            return result;

        }
    }

    public class King : ChessClass
    {
        public King(int x, int y, int code)
        {
            this.chess_cord = new Tuple<int, int>(x, y);
            this.chess_code = code;
            if (code != 0)
            {
                this.chess_repr = "♔";
            }
            else
            {
                this.chess_repr = "♚";
            }
            this.chess_steps = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(1, -1),
                
            };
        }
    }
}
