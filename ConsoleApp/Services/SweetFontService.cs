using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class SweetFontService : IFontService
    {
        public SweetFontService()
        {
            Console.WriteLine("Tworzenie SweetFontService");
        }

        public string Render(string input)
        {
            return Figgle.FiggleFonts.Sweet.Render(input);
        }
    }
}
