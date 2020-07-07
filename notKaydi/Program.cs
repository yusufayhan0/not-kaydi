using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using notKaydi.Model;

namespace notKaydi
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Dosyayı belirtilen klasöre atınız.");
            dosyaIzleme izle = new dosyaIzleme();
            izle.Dinle();
            Console.ReadKey();
        }
    }
}
