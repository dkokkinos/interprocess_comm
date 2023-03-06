using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipes.Core
{
    public interface IClient : INode, IDisposable
    {
        Task Connect();
        Task Send(string message);
        event EventHandler ConnectedToServer;
        event EventHandler ClientStarted;
    }
}
