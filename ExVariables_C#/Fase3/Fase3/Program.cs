using System;

namespace Fase3
{
    class MainClass
    {
        const int anyInicial = 1948;
        const int anyTraspas = 4;

        public static void Main(string[] args)
        {


            //FASE 3
            Console.WriteLine("\nFASE 3");

            int i = anyInicial;
            String esTraspas = "Aquest any és de traspàs";
            String noEsTraspas = "Aquest any no és de traspàs";

            bool anyNaixTras;

            int any = 1984;


            for (; i <= any; i++)
            {
                anyNaixTras = i % anyTraspas == 0 && i % 100 != 0 || i % 400 == 0;


                if (anyNaixTras)
                {
                    Console.WriteLine(i + "\n");
                    Console.WriteLine(esTraspas + "\n");
                }
                else
                {
                    Console.WriteLine(i + "\n");
                    Console.WriteLine(noEsTraspas + "\n");
                }
            }
        }
    }
}
