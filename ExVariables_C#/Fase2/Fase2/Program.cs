using System;

namespace Fase2
{
    class MainClass
    {
        //FASE 2
        const int anyInicial = 1948;
        const int anyTraspas = 4;
        public static void Main(string[] args)
        {
           
            Console.WriteLine("\nFASE 2");
            int any = 1984;


            int a = (any - anyInicial) / anyTraspas;

            Console.WriteLine(a + "\n");

            Console.ReadLine();
        }
    }
}
