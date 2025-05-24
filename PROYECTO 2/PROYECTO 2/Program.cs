using System;
using System.Threading;

class ProgramaEstacionamiento
{
    /*
    y muestra el menú principal
     */
    static void Main()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("   SISTEMA DE GESTION DE ESTACIONAMIENTO   ");
        Console.WriteLine("============================================");

        // Configuración del estacionamiento
        Console.WriteLine("\nCONFIGURACION INICIAL");
        Console.WriteLine("---------------------");
        int cant_Estacionamientos = LeerEntero("Cantidad de estacionamientos por piso (1-50):", 1, 50);
        int cant_Pisos = LeerEntero("Cantidad de pisos habilitados (1-10):", 1, 10);
        
        // Distribución de espacios
        Console.WriteLine("\nDISTRIBUCION DE ESPACIOS");
        Console.WriteLine("-----------------------");
        int cant_Moto = LeerEntero("Espacios para motos:", 0, cant_Estacionamientos * cant_Pisos);
        int cant_SUV = LeerEntero("Espacios para SUVs:", 0, cant_Estacionamientos * cant_Pisos - cant_Moto);
        
        // mapa del estacionamiento
        string[,] mapa = new string[cant_Pisos, cant_Estacionamientos];
        RellenarMapa(mapa, cant_Moto, cant_SUV);
        
