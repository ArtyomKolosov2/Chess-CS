using System;
using System.Collections.Generic;

namespace ChessClasses
{
    public class BoardClass
    {
        private List<ChessClass> chesses;
        private int 
            width = 8, 
            height = 8;
        public BoardClass()
        {
            chesses = new List<ChessClass>();
        }
         
        public void add_chess(ChessClass chess)
        {
            if (!(chesses.Count > (height * width)))
            {
               Chesses.Add(chess);
            }
        }

        public ChessClass find_chess_by_coords(Tuple<int, int> cords)
        {
            ChessClass result = null;
            for (int i = 0; i < Chesses.Count; i++)
            {
                if (Chesses[i].GetCords.Equals(cords))
                {
                    result = Chesses[i];
                    break;
                }
            }
            return result;
        }

        public List<ChessClass> Chesses
        {
            get {return chesses; }
        }

        public int Height
        {
            get {return height; }
        }
        public int Width
        {
            get { return width; }
        }
    }
}
