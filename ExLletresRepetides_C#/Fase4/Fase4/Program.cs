using System;
using System.Collections.Generic;
using System.Linq;

namespace Fase4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Fase 4\n");

            List<char> nom = new List<char>();


            nom.Add('C');
            nom.Add('r');
            nom.Add('i');
            nom.Add('s');
            nom.Add('t');
            nom.Add('i');
            nom.Add('a');
            nom.Add('n');
            nom.Add(' ');



            List<char> cognom = new List<char>();


            cognom.Add('P');
            cognom.Add('u');
            cognom.Add('n');
            cognom.Add('t');
            cognom.Add('í');


            nom.AddRange(cognom);
            List<char> fullName = new List<char>();



            foreach (char lletra in nom)
            {

                fullName.Add(lletra);

                if (lletra != cognom.Last())
                {
                    fullName.Add(',');
                }


            }

            Console.Write("FullName: [");
            foreach (char n in fullName)
            {
                if (n != ',')
                {
                    Console.Write("'");
                }
                Console.Write(n);
                if (n != ',')
                {
                    Console.Write("'");
                }
                else
                {
                    Console.Write(" ");
                }

            }
            Console.Write("]");



            Console.ReadLine();
        }

    }
}
