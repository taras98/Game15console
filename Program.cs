using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_game
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Game15 myGame = new Game15(4);

             myGame.start();
             myGame.print();
             Console.WriteLine("_____________________________________________");


             myGame.shuffler(10);
             myGame.print();
             Console.WriteLine("MANH = " + myGame.manh_distance());
             Console.WriteLine("_____________________________________________");
             Console.WriteLine("Початок");
             Console.WriteLine("_____________________________________________");


             int i = 1;
             while (true)
             {
                 if (myGame.manh_distance() == 0)
                 {
                     Console.WriteLine("Game over");
                     break;
                 }
                 else
                 {
                     Console.WriteLine("|" + i++ + "|");
                     myGame.search();
                     myGame.print();
                     Console.WriteLine("MANH = " + myGame.manh_distance());
                     Console.WriteLine("_____________________________________________");

                 }
             }
             */
            string writePath1 = @"z1.txt";

            Game15 myGame = new Game15(4);
            myGame.start();
            myGame.print();
            Console.WriteLine("_____________________________________________");

            int steps;
            Console.WriteLine("To read from text press 0");
            Console.WriteLine("To avto shuffler write count of steps");

            steps = Convert.ToInt32(Console.ReadLine());
            if (steps == 0)
            {
                using (StreamReader sr = new StreamReader(writePath1, System.Text.Encoding.Default))
                {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] divided = line.Split(',');
                        for (int j = 0; j < 4; j++)
                        {
                            myGame.getFromTxt(i, j, Convert.ToInt32(divided[j]));
                        }
                        i++;
                    }
                    myGame.print();
                    Console.WriteLine("_____________________________________________");

                }
            }
            else
            {
                myGame.shuffler(steps);
                using (StreamWriter sw = new StreamWriter(writePath1, false, System.Text.Encoding.Default))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (j == 3)
                            {
                                sw.Write(myGame.printInTxt(i, j));
                            }
                            else sw.Write(myGame.printInTxt(i, j) + ",");
                        }
                        sw.WriteLine();
                    }
                    sw.Close();
                }
            }



            myGame.listSearch();


            Console.ReadKey();
        }
    }
}
