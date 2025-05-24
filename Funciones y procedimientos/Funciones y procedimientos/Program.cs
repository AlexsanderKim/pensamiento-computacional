// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
/*
Console.WriteLine("hola porfavor escriba su nombre: ");
string nombre = Console.ReadLine();
Saludar();

Console.WriteLine ("ahora ingrese un numero para encontrar su factorial: ");
int numero = Convert.ToInt32(Console.ReadLine());
int factorial = CalcularFactorial(numero);
Console.WriteLine ($"el factorial de {numero} es {factorial}");

void Saludar()
{
    Console.WriteLine($"Buenos dias, {nombre}");
}

int CalcularFactorial (int num)
{
    int resultado = 1;
    for (int i = num; i > 0; i--)
    {
        resultado = resultado * i;
    }
    return resultado;
}
*/
using System;

class Program
{
    static void Main()
    {
        
        // Conversión de Celsius a Fahrenheit
        Console.Write("\nIngrese la temperatura en Celsius: ");
        string Celsius = Console.ReadLine();
        if (!string.IsNullOrEmpty(Celsius))
        {
            if (double.TryParse(Celsius, out double celsius))
            {
                double fahrenheit = CelsiusAFahrenheit(celsius);
                Console.WriteLine($"{celsius}°C equivalen a {fahrenheit}°F");
            }
            else
            {
                Console.WriteLine("Entrada no válida para Celsius.");
            }
        }

        // Conversión de Fahrenheit a Celsius
        Console.Write("\nIngrese la temperatura en Fahrenheit: ");
        string Fahrenheit = Console.ReadLine();
        if (!string.IsNullOrEmpty(Fahrenheit))
        {
            if (double.TryParse(Fahrenheit, out double fahrenheit))
            {
                double celsius = FahrenheitACelsius(fahrenheit);
                Console.WriteLine($"{fahrenheit}°F equivalen a {celsius}°C");
            }
            else
            {
                Console.WriteLine("Entrada no válida para Fahrenheit.");
            }
        }

        // Mostrar información del programador
        Console.WriteLine("\nInformación del programador:");
        MostrarInformacionProgramador();

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
    
    static double CelsiusAFahrenheit(double celsius)
    {
        return (celsius * 9/5) + 32;
    }
    
    static double FahrenheitACelsius(double fahrenheit)
    {
        return (fahrenheit - 32) * 5/9;
    }
    
    static void MostrarInformacionProgramador()
    {
        Console.WriteLine("escriba su nombre");
        string nombre = Console.ReadLine();
        Console.WriteLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy"));
    }
}