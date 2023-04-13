using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class DebugOuputService : IOutputService
    {
        public void WriteLine(string input)
        {
            Debug.WriteLine(input);
        }
    }
}
