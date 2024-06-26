using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Data
{
    public class DXml : IDObjetoGalactico
    {
        private int tiempo = 3000;

        XmlDocument xmlDocument;

        private string ruta = "XMLobjgal.xml";

        public string Ruta { get => ruta; set => ruta = value; }

        public void LoadXML()
        {
            xmlDocument = new XmlDocument();
            xmlDocument.Load("XMLobjgal.xml");
        }

        public List<ObjetoGalactico> ListarElementos()
        {
            Thread.Sleep(tiempo);
            LoadXML();
            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            XmlNodeList nodosOG = xmlDocument.DocumentElement?.ChildNodes;
            if (nodosOG != null)
            {
                foreach (XmlNode og in nodosOG)
                {
                    int id = int.Parse(og.Attributes["id"].Value);
                    string tipo = og.SelectSingleNode("tipo").InnerText;
                    string nombre = og.SelectSingleNode("nombre").InnerText;
                    DateTime descubrimiento = DateTime.Parse(og.SelectSingleNode("descubrimiento").InnerText);
                    double tamano = double.Parse(og.SelectSingleNode("tamano").InnerText);
                    double distanciaTierra = double.Parse(og.SelectSingleNode("distancia").InnerText);
                    bool agua = bool.Parse(og.SelectSingleNode("agua").InnerText);
                    bool vida = bool.Parse(og.SelectSingleNode("vida").InnerText);
                    bool atmosfera = bool.Parse(og.SelectSingleNode("atmosfera").InnerText);
                    ObjetoGalactico objeto = new ObjetoGalactico(id, tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera);

                    objetosEspaciales.Add(objeto);
                }
            }
         
            return objetosEspaciales;
        }

        private XmlNode CrearElementoTexto(string nombre, string valor)
        {
            XmlNode elemento = xmlDocument.CreateElement(nombre);
            XmlText texto = xmlDocument.CreateTextNode(valor);
            elemento.AppendChild(texto);
            return elemento;
        }

        public void AnadirElemento(ObjetoGalactico og)
        {
            Thread.Sleep(tiempo);
            LoadXML();
            XmlNode objetosGalacticos = xmlDocument.DocumentElement;

            XmlNode obg = xmlDocument.CreateElement("objgalactico");

            XmlAttribute atributo = xmlDocument.CreateAttribute("id");
            og.Id = UltimoId() + 1;
            obg.Attributes.Append(atributo).Value = og.Id.ToString();

            obg.AppendChild(CrearElementoTexto("tipo", og.Tipo));
            obg.AppendChild(CrearElementoTexto("nombre", og.Nombre));
            obg.AppendChild(CrearElementoTexto("descubrimiento", og.Descubrimiento.ToShortDateString()));
            obg.AppendChild(CrearElementoTexto("tamano", og.Tamano.ToString()));
            obg.AppendChild(CrearElementoTexto("distancia", og.DistanciaTierra.ToString()));
            obg.AppendChild(CrearElementoTexto("agua", og.Agua.ToString()));
            obg.AppendChild(CrearElementoTexto("vida", og.Vida.ToString()));
            obg.AppendChild(CrearElementoTexto("atmosfera", og.Atmosfera.ToString()));

            objetosGalacticos.AppendChild(obg);
            Guardar_xml();
        }

        public bool BorrarElemento(int id)
        {
            Thread.Sleep(tiempo);
            Boolean borrado = false; 
            LoadXML();
            XmlNode eliminado = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{id}']");

            if (eliminado != null)
            {
                xmlDocument.DocumentElement.RemoveChild(eliminado);
                Guardar_xml();
                borrado = true;
            }
            
            return borrado;

        }

        public bool ModificarElemento(int id, ObjetoGalactico nuevo)
        {
            Thread.Sleep(tiempo);
            Boolean modificado = false;
            LoadXML();
            nuevo.Id = id;

            // Utiliza nuevo.id directamente en el XPath
            XmlNode modificable = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']");

            if (modificable != null)
            {
                // Utiliza nuevo.id directamente en el XPath para acceder a cada nodo hijo
                XmlNode tipo = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']/tipo");
                tipo.InnerText = nuevo.Tipo;

                XmlNode nombre = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']/nombre");
                nombre.InnerText = nuevo.Nombre;

                XmlNode descubrimiento = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']/descubrimiento");
                descubrimiento.InnerText = nuevo.Descubrimiento.ToShortDateString();

                XmlNode tamano = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']/tamano");
                tamano.InnerText = nuevo.Tamano.ToString();

                XmlNode distancia = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']/distancia");
                distancia.InnerText = nuevo.DistanciaTierra.ToString();

                XmlNode agua = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']/agua");
                agua.InnerText = nuevo.Agua.ToString();

                XmlNode vida = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']/vida");
                vida.InnerText = nuevo.Vida.ToString();

                XmlNode atmosfera = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{nuevo.Id}']/atmosfera");
                atmosfera.InnerText = nuevo.Atmosfera.ToString();

                modificado = true;
                Guardar_xml();
            }

            return modificado;
        }

            public ObjetoGalactico VerDetallesElemento(int id)
        {
                Thread.Sleep(tiempo);
                ObjetoGalactico objetoGalactico;
                LoadXML();
                XmlNode nodoObjGalactico = xmlDocument.SelectSingleNode($"/objgalacticos/objgalactico[@id='{id}']");

            if (nodoObjGalactico != null)
            {
                int objetoId = int.Parse(nodoObjGalactico.Attributes["id"].Value);
                string tipo = nodoObjGalactico.SelectSingleNode("tipo").InnerText;
                string nombre = nodoObjGalactico.SelectSingleNode("nombre").InnerText;
                DateTime descubrimiento = DateTime.Parse(nodoObjGalactico.SelectSingleNode("descubrimiento").InnerText);
                double tamano = double.Parse(nodoObjGalactico.SelectSingleNode("tamano").InnerText);
                double distanciaTierra = double.Parse(nodoObjGalactico.SelectSingleNode("distancia").InnerText);
                bool agua = bool.Parse(nodoObjGalactico.SelectSingleNode("agua").InnerText);
                bool vida = bool.Parse(nodoObjGalactico.SelectSingleNode("vida").InnerText);
                bool atmosfera = bool.Parse(nodoObjGalactico.SelectSingleNode("atmosfera").InnerText);
                objetoGalactico = new ObjetoGalactico(objetoId, tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera);
            }
            else
            {
                objetoGalactico = null;
            }

                return objetoGalactico;
            }


            public List<ObjetoGalactico> MostrarAtmosferas()
        {
            Thread.Sleep(tiempo);
            LoadXML();
            XmlNodeList nodosObjGalacticos = xmlDocument.DocumentElement.SelectNodes("objgalactico");

            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            foreach (XmlNode objgalactico in nodosObjGalacticos)
            {
                XmlNode atmosferaNode = objgalactico.SelectSingleNode("atmosfera");

                if (atmosferaNode != null && atmosferaNode.InnerText.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    int id = int.Parse(objgalactico.Attributes["id"].Value);
                    string tipo = objgalactico.SelectSingleNode("tipo").InnerText;
                    string nombre = objgalactico.SelectSingleNode("nombre").InnerText;
                    DateTime descubrimiento = DateTime.Parse(objgalactico.SelectSingleNode("descubrimiento").InnerText);
                    double tamano = double.Parse(objgalactico.SelectSingleNode("tamano").InnerText);
                    double distanciaTierra = double.Parse(objgalactico.SelectSingleNode("distancia").InnerText);
                    bool agua = bool.Parse(objgalactico.SelectSingleNode("agua").InnerText);
                    bool vida = bool.Parse(objgalactico.SelectSingleNode("vida").InnerText);
                    bool atmosfera = bool.Parse(atmosferaNode.InnerText);

                    ObjetoGalactico objeto = new ObjetoGalactico(id, tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera);
                    objetosEspaciales.Add(objeto);
                }
            }

            return objetosEspaciales;
        }

        public List<ObjetoGalactico> MostrarDesdeFecha(DateTime fecha)
        {
            Thread.Sleep(tiempo);
            LoadXML();
            XmlNodeList nodosObjGalacticos = xmlDocument.DocumentElement.SelectNodes("objgalactico");

            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            foreach (XmlNode objgalactico in nodosObjGalacticos)
            {
                DateTime descubrimiento = DateTime.Parse(objgalactico.SelectSingleNode("descubrimiento").InnerText);

                if (descubrimiento > fecha)
                {
                    int id = int.Parse(objgalactico.Attributes["id"].Value);
                    string tipo = objgalactico.SelectSingleNode("tipo").InnerText;
                    string nombre = objgalactico.SelectSingleNode("nombre").InnerText;
                    double tamano = double.Parse(objgalactico.SelectSingleNode("tamano").InnerText);
                    double distanciaTierra = double.Parse(objgalactico.SelectSingleNode("distancia").InnerText);
                    bool agua = bool.Parse(objgalactico.SelectSingleNode("agua").InnerText);
                    bool vida = bool.Parse(objgalactico.SelectSingleNode("vida").InnerText);
                    bool atmosfera = bool.Parse(objgalactico.SelectSingleNode("atmosfera").InnerText);

                    ObjetoGalactico objeto = new ObjetoGalactico(id, tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera);
                    objetosEspaciales.Add(objeto);
                }
            }

            return objetosEspaciales;
        }

        public int UltimoId()
        {
            int id = 0;

            XmlNodeList nodosOG = xmlDocument.SelectNodes("/objgalacticos/objgalactico");

            if (nodosOG != null && nodosOG.Count > 0)
            {
                XmlNode ultimoNodo = nodosOG[nodosOG.Count - 1];
                XmlAttribute idAttribute = ultimoNodo.Attributes["id"];

                if (idAttribute != null)
                {
                    id = int.Parse(idAttribute.Value);
                }
            }

            return id;
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

            Boolean b = ComprobarFichero();

            if (b == false)
            {
                XDocument xel = XDocument.Parse("<objgalacticos></objgalacticos>");
                xel.Declaration = new XDeclaration("1.0", "UTF-8", "true");
                xel.Save(ruta);
            }
        }

        public bool ComprobarFichero()
        {
            try
            {

                XmlDocument xml = new XmlDocument();
                xml.Load(ruta);
                return true;
            }catch 
            {

                return false;
            }

        }

        private void Guardar_xml()
        {
            xmlDocument.Save(ruta);
          
        }

    }
}
