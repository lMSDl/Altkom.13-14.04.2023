using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Configuration.Models
{
    public class Greeting
    {
        public string Greeting1 { get; set; }
        public string Greeting2 { get; set; }

        public Targets Targets { get; set; }
    }
}
