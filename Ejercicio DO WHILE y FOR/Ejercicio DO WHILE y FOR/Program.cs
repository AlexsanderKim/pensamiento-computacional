// See https://aka.ms/new-console-template for more information
/*int numero;
//
do 
{
    Console.WriteLine ("Ingrese su edad");
    numero = Convert.ToInt32(Console.ReadLine());

    if (numero < 0)
    {
        Console.WriteLine ("Ingrese su edad de nuevo");
    }
    if (numero > 100)
    {
        Console.WriteLine ("Ingrese su edad de nuevo");
    }
} while (numero < 0 || numero > 100 );*/

Console.WriteLine ("ingrese un numero mayor a 0");
int numero;
numero = Convert.ToInt32 (Console.ReadLine ());
for (int actual = 1; actual <= 10; actual ++)
{
    Console.WriteLine ($"{numero} * {actual} = {numero * actual}");
}