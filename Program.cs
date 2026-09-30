using System;
using System.Diagnostics;
using System.IO;

namespace PracticaArbolesBST
{
        // 1. CLASE NODO (GENÉRICA)
        public class Nodo<T> where T : IComparable<T>
    {
        public T Valor { get; set; }
        public Nodo<T>? Izquierdo { get; set; }
        public Nodo<T>? Derecho { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    // 2. CLASE ÁRBOL BINARIO DE BÚSQUEDA (BST)
   
    public class ArbolBinarioBusqueda<T> where T : IComparable<T>
    {
        public Nodo<T>? Raiz { get; private set; }

        public ArbolBinarioBusqueda()
        {
            Raiz = null;
        }

        //  Método insertar
        public void Insertar(T valor)
        {
            Raiz = InsertarRecursivo(Raiz, valor);
        }

        private Nodo<T> InsertarRecursivo(Nodo<T>? nodo, T valor)
        {
            if (nodo == null)
            {
                return new Nodo<T>(valor);
            }

            // Comparar la regla  HI < R < HD
            if (valor.CompareTo(nodo.Valor) < 0)
            {
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            }
            else if (valor.CompareTo(nodo.Valor) > 0)
            {
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
            }

            return nodo;
        }

        // Recorrido In-Orden 
        public void InOrden(Nodo<T>? nodo)
        {
            if (nodo != null)
            {
                InOrden(nodo.Izquierdo);
                Console.Write($"{nodo.Valor} ");
                InOrden(nodo.Derecho);
            }
        }

        //  Recorrido Pre-Orden 
        public void PreOrden(Nodo<T>? nodo)
        {
            if (nodo != null)
            {
                Console.Write($"{nodo.Valor} ");
                PreOrden(nodo.Izquierdo);
                PreOrden(nodo.Derecho);
            }
        }

        // Recorrido Post-Orden 
        public void PostOrden(Nodo<T>? nodo)
        {
            if (nodo != null)
            {
                PostOrden(nodo.Izquierdo);
                PostOrden(nodo.Derecho);
                Console.Write($"{nodo.Valor} ");
            }
        }

        //  Gráfica Jerárquica en Consola 
        public void MostrarGraficaConsola(Nodo<T>? nodo, string prefijo = "", bool esUltimo = true)
        {
            if (nodo != null)
            {
                Console.WriteLine($"{prefijo}{(esUltimo ? "└── " : "├── ")}{nodo.Valor}");

                prefijo += esUltimo ? "    " : "│   ";

                bool tieneHijoIzquierdo = nodo.Izquierdo != null;
                bool tieneHijoDerecho = nodo.Derecho != null;

                if (tieneHijoIzquierdo || tieneHijoDerecho)
                {
                    if (nodo.Izquierdo != null)
                    {
                        MostrarGraficaConsola(nodo.Izquierdo, prefijo, tieneHijoDerecho == false);
                    }
                    if (nodo.Derecho != null)
                    {
                        MostrarGraficaConsola(nodo.Derecho, prefijo, true);
                    }
                }
            }
        }

        // Metod de Medición
        public int ObtenerAltura(Nodo<T>? nodo)
        {
            if (nodo == null) return 0;
            int altIzq = ObtenerAltura(nodo.Izquierdo);
            int altDer = ObtenerAltura(nodo.Derecho);
            return Math.Max(altIzq, altDer) + 1;
        }

        public int ContarNodos(Nodo<T>? nodo)
        {
            if (nodo == null) return 0;
            return 1 + ContarNodos(nodo.Izquierdo) + ContarNodos(nodo.Derecho);
        }
    }

    // 3. PROGRAMA PRINCIPAL
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("======================================================");
            Console.WriteLine("=          UNIVERSIDAD ESTATAL AMAZÓNICA              =");
            Console.WriteLine("=                  Practica N 4                       =");
            Console.WriteLine("= Tema:Árboles Binarios de Busqueda a partir de .Txt  =");
            Console.WriteLine("=          Alumna:Sonia Marisol Parra Tixi            =");
            Console.WriteLine("=         Docente: Magister Santiago Nogales          =");
            Console.WriteLine("======================================================\n");

            Stopwatch cronometro = new Stopwatch();

            // EJEMPLO 1: Datos numericos
            
            string rutaNum = "ejemplo1_numeros.txt";
            if (File.Exists(rutaNum))
            {
                Console.WriteLine("--- EJEMPLO 1: CÓDIGOS NUMÉRICOS ---");
                ArbolBinarioBusqueda<int> arbolNumeros = new ArbolBinarioBusqueda<int>();

                cronometro.Start();

                string contenido = File.ReadAllText(rutaNum);
                string[] valores = contenido.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in valores)
                {
                    if (int.TryParse(item, out int num))
                    {
                        arbolNumeros.Insertar(num);
                    }
                }

                cronometro.Stop();

                Console.WriteLine("\nGráfica en Consola:");
                arbolNumeros.MostrarGraficaConsola(arbolNumeros.Raiz);

                Console.Write("\nRecorrido In-Orden: ");
                arbolNumeros.InOrden(arbolNumeros.Raiz);

                Console.Write("\nRecorrido Pre-Orden: ");
                arbolNumeros.PreOrden(arbolNumeros.Raiz);

                Console.Write("\nRecorrido Post-Orden: ");
                arbolNumeros.PostOrden(arbolNumeros.Raiz);

                Console.WriteLine($"\n\nMétricas:");
                Console.WriteLine($"- Total de Nodos: {arbolNumeros.ContarNodos(arbolNumeros.Raiz)}");
                Console.WriteLine($"- Altura del Árbol: {arbolNumeros.ObtenerAltura(arbolNumeros.Raiz)}");
                Console.WriteLine($"- Tiempo de Ejecución: {cronometro.Elapsed.TotalMilliseconds:F3} ms ({cronometro.ElapsedTicks} ticks)");
            }
            else
            {
                Console.WriteLine($"Error: No se encontró el archivo '{rutaNum}'.");
            }

