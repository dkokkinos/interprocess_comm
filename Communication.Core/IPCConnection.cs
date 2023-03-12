using NamedPipes.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Core
{
    public interface IPCConnection : IDisposable
    {
        Task Send(string message);
        event EventHandler Disconnected; 
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}