        // mapa inicial y entrar al menú principal
        MostrarMapa(mapa, "todo");
        MenuPrincipal(mapa);
    }

    /*
     opciones disponibles y gestionando la navegación
     */
    static void MenuPrincipal(string[,] mapa)
    {
        // Datos para generación aleatoria de vehículos
        string[] tipos = { "moto", "sedan", "suv" };
        Random rand = new Random();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\nMENU PRINCIPAL");
            Console.WriteLine("--------------");
            Console.WriteLine("1. Ingresar vehículo manualmente");
            Console.WriteLine("2. Ingresar vehículos aleatorios");
            Console.WriteLine("3. Buscar vehículo");
            Console.WriteLine("4. Retirar vehículo");
            Console.WriteLine("5. Salir");
            
            int opcion = LeerEntero("\nSeleccione una opción (1-5):", 1, 5);

            switch (opcion)
            {
                case 1:
                    IngresarVehiculoManual(mapa);
                    break;
                case 2:
                    GenerarVehiculosAleatorios(mapa, tipos, rand);
                    break;
                case 3:
                    BuscarVehiculoPorPlaca(mapa);
                    break;
                case 4:
                    RetirarVehiculo(mapa);
                    break;
                case 5:
                    salir = ConfirmarSalida();
                    break;
            }
        }
    }

    /*
     * INGRESO MANUAL DE VEHICULO
     * solicitando todos los datos necesarios
     */
    static void IngresarVehiculoManual(string[,] mapa)
    {
        Console.WriteLine("\nINGRESO MANUAL DE VEHICULO");
        Console.WriteLine("--------------------------");
        
        // recopilación de datos del vehículo
        string marca = LeerMarca();
        string color = LeerColor();
        string placa = LeerPlaca();
        string tipo = LeerTipo();
        int hora = LeerEntero("Hora de entrada (6-20):", 6, 20);

        //espacios disponibles para el tipo seleccionado
        MostrarMapa(mapa, tipo);
        
        //aignación de espacio
        Console.Write("\nIngrese ubicación (formato piso-columna): ");
        try
        {
            var partes = Console.ReadLine().Split('-');
            if (partes.Length == 2 &&
                int.TryParse(partes[0], out int piso) &&
                int.TryParse(partes[1], out int col))
            {
                piso--; col--;
                
                if (piso >= 0 && piso < mapa.GetLength(0) && 
                    col >= 0 && col < mapa.GetLength(1))
                {
                    if (mapa[piso, col] == tipo)
                    {
                        mapa[piso, col] = placa;
                        Console.WriteLine("\nVehículo registrado exitosamente.");
                        MostrarMapa(mapa, "todo");
                        return;
                    }
                    MostrarError("El espacio no corresponde al tipo de vehículo o está ocupado");
                }
                else 
                {
                    MostrarError("Las coordenadas están fuera de rango");
                }
            }
            else 
            {
                MostrarError("Formato incorrecto. Use: piso-columna (ej: 2-5)");
            }
        }
        catch (Exception ex)
        {
            MostrarError($"Error inesperado: {ex.Message}");
        }
    }

    /*
     * GENERAR VEHICULOS ALEATORIOS
     */
    static void GenerarVehiculosAleatorios(string[,] mapa, string[] tipos, Random rand)
    {
        Console.WriteLine("\nINGRESO ALEATORIO DE VEHICULOS");
        Console.WriteLine("------------------------------");
        int cantidad = rand.Next(2, 7);
        Console.WriteLine($"Generando {cantidad} vehículos...\n");
        
        for (int i = 0; i < cantidad; i++)
        {
            string placa = GenerarPlaca(rand);
            string tipo = tipos[rand.Next(tipos.Length)];
            
            if (!AsignarEspacio(mapa, placa, tipo))
            {
                Console.WriteLine($"No hay espacio disponible para {placa} ({tipo})");
            }
        }
        
        MostrarMapa(mapa, "todo");
    }

    /*
     * BUSCAR VEHICULO POR PALCA
     */
    static void BuscarVehiculoPorPlaca(string[,] mapa)
    {
        Console.WriteLine("\nBUSQUEDA DE VEHICULO");
        Console.WriteLine("-------------------");
        Console.Write("Ingrese la placa a buscar: ");
        string placaBuscar = Console.ReadLine().ToUpper();
        
        for (int i = 0; i < mapa.GetLength(0); i++)
        {
            for (int j = 0; j < mapa.GetLength(1); j++)
            {
                if (mapa[i, j] == placaBuscar)
                {
                    Console.WriteLine($"\nVehículo encontrado:");
                    Console.WriteLine($"- Piso: {i + 1}");
                    Console.WriteLine($"- Espacio: {j + 1}");
                    return;
                }
            }
        }
        
        MostrarError("Vehículo no encontrado");
    }

    /*
     * RETIRAR VEHICULO
     */
    static void RetirarVehiculo(string[,] mapa)
    {
        try
        {
            Console.WriteLine("\nRETIRO DE VEHICULO");
            Console.WriteLine("------------------");
            Console.Write("Ingrese la placa del vehículo: ");
            string placa = Console.ReadLine().ToUpper();

            // Bucar el vehículo en el mapa
            bool encontrado = false;
            int pisoEncontrado = -1;
            int espacioEncontrado = -1;

            for (int i = 0; i < mapa.GetLength(0); i++)
            {
                for (int j = 0; j < mapa.GetLength(1); j++)
                {
                    if (mapa[i, j] == placa)
                    {
                        pisoEncontrado = i;
                        espacioEncontrado = j;
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado) break;
            }

            if (!encontrado)
            {
                MostrarError("Vehículo no encontrado");
                return;
            }

            //tiempo de estadía y monto a pagar
            Random rnd = new Random();
            int horaEntrada = rnd.Next(6, 20);
            int horaSalida = rnd.Next(horaEntrada, 24);
            int tiempoEstadia = horaSalida - horaEntrada;

            int montoAPagar = CalcularTarifa(tiempoEstadia);
            Console.WriteLine("\nINFORMACION DEL VEHICULO");
            Console.WriteLine($"Placa: {placa}");
            Console.WriteLine($"Ubicación: Piso {pisoEncontrado + 1}, Espacio {espacioEncontrado + 1}");
            Console.WriteLine($"Hora de entrada: {horaEntrada}:00");
            Console.WriteLine($"Hora de salida: {horaSalida}:00");
            Console.WriteLine($"Tiempo de estadía: {tiempoEstadia} horas");
            Console.WriteLine($"Total a pagar: Q{montoAPagar}");

            if (montoAPagar == 0)
            {
                Console.WriteLine("\nNo se requiere pago. Vehículo liberado.");
                mapa[pisoEncontrado, espacioEncontrado] = DeterminarTipoPorPosicion(mapa, pisoEncontrado, espacioEncontrado);
                return;
            }

            Console.Write("\nMétodo de pago (T=Tarjeta, E=Efectivo): ");
            string metodoPago = Console.ReadLine().ToUpper();

            if (metodoPago == "T")
            {
                Console.WriteLine("\nPago con tarjeta procesado exitosamente.");
                mapa[pisoEncontrado, espacioEncontrado] = DeterminarTipoPorPosicion(mapa, pisoEncontrado, espacioEncontrado);
                Console.WriteLine("Vehículo retirado con éxito.");
            }
            else if (metodoPago == "E")
            {
                ProcesarPagoEfectivo(montoAPagar);
                mapa[pisoEncontrado, espacioEncontrado] = DeterminarTipoPorPosicion(mapa, pisoEncontrado, espacioEncontrado);
                Console.WriteLine("\nVehículo retirado con éxito.");
            }
            else
            {
                MostrarError("Opción de pago no válida");
            }
        }
        catch (Exception ex)
        {
            MostrarError($"Error: {ex.Message}");
        }
    }

    // Relleno el mapa 
    static void RellenarMapa(string[,] mapa, int motos, int suvs)
    {
        int contador = 0;
        for (int i = 0; i < mapa.GetLength(0); i++)
        {
            for (int j = 0; j < mapa.GetLength(1); j++)
            {
                mapa[i, j] = contador++ < motos ? "moto" : 
                            (contador <= motos + suvs ? "suv" : "sedan");
            }
        }
    }

    // Asignar un vehículo
    static bool AsignarEspacio(string[,] mapa, string placa, string tipo)
    {
        for (int i = 0; i < mapa.GetLength(0); i++)
        {
            for (int j = 0; j < mapa.GetLength(1); j++)
            {
                if (mapa[i, j] == tipo)
                {
                    mapa[i, j] = placa;
                    Console.WriteLine($"{placa} ({tipo}) asignado a {i + 1}-{j + 1}");
                    return true;
                }
            }
        }
        return false;
    }

    // Muestra el mapa 
    static void MostrarMapa(string[,] mapa, string filtro)
    {
        Console.WriteLine($"\nMAPA DEL ESTACIONAMIENTO - {ObtenerTituloFiltro(filtro)}");
        Console.WriteLine(new string('-', 40));
        
        for (int i = 0; i < mapa.GetLength(0); i++)
        {
            for (int j = 0; j < mapa.GetLength(1); j++)
            {
                string celda = mapa[i, j];
                Console.ForegroundColor = ObtenerColorCelda(celda);

                if (filtro == "todo")
                    Console.Write($"{celda,-8}");
                else
                    Console.Write(celda == filtro ? $"{i + 1}-{j + 1,-6}" : "  X   ");

                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    //entrada numérica dentro de un rango
    static int LeerEntero(string mensaje, int min = int.MinValue, int max = int.MaxValue)
    {
        int valor;
        while (true)
        {
            Console.Write(mensaje + " ");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out valor) && valor >= min && valor <= max)
                return valor;
                
            MostrarError($"Ingrese un número entre {min} y {max}");
        }
    }

    //marca de vehículo (solo letras)
    static string LeerMarca()
    {
        string marca;
        do
        {
            Console.Write("Marca del vehículo (solo letras): ");
            marca = Console.ReadLine().Trim();
            
            if (string.IsNullOrEmpty(marca) || !EsSoloLetras(marca))
                MostrarError("La marca debe contener solo letras y espacios");
                
        } while (string.IsNullOrEmpty(marca) || !EsSoloLetras(marca));
        
        return marca;
    }

    //color (solo letras)
    static string LeerColor()
    {
        string color;
        do
        {
            Console.Write("Color del vehículo (solo letras): ");
            color = Console.ReadLine().Trim();
            
            if (string.IsNullOrEmpty(color) || !EsSoloLetras(color))
                MostrarError("El color debe contener solo letras y espacios");
                
        } while (string.IsNullOrEmpty(color) || !EsSoloLetras(color));
        
        return color;
    }

    //placa de vehículo (6 caracteres alfanuméricos)
    static string LeerPlaca()
    {
        string placa;
        do
        {
            Console.Write("Placa (6 caracteres alfanuméricos): ");
            placa = Console.ReadLine().ToUpper();
            
            if (placa.Length != 6 || !EsAlfanumerico(placa))
                MostrarError("La placa debe tener exactamente 6 caracteres alfanuméricos");
                
        } while (placa.Length != 6 || !EsAlfanumerico(placa));
        
        return placa;
    }

    //tipo de vehículo
    static string LeerTipo()
    {
        string tipo;
        do
        {
            Console.Write("Tipo (moto/sedan/suv): ");
            tipo = Console.ReadLine().ToLower();
            
            if (tipo != "moto" && tipo != "suv" && tipo != "sedan")
                MostrarError("Ingrese un tipo válido (moto, sedan o suv)");
                
        } while (tipo != "moto" && tipo != "suv" && tipo != "sedan");
        
        return tipo;
    }
    // Determina el tipo de vehículo para un espacio específico
    static string DeterminarTipoPorPosicion(string[,] mapa, int i, int j)
    {
        int totalMotos = 0, totalSUVs = 0;
        foreach (var spot in mapa)
        {
            if (spot == "moto") totalMotos++;
            if (spot == "suv") totalSUVs++;
        }

        if (i * mapa.GetLength(1) + j < totalMotos) return "moto";
        if (i * mapa.GetLength(1) + j < totalMotos + totalSUVs) return "suv";
        return "sedan";
    }

    //placa aleatoria
    static string GenerarPlaca(Random rand)
    {
        const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numeros = "0123456789";
        string placa = "";
        for (int i = 0; i < 3; i++) placa += letras[rand.Next(letras.Length)];
        for (int i = 0; i < 3; i++) placa += numeros[rand.Next(numeros.Length)];
        return placa;
    }

    //tarifa según el tiempo de estadía
    static int CalcularTarifa(int horas)
    {
        if (horas <= 1) return 0;
        else if (horas <= 4) return 15;
        else if (horas <= 7) return 45;
        else if (horas <= 12) return 60;
        else return 150;
    }

    //pago en efectivo con diferentes montos
    static void ProcesarPagoEfectivo(int montoAPagar)
    {
        int totalPagado = 0;
        int[] denominaciones = { 100, 50, 20, 10, 5 };
        
        Console.WriteLine("\nDENOMINACIONES ACEPTADAS: Q100, Q50, Q20, Q10, Q5");
        
        while (totalPagado < montoAPagar)
        {
            Console.WriteLine($"\nTotal a pagar: Q{montoAPagar}");
            Console.WriteLine($"Total ingresado: Q{totalPagado}");
            Console.WriteLine($"Faltante: Q{montoAPagar - totalPagado}");
            
            Console.Write("Ingrese denominación (100,50,20,10,5): Q");
            int billete;
            while (!int.TryParse(Console.ReadLine(), out billete) || !Array.Exists(denominaciones, x => x == billete))
            {
                MostrarError("Denominación no válida");
                Console.Write("Ingrese denominación (100,50,20,10,5): Q");
            }
            
            totalPagado += billete;
        }

        if (totalPagado > montoAPagar)
        {
            int cambio = totalPagado - montoAPagar;
            Console.WriteLine($"\nCambio: Q{cambio}");
            Console.WriteLine("Desglose:");
            foreach (int denom in denominaciones)
            {
                if (cambio >= denom)
                {
                    int cantidad = cambio / denom;
                    Console.WriteLine($"Q{denom} x {cantidad}");
                    cambio %= denom;
                }
            }
        }
    }

    //cadena contiene solo letras y espacios
    static bool EsSoloLetras(string texto)
    {
        foreach (char c in texto)
        {
            if (!char.IsLetter(c) && c != ' ')
                return false;
        }
        return true;
    }

    // cadena es alfanumérica
    static bool EsAlfanumerico(string texto)
    {
        foreach (char c in texto)
        {
            if (!char.IsLetterOrDigit(c))
                return false;
        }
        return true;
    }

    //mensajes de error en color rojo
    static void MostrarError(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error: " + mensaje);
        Console.ResetColor();
    }

    //color para mostrar cada tipo de espacio
    static ConsoleColor ObtenerColorCelda(string celda)
    {
        return celda switch
        {
            "moto" => ConsoleColor.Cyan,
            "suv" => ConsoleColor.Yellow,
            "sedan" => ConsoleColor.Green,
            _ => ConsoleColor.White
        };
    }

    //título para el mapa
    static string ObtenerTituloFiltro(string filtro)
    {
        return filtro switch
        {
            "todo" => "TODOS LOS ESPACIOS",
            "moto" => "SOLO MOTOS",
            "suv" => "SOLO SUVS",
            "sedan" => "SOLO SEDAN",
            _ => "FILTRADO"
        };
    }

    // salir
    static bool ConfirmarSalida()
    {
        Console.Write("\n¿Confirmar salida? (s/n): ");
        return Console.ReadLine().ToLower() == "s";
    }
}