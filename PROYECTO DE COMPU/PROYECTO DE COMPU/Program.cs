using System;

class Program
{
    static bool CheckGameOver(int energia_Total)
    {
        if (energia_Total <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Su energía es de: " + energia_Total);
            Console.WriteLine("\n¡GAME OVER! Te has quedado sin energía.");
            Console.ResetColor();
            return true;
        }
        return false;
    }

    static void MostrarBarraProgreso(string nombre, int valor, int maximo)
    {
        int porcentaje = (int)((double)valor / maximo * 100);
        string barra = new string('■', porcentaje / 2) + new string(' ', 50 - porcentaje / 2);
        Console.WriteLine($"{nombre}: [{barra}] {porcentaje}%"); /// barra de informacion de sobre la cantidad de comida, energia y agua
    }

    static void Main()
    {
        Random rnd = new Random();
        int energia_Total = rnd.Next(60, 76);
        int comida_Total = rnd.Next(25, 31);
        int agua_Total = rnd.Next(20, 31);
        int botella_Total = 1;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Bienvenido a la Aventura en la Isla ===");
        Console.ResetColor();
        Console.WriteLine($"Energía inicial: {energia_Total} | Comida inicial: {comida_Total} | Agua inicial: {agua_Total} | Botella inicial: {botella_Total}");
        Console.WriteLine("--------------------------------------------");
        ///Mostrar valores iniciales

        for (int dia = 1; dia <= 10; dia++) ///Contador dias
        {
            Console.WriteLine($"\n=== DÍA {dia} ===");

            int opcion;
            int probabilidad_Comida;
            int probabilidad_Agua;
            double probabilidad_Nocturno = 10;
            int probabilidad_Eventualidad;
            int residuo_Comida;
            int residuo_Agua;
///Declarar variables

            do
            {
                Console.WriteLine("\nElige una acción:"); ///Menu de acciones
                Console.WriteLine("1. Buscar comida");
                Console.WriteLine("2. Buscar agua");
                Console.WriteLine("3. Explorar la isla");
                Console.WriteLine("4. Descansar");

                while (!int.TryParse(Console.ReadLine(), out opcion)) ///Convierte directamente a int y evalua
                {
                    Console.WriteLine("¡Error! Opción no válida");
                }

                string accionElegida = opcion switch
                {
                    1 => "Buscando comida...",
                    2 => "Buscando agua...",
                    3 => "Explorando la isla...",
                    4 => "Descansando...",
                    _ => "Acción no válida"
                };   ///Valor de retorno del switch

                Console.WriteLine(accionElegida);
                Thread.Sleep(2000);


                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Comida encontrada!");
                        int energiaPerdidaComida = rnd.Next(5, 16);
                        energia_Total = Math.Max(0, energia_Total - energiaPerdidaComida);
                        Console.WriteLine ($"Perdiste {energiaPerdidaComida} de energía por buscar comida");
                        probabilidad_Comida = rnd.Next(1, 101);
                        if (probabilidad_Comida <= 30)
                        {
                            comida_Total += 30;
                            Console.WriteLine("Ganaste 30 de comida por encontrar Peces");
                        }
                        else if (probabilidad_Comida <= 80)
                        {
                            comida_Total += 25;
                              Console.WriteLine("Ganaste 25 de comida por encontrar Frutas");
                        }
                        else
                        {
                            comida_Total += 10;
                           Console.WriteLine("Ganaste 10 de comida por encontrar Semillas");
                            
                        }
                        break;

                    case 2:
                        Console.WriteLine("Haz encontrado agua!");
                        int energiaPerdidaAgua = rnd.Next(5, 21);
                        energia_Total = Math.Max(0, energia_Total - energiaPerdidaAgua);
                        Console.WriteLine ($"Perdiste {energiaPerdidaAgua} de energía por buscar agua");
                        probabilidad_Agua = rnd.Next(1, 101);
                        if (probabilidad_Agua <= 80)
                        {
                            agua_Total += (20 * botella_Total);
                        }
                        else
                        {
                            energia_Total = Math.Max(0, energia_Total - 10);
                            Console.WriteLine ($"Perdiste 10 de energía por encontrar agua contaminada.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Haz explorado la isla");
                        probabilidad_Eventualidad = rnd.Next(1, 101);
                        if (probabilidad_Eventualidad <= 30)
                        {
                            energia_Total = Math.Max(0, energia_Total - 10);
                            Console.WriteLine ($"Perdiste 10 de energía al enfrentar animales salvajes.");
                        }
                        else if (probabilidad_Eventualidad <= 60)
                        {
                            energia_Total = Math.Max(0, energia_Total - 20);
                            Console.WriteLine ($"Perdiste 20 de energía al enfrentarse a terrenos peligrosos.");
                        }
                        else
                        {
                            botella_Total += 1;
                            Console.WriteLine ($"Ganaste 1 botella al explorar la isla.");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Que buen descanso...");
                        energia_Total += 20;
                        Console.WriteLine ($"Ganaste 20 de energía al descansar.");
                        probabilidad_Nocturno += 10;
                        break;

                    default:
                        Console.WriteLine("¡Error! Opción no válida");
                        break;
                }

                
               
            } while (opcion < 1 || opcion > 4);
///FINAL DEL DIA
            Console.WriteLine("\nFinal del Día");
            Console.WriteLine("Hora de consumir recursos");

            if (comida_Total < 20)
            {
                residuo_Comida = 20 - comida_Total;
                comida_Total = 0;
                energia_Total = Math.Max(0, energia_Total - residuo_Comida);
                Console.WriteLine($"No hubo suficiente comida, perdiste {residuo_Comida} de energía. Energía: {energia_Total}");
                if (CheckGameOver(energia_Total)) return;
            }
            else
            {
                comida_Total -= 20;
                Console.WriteLine($"Perdiste 20 de comida, comida restante: {comida_Total}");
            }

            if (agua_Total < 15)
            {
                residuo_Agua = 15 - agua_Total;
                agua_Total = 0;
                energia_Total = Math.Max(0, energia_Total - residuo_Agua);
                Console.WriteLine($"No hubo suficiente agua, perdiste {residuo_Agua} de energía. Energía: {energia_Total}");
                if (CheckGameOver(energia_Total)) return;
            }
            else
            {
                agua_Total -= 15;
                Console.WriteLine($"Perdiste 15 de agua, agua restante: {agua_Total}");
            }

            int probabilidad_Nocturno2 = rnd.Next(1, 101);
            int probabilidad_Nocturno3 = rnd.Next(1, 101);

            if (probabilidad_Nocturno2 <= probabilidad_Nocturno)
            {
                if (probabilidad_Nocturno3 <= 33)
                {
                    Console.WriteLine("Evento Nocturno: Lluvia");
                    agua_Total += (5 * botella_Total);
                    Console.WriteLine($"Ganaste {5 * botella_Total} agua gracias a la lluvia. Agua: {agua_Total}");
                }
                else if (probabilidad_Nocturno3 <= 66)
                {
                    Console.WriteLine("Evento Nocturno: Animales salvajes");
                    comida_Total = Math.Max(0, comida_Total - 10);
                    Console.WriteLine($"Perdiste 10 de comida. Total: {comida_Total}");
                }
                else
                {
                    Console.WriteLine("Evento Nocturno: Clima frío");
                    energia_Total = Math.Max(0, energia_Total - 10);
                    Console.WriteLine($"Perdiste 10 de energía. Total: {energia_Total}");
                    if (CheckGameOver(energia_Total)) return;
                }
            }

            Console.WriteLine("\nResumen del día:");
            MostrarBarraProgreso("Comida", comida_Total, 100);
            MostrarBarraProgreso("Agua", agua_Total, 100);
            MostrarBarraProgreso("Energía", energia_Total, 100);
            

            Console.WriteLine("--------------------------------------------");
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n¡Has completado 10 días en la isla!"); ///Mensaje si gana
        Console.ResetColor();
    }
}