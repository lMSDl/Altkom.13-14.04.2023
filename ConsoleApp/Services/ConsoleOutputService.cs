using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class ConsoleOutputService : IOutputService
    {
        private IFontService _fontService;

        public ConsoleOutputService(IFontService fontService)
        {
            Console.WriteLine("Tworzenie ConsoleOutputService");
            _fontService = fontService;
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(_fontService.Render(input));
        }
    }
}
