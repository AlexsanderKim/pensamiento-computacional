// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Random generador = new Random ();
int aleatorio = generador.Next (0,51);
Console.WriteLine ("adivine el numero generado aleatoriamente");
int numRandom;
numRandom = Convert.ToInt32 (Console.ReadLine());
while (numRandom != aleatorio)
{
    if (numRandom < aleatorio )
            {
                Console.WriteLine ("el numero que elegiste es menor");
            }
    else if (numRandom > aleatorio)
            {
                Console.WriteLine ("el numero que elegiste es mayor");
            }
            numRandom = Convert.ToInt32 (Console.ReadLine ());
}
Console.WriteLine ("Felicidades el numero es " + aleatorio);