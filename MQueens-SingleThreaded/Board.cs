using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQueens_SingleThreaded
{
    public class Board
    {
        public int N { get; set; }

        private readonly LinkedList<Positions> positions = new LinkedList<Positions>(); 

        public Board(int n)
        {
            this.N = n;
        }

        public bool IsPositionCovered(int x, int y)
        {
            foreach(Positions board in this.positions)
            {
                if (board.X == x) { return true; }
                if (board.Y == y) { return true; }
                if (Math.Abs(board.X - x) == Math.Abs(board.Y - y)) { return true; }
            }
            return false;
        }

        public bool BoardCovered()
        {
            for (int x = 0; x < N; x++)
            {
                for (int y = 0; y < N; y++)
                {
                    if (!this.IsPositionCovered(x, y)) { return false; }
                }
            }
            return true;
        }

        public void AppendPosition(Positions position)
        {
            this.positions.AddLast(position);
        }

        public void RemovePosition(Positions position)
        {
            this.positions.Remove(position);
        }
    }
}