            Console.WriteLine("\n----------------------------------------------------");

           // EJEMPLO 2: Cadena de texto
            // ----------------------------------------------------
            string rutaTexto = "ejemplo2_palabras.txt";
            if (File.Exists(rutaTexto))
            {
                Console.WriteLine("--- EJEMPLO 2: CATEGORÍAS TICs (TEXTO) ---");
                ArbolBinarioBusqueda<string> arbolTexto = new ArbolBinarioBusqueda<string>();

                cronometro.Restart();

                string contenido = File.ReadAllText(rutaTexto);
                string[] valores = contenido.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in valores)
                {
                    arbolTexto.Insertar(item.Trim());
                }

                cronometro.Stop();

                Console.WriteLine("\nGráfica en Consola:");
                arbolTexto.MostrarGraficaConsola(arbolTexto.Raiz);

                Console.Write("\nRecorrido In-Orden: ");
                arbolTexto.InOrden(arbolTexto.Raiz);

                Console.Write("\nRecorrido Pre-Orden: ");
                arbolTexto.PreOrden(arbolTexto.Raiz);

                Console.Write("\nRecorrido Post-Orden: ");
                arbolTexto.PostOrden(arbolTexto.Raiz);

                Console.WriteLine($"\n\nMétricas:");
                Console.WriteLine($"- Total de Nodos: {arbolTexto.ContarNodos(arbolTexto.Raiz)}");
                Console.WriteLine($"- Altura del Árbol: {arbolTexto.ObtenerAltura(arbolTexto.Raiz)}");
                Console.WriteLine($"- Tiempo de Ejecución: {cronometro.Elapsed.TotalMilliseconds:F3} ms ({cronometro.ElapsedTicks} ticks)");
            }
            else
            {
                Console.WriteLine($"Error: No se encontró el archivo '{rutaTexto}'.");
            }

            Console.WriteLine("\n====================================================");
        }
    }
}
