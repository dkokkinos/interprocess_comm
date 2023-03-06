using Communication.Core;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipes.Core
{
    public abstract class NamedPipeBase<T> : INode
        where T : PipeStream
    {
        protected readonly string _name;
        protected T Pipe;
        private StreamString _stream;

        public NamedPipeBase(string pipeName)
        {
            _name = pipeName;
        }

        public event EventHandler Disconnected;
        private void OnDisconnected()
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler MessageReceived;
        private void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }

        protected void Initialize(T pipeStream)
        {
            Pipe = pipeStream;
            _stream = new StreamString(pipeStream);
        }

        protected async Task StartReading()
        {
            await Task.Factory.StartNew(async () =>
            {
                try
                {
                    while (true)
                    {
                        var message = await _stream.ReadString();
                        OnMessageReceived(message);
                    }
                }
                catch (InvalidOperationException)
                {
                    OnDisconnected();
                    Dispose();
                }
            });
        }

        public async Task Send(string message)
        {
            await _stream.WriteString(message);
        }

        public abstract void Dispose();
    }
}
