using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Core
{
    public interface INode : IDisposable
    {
        event EventHandler Disconnected; 
        event EventHandler MessageReceived;
    }
}
