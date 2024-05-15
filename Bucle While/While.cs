
class While
{
    static void Main(string[] args)
    {
        
        int numero = 0;
        int cont = 0;
        Random random = new Random(); // Instanciamos la clase random
        int numeroAleatorio = random.Next(1, 21);
        while (numero!=numeroAleatorio)
        {
            Console.WriteLine("Introduce un numero del 1 al 20");
            numero = int.Parse(Console.ReadLine());
            if (numero>numeroAleatorio)
            {
                Console.WriteLine("El numero es mas bajo");
            }
            if (numero<numeroAleatorio)
            {
                Console.WriteLine($"El numero es más alto");
            }

            cont++;
            Console.WriteLine($"Los intentos son {cont}");

            if (numero==numeroAleatorio)
            {
                Console.WriteLine($"Has acertado");
                break;
            }

        }


    }

}
