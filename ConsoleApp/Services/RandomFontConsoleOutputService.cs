using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class RandomFontConsoleOutputService : IOutputService
    {
        private IFontService _fontService;

        public RandomFontConsoleOutputService(IEnumerable<IFontService> fontServices)
        {
            Console.WriteLine("Tworzenie RandomFontConsoleOutputService");
            Thread.Sleep(1000);
            var value = new Random(DateTime.Now.Second).Next(0, fontServices.Count());

            _fontService = fontServices.Skip(value).First();
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(_fontService.Render(input));
        }
    }
}
