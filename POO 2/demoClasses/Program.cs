// See https://aka.ms/new-console-template for more information
/*
using System.Runtime.CompilerServices;
Carro carro1 = new Carro("Honda", "Civic", 2010);
carro1.MostrarInfo();
Console.WriteLine("--------------------");
Carro carro2 = new Carro("Toyota", "Supra", 2000);
int VelocidadCarro2 = carro2.MostrarVelocidad();
Console.WriteLine($"Velocidad del carro 2: {VelocidadCarro2}");


class Carro
{
    private string Marca;
    private string Linea;
    private int Modelo;



    public Carro(string marca, string linea, int modelo)
    {
    Marca = marca;
    Linea = linea;
    Modelo = modelo;
    }

    public void MostrarInfo()
    {
        Console.WriteLine($"Marca: {Marca}");
        Console.WriteLine($"Linea: {Linea}");
        Console.WriteLine($"Modelo: {Modelo}");
    }

    public int MostrarVelocidad()
    {
        Random aleatorio = new Random();
        int velocidad = aleatorio.Next(0, 100,+1);
        return Velocidad;
    }
}
*/

using System;

class Cilindro
{
    public double Radio;
    public double Altura;

    public double CalcularVolumen()
    {
        return 3.1416 * Radio * Radio * Altura;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("CALCULADOR DE VOLUMEN DE CILINDRO");
        
        Console.Write("Radio: ");
        double radio = Convert.ToDouble(Console.ReadLine());
        
        Console.Write("Altura: ");
        double altura = Convert.ToDouble(Console.ReadLine());

        Cilindro cil = new Cilindro();
        cil.Radio = radio;
        cil.Altura = altura;

        Console.WriteLine($"Volumen: {cil.CalcularVolumen():F2}");
    }
}