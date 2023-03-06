using Communication.Core;
using NamedPipes.Core;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipes.Server
{
    public class NamedPipeServer : NamedPipeBase<NamedPipeServerStream>, IServer
    {
        public NamedPipeServer(string name)
            : base(name)
        {
        }

        public event EventHandler ClientConnected;
        private void OnClientConnected()
        {
            ClientConnected?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ServerStarted;
        private void OnServerStarted()
        {
            ServerStarted?.Invoke(this, EventArgs.Empty);
        }

        public async Task Start()
        {
            Initialize(new NamedPipeServerStream(_name, PipeDirection.InOut, 1,
                    PipeTransmissionMode.Message, PipeOptions.Asynchronous));

            try
            {
                Pipe.BeginWaitForConnection(WaitForConnectionCallBack, null);

                OnServerStarted();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void WaitForConnectionCallBack(IAsyncResult result)
        {
            Pipe.EndWaitForConnection(result);
            OnClientConnected();

            StartReading().GetAwaiter().GetResult();
        }

        public override void Dispose()
        {
            Pipe?.Disconnect();
            Pipe?.Dispose();
        }
    }
}
