using System;
using System.Collections.Generic;

namespace RegistroBiblioteca
{
    class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }

        public Libro(int id, string titulo)
        {
            Id = id;
            Titulo = titulo;
            Categoria = "Sin Categoría";
        }
    }

    class Program
    {
        // CONJUNTO: Categorías de libros únicas
        static HashSet<string> categorias = new HashSet<string>();

        // MAPA: Clave = ID (int), Valor = Libro
        static Dictionary<int, Libro> libros = new Dictionary<int, Libro>();

        static void Main(string[] args)
        {
            CargarBase();

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n--- MENU DE BIBLIOTECA ---");
                Console.WriteLine("1. Registrar categoría");
                Console.WriteLine("2. Registrar libro");
                Console.WriteLine("3. Asignar categoría a libro");
                Console.WriteLine("4. Ver reporte");
                Console.WriteLine("5. Salir");
                Console.Write("Elija una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1": AgregarCategoria(); break;
                    case "2": AgregarLibro(); break;
                    case "3": AsignarCategoria(); break;
                    case "4": MostrarReporte(); break;
                    case "5": salir = true; break;
                    default: Console.WriteLine("Opción incorrecta."); break;
                }
            }
        }

        static void CargarBase()
        {
            categorias.Add("Programación");
            categorias.Add("Literatura");

            // Instancia de libros corregida
            Libro l1 = new Libro(101, "Don Quijote de la Mancha") { Categoria = "Literatura" };
            Libro l2 = new Libro(102, "Estructura de datos") { Categoria = "Programación" };

            // Inserción correcta de objetos de tipo Libro
            libros.Add(l1.Id, l1);
            libros.Add(l2.Id, l2);
        }

        static void AgregarCategoria()
        {
            Console.Write("Ingrese nombre de la categoría: ");
            string categoria = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("El nombre de la categoría no puede estar vacío.");
                return;
            }

            if (categorias.Add(categoria))
            {
                Console.WriteLine("Categoría registrada correctamente.");
            }
            else
            {
                Console.WriteLine("La categoría ya existe.");
            }
        }

        static void AgregarLibro()
        {
            Console.Write("Ingrese ID del libro: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Debe ingresar un número.");
                return;
            }

            if (libros.ContainsKey(id))
            {
                Console.WriteLine("Ya existe un libro con ese ID.");
            }
            else
            {
                Console.Write("Ingrese título del libro: ");
                string titulo = Console.ReadLine() ?? "";

                Libro nuevo = new Libro(id, titulo);
                libros.Add(id, nuevo);
                Console.WriteLine("Libro registrado.");
            }
        }

        static void AsignarCategoria()
        {
            Console.Write("Ingrese ID del libro: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Debe ingresar un número.");
                return;
            }

            if (libros.ContainsKey(id))
            {
                Console.Write("Ingrese nombre de la categoría: ");
                string categoria = Console.ReadLine() ?? "";

                if (categorias.Contains(categoria))
                {
                    libros[id].Categoria = categoria;
                    Console.WriteLine("Categoría asignada exitosamente.");
                }
                else
                {
                    Console.WriteLine("La categoría no existe.");
                }
            }
            else
            {
                Console.WriteLine("El libro no existe.");
            }
        }

        static void MostrarReporte()
        {
            Console.WriteLine("\n--- CATEGORÍAS REGISTRADAS ---");
            foreach (string cat in categorias)
            {
                Console.WriteLine(" - " + cat);
            }

            Console.WriteLine("\n--- CATÁLOGO DE LIBROS ---");
            foreach (KeyValuePair<int, Libro> par in libros)
            {
                Console.WriteLine($"ID: {par.Key} | Título: {par.Value.Titulo} | Categoría: {par.Value.Categoria}");
            }
        }
    }
}