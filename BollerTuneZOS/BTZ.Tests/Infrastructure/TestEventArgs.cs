using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTZ.Tests.Infrastructure
{
    public class TestEventArgs : EventArgs
    {
        public bool Success { get; set; }

        public List<string> ErrorMessages { get; set; } 
    }
}
