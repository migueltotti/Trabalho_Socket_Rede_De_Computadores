using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

public class OneConnection
{
    public void RunServer(int port, IPAddress ip, bool debug)
    {
        var listener = new TcpListener(ip, port);
        listener.Start();

        Console.Clear();
        Console.WriteLine($"Servidor aguardando conexão em TCP {ip}:{port}...");

        // Aceita UMA conexão
        var client = listener.AcceptTcpClient();

        var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
        var clientIp = remoteEndPoint?.Address;
        var clientPort = remoteEndPoint?.Port;

        Console.WriteLine("\nCliente conectado!");
        Console.WriteLine($"Socket: [TCP {clientIp}:{clientPort}]");

        if (debug)
        {
            Console.WriteLine(">>> Pressione qualquer tecla para continuar (tempo para rodar netstat)...");
            Console.ReadKey();
        }

        var stream = client.GetStream();

        // Recebe mensagem
        var buffer = new byte[1024];
        var bytesRead = stream.Read(buffer, 0, buffer.Length);

        var mensagem = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"Recebido do cliente: \n>> {mensagem}");

        // Envia resposta
        var resposta = Encoding.UTF8.GetBytes("Mensagem recebida com sucesso");
        stream.Write(resposta, 0, resposta.Length);

        if (debug)
        {
            Console.WriteLine(">>> Pressione qualquer tecla para encerrar conexão...");
            Console.ReadKey();
        }

        // Fecha conexão
        stream.Close();
        client.Close();

        // Para o servidor
        listener.Stop();

        Console.WriteLine("\nServidor finalizado.\n");
    }
}
