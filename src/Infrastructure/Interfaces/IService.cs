﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IService<T>
    {
        bool Add(T item);
    }
}
