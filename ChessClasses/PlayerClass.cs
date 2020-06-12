using System;

namespace ChessClasses
{
    public class Player
    {
        private int chess_code = 0;
        private string name;
        public void move(Tuple<int, int> cords)
        {
            ;
        }

        public string Name
        {
            get {return this.name; }
            set {this.name = value; }
        }
        public int ChessCode
        {
            get {return this.chess_code; }
            set {this.chess_code = value; }
        }
    }
}
