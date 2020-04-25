using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IApplicationLogger<T>
    {
        void LogError(Exception e);
    }
}
