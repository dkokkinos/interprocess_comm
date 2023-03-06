using System;
using System.Threading.Tasks;

namespace Communication.Core
{
    public interface IServer : INode, IDisposable
    {
        Task Start();
        Task Send(string message);
        event EventHandler ServerStarted;
        event EventHandler ClientConnected;
    }
}
