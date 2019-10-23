using System;
using System.Collections.Generic;

namespace Fase2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //FASE 2

            Console.WriteLine("FASE 2\n");

            List<char> nom = new List<char>();
            bool esNumero = true;

            nom.Add('c');
            nom.Add('r');
            nom.Add('i');
            nom.Add('s');
            nom.Add('t');
            nom.Add('i');
            nom.Add('a');
            nom.Add('n');
            nom.Add('1');

            foreach (char n in nom)
            {

                if (n == 'a' || n == 'e' || n == 'i' || n == 'o' || n == 'u')
                {
                    Console.Write(n + "\t");
                    Console.Write("VOCAL\n");
                }
                else if (esNumero == int.TryParse(Convert.ToString(n), out int valor))
                {
                    Console.WriteLine("Els noms de persones no contenen números!");
                }
                else
                {
                    Console.Write(n + "\t");
                    Console.Write("CONSONANT\n");
                }
            }

            Console.ReadLine();

        }
    }
}
