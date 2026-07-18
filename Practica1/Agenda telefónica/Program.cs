using System;

namespace AgendaTelefonicaUEA
{
    // Estructura compuesta para almacenar datos
    public struct Direccion
    {
        public string CallePrincipal { get; set; }
        public string Ciudad { get; set; }

        public Direccion(string calle, string ciudad)
        {
            CallePrincipal = calle;
            Ciudad = ciudad;
        }
    }

    // Registro inmutable para encapsular los datos del contacto
    public record Contacto(string Nombre, string Telefono, string Correo, Direccion Ubicacion);

    class Program
    {
        // Vector de tamaño fijo 
        static Contacto[] vectorAgenda = new Contacto[50];
        static int contadorElementos = 0;

        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("====================================================");
                Console.WriteLine("        ESTRUCTURA DE DATOS(PROYECTO 1)      ");
                Console.WriteLine("          SISTEMA DE AGENDA TELEFÓNICA              ");
                Console.WriteLine("====================================================");
                Console.WriteLine("1. Ingresar un nuevo contacto");
                Console.WriteLine("2. Visualizar todos los contactos");
                Console.WriteLine("3. Consultar contacto por nombre");
                Console.WriteLine("4. Salir ");
                Console.WriteLine("====================================================");
                Console.Write("Seleccione una opción (1-4): ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) continue;

                switch (opcion)
                {
                    case 1:
                        EjecutarRegistro();
                        break;
                    case 2:
                        EjecutarVisualizacion();
                        break;
                    case 3:
                        EjecutarConsulta();
                        break;
                }
            } while (opcion != 4);
        }

        static void EjecutarRegistro()
        {
            Console.Clear();
            Console.WriteLine("--- PROCESO: REGISTRAR CONTACTO ---");

            if (contadorElementos >= vectorAgenda.Length)
            {
                Console.WriteLine("\n[ERROR]: Capacidad máxima del vector alcanzada (50 contactos).");
                Console.ReadKey();
                return;
            }

            Console.Write("Ingrese el Nombre Completo: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese el Número Telefónico: ");
            string telefono = Console.ReadLine();
            Console.Write("Ingrese el Correo Electrónico: ");
            string correo = Console.ReadLine();
            Console.Write("Ingrese la Calle Principal: ");
            string calle = Console.ReadLine();
            Console.Write("Ingrese la Ciudad de Residencia: ");
            string ciudad = Console.ReadLine();

            Direccion direccionContacto = new Direccion(calle, ciudad);
            vectorAgenda[contadorElementos] = new Contacto(nombre, telefono, correo, direccionContacto);
            contadorElementos++;
            Console.WriteLine("\n[ÉXITO]: Datos guardados correctamente en la memoria del vector.");
            Console.ReadKey();
        }

        static void EjecutarVisualizacion()
        {
            Console.Clear();
            Console.WriteLine("--- PROCESO: VISUALIZAR AGENDA ---");

            if (contadorElementos == 0)
            {
                Console.WriteLine("\nLa agenda no contiene registros actualmente.");
            }
            else
            {
                for (int i = 0; i < contadorElementos; i++)
                {
                    Console.WriteLine($"\nContacto N.°: {i + 1}");
                    Console.WriteLine($"Nombre:    {vectorAgenda[i].Nombre}");
                    Console.WriteLine($"Teléfono:  {vectorAgenda[i].Telefono}");
                    Console.WriteLine($"Correo:    {vectorAgenda[i].Correo}");
                    Console.WriteLine($"Dirección: {vectorAgenda[i].Ubicacion.CallePrincipal} - {vectorAgenda[i].Ubicacion.Ciudad}");
                    Console.WriteLine("----------------------------------------------------");
                }
            }
            Console.ReadKey();
        }

        static void EjecutarConsulta()
        {
            Console.Clear();
            Console.WriteLine("--- PROCESO: CONSULTAR POR NOMBRE ---");
            Console.Write("Escriba el criterio de búsqueda: ");
            string criterio = Console.ReadLine();
            bool coincidencia = false;

            for (int i = 0; i < contadorElementos; i++)
            {
                // Búsqueda 
                if (vectorAgenda[i].Nombre.Contains(criterio, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\n[Coincidencia Indexada en Posición {i}]:");
                    Console.WriteLine($"Nombre:   {vectorAgenda[i].Nombre}");
                    Console.WriteLine($"Teléfono: {vectorAgenda[i].Telefono}");
                    Console.WriteLine($"Correo:   {vectorAgenda[i].Correo}");
                    coincidencia = true;
                }
            }

            if (!coincidencia)
            {
                Console.WriteLine("\nNo se encontraron registros con el criterio especificado.");
            }
            Console.ReadKey();
        }
    }
}