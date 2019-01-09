using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoF
{
    class Playlist
    {
        public string nombre { get; set; }
        public string logo { get; set; }
        public int ID { get; set; }
        public List<Cancion> Cancions = new List<Cancion>();

        public Playlist(string nm, string lg, int id) {
            nombre = nm;
            logo = lg;
            ID = id;

        }

        public void AgregarCancion(Cancion cancion) {
            Cancions.Add(cancion);
        }

        public void RemoverCancion(Cancion cancion) {
            Cancions.Remove(cancion);
        }

        public void CantidadCanciones() {
            Console.WriteLine("Cantidad de canciones: " + Cancions.Count);
        }

        public void ImprimirCanciones() {
            foreach (Cancion k in Cancions) //Se imprimen las canciones
            {
                Console.WriteLine("ID: " + k.ID);
                Console.WriteLine("Nombre: " + k.Nombre);
                Console.WriteLine("Artistsa: " + k.Artista);
                Console.WriteLine("Album: " + k.Album);
                Console.WriteLine("Genero: " + k.Genero);
                Console.WriteLine("Duracion: " + k.Duracion);
                Console.WriteLine("Calidad: " + k.Calidad);
                Console.WriteLine("Formato: " + k.Formato + "\n");
            }
        }

        public void PrintSpecific(string name) {
            foreach (Cancion k in Cancions) //Se imprimen las canciones
            {
                if (name == k.Nombre) {
                    Console.WriteLine("ID: " + k.ID);
                    Console.WriteLine("Nombre: " + k.Nombre);
                    Console.WriteLine("Artistsa: " + k.Artista);
                    Console.WriteLine("Album: " + k.Album);
                    Console.WriteLine("Genero: " + k.Genero);
                    Console.WriteLine("Duracion: " + k.Duracion);
                    Console.WriteLine("Calidad: " + k.Calidad);
                    Console.WriteLine("Formato: " + k.Formato + "\n");
                }
            }
        }
    }
}
