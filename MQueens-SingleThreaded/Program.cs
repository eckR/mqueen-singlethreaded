using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQueens_SingleThreaded
{
    using System.Diagnostics;

    public class Program
    {
        private static string Usage
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("MQueens_SingleThreaded {n} {r/sr}");
                builder.Append("n - field size of chess field");
                builder.Append("r/sr - r for full recursive algorithm, sr for sequential-recursive algorithm");
                return builder.ToString();
            }
        }

        public static void Main(string[] args)
        {
            for (int i = 1; i < 12; i++)
            {
                Main_Method(new string[] {i.ToString(), "sr"});
            }
            while (true)
            {
                
            }
        }

        public static void Main_Method(string[] args)
        {           
            int n = 0;
            //Check the arguments
            if (args.Count() != 2)
            {
                Console.WriteLine("Invalid Number of Arguments");
                Console.WriteLine(Usage);
                return;
            }
            try
            {
                n = Int32.Parse(args[0]);
            }
            catch (Exception)
            {
                Console.WriteLine("Field Size Parameter is not valid");
                Console.WriteLine(Usage);
                return;
            }
            string algorithm = args[1];
            if (algorithm != "r" && algorithm != "sr")
            {
                Console.WriteLine("algorithm name is not valid");
                Console.WriteLine(Usage);
                return;
            }

            //Start the stopwatch
            Stopwatch watch = new Stopwatch();
            watch.Start();
            long solutions = 0;
            Board board = new Board(n);
            if (algorithm == "r")
            {
                solutions = FullRecursive(board, n*n-1);
            }
            else
            {
                solutions = SequentialRecursive(board, n*n-1);
            }
            watch.Stop();
            Console.WriteLine("mqueens({0}) = {1}, runtime = {2}\n", n , solutions, watch.ElapsedMilliseconds);
        }

        public static long FullRecursive(Board board, int prevPos)
        {
            long solutions = 0;
            if (prevPos < 0)
            {
                return board.BoardCovered() ? 1 : 0;
            }
            int x = prevPos % board.N;
            int y = prevPos / board.N;
            Positions positions = new Positions { X = x, Y = y };
            if (!board.IsPositionCovered(x, y))
            {
                board.AppendPosition(positions);
                solutions += FullRecursive(board, prevPos - 1);
                board.RemovePosition(positions);
            }
            solutions += FullRecursive(board, prevPos - 1);
            return solutions;
        }

        public static long SequentialRecursive(Board board, int prevPos)
        {
            long solutions = 0;
            int x;
            int y;
            for (; prevPos >= 0; prevPos--)
            {
                x = prevPos % board.N;
                y = prevPos / board.N;
                if (!board.IsPositionCovered(x, y))
                {
                    Positions positions = new Positions { X = x, Y = y };
                    board.AppendPosition(positions);
                    solutions += SequentialRecursive(board, prevPos);
                    board.RemovePosition(positions);
                }
            }
            if (board.BoardCovered())
            {
                solutions++;
            }
            return solutions;
        }
    }
}
