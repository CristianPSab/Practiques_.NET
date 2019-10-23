using System;
using System.Collections.Generic;

namespace Fase3
{
    class Program
    {
        static void Main(string[] args)
        {
            //FASE 3
            Console.WriteLine("FASE 3");

            List<char> nom = new List<char>();


            nom.Add('c');
            nom.Add('r');
            nom.Add('i');
            nom.Add('s');
            nom.Add('t');
            nom.Add('i');
            nom.Add('a');
            nom.Add('n');


            var diccionari = new Dictionary<char, int>();


            for (int i = 0; i < nom.Count; i++)
            {
                char extension = nom[i];

                if (diccionari.ContainsKey(extension))
                    diccionari[extension]++;
                else
                    diccionari.Add(extension, 1);
            }


            foreach (var lletra in diccionari)
            {

                Console.WriteLine(string.Format("{0}: Valores repetidos\t{1}", lletra.Key, lletra.Value));
            }

            Console.ReadKey();


        }
    }
}
