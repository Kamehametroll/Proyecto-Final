using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoF
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<Cancion> Canciones = new List<Cancion>();  // Aqui se almacenan las canciones
            List<Playlist> Playlist = new List<Playlist>(); //Aqui se almacenan las playlist

            int IdCanciones; //De aqui saldra el ID de las canciones, cada vez que añadamos una cancion le sumaremos +1 a este contador. Modificacion: Al iniciar programa es 1.
            int IdPlaylist;
            bool salir = false;
            
            DataManager.RecuperarID(out IdCanciones, out IdPlaylist);
            DataManager.RecuperarCanciones(out Canciones);
            DataManager.RecuperarPlaylists(out Playlist);

            #region Menu principal
            while (!salir)
            {
                Console.Clear();
                int decMenu = 0;
                
                Console.WriteLine("1- Menu Canciones");
                Console.WriteLine("2- Menu Playlist");
                Console.WriteLine("3- Generar reporte");
                Console.WriteLine("4- Exportar e Importar");
                Console.WriteLine("5- Salir\n");

                if(!int.TryParse(Console.ReadLine(), out decMenu))
                    ErrorManager.ValorInvalido();

               

                switch (decMenu)
                {
                    case 1:

                        MenuCanciones(Canciones, ref IdCanciones);



                        break;

                    case 2:

                        MenuListas(Playlist, ref IdPlaylist, Canciones);
                        break;

                    case 3:

                        MenuReporte(Playlist);
                        break;


                    case 4:

                        break;
                    case 5:
                        salir = true;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Ingrese una opcion valida\n");
                        break;
                }
                
            }
            #endregion

            DataManager.Grabar(Canciones, Playlist, IdCanciones, IdPlaylist);

            Console.WriteLine("Pulse cualquier tecla para salir (incluso la de apagado :v)");
            Console.ReadKey();

        }

        private static void MenuReporte(List<Playlist> listas)
        {
            int decReporte=0;
            bool salirReporte=false;

            

            while (!salirReporte)
            {
                Console.WriteLine("1- PDF");
                Console.WriteLine("2- CSV");
                Console.WriteLine("3- Excel");
                Console.WriteLine("4- Atras\n");

                if (!int.TryParse(Console.ReadLine(), out decReporte))
                    ErrorManager.ValorInvalido();

                switch (decReporte)
                {
                    case 1:
                        DataManager.ReportePDF(listas);
                        Console.WriteLine("Reporte generado exitosamente");
                        Console.ReadKey();
                        break;

                    case 2:
                        DataManager.ReporteCSV(listas);
                        Console.WriteLine("Reporte generado exitosamente");
                        Console.ReadKey();
                        break;

                    case 3:
                        DataManager.ReporteExcel(listas, true);
                        Console.WriteLine("Reporte generado exitosamente");
                        Console.ReadKey();
                        break;

                    case 4:
                        salirReporte = true;
                        break;

                    default:
                        ErrorManager.ValorInvalido();
                        break;
                }
            }
        }

        private static List<Playlist> MenuListas(List<Playlist> Playlist, ref int IdPlaylist, List<Cancion> Canciones)
        {
            int decPlaylist = 0;
            string decPlaylist1;
            bool salirPlaylist = false;
            int cantCanciones = 0;
            int count = 0;
            int decCount = 0;

            while (!salirPlaylist)
            {
                Console.Clear();
                Console.WriteLine("1- Agregar lista de canciones");
                Console.WriteLine("2- Listar todas las listas de canciones");
                Console.WriteLine("3- Editar playlist");
                Console.WriteLine("4- Borrar playlist");
                Console.WriteLine("5- Buscar cancion en playlist");
                Console.WriteLine("6- Importar y exportar");
                Console.WriteLine("7- Salir");

                Console.WriteLine(" ");
                do
                {
                    if (decCount >= 1) Console.WriteLine("Inserte un caracter valido");
                    decPlaylist1 = Console.ReadLine();
                    decCount++;
                } while (decPlaylist1 != "1" || decPlaylist1 != "2" || decPlaylist1 != "3" || decPlaylist1 != "4" || decPlaylist1 != "5" || decPlaylist1 != "6" || decPlaylist1 != "7");
                decPlaylist = Int32.Parse(Console.ReadLine());

                switch (decPlaylist)
                {
                    case 1:

                        Playlist.Add(AgregarPlaylist(IdPlaylist));
                        cantCanciones++;
                        IdPlaylist++;
                        break;

                    case 2:

                        Console.Clear();
                        foreach (Playlist p in Playlist)
                        {
                            Console.WriteLine("ID: " + p.ID);
                            Console.WriteLine("Nombre: " + p.nombre);
                            Console.WriteLine("Logo: " + p.logo);
                            p.CantidadCanciones();
                            Console.WriteLine(" ");
                            Console.WriteLine(" ");
                        }
                        Console.ReadLine();
                        break;
                    case 3:
                        int des = 0;
                        Console.Clear();
                        Console.WriteLine("Introduzca el ID de la lista a editar");
                        int IDedit = Int32.Parse(Console.ReadLine());

                        foreach (Playlist o in Playlist)
                        {
                            if (o.ID == IDedit)
                            {
                                Console.Clear();
                                bool force = true;
                                Console.WriteLine("1- Editar nombre");
                                Console.WriteLine("2- Editar logo");
                                Console.WriteLine("3- Agregar cancion");
                                Console.WriteLine("4- Listar canciones");
                                Console.WriteLine("5- Borrar cancione");
                                Console.WriteLine("6- Atras");
                                des = Int32.Parse(Console.ReadLine());
                                switch (des)
                                {
                                    case 1:
                                        string vNom;
                                        Console.WriteLine("Ingrese el nuevo nombre");
                                        vNom = Console.ReadLine();
                                        foreach (Playlist p in Playlist)
                                        {
                                            if (p.ID == IDedit)
                                            {
                                                p.nombre = vNom;
                                            }
                                        }
                                        break;
                                    case 2:
                                        string vLogo;
                                        Console.WriteLine("Ingrese el nuevo logo");
                                        vLogo = Console.ReadLine();
                                        foreach (Playlist p in Playlist)
                                        {
                                            if (p.ID == IDedit)
                                            {
                                                p.logo = vLogo;
                                            }
                                        }
                                        break;
                                    case 3:

                                        string vNombreC;
                                        Console.WriteLine("Ingrese el nombre de la cancion a agregar:");
                                        vNombreC = Console.ReadLine();
                                        foreach (Playlist p in Playlist)
                                        {
                                            if (p.ID == IDedit)
                                            {
                                                foreach (Cancion c in Canciones)
                                                {
                                                    if (c.Nombre == vNombreC)
                                                    {
                                                        count++;
                                                        p.AgregarCancion(c);
                                                    }
                                                }
                                            }

                                        }
                                        if (count <= 0)
                                        {
                                            Console.WriteLine("Esa cancion no existe");
                                        }
                                        Console.ReadLine();
                                        break;
                                    case 4:
                                        foreach (Playlist p in Playlist)
                                        {
                                            if (p.ID == IDedit)
                                            {
                                                p.ImprimirCanciones();
                                            }
                                        }
                                        Console.ReadLine();
                                        break;
                                    case 5:
                                        Console.WriteLine("Que cancion desea remover?");
                                        string canRemover;
                                        canRemover = Console.ReadLine();
                                        foreach (Playlist p in Playlist)
                                        {
                                            if (p.ID == IDedit)
                                            {
                                                foreach (Cancion c in Canciones)
                                                {
                                                    if (canRemover == c.Nombre)
                                                    {
                                                        p.RemoverCancion(c);
                                                        count--;
                                                        Console.WriteLine("Cancion removida exitosamente");
                                                        Console.ReadLine();
                                                        force = false;
                                                    }
                                                }
                                            }

                                        }
                                        if (force)
                                        {
                                            Console.WriteLine("No se encontro la cancion que desea remover");
                                        }
                                        break;
                                    case 6:
                                        break;
                                    default:
                                        Console.WriteLine("Esa no es una opcion");
                                        break;

                                }

                                break;
                            }
                        }
                        break;
                    case 4:
                        int playremove;
                        bool force1 = false;
                        Console.WriteLine("Ingrese el ID de la Playlist que desea borrar");
                        playremove = Int32.Parse(Console.ReadLine());
                        for (int i = 0; i < Playlist.Count; i++)
                        {
                            if (Playlist[i].ID == playremove)
                            {
                                Playlist.RemoveAt(i);
                                Console.WriteLine("Playlist eliminada correctamente");
                                force1 = false;
                            }
                        }
                        if (force1)
                        {
                            Console.WriteLine("No se encontro la Playlist que desea eliminiar");
                        }
                        break;
                    case 5:
                        int idFound = 0;
                        string fSong;
                        Console.WriteLine("Ingrese el ID de la Playlist en la cual quiere buscar");
                        idFound = Int32.Parse(Console.ReadLine());
                        foreach (Playlist p in Playlist)
                        {
                            if (p.ID == idFound)
                            {
                                Console.WriteLine("Ingrese el nombre de la cancion a borrar");
                                fSong = Console.ReadLine();
                                p.PrintSpecific(fSong);
                            }
                        }
                        Console.ReadLine();
                        break;
                    case 6:
                        Console.Clear();
                        string decision;
                        decCount = 0;
                        Console.WriteLine("1- Importar");
                        Console.WriteLine("2- Exportar");
                        Console.WriteLine("3- Atras");
                        do {
                        if (decCount >= 1) Console.WriteLine("Inserte un caracter valido");
                        decision = Console.ReadLine();
                        decCount++;
                        } while (decision != "1" || decision != "2" || decision!="3");
                        if (decision == "1") //Importar
                        {
                            Console.Clear();
                            Console.WriteLine("elija el formato:\n1- JSON\n2- XML\n1- Salir");
                            decCount = 0;
                            do{
                                if (decCount >= 1) Console.WriteLine("Inserte un caracter valido");
                                decision = Console.ReadLine();
                                decCount++;
                            } while (decision != "1" || decision != "2" || decision != "3");
                            if(decision == "1")
                            {
                                DataManager.ImportarJson(ref Canciones);
                                Console.WriteLine("Ïmportacíon exitosa");
                            }
                            if (decision == "2")
                            {
                                DataManager.ImportarXML(ref Canciones);
                                Console.WriteLine("Ïmportacíon exitosa");
                            }
                        }
                        else if (decision == "2") //Exportar
                        {
                            Console.Clear();
                            Console.WriteLine("elija el formato:\n1- JSON\n2- XML\n1- Salir");
                            decCount = 0;
                            do
                            {
                                if (decCount >= 1) Console.WriteLine("Inserte un caracter valido");
                                decision = Console.ReadLine();
                                decCount++;
                            } while (decision != "1" || decision != "2" || decision != "3");
                            if (decision == "1")
                            {
                                DataManager.ExportarJson(Canciones);
                                Console.WriteLine("Ïmportacíon exitosa");
                            }
                            if (decision == "2")
                            {
                                DataManager.ExportarXML(Canciones);
                                Console.WriteLine("Ïmportacíon exitosa");
                            }
                        }
                        break;
                    case 7:
                        salirPlaylist = true;
                        break;
                }


            }

            return Playlist; 
        }

        private static List<Cancion> MenuCanciones(List<Cancion> Canciones, ref int IdCanciones)
        {
            
            int decCanciones = 0;
            bool salirCanciones = false;

            while (!salirCanciones)
            {

                Console.WriteLine("1- Agregar cancion");
                Console.WriteLine("2- Listar todas las canciones");
                Console.WriteLine("3- Editar cancion");
                Console.WriteLine("4- Borrar cancion");
                Console.WriteLine("5- Buscar");
                Console.WriteLine("6- Atras");
                Console.WriteLine(" ");

                if (!int.TryParse(Console.ReadLine(), out decCanciones))
                    ErrorManager.ValorInvalido();

                Console.Clear();
                switch (decCanciones)
                {
                    case 1:

                        Canciones.Add(AgregarCancion(IdCanciones)); //Se crea un objeto Cancion con sus respectivos parametros
                        IdCanciones++;

                        break;

                    case 2:

                        ListarCanciones(Canciones);

                        break;

                    case 3:

                        EditarCanciones(Canciones);

                        break;

                    case 4:

                        EliminarCanciones(Canciones);

                        break;

                    case 5:

                        BuscarCanciones(Canciones);

                        break;

                    case 6:

                        salirCanciones = true;

                        break;

                    default:
                        Console.WriteLine("inserte una opcion valida");
                        Console.ReadKey();
                        break;
                }
            }

            return Canciones;
        }



        #region Funciones
        static Cancion AgregarCancion(int IdCanciones)
        {

            string nombre, artista, album, genero, calidad, formato;
            double duracion;

            Console.WriteLine("Introduzca el nombre");
            nombre = Convert.ToString(Console.ReadLine());

            Console.WriteLine(" ");
            Console.WriteLine("Introduzca el artista");
            artista = Convert.ToString(Console.ReadLine());

            Console.WriteLine(" ");
            Console.WriteLine("Introduzca el album");

            album = Convert.ToString(Console.ReadLine());

            Console.WriteLine(" ");
            Console.WriteLine("Introduzca el genero");
            genero = Convert.ToString(Console.ReadLine());

            Console.WriteLine(" ");
            Console.WriteLine("Introduzca la duracion");
            duracion = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine(" ");

            Console.WriteLine("Introduzca la calidad");
            calidad = Console.ReadLine();

            Console.WriteLine(" ");

            Console.WriteLine("Introduzca el formato");
            formato = Console.ReadLine();

            Console.WriteLine(" ");

            Console.Clear();
            Console.WriteLine("Cancion agregada exitosamente");
            Console.WriteLine(" ");
            Console.ReadKey();

            Cancion cancion = new Cancion(IdCanciones, nombre, artista, album, genero, duracion, calidad, formato);
            return cancion;
        }

        static void ListarCanciones(List<Cancion> Canciones)
        {
            foreach (Cancion k in Canciones) //Se imprimen las canciones
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

            Console.ReadKey();
        }

        static void EditarCanciones(List<Cancion> Canciones)
        {
            int id, dec1 = 0;

            Console.WriteLine("Ingrese el ID de la cancion a editar:");
            id = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine("Editar:");
            Console.WriteLine("\t1- Nombre");
            Console.WriteLine("\t2- Artista");
            Console.WriteLine("\t3- Album");
            Console.WriteLine("\t4- Genero");
            Console.WriteLine("\t5- Duracion");
            Console.WriteLine("\t6- Calidad");
            Console.WriteLine("\t7- Formato");


            dec1 = int.Parse(Console.ReadLine());



            switch (dec1)
            {
                case 1:
                    string vNom;
                    Console.WriteLine("Ingrese el nombre nuevo");
                    vNom = Console.ReadLine();

                    foreach (Cancion c in Canciones)
                    {
                        if (c.ID == id) //Se busca la cancion cuyo ID sea ingresado.
                        {
                            c.Nombre = vNom;
                        }
                    }
                    break;

                case 2:
                    string vArtista; //Variable que tiene el nombre del nuevo artista que se modificara
                    Console.WriteLine("Ingrese el nuevo artista");
                    vArtista = Console.ReadLine();

                    foreach (Cancion c in Canciones)
                    {
                        if (c.ID == id) //Se busca la cancion cuyo ID sea ingresado.
                        {
                            c.Artista = vArtista;
                        }
                    }
                    break;

                case 3:
                    string vAlbum; //Variable que tiene el nombre del nuevo artista que se modificara
                    Console.WriteLine("Ingrese el nuevo nombre del album");
                    vAlbum = Console.ReadLine();

                    foreach (Cancion c in Canciones)
                    {
                        if (c.ID == id) //Se busca la cancion cuyo ID sea ingresado.
                        {
                            c.Album = vAlbum;
                        }
                    }
                    break;

                case 4:
                    string vGenero; //Variable que tiene el nombre del nuevo artista que se modificara
                    Console.WriteLine("Ingrese el nuevo genero");
                    vGenero = Console.ReadLine();

                    foreach (Cancion c in Canciones)
                    {
                        if (c.ID == id) //Se busca la cancion cuyo ID sea ingresado.
                        {
                            c.Genero = vGenero;
                        }
                    }
                    break;

                case 5:
                    double vDuracion; //Variable que tiene el nombre del nuevo artista que se modificara
                    Console.WriteLine("Ingrese la nueva duracion");
                    vDuracion = Int32.Parse(Console.ReadLine());

                    foreach (Cancion c in Canciones)
                    {
                        if (c.ID == id) //Se busca la cancion cuyo ID sea ingresado.
                        {
                            c.Duracion = vDuracion;
                        }
                    }
                    break;
                case 6:
                    string vCalidad; //Variable que tiene el nombre del nuevo artista que se modificara
                    Console.WriteLine("Ingrese la nueva calidad");
                    vCalidad = Console.ReadLine();

                    foreach (Cancion c in Canciones)
                    {
                        if (c.ID == id) //Se busca la cancion cuyo ID sea ingresado.
                        {
                            c.Calidad = vCalidad;
                        }
                    }
                    break;

                case 7:
                    string vFormato; //Variable que tiene el nombre del nuevo artista que se modificara
                    Console.WriteLine("Ingrese el nuevo formato");
                    vFormato = Console.ReadLine();

                    foreach (Cancion c in Canciones)
                    {
                        if (c.ID == id) //Se busca la cancion cuyo ID sea ingresado.
                        {
                            c.Formato = vFormato;
                        }
                    }
                    break;

                default:
                    Console.WriteLine("inserte una opcion valida");
                    break;
            }
        }

        static void EliminarCanciones(List<Cancion> Canciones)
        {
            int vElim;
            bool vConf = false;
            Console.WriteLine("Ingrese el ID de la cancion a elminar");
            vElim = int.Parse(Console.ReadLine());

            for (int i = 0; i < Canciones.Count; i++)
            {
                if (Canciones[i].ID == vElim)
                {
                    Canciones.RemoveAt(i);
                    i--;
                    vConf = true;
                }
            }
            if (vConf)
            {
                Console.WriteLine("Cancion eliminada exitosamente");
                vConf = false;
            }
            else
            {
                Console.WriteLine("No existe ninguna cancion asociada a ese ID");
            }
            Console.ReadKey();
        }

        static void BuscarCanciones(List<Cancion> Canciones)
        {
            int found = 0;
            Console.WriteLine("Buscar:\n");
            Console.WriteLine("\t1- Por Artista");
            Console.WriteLine("\t2- Por Genero");
            Console.WriteLine("\t3- Por Nombre");


            int des = Int32.Parse(Console.ReadLine());

            switch (des)
            {
                case 1:
                    Console.WriteLine("Ingrese el nombre del artista");
                    string vArtista = Console.ReadLine();
                    foreach (Cancion k in Canciones) //Se imprimen las canciones
                    {
                        if (k.Artista == vArtista)
                        {
                            Console.WriteLine("ID: " + k.ID);
                            Console.WriteLine("Nombre: " + k.Nombre);
                            Console.WriteLine("Artistsa: " + k.Artista);
                            Console.WriteLine("Album: " + k.Album);
                            Console.WriteLine("Genero: " + k.Genero);
                            Console.WriteLine("Duracion: " + k.Duracion);
                            Console.WriteLine("Calidad: " + k.Calidad);
                            Console.WriteLine("Formato: " + k.Formato + "\n");
                            found++;
                        }
                    }
                    if (found <= 0) {
                        Console.WriteLine("No se encontro ninguna cancion de ese artista");
                    }
                    break;

                case 2:
                    Console.WriteLine("Ingrese el genero de la cancion");
                    string vGenero = Console.ReadLine();
                    foreach (Cancion k in Canciones) //Se imprimen las canciones
                    {
                        if (k.Genero == vGenero)
                        {
                            Console.WriteLine("ID: " + k.ID);
                            Console.WriteLine("Nombre: " + k.Nombre);
                            Console.WriteLine("Artistsa: " + k.Artista);
                            Console.WriteLine("Album: " + k.Album);
                            Console.WriteLine("Genero: " + k.Genero);
                            Console.WriteLine("Duracion: " + k.Duracion);
                            Console.WriteLine("Calidad: " + k.Calidad);
                            Console.WriteLine("Formato: " + k.Formato + "\n");
                            found++;

                        }
                        if (found <= 0)
                        {
                            Console.WriteLine("No se encontro ninguna cancion de ese genero");
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Ingrese el nombre de la cancion");
                    string vNombre = Console.ReadLine();
                    foreach (Cancion k in Canciones) //Se imprimen las canciones
                    {
                        if (k.Nombre == vNombre)
                        {
                            Console.WriteLine("ID: " + k.ID);
                            Console.WriteLine("Nombre: " + k.Nombre);
                            Console.WriteLine("Artistsa: " + k.Artista);
                            Console.WriteLine("Album: " + k.Album);
                            Console.WriteLine("Genero: " + k.Genero);
                            Console.WriteLine("Duracion: " + k.Duracion);
                            Console.WriteLine("Calidad: " + k.Calidad);
                            Console.WriteLine("Formato: " + k.Formato + "\n");
                            found++;

                        }
                        if (found <= 0)
                        {
                            Console.WriteLine("No se encontro ninguna cancion con ese nombre");
                        }
                    }
                    break;

            }

            Console.ReadLine();
        }

        static Playlist AgregarPlaylist(int IdPlaylist) {
            

            string nombre, logo;
            Console.Clear();
            Console.WriteLine("Ingrese el nombre de la Playlist: ");
            nombre = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Ingrese el logo:");
            logo = Console.ReadLine();
            Console.WriteLine("");
            Console.Clear();
            Console.WriteLine("Playlist agregada exitosamente");
            Console.ReadLine();
            Console.Clear();
            Playlist playlist = new Playlist(nombre, logo, IdPlaylist);
            return playlist;
            
        }
        #endregion
    }
}
