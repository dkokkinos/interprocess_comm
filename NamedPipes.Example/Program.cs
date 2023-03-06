using Communication.Core;
using NamedPipes.Client;
using NamedPipes.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipes.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServer server = new NamedPipeServer("mypipe");
            server.ServerStarted += (_, args) =>
                Console.WriteLine("SERVER => Server started.");
            server.ClientConnected += (_, args) =>
                Console.WriteLine("SERVER => A client connected.");
            server.MessageReceived += (_, args) =>
                Console.WriteLine($"SERVER => Message received from client: {(args as MessageReceivedEventArgs).Message}");
            server.Disconnected += (_, args) =>
               Console.WriteLine($"SERVER => A client disconnected.");

            await server.Start();


            IClient client = new NamedPipeClient("mypipe");
            client.ClientStarted += (_, args)
                => Console.WriteLine("CLIENT => Client started.");
            client.ConnectedToServer += (_, args)
                => Console.WriteLine("CLIENT => Client connected to server.");
            client.MessageReceived += (_, args) =>
                Console.WriteLine($"CLIENT => Message received from server: {(args as MessageReceivedEventArgs).Message}");
            client.Disconnected += (_, args) =>
               Console.WriteLine($"CLIENT => Server disconnected.");

            await client.Connect();

            while (true)
            {
                var command = Console.ReadLine();
                if (command.StartsWith("s:"))
                {
                    var message = command.Replace("s:", string.Empty);
                    if (message == "stop")
                        server.Dispose();
                    else if (message == "start")
                        await server.Start();
                    else
                        await server.Send(message);
                }
                else if (command.StartsWith("c:"))
                {
                    var message = command.Replace("c:", string.Empty);
                    if (message == "stop")
                        client.Dispose();
                    else if (message == "start")
                        await client.Connect();
                    else
                        await client.Send(message);
                }
                else
                    break;
            }
        }
    }
}
