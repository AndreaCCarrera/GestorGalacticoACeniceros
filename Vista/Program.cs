using BBusiness;
using Model;
using System;
using System.Globalization;

namespace Vista
{
    internal class Program
    {
        private static IBusiness b;

        static void Main(string[] args)
        {
            b = new Business();

            Menu();

            Console.ReadLine();
        }

        public static void Menu()

        {

            int opcion = 11;
            while (opcion != 0)
            {
                Console.WriteLine("Introduce la opcion que deseas hacer:");
                Console.WriteLine("1 Listar elementos registrados."); // Lista completa
                Console.WriteLine("2 Añadir elemento.");
                Console.WriteLine("3 Borrar elemento.");
                Console.WriteLine("4 Modificar elemento.");
                Console.WriteLine("5 Ver detalles del elemento."); // Solo un elemento
                Console.WriteLine("6 Mostrar elementos en base a si tienen atmósfera o no."); // Boolean
                Console.WriteLine("7 Mostrar elementos descubiertos a partir de una fecha.");
                Console.WriteLine("0 Salir.");
                string input = Console.ReadLine();
                opcion = Int32.Parse(input);

                switch (opcion)
                {
                    case 1: // Listar elementos


                        if (b.ListarElementos().Count != 0)
                        {
                           foreach (ObjetoGalactico ogal in b.ListarElementos())
                            {
                                Console.WriteLine("-->" + ogal);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay elementos disponibles.");
                        }

                        break;

                    case 2: // Añadir elemento

                    //   b.AnadirElemento(new ObjetoGalactico("Satelite", "Luna", new DateTime(2000, 4, 1), 7000.9, 300000.45, false, false, false));
                    //   Console.WriteLine("Elemento añadido.");
                    //  break;

                        Console.WriteLine("Introduce el tipo de objeto galáctico");
                        string tipo = Console.ReadLine();
                        Console.WriteLine("Introduce el nombre del objeto");
                        string nombre = Console.ReadLine();
                        Console.WriteLine("Introduce la fecha de descubrimiendo en formato DD/MM/AAAA");
                        input = Console.ReadLine();
                        DateTime fecha = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Console.WriteLine("Introduce el tamaño del objeto");
                        input = Console.ReadLine();
                        double tamano = double.Parse(input, CultureInfo.InvariantCulture);
                        Console.WriteLine("Introduce la distancia del objeto a la Tierra");
                        input = Console.ReadLine();
                        double distancia = double.Parse(input, CultureInfo.InvariantCulture);
                        Console.WriteLine("Escribe true si tiene agua y false si no");
                        input = Console.ReadLine();
                        bool agua = bool.Parse(input);
                        Console.WriteLine("Escribe true si tiene vida y false si no");
                        input = Console.ReadLine();
                        bool vida = bool.Parse(input);
                        Console.WriteLine("Escribe true si tiene atmósfera y false si no");
                        input = Console.ReadLine();
                        bool atmosfera = bool.Parse(input);
                        ObjetoGalactico og = new ObjetoGalactico(tipo, nombre, fecha, tamano, distancia, agua, vida, atmosfera);

                        b.AnadirElemento(og);
                        Console.WriteLine("Elemento añadido.");

                        break;

                    case 3: //  Borrar elemento

                        Console.WriteLine("Introduce el id del objeto galáctico a borrar");
                        input = Console.ReadLine();
                        int id = Int32.Parse(input);

                        if (b.BorrarElemento(id))
                        {
                            Console.WriteLine("Elemento borrado.");

                        }
                        else
                        {

                            Console.WriteLine("No existe el elemento a borrar.");
                        }

                        break;

                    case 4: // Modificar elemento

                        Console.WriteLine("Introduce el id del objeto galáctico a modificar");
                        input = Console.ReadLine();
                        id = Int32.Parse(input);
                        Console.WriteLine("Introduce el nuevo tipo de objeto galáctico");
                        tipo = Console.ReadLine();
                        Console.WriteLine("Introduce el nuevo nombre del objeto");
                        nombre = Console.ReadLine();
                        Console.WriteLine("Introduce la nueva fecha de descubrimiento");
                        input = Console.ReadLine();
                        fecha = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Console.WriteLine("Introduce el nuevo tamaño del objeto");
                        input = Console.ReadLine();
                        tamano = double.Parse(input, CultureInfo.InvariantCulture);
                        Console.WriteLine("Introduce la nueva distancia del objeto a la Tierra");
                        input = Console.ReadLine();
                        distancia = double.Parse(input, CultureInfo.InvariantCulture);
                        Console.WriteLine("Escribe true si tiene agua y false si no");
                        input = Console.ReadLine();
                        agua = bool.Parse(input);
                        Console.WriteLine("Escribe true si tiene vida y false si no");
                        input = Console.ReadLine();
                        vida = bool.Parse(input);
                        Console.WriteLine("Escribe true si tiene atmósfera y false si no");
                        input = Console.ReadLine();
                        atmosfera = bool.Parse(input);
                        og = new ObjetoGalactico(id, tipo, nombre, fecha, tamano, distancia, agua, vida, atmosfera);

                        if (b.ModificarElemento(id, og))
                        {
                            Console.WriteLine("Elemento modificado.");

                        }
                        else
                        {

                            Console.WriteLine("No existe el elemento a modificar.");
                        }

                        break;


                    case 5: // Ver detalles de elemento

                        Console.WriteLine("Introduce el id del elemento a mostrar");
                        input = Console.ReadLine();
                        id = Int32.Parse(input);
                        if (b.VerDetallesElemento(id)!=null)
                        {
                            Console.WriteLine(b.VerDetallesElemento(id));
                        }
                        else
                        {
                            Console.WriteLine("No existe el elemento a mostrar.");
                        }
                        break;

                    case 6: // Mosotrar los elementos que tienen atmósfera

                        if (b.MostrarAtmosferas().Count != 0)
                        {
                            foreach (ObjetoGalactico ogal in b.MostrarAtmosferas())
                            {
                                Console.WriteLine("-->" + ogal);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay elementos con atmósfera.");
                        }

                        break;

                    case 7: // Mostrar elementos descubiertos a partir de una fecha

                        Console.WriteLine("Introduce la fecha desde la que quieres buscar en formato DD/MM/AAAA");
                        input = Console.ReadLine();
                        fecha = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        

                        if (b.MostrarDesdeFecha(fecha).Count != 0)
                        {
                            foreach (ObjetoGalactico ogal in b.MostrarDesdeFecha(fecha))
                            {
                                Console.WriteLine("-->" + ogal);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay elementos disponibles desde esa fecha.");
                        }

                        break;

                    case 0:

                     
                        break;

                    default:
                        Console.WriteLine("No has introducido una opción válida");
                        break;
                }

               
            }

            Console.WriteLine("¡Gracias por usar nuestra aplicación!");


        }
    }
}
