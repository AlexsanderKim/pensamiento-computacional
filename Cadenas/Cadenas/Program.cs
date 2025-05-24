// See https://aka.ms/new-console-template for more information
/*
Console.WriteLine ("ingrese un texto:");
string texto = Console.ReadLine();

for (int i = 0; i < texto.Length; i++)
{
    string caracter = texto[i].ToString();
    Console.WriteLine(caracter);
    if (texto == " ")
    {
        string contador[] texto.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"\n1. Cantidad de palabras: {contador[].Length}");
    }
}
*/

using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Ingrese un texto:");
        string texto = Console.ReadLine();
        
        // 1. Contar palabras
        string[] palabras = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"\n1. Cantidad de palabras: {palabras.Length}");
        
        // 2. Texto con primeras letras en mayúscula
        string textoModificado = "";
        foreach (string palabra in palabras)
        {
            if (palabra.Length > 0)
            {
                textoModificado += char.ToUpper(palabra[0]) + palabra.Substring(1) + " ";
            }
        }
        
        Console.WriteLine($"\n2. Texto modificado:\n{textoModificado.Trim()}");
    }
}