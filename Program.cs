using System;
using System.Collections.Generic;

class Elemento
{
    public string Nombre { get; set; }
    public int Peso { get; set; }
    public int Calorias { get; set; }

    public Elemento(string nombre, int peso, int calorias)
    {
        Nombre = nombre;
        Peso = peso;
        Calorias = calorias;
    }
}

class Program
{
    static List<Elemento> EncontrarCombinacionOptima(List<Elemento> elementos, int pesoMaximo, int caloriasMinimas)
    {
        List<Elemento> mejorCombinacion = new List<Elemento>();
        int mejorPeso = int.MaxValue;

        int n = elementos.Count;
        int totalCombinaciones = 1 << n; // 2^n combinaciones posibles

        for (int i = 0; i < totalCombinaciones; i++)
        {
            List<Elemento> combinacionActual = new List<Elemento>();
            int pesoTotal = 0, caloriasTotal = 0;

            for (int j = 0; j < n; j++)
            {
                if ((i & (1 << j)) > 0) // Si el bit está activo, incluir el elemento
                {
                    combinacionActual.Add(elementos[j]);
                    pesoTotal += elementos[j].Peso;
                    caloriasTotal += elementos[j].Calorias;
                }
            }

            if (caloriasTotal >= caloriasMinimas && pesoTotal <= pesoMaximo && pesoTotal < mejorPeso)
            {
                mejorCombinacion = new List<Elemento>(combinacionActual);
                mejorPeso = pesoTotal;
            }
        }

        return mejorCombinacion;
    }

    static void Main()
    {
        List<Elemento> elementos = new List<Elemento>
        {
            new Elemento("E1", 5, 3),
            new Elemento("E2", 3, 5),
            new Elemento("E3", 5, 2),
            new Elemento("E4", 1, 8),
            new Elemento("E5", 2, 3)
        };

        int pesoMaximo = 10;
        int caloriasMinimas = 15;

        List<Elemento> resultado = EncontrarCombinacionOptima(elementos, pesoMaximo, caloriasMinimas);

        Console.WriteLine("\n🔹 Elementos seleccionados:");
        if (resultado.Count == 0)
        {
            Console.WriteLine("❌ No hay combinación válida.");
        }
        else
        {
            foreach (var e in resultado)
            {
                Console.WriteLine($"✅ {e.Nombre} - Peso: {e.Peso}, Calorías: {e.Calorias}");
            }
        }
    }
}
