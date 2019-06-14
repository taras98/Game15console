using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_game
{
    class Game15
    {
        int size;
        static Random rand = new Random();
        Map map;

        public Game15(int Size = 4)
        {
            size = Size;
            map = new Map(size);

        }

        public void print()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(map.board[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public int printInTxt(int i, int j)
        {
            return map.board[i, j];
        }

        public void getFromTxt(int i, int j, int x)
        { 
            map.board[i, j] = x;
        }
        //public void print_search(int[,] map_search)
        //{
        //    for (int i = 0; i < size; i++)
        //    {
        //        for (int j = 0; j < size; j++)
        //        {
        //            Console.Write(map_search[i, j] + "\t");
        //        }
        //        Console.WriteLine();
        //    }
        //}
        public void start()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map.board[i, j] = coordsToPosition(i, j) + 1;
                }
            }
            map.spaceX = size - 1;
            map.spaceY = size - 1;
            map.board[map.spaceX, map.spaceY] = 0;
        }
        public int shift(int position)
        {
            int x, y;

            positionToCoords(position, out x, out y);
            if (Math.Abs(map.spaceX - x) + Math.Abs(map.spaceY - y) != 1) return -1;
            map.board[map.spaceX, map.spaceY] = map.board[x, y];//!
            map.board[x, y] = 0;
            map.spaceX = x;
            map.spaceY = y;
            return position;
        }

        public void shuffler(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                int a = rand.Next(0, 4);
                int x = map.spaceX;
                int y = map.spaceY;
                switch (a)
                {
                    case 0:
                        x++;
                        break; // ліво
                    case 1:
                        x--;
                        break; //право
                    case 2:
                        y++;
                        break; // верх
                    case 3:
                        y--;
                        break;  //низ
                }
                shift(coordsToPosition(x, y));
            }

        }
        //public int linear_conflict()
        //{

        //}
        //тільки для малої кількості ходів (5)


        //Евристика Мантхеттенська відстань
        public int manh_distance()
        {
            int[] row = new int[16] { 3, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 };
            int[] col = new int[16] { 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2 };

            int r = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map.board[i, j] != 0)
                        r += Math.Abs(row[map.board[i, j]] - i) + Math.Abs(col[map.board[i, j]] - j);
            return r;
        }
        //пошук в глибину???
        public int manh_distance_search(int[,] map_search)
        {
            int[] row = new int[16] { 3, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 };
            int[] col = new int[16] { 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2 };

            int r = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map_search[i, j] != 0)
                        r += Math.Abs(row[map_search[i, j]] - i) + Math.Abs(col[map_search[i, j]] - j);
            return r;
        }

        
        public void listSearch()
        {
            //LinkedList<Map> close = new LinkedList<Map>();
            //LinkedList<Map> open = new LinkedList<Map>();
            List<Map> close = new List<Map> ();
            List<Map> open = new List<Map>();
            
            //map.setH(10);
            map.setF();
            open.Add(map);
            while (open.Count > 0)
            {
                Map x = getWithMinF(open);
                //if (x.board[2, 2] == 0 && x.board[2, 3] == 15 && x.board[3, 2] == 12 && x.board[3, 3] == 11)
                //{
                //    Console.WriteLine("!!!!!!!!!!!!!");
                //    break;
                //}
                //добавити провірку на кінець
                if (x.getG() == 0)
                {
                    List<Map> z = new List<Map>();
                    x.getAllParent(z);
                    z.Reverse();
                    foreach (var item in z)
                    {
                        try
                        {
                            item.print();
                            Console.WriteLine(item.direction());

                        }
                        catch 
                        {
                        }
                        
                    }
                    //x.lastPrint();
                    break;

                }
                if (x.getF() > 25)
                {
                    Console.WriteLine("To much steps");
                    break;
                }
                open.Remove(x);
                close.Add(x);
               // List<Map> neighbors = new List<Map>();
                Map map1 = new Map(size, x);
                Map map2 = new Map(size, x);
                Map map3 = new Map(size, x);
                Map map4 = new Map(size, x);
                Array.Copy(x.board, map1.board, x.board.Length);
                Array.Copy(x.board, map2.board, x.board.Length);
                Array.Copy(x.board, map3.board, x.board.Length);
                Array.Copy(x.board, map4.board, x.board.Length);


                try
                {
                    map1.spaceX = x.spaceX;
                    map1.spaceY = x.spaceY;
                    map1.board[map1.spaceX, map1.spaceY] = map1.board[map1.spaceX + 1, map1.spaceY];
                    map1.board[map1.spaceX + 1, map1.spaceY] = 0;
                    map1.spaceX = map1.spaceX + 1;
                    map1.setF();
                    open.Add(map1);
                    //map1.print();                   

                }
                catch {
                    open.Remove(map1);
                }

                try
                {
                    map2.spaceX = x.spaceX;
                    map2.spaceY = x.spaceY;
                    map2.board[map2.spaceX, map2.spaceY] = map.board[map2.spaceX - 1, map2.spaceY];
                    map2.board[map2.spaceX - 1, map2.spaceY] = 0;
                    map2.spaceX = map2.spaceX - 1;
                    map2.setF();
                    open.Add(map2);

                }
                catch
                {
                    open.Remove(map2);
                }

                try
                {
                    map3.spaceX = x.spaceX;
                    map3.spaceY = x.spaceY;
                    map3.board[map3.spaceX, map3.spaceY] = map.board[map3.spaceX, map3.spaceY + 1];
                    map3.board[map3.spaceX, map3.spaceY + 1] = 0;
                    map3.spaceY = map3.spaceY + 1;
                    map3.setF();
                    open.Add(map3);

                }
                catch {
                    open.Remove(map3);
                }


                try
                {
                    map4.spaceX = x.spaceX;
                    map4.spaceY = x.spaceY;
                    map4.board[map4.spaceX, map4.spaceY] = map.board[map4.spaceX, map4.spaceY - 1];
                    map4.board[map4.spaceX, map4.spaceY - 1] = 0;
                    map4.spaceY = map4.spaceY - 1;
                    map4.setF();
                    open.Add(map4);

                }
                catch {
                    open.Remove(map4);
                }

            }
            Console.WriteLine("Game Over");
            //foreach (var item in close)
            //{
            //    item.print();
            //}
            Console.WriteLine(open.Count);
            Console.WriteLine(close.Count);
            

        }
        private Map getWithMinF(ICollection<Map> open)
        {
            Map res = null;
            int min = int.MaxValue;
            foreach (var item in open)
            {
                if (item.getF() < min)
                {
                    min = item.getF();
                    res = item;
                }
            }
            return res;
        }

        public void search()
        {

            int infinity = 999;
            int[,] map1 = new int[size, size];
            int[,] map2 = new int[size, size];
            int[,] map3 = new int[size, size];
            int[,] map4 = new int[size, size];

            Array.Copy(map.board, map1, map.board.Length);
            Array.Copy(map.board, map2, map.board.Length);
            Array.Copy(map.board, map3, map.board.Length);
            Array.Copy(map.board, map4, map.board.Length);

            int map1X = map.spaceX;
            int map1Y = map.spaceY;
            int map2X = map.spaceX;
            int map2Y = map.spaceY;
            int map3X = map.spaceX;
            int map3Y = map.spaceY;
            int map4X = map.spaceX;
            int map4Y = map.spaceY;
            int[] manh = new int[size];
            for (int i = 0; i < size; i++)
            {
                manh[i] = infinity;
            }
            int minManh = infinity;
            int mapNumber = 1;

            try
            {
                map1[map.spaceX, map.spaceY] = map.board[map.spaceX + 1, map.spaceY];
                map1[map.spaceX + 1, map.spaceY] = 0;
                map1X = map.spaceX + 1;
                manh[0] = manh_distance_search(map1);

            }
            catch
            {
                manh[0] = infinity;
            }

            try
            {
                map2[map.spaceX, map.spaceY] = map.board[map.spaceX - 1, map.spaceY];
                map2[map.spaceX - 1, map.spaceY] = 0;
                map2X = map.spaceX - 1;
                manh[1] = manh_distance_search(map2);
            }
            catch
            {
                manh[1] = infinity;
            }
            try
            {
                map3[map.spaceX, map.spaceY] = map.board[map.spaceX, map.spaceY + 1];
                map3[map.spaceX, map.spaceY + 1] = 0;
                map3Y = map.spaceY + 1;
                manh[2] = manh_distance_search(map3);
            }
            catch
            {
                manh[2] = infinity;
            }

            try
            {

                map4[map.spaceX, map.spaceY] = map.board[map.spaceX, map.spaceY - 1];
                map4[map.spaceX, map.spaceY - 1] = 0;
                map4Y = map.spaceY - 1;
                manh[3] = manh_distance_search(map4);

            }
            catch
            {
                manh[3] = infinity;
            }
            for (int i = 0; i < size; i++)
            {
                if (manh[i] < minManh)
                {
                    minManh = manh[i];
                    mapNumber = i + 1;
                }
            }
            if (mapNumber == 1)
            {
                Array.Copy(map1, map.board, map.board.Length);
                map.spaceX = map1X;
                map.spaceY = map1Y;
            }
            if (mapNumber == 2)
            {
                Array.Copy(map2, map.board, map.board.Length);
                map.spaceX = map2X;
                map.spaceY = map2Y;
            }
            if (mapNumber == 3)
            {
                Array.Copy(map3, map.board, map.board.Length);
                map.spaceX = map3X;
                map.spaceY = map3Y;
            }
            if (mapNumber == 4)
            {
                Array.Copy(map4, map.board, map.board.Length);
                map.spaceX = map4X;
                map.spaceY = map4Y;
            }

        }




        public bool check_numbers()
        {
            if (!(map.spaceX == size - 1 && map.spaceX == size - 1)) return false;
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (!(x == size - 1 && y == size - 1))
                        if (map.board[x, y] != coordsToPosition(x, y) + 1)
                            return false;
            return true;


        }
        public int getNumber(int position)
        {
            int x, y;
            positionToCoords(position, out x, out y);
            if (x < 0 || x >= size) return 0;
            if (y < 0 || y >= size) return 0;
            return map.board[x, y];
        }

        private int coordsToPosition(int y, int x)
        {
            if (x < 0) x = 0;
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;
            return y * size + x;
        }
        private void positionToCoords(int position, out int x, out int y)
        {
            if (position < 0) position = 0;
            if (position > size * size - 1) position = (size * size) - 1;
            x = position % size;
            y = position / size;
        }
    }
}
