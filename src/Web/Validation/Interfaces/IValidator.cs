using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Validation.Interfaces
{
    public interface IValidator<T>
    {
        public List<string> ErrorList { get; }
        public bool IsValid(T item);
    }
}
