using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_game
{
    class Map
    {
        int size;
        public int[,] board;
        public int spaceX;
        public int spaceY;

        private int g;
        private int h;
        private int f;

        Map parent;

        public Map(int Size)
        {
            size = Size;
            board = new int[size, size];
            h = 2;
            parent = null;
        }
        public Map(int Size, Map Parent)
        {
            size = Size;
            board = new int[size, size];
            parent = Parent;
            h = parent.h + 1;
        }

        public List<Map> getAllParent(List<Map> maps)
        {
            maps.Add(parent);
            try
            {
                return parent.getAllParent(maps);
            }
            catch (Exception)
            {

                return null;
            }
            
        }
        public int getF()
        {
            return f;
        }
        public void setF()
        {
            f = getG() + h;
        }
        //public void setH()
        //{
        //    h = parent.h + 1;
        //}
        public int getG()
        {
            int[] row = new int[16] { 3, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 };
            int[] col = new int[16] { 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2 };

            int r = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (board[i, j] != 0)
                        r += Math.Abs(row[board[i, j]] - i) + Math.Abs(col[board[i, j]] - j);
            return r;
        }

        public int getH()
        {

            return h;
        }
        public void setH(int H)
        {
            h = H;
        }


        public void lastPrint()
        {
            Console.WriteLine("______________________________");
            parent.print();
            Console.WriteLine("|G=" + parent.getG() + "| H=" + parent.getH() + "| F=" + parent.getF() + "|");
            Console.WriteLine("______________________________");

            try
            {
                parentPrint(parent);
            }
            catch
            {
            }
        }
        public void parentPrint(Map map)
        {
            map.parent.print();
            map.parentPrint(map.parent);
        } 
        public string direction()
        {
            int x = 0;
            int y = 0; 
            string str = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i,j]!=parent.board[i,j]&&board[i,j]>0)
                    {
                        x = board[i, j];
                        y = i*size+ j;
                    }
                }
            }
            str = x + ", " + y;
            return str;
        }

        public void print()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(board[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("|G=" + getG() + "| H=" + getH() + "| F=" + getF() + "|");
            Console.WriteLine("______________________________");

            // Console.WriteLine(direction());
        }

    }
}
