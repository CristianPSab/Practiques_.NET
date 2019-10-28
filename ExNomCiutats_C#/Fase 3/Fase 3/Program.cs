using System;
using System.Text.RegularExpressions;

namespace Fase_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fase 3\n");
            Console.WriteLine("Introdueix-hi els següents noms:\n");

            while (true)
            {

                string a = Console.ReadLine();
                string b = Console.ReadLine();
                string c = Console.ReadLine();
                string d = Console.ReadLine();
                string e = Console.ReadLine();
                string f = Console.ReadLine();

                string[] arrayCiutats = new string[] { a, b, c, d, e, f };
                string[] arrayCiutatsModificades = new string[] { a, b, c, d, e, f };



                int length = arrayCiutats.Length;

                for (int r = 0; r < length; r++)
                {

                    arrayCiutatsModificades[r] = Regex.Replace(arrayCiutats[r], @"[aAàá´ÀÁ]", "4");
                }
                Array.Sort(arrayCiutatsModificades);

                Console.Write("\nLes 6 ciutats introduïdes són: ");

                for (int i = 0; i < length; i++)
                {
                    if (arrayCiutatsModificades.Length - 2 == i)
                    {
                        Console.Write(arrayCiutatsModificades[i] + " i ");

                    }
                    else if (arrayCiutatsModificades.Length - 1 == i)
                    {
                        Console.Write(arrayCiutatsModificades[i] + ". ");

                    }
                    else
                    {
                        Console.Write(arrayCiutatsModificades[i] + " , ");

                    }
                }

                Console.ReadLine();
                break;
            }
        }
    }
}
