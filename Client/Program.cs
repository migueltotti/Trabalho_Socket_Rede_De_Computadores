using System.Net.Sockets;
using System.Text;

namespace Client;

internal class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        var serverHostname = "localhost"; // 127.0.0.1
        var serverPort = 5000;

        var client = new TcpClient(serverHostname, serverPort);

        Console.WriteLine("\nConectado ao servidor!");
        Console.WriteLine($"{serverHostname}:{serverPort}");

        var stream = client.GetStream();

        // Envia mensagem
        Console.WriteLine("Escreva a menasgem que deseja enviar ao servidor:");
        Console.Write(">> ");
        var mensagem = Console.ReadLine() ?? "";
        var dados = Encoding.UTF8.GetBytes(mensagem.Replace(">>", "").Trim());

        stream.Write(dados, 0, dados.Length);

        // Recebe resposta
        var buffer = new byte[1024];
        var bytesRead = stream.Read(buffer, 0, buffer.Length);

        var resposta = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"\nResposta do servidor: {resposta}");

        // Fecha conexão
        stream.Close();
        client.Close();

        Console.WriteLine("\nCliente finalizado.\n");
    }
}
