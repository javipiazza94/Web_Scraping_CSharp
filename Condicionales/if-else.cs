
class Condicionales
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hola mundo!");

        Console.WriteLine("Introduce tu edad");
        int edad = int.Parse(Console.ReadLine());
        Console.WriteLine($"El numero es: {edad}");

        Console.WriteLine("Dime si tienes carnet");
        string carnet = Console.ReadLine();
        Console.WriteLine($"El usuario tiene carnet: {carnet}");
        int comparacion = string.Compare(carnet, "si", true);

        if (edad>=18 && comparacion.Equals(0))
        {
            Console.WriteLine($"El usuario puede conducir");
        }
        else
        {
            if ((edad >= 18 && comparacion.Equals(-1)))
            {
                Console.WriteLine($"El usuario tiene que sacarse el carnet para poder conducir");
            } 
            else 
            {
                Console.WriteLine($"El usuario NO puede conducir");
            }
        }

    }

}
