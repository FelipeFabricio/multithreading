using System.Diagnostics;

namespace ParallelAndPlinq;

static class Program
{
    static void Main(string[] args)
    {
        ProcessamentoSimples();
        ProcessamentoComplexo();
    }

    #region Processamento Simples

    static void ProcessamentoSimples()
    {
        var data = GerarDadosFicticios(500000);
        var sequentialTime = ProcessamentoSequencialSimples(data);
        var parallelForTime = ProcessamentoSimplesComParallelFor(data);
        var plinqTime = ProcessamentoSimplesComPlinq(data);

        // Exibir resultados
        Console.WriteLine("\nProcessamento Simples:");
        Console.WriteLine($"Tempo de execução sequencial: {sequentialTime} ms");
        Console.WriteLine($"Tempo de execução com Parallel.For: {parallelForTime} ms");
        Console.WriteLine($"Tempo de execução com PLINQ: {plinqTime} ms");
    }
    
    // Processamento Sequencial
    static long ProcessamentoSequencialSimples(List<MockData> data)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (var item in data)
        {
            item.ProcessedValue = Math.Sqrt(item.Value); // Simula um cálculo pesado
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    // Processamento com Parallel.For
    static long ProcessamentoSimplesComParallelFor(List<MockData> data)
    {
        var stopwatch = Stopwatch.StartNew();

        Parallel.For(0, data.Count, i =>
        {
            data[i].ProcessedValue = Math.Sqrt(data[i].Value); // Simula um cálculo pesado
        });

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    // Processamento com PLINQ
    static long ProcessamentoSimplesComPlinq(List<MockData> data)
    {
        var stopwatch = Stopwatch.StartNew();

        var processedData = data.AsParallel().Select(item =>
        {
            item.ProcessedValue = Math.Sqrt(item.Value); // Simula um cálculo pesado
            return item;
        }).ToList();

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    #endregion
    
    #region Processamento Complexo

    static void ProcessamentoComplexo()
    {
        var data = GerarDadosFicticios(500000);
        var sequentialTime = ProcessamentoSequencialPesado(data);
        var parallelForTime = ProcessamentoPesadoComParallelFor(data);
        var plinqTime = ProcessamentoPesadoComPlinq(data);

        // Exibir resultados
        Console.WriteLine("\nProcessamento Complexo:");
        Console.WriteLine($"Tempo de execução sequencial: {sequentialTime} ms");
        Console.WriteLine($"Tempo de execução com Parallel.For: {parallelForTime} ms");
        Console.WriteLine($"Tempo de execução com PLINQ: {plinqTime} ms");
    }

    // Processamento Sequencial Pesado
    static long ProcessamentoSequencialPesado(List<MockData> data)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (var item in data)
        {
            // Simula um cálculo pesado com um loop intenso
            item.ProcessedValue = CalculoIntensivo(item.Value);
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    // Processamento Pesado com Parallel.For
    static long ProcessamentoPesadoComParallelFor(List<MockData> data)
    {
        var stopwatch = Stopwatch.StartNew();

        Parallel.For(0, data.Count, i => { data[i].ProcessedValue = CalculoIntensivo(data[i].Value); });

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    // Processamento Pesado com PLINQ
    static long ProcessamentoPesadoComPlinq(List<MockData> data)
    {
        var stopwatch = Stopwatch.StartNew();

        var processedData = data.AsParallel().Select(item =>
        {
            item.ProcessedValue = CalculoIntensivo(item.Value);
            return item;
        }).ToList();

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    // Simula um cálculo intensivo (mais pesado)
    static double CalculoIntensivo(int value)
    {
        double result = 0;
        for (int i = 0; i < 10000; i++)
        {
            result += Math.Sqrt(value) * Math.Sin(value) * Math.Cos(value);
        }

        return result;
    }

    #endregion
    
    // Método para gerar uma lista de dados fictícios
    static List<MockData> GerarDadosFicticios(int count)
    {
        var random = new Random();
        return Enumerable.Range(1, count)
            .Select(i => new MockData
            {
                Id = i,
                Value = random.Next(1, 1000)
            }).ToList();
    }
}

// Classe para simular dados
class MockData
{
    public int Id { get; set; }
    public int Value { get; set; }
    public double ProcessedValue { get; set; }
}