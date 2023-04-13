using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class TengwarFontService : IFontService
    {
        public TengwarFontService()
        {
            Console.WriteLine("Tworzenie TengwarFontService");
        }

        public string Render(string input)
        {
            return Figgle.FiggleFonts.Tengwar.Render(input);
        }
    }
}
