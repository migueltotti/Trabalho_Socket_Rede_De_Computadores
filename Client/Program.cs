using System.Net.Sockets;
using System.Text;

namespace Client;

internal class Program
{
    static void Main(string[] args)
    {
        var serverHostname = "localhost"; // 127.0.0.1
        var serverPort = 5000;

        var client = new TcpClient(serverHostname, serverPort);

        Console.WriteLine("Conectado ao servidor!");

        var stream = client.GetStream();

        // Envia mensagem
        Console.WriteLine("Escreva a menasgem que deseja enviar ao servidor:");
        var mensagem = Console.ReadLine() ?? "";
        var dados = Encoding.UTF8.GetBytes(mensagem);

        stream.Write(dados, 0, dados.Length);

        // Recebe resposta
        var buffer = new byte[1024];
        var bytesRead = stream.Read(buffer, 0, buffer.Length);

        var resposta = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"Resposta do servidor: {resposta}");

        // Fecha conexão
        stream.Close();
        client.Close();

        Console.WriteLine("Cliente finalizado.");
    }
}
