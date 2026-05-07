using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

public class MultipleConnection
{
    public async Task RunServer(int port, IPAddress ip)
    {
        var listener = new TcpListener(ip, port);
        listener.Start();

        Console.Clear();
        Console.WriteLine($"Servidor escutando em {ip}:{port} usando protocolo TCP");

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();

            var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            var clientIp = remoteEndPoint?.Address;
            var clientPort = remoteEndPoint?.Port;

            Console.WriteLine("\nCliente conectado!");
            Console.WriteLine($"Socket: [TCP { clientIp}:{ clientPort}]");

            // Task.Run instancia uma nova thread para lidar com várias conexões simultâneas
            _ = Task.Run(async () =>
            {
                using var stream = client.GetStream();

                var buffer = new byte[1024];
                var bytesRead = await stream.ReadAsync(buffer);

                var received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Recebido do cliente: \n>> {received}");

                var response = Encoding.UTF8.GetBytes("Mensagem recebida pelo servidor");
                await stream.WriteAsync(response);

                client.Close();
                Console.WriteLine("\nConexão encerrada\n");
            });

        }
    }
}