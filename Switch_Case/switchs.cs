class Switch
{
    static void Main(string[] args)
    {
        Console.WriteLine("Introduce tu edad");
        int edad = int.Parse(Console.ReadLine());

        switch (edad)
        {
            case int n when n < 18:
                Console.WriteLine("Eres un niño");
                break;
            case int n when n >= 18 && n <= 30:
                Console.WriteLine("Eres un joven");
                break;
            case int n when n > 30 && n <= 65:
                Console.WriteLine("Eres un adulto");
                break;
            case int n when n > 65:
                Console.WriteLine("Eres un viejo");
                break;
            default:
                Console.WriteLine("Eres un pichabrava");
                break;
        }

        Console.WriteLine("Introduce un mes");
        int mes = int.Parse(Console.ReadLine());

        switch (mes)
        {
            case 1:
                Console.WriteLine("Enero");
                break;
            case 2:
                Console.WriteLine("Febrero");
                break;
            case 3:
                Console.WriteLine("Marzo");
                break;
            case 4:
                Console.WriteLine("Abril");
                break;
            case 5:
                Console.WriteLine("Mayo");
                break;
            case 6:
                Console.WriteLine("Junio");
                break;
            case 7:
                Console.WriteLine("Julio");
                break;
            case 8:
                Console.WriteLine("Agosto");
                break;
            case 9:
                Console.WriteLine("Septiembre");
                break;
            case 10:
                Console.WriteLine("Octubre");
                break;
            case 11:
                Console.WriteLine("Noviembre");
                break;
            case 12:
                Console.WriteLine("Diciembre");
                break;
            default:
                Console.WriteLine("ESE MES NO EXISTE, BRIBÓN");
                break;
        }


    }

}
