using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoF
{
    class Cancion
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Artista { get; set; }
        public string Album { get; set; }
        public string Genero { get; set; }
        public double Duracion { get; set; }
        public string Calidad { get; set; }
        public string Formato { get; set; }

        public Cancion(int a, string b, string c, string d, string e, double f, string g, string h)
        {
            ID = a;
            Nombre = b;
            Artista = c;
            Album = d;
            Genero = e;
            Duracion = f;
            Calidad = g;
            Formato = h;
        }


    }   
}
