using System;

namespace Fase1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Fase 1\n");

            Console.WriteLine("Introdueix-hi els següents noms:\n");
            while (true)
            {

                string a = Console.ReadLine();
                string b = Console.ReadLine();
                string c = Console.ReadLine();
                string d = Console.ReadLine();
                string e = Console.ReadLine();
                string f = Console.ReadLine();
                Console.WriteLine("\nLes 6 ciutats introduïdes són: " + a + " , " + b + " , " + c + " , " + d + " , " + e + " i " + f + ".");
                Console.ReadLine();
                break;
            }


        }
    }
}
