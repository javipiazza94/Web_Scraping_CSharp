
class For
{
    static void Main(string[] args)
    {

        // ARRAYS
        // int [] miMatriz; --> Declarar
        //miMatriz = new int[4] --> Inicializar
        // miMatriz[1] = 58 --> Asignar valor en la posicion

        var datos = new[] { 23, 78, 97679.344, 986.98 }; // --> Lo asigna a double

        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        for (int i = 0; i < array.Length; i++) 
        {
            Console.WriteLine(array[i]);
        }

        // Array de clases anonimas --> No hace falta instanciar el nombre de la clase para almacenarla en un array

        var personas = new[]
        {
            new{nombre = "Juan", edad = 23},
            new{nombre = "Pepe", edad = 83}
        };

        Console.WriteLine(personas[1]);

        foreach (var i in personas)
        {
            Console.WriteLine($"{i.edad}");
        
        }

    }

}