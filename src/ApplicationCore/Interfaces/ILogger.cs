using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface ILogger<T>
    {
        void LogError(Exception e);
    }
}
