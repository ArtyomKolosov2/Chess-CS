using System;
using System.Collections.Generic;
using System.Text;

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
            return this.chess_repr ?? "";
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

        public virtual bool check_step(Tuple<int, int> destination)
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
            get { return this.chess_code; }
        }

        public List<Tuple<int, int>> ChessSteps
        {
            get { return this.chess_steps; }
        }

        public string ChessRepr
        {
            get { return this.chess_repr; }
        }

        public Tuple<int, int> GetCords
        {
            get { return this.chess_cord; }
            set { chess_cord = value; }
        }
        
    }

    public class Pawn : ChessClass
    {
        private bool is_started = false;
        
        public Pawn(int x, int y, int code)
        {
            this.chess_cord = new Tuple<int, int>(x, y);
            this.chess_code = code;
            if (code != 0)
            {
                this.chess_repr = "♙";
            }
            else
            {
                this.chess_repr = "♟";
            }
            this.chess_steps = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, 1)
            };
        }
        public bool IsStarted
        {
            get { return this.is_started; }
            set { this.is_started = value; }
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
                new Tuple<int, int>(0, 1)
            };
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
                new Tuple<int, int>(0, 1)
            };
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
                new Tuple<int, int>(0, 1)
            };
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
                new Tuple<int, int>(0, 1)
            };
        }
    }
}
