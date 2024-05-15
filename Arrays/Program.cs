using System;

class Arrays
{
    static void Main(string[] args)
    {
        int[] miMatriz = new int[4];
        miMatriz[0] = 58;
        miMatriz[1] = 8;
        miMatriz[2] = 87;
        miMatriz[3] = 7438;

        foreach (int num in miMatriz)
        {
            Console.WriteLine(num);
        }

        Console.WriteLine();
        OperarMatriz(miMatriz);

        Console.WriteLine();
        leerArray();

    }
    static void OperarMatriz(int[] miMatriz)
    {
        for (int i = 0; i < miMatriz.Length; i++)
        {
            miMatriz[i] *= 10;
            Console.WriteLine(miMatriz[i]);
        }
    }

    static void leerArray()
    {
        Console.WriteLine("Introduce un array manual, dime el numero de elementos que quieres");
        int respuesta = int.Parse(Console.ReadLine());
        int[] arrayManual = new int[respuesta];
        for (int i = 0;i <respuesta; i++) 
            {
                Console.WriteLine("Introduce los elementos");
                int elementos = int.Parse(Console.ReadLine());
                arrayManual[i]= elementos;
            }
    }

}