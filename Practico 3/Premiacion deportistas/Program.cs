using System;
using System.Collections.Generic;

namespace PremiacionDeportes
{
    class Deportista
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Disciplina { get; set; }

        public Deportista(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Disciplina = "Sin Disciplina";
        }
    }

    class Program
    {
        // CONJUNTO: Disciplinas deportivas únicas
        static HashSet<string> disciplinas = new HashSet<string>();

        // MAPA: Clave = ID (int), Valor = Deportista
        static Dictionary<int, Deportista> deportistas = new Dictionary<int, Deportista>();

        static void Main(string[] args)
        {
            CargarBase();

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n--- ** MENU PREMIACION DEPORTIVA ** ---");
                Console.WriteLine("1. Registrar disciplina");
                Console.WriteLine("2. Registrar deportista");
                Console.WriteLine("3. Asignar disciplina a deportista");
                Console.WriteLine("4. Ver reporte");
                Console.WriteLine("5. Salir");
                Console.Write("Elija una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": AgregarDisciplina(); break;
                    case "2": AgregarDeportista(); break;
                    case "3": AsignarDisciplina(); break;
                    case "4": MostrarReporte(); break;
                    case "5": salir = true; break;
                    default: Console.WriteLine("Opción incorrecta."); break;
                }
            }
        }

        static void CargarBase()
        {
            disciplinas.Add("Atletismo");
            disciplinas.Add("Ciclismo");

            Deportista d1 = new Deportista(1, "Michael Jordan") { Disciplina = "Basket" };
            Deportista d2 = new Deportista(2, "Richard Carapaz") { Disciplina = "Ciclismo" };

            deportistas.Add(d1.Id, d1);
            deportistas.Add(d2.Id, d2);
        }

        static void AgregarDisciplina()
        {
            Console.Write("Ingrese nombre de la disciplina: ");
            string disciplina = Console.ReadLine();

            if (disciplinas.Add(disciplina))
            {
                Console.WriteLine("Disciplina registrada correctamente.");
            }
            else
            {
                Console.WriteLine("La disciplina ya existe.");
            }
        }

        static void AgregarDeportista()
        {
            Console.Write("Ingrese ID del deportista: ");
            int id = int.Parse(Console.ReadLine());

            if (deportistas.ContainsKey(id))
            {
                Console.WriteLine("Ya existe un deportista con ese ID.");
            }
            else
            {
                Console.Write("Ingrese nombre del deportista: ");
                string nombre = Console.ReadLine();

                Deportista nuevo = new Deportista(id, nombre);
                deportistas.Add(id, nuevo);
                Console.WriteLine("Deportista registrado.");
            }
        }

        static void AsignarDisciplina()
        {
            Console.Write("Ingrese ID del deportista: ");
            int id = int.Parse(Console.ReadLine());

            if (deportistas.ContainsKey(id))
            {
                Console.Write("Ingrese nombre de la disciplina: ");
                string disciplina = Console.ReadLine();

                if (disciplinas.Contains(disciplina))
                {
                    deportistas[id].Disciplina = disciplina;
                    Console.WriteLine("Disciplina asignada exitosamente.");
                }
                else
                {
                    Console.WriteLine("La disciplina no existe.");
                }
            }
            else
            {
                Console.WriteLine("El deportista no existe.");
            }
        }

        static void MostrarReporte()
        {
            Console.WriteLine("\n--- DISCIPLINAS REGISTRADAS ---");
            foreach (string dis in disciplinas)
            {
                Console.WriteLine(" - " + dis);
            }

            Console.WriteLine("\n--- LISTA DE DEPORTISTAS ---");
            foreach (KeyValuePair<int, Deportista> par in deportistas)
            {
                Console.WriteLine($"ID: {par.Key} | Nombre: {par.Value.Nombre} | Disciplina: {par.Value.Disciplina}");
            }
        }
    }
}
