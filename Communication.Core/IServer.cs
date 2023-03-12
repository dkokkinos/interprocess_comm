using System;
using System.Threading.Tasks;

namespace Communication.Core
{
    public interface IServer : IPCConnection
    {
        Task Start();
        event EventHandler ServerStarted;
        event EventHandler ClientConnected;
    }
}
