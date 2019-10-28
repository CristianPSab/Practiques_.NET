using System;
using System.Linq;

namespace Fase_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fase 4\n");

            Console.WriteLine("Introdueix-hi els següents noms:\n");

            while (true)
            {

                string a = Console.ReadLine();
                string b = Console.ReadLine();
                string c = Console.ReadLine();
                string d = Console.ReadLine();
                string e = Console.ReadLine();
                string f = Console.ReadLine();


                char[] ciutatA = new char[] { };
                char[] ciutatB = new char[] { };
                char[] ciutatC = new char[] { };
                char[] ciutatD = new char[] { };
                char[] ciutatE = new char[] { };
                char[] ciutatF = new char[] { };


                int length1 = a.Length;
                int length2 = b.Length;
                int length3 = c.Length;
                int length4 = d.Length;
                int length5 = e.Length;
                int length6 = f.Length;


                for (int r = 0; r < length1; r++)
                {
                    ciutatA = a.ToCharArray();
                }

                for (int r = 0; r < length2; r++)
                {
                    ciutatB = b.ToCharArray();
                }

                for (int r = 0; r < length3; r++)
                {
                    ciutatC = c.ToCharArray();
                }

                for (int r = 0; r < length4; r++)
                {
                    ciutatD = d.ToCharArray();
                }

                for (int r = 0; r < length5; r++)
                {
                    ciutatE = e.ToCharArray();
                }
                for (int r = 0; r < length6; r++)
                {
                    ciutatF = f.ToCharArray();
                }

                Console.Write("\nLes 6 ciutats introduïdes són: ");


                Console.Write(ciutatA.Reverse().ToArray());


                Console.Write(" , ");


                Console.Write(ciutatB.Reverse().ToArray());


                Console.Write(" , ");


                Console.Write(ciutatC.Reverse().ToArray());


                Console.Write(" , ");


                Console.Write(ciutatD.Reverse().ToArray());


                Console.Write(" , ");



                Console.Write(ciutatE.Reverse().ToArray());


                Console.Write(" i ");


                Console.Write(ciutatF.Reverse().ToArray());



                Console.Write(". ");
                Console.ReadLine();
                break;
            }
        }
    }
}
