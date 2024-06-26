using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Data
{
    public class DFichObjeto : IDObjetoGalactico
    {

        public string ruta = "prueba.txt";

        public string Ruta { get => ruta; set => ruta = value; }


        public List<ObjetoGalactico> ListarElementos()
        {

            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                string primeraLinea = sr.ReadLine();

                List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

                while (!sr.EndOfStream)
                {
                    string linea = sr.ReadLine();
                    string[] elementos = linea.Split(',');
                    int id = Int32.Parse(elementos[0]);
                    string tipo = elementos[1];
                    string nombre = elementos[2];
                    DateTime descubrimiento = DateTime.ParseExact(elementos[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    double tamano = double.Parse(elementos[4], CultureInfo.InvariantCulture);
                    double distanciaTierra = double.Parse(elementos[5], CultureInfo.InvariantCulture);
                    bool agua = bool.Parse(elementos[6]);
                    bool vida = bool.Parse(elementos[7]);
                    bool atmosfera = bool.Parse(elementos[8]);

                    ObjetoGalactico objeto = new ObjetoGalactico(id, tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera);
                    objetosEspaciales.Add(objeto);
                }
                return objetosEspaciales;
            }

        }

        public void AnadirElemento(ObjetoGalactico og)
        {

            og.Id = UltimoId() + 1;

            using (FileStream fs = new FileStream(ruta, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                string completo = og.FormatoCorrecto();
                sw.WriteLine(completo);
            }

        }

        public bool BorrarElemento(int id)
        {
            bool encontrado = false;
   
            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                string primeraLinea = sr.ReadLine();
                int cont = 1;

                while (!sr.EndOfStream)
                {
                    string linea = sr.ReadLine();
                    string[] elementos = linea.Split(',');
                    int ident = cont++;
                    int aux= Int32.Parse(elementos[0]);
                    string tipo = elementos[1];
                    string nombre = elementos[2];
                    DateTime descubrimiento = DateTime.ParseExact(elementos[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    double tamano = double.Parse(elementos[4], CultureInfo.InvariantCulture);
                    double distanciaTierra = double.Parse(elementos[5], CultureInfo.InvariantCulture);
                    bool agua = bool.Parse(elementos[6]);
                    bool vida = bool.Parse(elementos[7]);
                    bool atmosfera = bool.Parse(elementos[8]);

                    if (aux != id)
                    {

                    ObjetoGalactico objeto = new ObjetoGalactico(ident, tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera);
                    objetosEspaciales.Add(objeto);
                    

                    }
                    else
                    {
                        encontrado = true;
                        cont--;
                    }

                }
            }

            if (encontrado)
            {
                
                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Id,Tipo,Nombre,FechaDescubrimiento,Tamaño,DistanciaTierra,Agua,Vida,Atmósfera");
                    foreach (ObjetoGalactico og in objetosEspaciales)
                    {
                        string completo = og.FormatoCorrecto();
                        sw.WriteLine(completo);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModificarElemento(int id, ObjetoGalactico nuevo)
        {
            bool encontrado = false;
            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                string primeraLinea = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string linea = sr.ReadLine();
                    string[] elementos = linea.Split(',');
                    int ident = Int32.Parse(elementos[0]);
                    string tipo = elementos[1];
                    string nombre = elementos[2];
                    DateTime descubrimiento = DateTime.ParseExact(elementos[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    double tamano = double.Parse(elementos[4], CultureInfo.InvariantCulture);
                    double distanciaTierra = double.Parse(elementos[5], CultureInfo.InvariantCulture);
                    bool agua = bool.Parse(elementos[6]);
                    bool vida = bool.Parse(elementos[7]);
                    bool atmosfera = bool.Parse(elementos[8]);

                    if (ident == id)
                    {
                        objetosEspaciales.Add(nuevo);
                        encontrado = true;

                    }
                    else
                    {
                        ObjetoGalactico objeto = new ObjetoGalactico(ident, tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera);
                        objetosEspaciales.Add(objeto);

                    }

                }
            }

            if (encontrado)
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Id,Tipo,Nombre,FechaDescubrimiento,Tamaño,DistanciaTierra,Agua,Vida,Atmósfera");
                    foreach (ObjetoGalactico og in objetosEspaciales)
                    {
                        string completo = og.FormatoCorrecto();
                        sw.WriteLine(completo);
                    }
                }
            }

            return encontrado;
        }

        public ObjetoGalactico VerDetallesElemento(int id)
        {
            ObjetoGalactico og = null;
            List<ObjetoGalactico> objetos = ListarElementos();

            foreach (ObjetoGalactico og2 in objetos)
            {
                if (og.Id == id)
                {
                    og = og2;
                    break;
                }
            }

            return og;
        }

     
        public List<ObjetoGalactico> MostrarAtmosferas()
        {

            List<ObjetoGalactico> objetos = ListarElementos();
            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            foreach (ObjetoGalactico og in objetos)
            {
                if (og.Atmosfera)
                {
                    objetosEspaciales.Add(og);
                }
            }

            return objetosEspaciales;
        }
        public List<ObjetoGalactico> MostrarDesdeFecha(DateTime fecha)
        {

            List<ObjetoGalactico> objetos = ListarElementos();
            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            foreach (ObjetoGalactico og in objetos)
            {
                if (og.Descubrimiento>fecha)
                {
                    objetosEspaciales.Add(og);
                }
            }

            return objetosEspaciales;
        }

        public int UltimoId()
        {
            List<ObjetoGalactico> lista = ListarElementos();


            if (ListarElementos().Count==0)
            {
               return 0;
            }
            else
            {
                ObjetoGalactico ultimo = lista.Last();
                return ultimo.Id;
            }


        }
        public bool ExisteConexion()
        {

            return File.Exists(ruta);
        }

        public bool CrearData()
        {
            FileStream fs = File.Create(ruta);
            fs.Close();

            return true;

        }


        public void NormalizarData()
        {
            Boolean b = false;
            string cabecera = "Id,Tipo,Nombre,FechaDescubrimiento,Tamaño,DistanciaTierra,Agua,Vida,Atmósfera";
            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                if (sr.ReadLine() == cabecera)
                {
                    b = true;
                }
            }

            if(b==false)
            {
                    using (FileStream sf = new FileStream(ruta, FileMode.Create, FileAccess.Write))
                    using (StreamWriter sw = new StreamWriter(sf))
                    {
                        sw.WriteLine(cabecera);
                    }
                }
            }

        }
    }

