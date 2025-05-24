// See https://aka.ms/new-console-template for more information
Console.WriteLine("Ejemplo de arreglos");
/*
string[] nombres = ["Juan", "Pedro", "Luisa", "Adriana"];
string[] apellidos = new string [5];

string nombre = nombres [3];

Console.WriteLine($"Nombre seleccionado: {nombre}");
*/
/*
string[] nombres = ["Juan", "Pedro", "Luisa", "Adriana"];
string[] apellidos = new string[5];

string nombre = nombres [5];

for (int i = 0; i < apellidos.Length; i++)
{
    Console.WriteLine ($"Ingrese el apellido no. {i}:");
    apellidos[i] = Console.ReadLine();
}

for (int i = 0; i < apellidos.Length; i++)
{
    Console.WriteLine($"Apellido no. {i}" + apellidos[i]);
}
*/ 

string [] estudiantes = ["Juan", "Pedro", "Luisa", "Adriana", "Sofia"];
int [] notas = [88, 75, 96, 77, 59];

for (int i = 0; i < estudiantes.Length; i++)
{
    Console.WriteLine ($"el nombre de los estudiantes es: " + estudiantes[i] + " y sus notas son: " + notas[i]);
}
int promedio = (88 + 75 + 96 + 77 + 59 / 5);
Console.WriteLine ("promedio:" + promedio);