using System;
using System.Reflection;
using System.Text.RegularExpressions;

class Coche
{
    // Atributos
    // Propiedades públicas para acceder a los atributos privados
    public string marca
    {
        get { return marca; }
        set { marca = value; }
    }

    public string modelo
    {
        get { return modelo; }
        set { modelo = value; }
    }

    public int año
    {
        get { return año; }
        set { año = value; }
    }

    // Constructor
    public Coche(string marca, string modelo, int año)
    {
        marca = marca;
        modelo = modelo;
        año = año;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Crear objetos Coche
        Coche coche1 = new Coche("Toyota", "Corolla", 2020);
        Coche coche2 = new Coche("Ford", "Focus", 2018);
        Coche coche3 = new Coche("Honda", "Civic", 2019);

        // Mostrar información de los coches
        Console.WriteLine("Coche 1:");
        Console.WriteLine($"Marca: {coche1.marca}, Modelo: {coche1.modelo}, Año: {coche1.año}");

        Console.WriteLine("\nCoche 2:");
        Console.WriteLine($"Marca: {coche2.marca}, Modelo: {coche2.modelo}, Año: {coche2.año}");

        Console.WriteLine("\nCoche 3:");
        Console.WriteLine($"Marca: {coche3.marca}, Modelo: {coche3.modelo}, Año: {coche3.año}");
    }
}

