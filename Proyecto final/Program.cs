using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public class cancion
        {
            public cancion(string name, string alb, string artist, string genere, int duration) // El constructor de la clase
            {
                this.nombre = name;
                this.album = alb;
                this.artista = artist;
                this.genero = genere;
                this.duracion = duration;
            }
            public string nombre;
            public int id;
            public string album;
            public string artista;
            public string genero;
            public string formato;
            public int duracion; 
        }
    }
}
