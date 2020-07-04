using System;

namespace ChessClasses
{
    [Serializable]
    public class Player
    {
        private int chess_code = 0;
        private string name = null;

        public Player() { }
        public Player(string name, int chess_code)
        {
            this.name = name;
            this.chess_code = chess_code;
        }
        public Player(int chess_code)
        {
            this.chess_code = chess_code;
        }
        public void move(Tuple<int, int> cords)
        {
            ;
        }
        public override string ToString()
        {
            return chess_code != 0 ? "White" : "Black"; 
        }
        public string Name
        {
            get {return name ?? $"Player{chess_code+1}"; }
            set {name = value; }
        }
        public int ChessCode
        {
            get {return chess_code; }
        }

    }
}
