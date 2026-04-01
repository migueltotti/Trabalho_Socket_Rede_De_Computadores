using System.Net;

namespace Server;

internal class Program
{
    static async Task Main(string[] args)
    {
        var ip = IPAddress.Any;
        var port = 5000;

        Console.Clear();
        Console.WriteLine("Escolha o modo do servidor:");
        Console.WriteLine("S - Simples (apenas uma conexão)");
        Console.WriteLine("M - Multiplas conexões");
        Console.Write(":");
        var serverMode = Console.ReadLine()?.Replace(":","").ToUpper() ?? "S";

        switch (serverMode)
        {
            case "S":
                Console.WriteLine("\nDeseja rodar em debug? (S - Sim, N - Não):");
                var debugInput = Console.ReadLine()?.ToUpper() ?? "S";

                var simplesServer = new OneConnection();
                simplesServer.RunServer(port, ip, debugInput == "S");

                break;

            case "M":
                var complexServer = new MultipleConnection();
                await complexServer.RunServer(port, ip);

                break;
        }
    }
}
