using ConsoleApp.Configuration.Models;
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
        private AppConfig _appConfig;
        public DebugOuputService(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public void WriteLine(string input)
        {
            Debug.WriteLine(_appConfig.Greetings.Targets.Person);
            Debug.WriteLine(input);
        }
    }
}
