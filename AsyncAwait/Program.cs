class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("1 - Iniciando a aplicação...");

        // Chama o método assíncrono sem await (executado de forma síncrona)
        MetodoAsync();
        Console.WriteLine("2 - Método assíncrono sem await chamado.");

        // Chama o método assíncrono com await (executado de forma assíncrona)
        await MetodoAsyncComAwait();
        Console.WriteLine("3 - Método assíncrono com await chamado e finalizado.");

        Console.WriteLine("Finalizando a aplicação...");
        Console.ReadLine();
    }

    public static async Task MetodoAsync()
    {
        // Este método é marcado como async, mas se não houver await, ele será executado de forma síncrona.
        Console.WriteLine("1.1 - Executando de forma síncrona");
        
        // Simula algum processamento
        await Task.Delay(1000);  // Task.Delay é usado para mostrar a diferença
        
        Console.WriteLine("1.2 - MétodoAsync finalizado (após 1 segundos)");
    }

    public static async Task MetodoAsyncComAwait()
    {
        // Este método usa await, então será executado de forma assíncrona
        Console.WriteLine("2.1 - Iniciando execução assíncrona com await");
        
        await Task.Delay(2000);  // Simula uma operação assíncrona
        
        Console.WriteLine("2.2 - MétodoAsyncComAwait finalizado (após 2 segundos)");
    }
}