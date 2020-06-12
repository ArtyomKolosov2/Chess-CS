using System;
using System.Collections.Generic;

namespace ChessClasses
{
    public class Board
    {
        private List<ChessClass> chesses;
        private int 
            width = 8, 
            height = 8;
        public Board()
        {
            this.chesses = new List<ChessClass>();
        }
         
        public void add_chess(ChessClass chess)
        {
            if (!(this.chesses.Count > (this.height * this.width)))
            {
                this.Chesses.Add(chess);
            }
        }

        public List<ChessClass> Chesses
        {
            get {return this.chesses; }
        }

        public int Height
        {
            get {return this.height; }
        }
        public int Width
        {
            get { return this.width; }
        }
    }
}
