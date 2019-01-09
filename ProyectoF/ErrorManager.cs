using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoF
{
    static class ErrorManager
    {
        static public void ValorInvalido()
        {
            Console.WriteLine("Error: Inserte una opcion valida");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
