using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.BackgroundServices
{
    public class DelayBackgroundServices
    {
        public bool CanStart { get; private set; }
        public void Start()
        {
            CanStart = true;
        }
    }
}
