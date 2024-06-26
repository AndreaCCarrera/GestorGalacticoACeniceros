using HtmlAgilityPack;
using Model;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business
{
    public static class ScraperHTML
    {
        public static async Task<ObjetoGalactico> ImportDataAsync(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync(url);

            ObjetoGalactico og = new ObjetoGalactico();

            var nombreNode = doc.DocumentNode.SelectSingleNode("//h1[@id='firstHeading']");
            string nombre = nombreNode != null ? nombreNode.InnerText : "";
            int indiceParentesis = nombre.IndexOf('(');
            if (indiceParentesis != -1)
            {
                nombre = nombre.Substring(0, indiceParentesis);
            }
            og.Nombre = nombre.Trim();
            og.Nombre = nombre ?? "Desconocido";

            // Inicializo propiedades en caso de que no se encuentren en la página
            og.Descubrimiento = new DateTime(1753, 1, 1);
            og.Tipo = "Desconocido";
            og.DistanciaTierra = 0.0;
            og.Tamano = 0;
            og.Atmosfera = false;
            og.Agua = false;

            var tablaInfobox = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'infobox')]");
            if (tablaInfobox != null)
            {
                var filas = tablaInfobox.SelectNodes(".//tr");
                foreach (var fila in filas)
                {
                    var tituloNode = fila.SelectSingleNode("th");
                    var datoNode = fila.SelectSingleNode("td");

                    if (tituloNode != null && datoNode != null)
                    {
                        var tituloTexto = tituloNode.InnerText.Trim();
                        var datoTexto = datoNode.InnerText.Trim();

                        if (tituloTexto.Contains("Fecha") || tituloTexto.Contains("Descubridor"))
                        {
                            if (DateTime.TryParseExact(datoTexto, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime desc))
                            {
                                og.Descubrimiento = desc;
                            }
                            else
                            {
                                og.Descubrimiento = new DateTime(1753, 1, 1);

                            }
                        }

                        if (tituloTexto.Contains("Categoría"))
                        {
                            og.Tipo = datoTexto.Split(' ')[0];
                        }

                        if (tituloTexto.Contains("Distancia estelar"))
                        {
                            Match match = Regex.Match(datoTexto, @"\b\d+\b");

                            if (match.Success)
                            {
                                if (double.TryParse(match.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double distancia))
                                {
                                    og.DistanciaTierra = distancia;
                                }
                            }
                            else
                            {
                                og.DistanciaTierra = 10000.0;
                            }
                        }

                        if (tituloTexto.Contains("Magnitud aparente"))
                        {
                            Match match = Regex.Match(datoTexto, @"-?\d+,\d+");

                            if (match.Success)
                            {
                                string tamanoNumerico = match.Value.Replace(',', '.');

                                if (double.TryParse(tamanoNumerico, NumberStyles.Any, CultureInfo.InvariantCulture, out double tamano))
                                {
                                    og.Tamano = tamano;
                                }

                            }
                            else
                            {
                                og.DistanciaTierra = 1000.0;
                            }
                        }
                        
                    }

                    var caracteristicasAtmosfericasNode = doc.DocumentNode.SelectSingleNode("//th[contains(text(), 'Características atmosféricas')]");
                    og.Atmosfera = caracteristicasAtmosfericasNode != null;

                    var agua = doc.DocumentNode.SelectSingleNode("//th[contains(text(), 'Agua')]");
                    og.Agua = agua != null;

                    //HtmlNode caracteristicasAtmosfericasNode = doc.DocumentNode.SelectSingleNode("//th[contains(text(), 'Características atmosféricas')]");
                    //og.Atmosfera = caracteristicasAtmosfericasNode != null;

                    //HtmlNode aguaNode = doc.DocumentNode.SelectSingleNode("//th[contains(text(), 'Agua')]");
                    //og.Agua = aguaNode != null;

                    og.Vida = nombre.IndexOf("Tierra", StringComparison.OrdinalIgnoreCase) >= 0;

                }
            }

            return og;
        }
    }


}



