using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


namespace Data
{
    public class DFichAleatObjeto : IDObjetoGalactico
    {

        public string ruta = "prueba.txt";
        public string Ruta { get => ruta; set => ruta = value; }

        public List<ObjetoGalactico> ListarElementos()
        {
            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(60, SeekOrigin.Begin);

                byte[] lectura = new byte[60];

                while (fs.Read(lectura, 0, lectura.Length) == lectura.Length)
                {
                    string og = Encoding.ASCII.GetString(lectura);

                    int act = int.Parse(og.Substring(0, 1).Trim());
                    int id = int.Parse(og.Substring(1, 4).Trim());
                    string tipo = og.Substring(5, 10).Trim();
                    string nombre = og.Substring(15, 14).Trim();
                    DateTime descubrimiento = DateTime.ParseExact(og.Substring(29, 10).Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    double tamano = double.Parse(og.Substring(39, 9).Trim(), CultureInfo.InvariantCulture);
                    double distanciaTierra = double.Parse(og.Substring(48, 9).Trim(), CultureInfo.InvariantCulture);
                    bool agua;
                    bool vida;
                    bool atmosfera;
                    if (og.Substring(57, 1).Equals("0"))
                    {
                        agua = false;
                    }
                    else
                    {
                        agua = true;
                    }
                    if (og.Substring(58, 1).Equals("0"))
                    {
                        vida = false;
                    }
                    else
                    {
                        vida = true;
                    }
                    if (og.Substring(59, 1).Equals("0"))
                    {
                        atmosfera = false;
                    }
                    else
                    {
                        atmosfera = true;
                    }

                    ObjetoGalactico objeto = new ObjetoGalactico(id, tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera);
                    if (act == 1)
                    {
                        objetosEspaciales.Add(objeto);
                    }
                }
                return objetosEspaciales;
            }

        }

        public void AnadirElemento(ObjetoGalactico og)
        {

            og.Id = UltimoId() + 1;

            using (FileStream fs = new FileStream(ruta, FileMode.Append, FileAccess.Write))

            {
                string completo = FormatoAleatorio(og);
                fs.Seek(0, SeekOrigin.End);
                byte[] escritura = new byte[60];
                escritura = Encoding.ASCII.GetBytes(completo);
                fs.Write(escritura, 0, escritura.Length);
            }

        }

        public bool BorrarElemento(int id)
        {

            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite))
            {
                if (id + 1 <= (fs.Length / 60))
                {
                    int posicion = id * 60;
                    fs.Seek(posicion, SeekOrigin.Begin);
                    fs.WriteByte(48); // 48 porque es el valor ASCII de '0'
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

        public bool ModificarElemento(int id, ObjetoGalactico nuevo)
        {
            nuevo.Id = id;

            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite))
            {
                if (id + 1 <= (fs.Length / 60))
                {
                    int posicion = id * 60;
                    fs.Seek(posicion, SeekOrigin.Begin);
                    string completo = FormatoAleatorio(nuevo);
                    byte[] escritura = new byte[60];
                    escritura = Encoding.ASCII.GetBytes(completo);
                    fs.Write(escritura, 0, escritura.Length);
                    return true;

                }
                else
                {
                    return false;
                }
            }

        }

        public ObjetoGalactico VerDetallesElemento(int id)
        {

            List<ObjetoGalactico> objetos = ListarElementos();

            foreach (ObjetoGalactico og in objetos)
            {

                if (og.Id == id)
                {
                    return og;
                }
            }
            return null;
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
                if (og.Descubrimiento > fecha)
                {
                    objetosEspaciales.Add(og);
                }
            }

            return objetosEspaciales;
        }

        public int UltimoId()
        {
            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(-60, SeekOrigin.End);

                byte[] lectura = new byte[5];
                fs.Read(lectura, 0, 5);
                string og = Encoding.ASCII.GetString(lectura);

                if (og.Substring(0, 4).Equals(" Id "))
                {
                    return 0;
                }
                else
                {

                    return Int32.Parse(og.Substring(1, 4));
                }

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
            Boolean b;
            string cabecera = " Id  Tipo      Nombre        FechaDesc Tamano   Distance AVA";
            using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            {
                byte[] cab = new byte[60];
                fs.Read(cab, 0, 60);
                string original = Encoding.ASCII.GetString(cab);
                if (original.Equals(cabecera))
                {
                    b = true;
                }
                else
                {

                    b = false;
                }
            }

            if (b == false)
            {
                using (FileStream sf = new FileStream(ruta, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(sf))
                {
                    sw.Write(cabecera);
                }
            }
        }

        public string FormatoAleatorio(ObjetoGalactico og)
        {
            string atm = "0";
            string life = "0";
            string aq = "0";
            if (og.Atmosfera == true)
            {
                atm = "1";
            }
            if (og.Vida == true)
            {
                life = "1";
            }
            if (og.Agua == true)
            {
                aq = "1";
            }


            return "1" + og.Id.ToString("0000") + og.Tipo.PadRight(10, ' ') + og.Nombre.PadRight(14, ' ') + og.Descubrimiento.ToString("dd/MM/yyyy") + og.Tamano.ToString(CultureInfo.InvariantCulture).PadRight(9, ' ') + og.DistanciaTierra.ToString(CultureInfo.InvariantCulture).PadRight(9, ' ') + aq + life + atm;

        }

    }
}

