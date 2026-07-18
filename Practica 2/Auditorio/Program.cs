using System;
using System.Collections.Generic;
using System.Threading;

//  representa al objeto de estudio
public class Asistente
{
    public string Nombre { get; set; }
    public int ID { get; set; }

    public Asistente(int id)
    {
        ID = id;
        Nombre = "Asistente " + id;
    }
}

// Clase(lógica y la estructura de datos) 
public class GestorAuditorio
{private Queue<Asistente> colaEspera = new Queue<Asistente>();
    private int contadorAsientos = 1;
    private readonly object bloqueo = new object();

    public void EncolarAsistente(Asistente a)
    {
        colaEspera.Enqueue(a);
    }

    // Requerimiento de Reportería
    public void ConsultarCola()
    {
        Console.WriteLine("\n--- Reporte: Asistentes en espera ---");
        foreach (var a in colaEspera)
        {
            Console.WriteLine($"Pendiente: {a.Nombre}");
        }
    }
    public void RegistrarAsistente()
    {
        while (true)
        {
            Asistente actual = null;
            lock (bloqueo)
            {
                if (colaEspera.Count == 0) return;
                actual = colaEspera.Dequeue();
                
                Console.WriteLine($"{Thread.CurrentThread.Name} registró a {actual.Nombre} -> Asiento {contadorAsientos}");
                contadorAsientos++;
            }
            Thread.Sleep(100);
        }
    }
}

class Program
{ static void Main(string[] args)
    {   GestorAuditorio gestor = new GestorAuditorio();
        for (int i = 1; i <= 100; i++)
        {
            gestor.EncolarAsistente(new Asistente(i));
        }

        // Reportería antes de procesar
        gestor.ConsultarCola();

        Thread registro1 = new Thread(gestor.RegistrarAsistente) { Name = "Registrador 1" };
        Thread registro2 = new Thread(gestor.RegistrarAsistente) { Name = "Registrador 2" };
        registro1.Start();
        registro2.Start();
        registro1.Join();
        registro2.Join();

        Console.WriteLine("\nProceso finalizado. Todos los asistentes fueron registrados.");
        
    }
}