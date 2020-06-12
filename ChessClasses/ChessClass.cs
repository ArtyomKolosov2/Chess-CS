using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClasses
{
    public class ChessClass
    {
        protected int chess_code = 0;
        protected string chess_repr = "";
        protected Tuple<int, int> chess_cord;
        protected List<Tuple<int, int>> chess_steps;

        public override string ToString()
        {
            return this.chess_repr;
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
                new Tuple<int, int>(0, 1)
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
