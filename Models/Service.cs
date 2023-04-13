using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Service
    {

        public IWorker Worker { private get; set; }

        public Service(IWorker worker)
        {
            Worker = worker;
        }

        public void Run(IWorker worker)
        {
            Worker = worker;
            Run();
        }

        public void Run()
        {
            Worker.Work();
        }
    }
}
