using System;
using System.Collections.Generic;

namespace TorneoFutbol
{
    class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Equipo { get; set; }

        public Jugador(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Equipo = "Sin Equipo";
        }
    }

    class Program
    {
        // CONJUNTO: Nombres de equipos únicos
        static HashSet<string> equipos = new HashSet<string>();

        // MAPA: Clave = ID (int), Valor = Jugador
        static Dictionary<int, Jugador> jugadores = new Dictionary<int, Jugador>();

        static void Main(string[] args)
        {
            CargarBase();

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n--- ** MENU TORNEO DE FUTBOL ** ---");
                Console.WriteLine("1. Registrar equipo");
                Console.WriteLine("2. Registrar jugador");
                Console.WriteLine("3. Asignar jugador a equipo");
                Console.WriteLine("4. Ver reporte");
                Console.WriteLine("5. Salir");
                Console.Write("Elija una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": AgregarEquipo(); break;
                    case "2": AgregarJugador(); break;
                    case "3": AsignarEquipo(); break;
                    case "4": MostrarReporte(); break;
                    case "5": salir = true; break;
                    default: Console.WriteLine("Opción incorrecta."); break;
                }
            }
        }

        static void CargarBase()
        {
            equipos.Add("Liga de Quito");
            equipos.Add("Emelec");

            Jugador j1 = new Jugador(1, "Pablo Fiallos") { Equipo = "Barcelona" };
            Jugador j2 = new Jugador(2, "Cristian Vinueza") { Equipo = "Emelec" };

            jugadores.Add(j1.Id, j1);
            jugadores.Add(j2.Id, j2);
        }

        static void AgregarEquipo()
        {
            Console.Write("Ingrese nombre del equipo: ");
            string equipo = Console.ReadLine();

            if (equipos.Add(equipo))
            {
                Console.WriteLine("Equipo registrado correctamente.");
            }
            else
            {
                Console.WriteLine("El equipo ya existe.");
            }
        }

        static void AgregarJugador()
        {
            Console.Write("Ingrese ID del jugador: ");
            int id = int.Parse(Console.ReadLine());

            if (jugadores.ContainsKey(id))
            {
                Console.WriteLine("Ya existe un jugador con ese ID.");
            }
            else
            {
                Console.Write("Ingrese nombre del jugador: ");
                string nombre = Console.ReadLine();

                Jugador nuevo = new Jugador(id, nombre);
                jugadores.Add(id, nuevo);
                Console.WriteLine("Jugador registrado.");
            }
        }

        static void AsignarEquipo()
        {
            Console.Write("Ingrese ID del jugador: ");
            int id = int.Parse(Console.ReadLine());

            if (jugadores.ContainsKey(id))
            {
                Console.Write("Ingrese nombre del equipo: ");
                string equipo = Console.ReadLine();

                if (equipos.Contains(equipo))
                {
                    jugadores[id].Equipo = equipo;
                    Console.WriteLine("Jugador asignado exitosamente.");
                }
                else
                {
                    Console.WriteLine("El equipo no existe.");
                }
            }
            else
            {
                Console.WriteLine("El jugador no existe.");
            }
        }

        static void MostrarReporte()
        {
            Console.WriteLine("\n-- EQUIPOS REGISTRADOS --");
            foreach (string eq in equipos)
            {
                Console.WriteLine(" - " + eq);
            }

            Console.WriteLine("\n-- LISTA DE JUGADORES --");
            foreach (KeyValuePair<int, Jugador> par in jugadores)
            {
                Console.WriteLine($"ID: {par.Key} | Nombre: {par.Value.Nombre} | Equipo: {par.Value.Equipo}");
            }
        }
    }
}