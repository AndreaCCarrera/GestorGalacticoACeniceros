using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Data
{
    public class DJson : IDObjetoGalactico
    {

        private string ruta = "JSONobjGal.json";

        public string Ruta { get => ruta; set => ruta = value; }


        public List<ObjetoGalactico> ListarElementos()

        {
            string contenidoJson = File.ReadAllText(ruta);


            List<ObjetoGalactico> objetosEspaciales = JsonConvert.DeserializeObject<List<ObjetoGalactico>>(contenidoJson);

            if(objetosEspaciales == null)
            {

                objetosEspaciales =  new List<ObjetoGalactico>();
            }

            return objetosEspaciales;
        }
        public void AnadirElemento(ObjetoGalactico og)
        {

            og.Id = UltimoId() + 1;
            List<ObjetoGalactico> listaOG = ListarElementos();
            listaOG.Add(og);
            string stringOG = JsonConvert.SerializeObject(listaOG, Formatting.Indented);
            File.WriteAllText(ruta, stringOG);
        }

        public bool BorrarElemento(int id) // Reescribir el JSON con la lista sin el elemento
        {

            Boolean borrado = false;

            string contenidoJson = File.ReadAllText(ruta);

            List<ObjetoGalactico> nuevaLista = new List<ObjetoGalactico>();

            List<ObjetoGalactico> objetosEspaciales = JsonConvert.DeserializeObject<List<ObjetoGalactico>>(contenidoJson);
            foreach(ObjetoGalactico og in objetosEspaciales)
            {
                if(og.Id!= id)
                {
                    nuevaLista.Add(og);
                }
                else
                {
                    borrado = true;
                }

                if (borrado)
                {
                    string stringOG = JsonConvert.SerializeObject(nuevaLista, Formatting.Indented);
                    File.WriteAllText(ruta, stringOG);
                }

            }

            return borrado;
        }
        public bool ModificarElemento(int id, ObjetoGalactico nuevo) 
        {

            Boolean modificado = false;

            string contenidoJson = File.ReadAllText(ruta);

            List<ObjetoGalactico> nuevaLista = new List<ObjetoGalactico>();

            List<ObjetoGalactico> objetosEspaciales = JsonConvert.DeserializeObject<List<ObjetoGalactico>>(contenidoJson);
            foreach (ObjetoGalactico og in objetosEspaciales)
            {
                if (og.Id != id)
                {
                    nuevaLista.Add(og);
                }
                else
                {
                    nuevaLista.Add(nuevo);
                    modificado = true;
                }


                if (modificado)
                {
                    string stringOG = JsonConvert.SerializeObject(nuevaLista, Formatting.Indented);
                    File.WriteAllText(ruta, stringOG);
                }

            }

            return modificado;
        }

        public ObjetoGalactico VerDetallesElemento(int id)
        {
            ObjetoGalactico objetoGalactico = null;

            string contenidoJson = File.ReadAllText(ruta);

            List<ObjetoGalactico> objetosEspaciales = JsonConvert.DeserializeObject<List<ObjetoGalactico>>(contenidoJson);
            foreach (ObjetoGalactico og in objetosEspaciales)
            {
                if (og.Id == id)
                {
                    objetoGalactico = og;
                    break;
                }

            }
                return objetoGalactico;
        }
        public List<ObjetoGalactico> MostrarAtmosferas()
        {
        
            string contenidoJson = File.ReadAllText(ruta);

            List<ObjetoGalactico> nuevaLista = new List<ObjetoGalactico>();

            List<ObjetoGalactico> objetosEspaciales = JsonConvert.DeserializeObject<List<ObjetoGalactico>>(contenidoJson);
            foreach (ObjetoGalactico og in objetosEspaciales)
            {
                if (og.Atmosfera)
                {
                    nuevaLista.Add(og);
                   
                }
            }
            return nuevaLista;
        }

        public List<ObjetoGalactico> MostrarDesdeFecha(DateTime fecha)
        {

            string contenidoJson = File.ReadAllText(ruta);

            List<ObjetoGalactico> nuevaLista = new List<ObjetoGalactico>();

            List<ObjetoGalactico> objetosEspaciales = JsonConvert.DeserializeObject<List<ObjetoGalactico>>(contenidoJson);
            foreach (ObjetoGalactico og in objetosEspaciales)
            {
                if (og.Descubrimiento > fecha)
                {
                    nuevaLista.Add(og);
                }
            }

            return nuevaLista;
        }
        public int UltimoId()
        {
            List<ObjetoGalactico> lista = ListarElementos();


            if (ListarElementos().Count == 0)
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
            // No se contempla en este formato
        }

    }
}
