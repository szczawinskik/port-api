using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Messages
{
    public abstract class MessageBase
    {
        public string Name { get; set; }
        public abstract string Message { get; }
    }
}
