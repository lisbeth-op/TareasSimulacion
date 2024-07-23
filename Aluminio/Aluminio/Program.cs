using System;
using System.Collections.Generic;

public class Lingote
{
    public int Id { get; set; }
    public DateTime FechaCompra { get; set; }
    public bool Reciclado { get; set; }
}


public class Marco
{
    public int Id { get; set; }
    public DateTime FechaProduccion { get; set; }
    public DateTime FechaVenta { get; set; }
    public bool CompromisoReciclaje { get; set; }
    public bool Reciclado { get; set; }
}


public class SimulacionMarcosAluminio
{
    private List<Lingote> inventarioLingotes;
    private List<Marco> marcosProducidos;

    public SimulacionMarcosAluminio()
    {
        inventarioLingotes = new List<Lingote>();
        marcosProducidos = new List<Marco>();
    }

   
    public void ComprarLingotes(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            Lingote lingote = new Lingote
            {
                Id = inventarioLingotes.Count + 1,
                FechaCompra = DateTime.Now
            };
            inventarioLingotes.Add(lingote);
        }
        Console.WriteLine($"Se han comprado {cantidad} lingotes.");
    }

   
    public void ProducirMarcos(int cantidad)
    {
       
        int lingotesUtilizados = Math.Min(cantidad, inventarioLingotes.Count);
        for (int i = 0; i < lingotesUtilizados; i++)
        {
            Lingote lingoteUtilizado = inventarioLingotes[i];
            Marco marco = new Marco
            {
                Id = marcosProducidos.Count + 1,
                FechaProduccion = DateTime.Now,
                CompromisoReciclaje = true  
            };
            marcosProducidos.Add(marco);
            inventarioLingotes.RemoveAt(i);
        }
        Console.WriteLine($"Se han producido {lingotesUtilizados} marcos.");
    }

    
    public void VenderYReciclarMarcos()
    {
        foreach (var marco in marcosProducidos)
        {
            marco.FechaVenta = DateTime.Now;
            
            if (marco.CompromisoReciclaje && new Random().NextDouble() < 0.5) 
            {
                marco.Reciclado = true;
            }
        }
        Console.WriteLine("Se han vendido y reciclado los marcos.");
    }

    
    public void MostrarEstadoMarcos()
    {
        Console.WriteLine("Estado de los marcos producidos:");
        foreach (var marco in marcosProducidos)
        {
            Console.WriteLine($"Marco {marco.Id}: Producción - {marco.FechaProduccion}, Venta - {marco.FechaVenta}, Reciclado - {marco.Reciclado}");
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        SimulacionMarcosAluminio simulacion = new SimulacionMarcosAluminio();

        // Simular la compra de lingotes, producción de marcos y venta/reciclaje de marcos
        simulacion.ComprarLingotes(20);
        simulacion.ProducirMarcos(15);
        simulacion.VenderYReciclarMarcos();
        simulacion.MostrarEstadoMarcos();

        Console.ReadLine();
    }
}