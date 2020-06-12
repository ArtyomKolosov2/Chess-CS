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
        
        public GameEngine()
        {
            this.board = new Board();
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
                    this.board.add_chess(new Horse(i, 0, 0));
                    this.board.add_chess(new Horse(i, 7, 1));
                }
                else if (i == 0 || i == 7)
                {
                    this.board.add_chess(new Rook(i, 0, 0));
                    this.board.add_chess(new Rook(i, 7, 1));
                }
                else if (i == 1 || i == 6)
                {
                    this.board.add_chess(new Elephant(i, 0, 0));
                    this.board.add_chess(new Elephant(i, 7, 1));
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
