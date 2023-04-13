using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class SubZeroFontService : IFontService
    {
        public SubZeroFontService()
        {
            Console.WriteLine("Tworzenie SubZeroFontService");
        }
        public string Render(string input)
        {
            return Figgle.FiggleFonts.SubZero.Render(input);
        }
    }
}
