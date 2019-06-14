using System;
using System.Collections.Generic;
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

            Game15 myGame = new Game15(4);
            myGame.start();
            myGame.print();
            Console.WriteLine("_____________________________________________");

            myGame.shuffler(40);
            myGame.print();
            myGame.listSearch();


            Console.ReadKey();
        }
    }
}
