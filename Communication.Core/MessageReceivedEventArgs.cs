using System;
using System.Collections.Generic;
using System.Text;

namespace NamedPipes.Core
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; }

        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
    }
}
