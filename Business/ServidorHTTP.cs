using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business
{
    public class ServidorHTTP
    {
        private Socket servidor;
        private IPEndPoint localEndPoint;
        private DBDSQLServer data;
        //private IDObjetoGalactico data;
        //private DJson dJson;
        public event Action<string> ActualizarTextBox;

        public ServidorHTTP()
        {
            data = new DBDSQLServer();

    }   

    // Lanza el servidor y procesa las peticiones de clientes
        public void Arrancar()
        {
            Task.Run(() =>
            {
                try
                {
                    servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    localEndPoint = new IPEndPoint(IPAddress.Any, 15000);

                    servidor.Bind(localEndPoint);
                    servidor.Listen(10);
                    Console.WriteLine("Servidor HTTP iniciado. Esperando solicitudes...");

                    while (true)
                    {
                        Socket conexionCliente = servidor.Accept();
                        Task.Run(() => ManejarSolicitud(conexionCliente));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al iniciar el servidor: " + ex.Message);
                }
            });
        }
        // Analiza las peticiones del cliente y en función de cual sea el case, hace unas funciones u otras, separadas por GET y POST
        private void ManejarSolicitud(Socket conexionCliente)
        {
        try
        {
            byte[] buffer = new byte[2048];
            int bytesRecibidos = conexionCliente.Receive(buffer);
            string datos = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);
            Console.WriteLine("Texto recibido: " + Environment.NewLine + datos);
            ActualizarTextBox?.Invoke("Solicitud recibida: " + datos);


            string[] lineas = datos.Split(new[] { "\r\n" }, StringSplitOptions.None);
            string primeraLinea = lineas.FirstOrDefault();
            string[] partes = primeraLinea.Split(' ');
            string metodo;
            string ruta;
            string[] partesRuta;
            string parametros = "0";

            if (primeraLinea != null)
            {
                partes = primeraLinea.Split(' ');


                if (partes.Length >= 2)
                {
                    metodo = partes[0];
                    ruta = partes[1];

                    if (ruta.Contains("?"))
                    {
                        partesRuta = ruta.Split('?');
                        ruta = partesRuta[0];
                        parametros = partesRuta[1];

                    }


                    if (metodo == "GET")
                    {
                        switch (ruta)
                        {
                            case "/":
                                EnviarHTMLPrincipal(conexionCliente);
                                break;

                            case "/detalles.html":
                                int idDetalles;
                                string archivo = "FORMATO/detalles.html";
                                if (parametros.Length > 1)
                                {
                                    string[] parametrosSeparados = parametros.Split('&');
                                    foreach (string parametro in parametrosSeparados)
                                    {
                                        string[] claveValor = parametro.Split('=');
                                        if (claveValor.Length == 2 && claveValor[0] == "id")
                                        {
                                            if (int.TryParse(claveValor[1], out idDetalles))
                                            {
                                                ObjetoGalactico detallesElemento = data.VerDetallesElemento(idDetalles);
                                                EnviarDetallesConDatos(conexionCliente, detallesElemento, archivo);

                                                return;
                                            }
                                        }
                                    }
                                }
                                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.BadRequest, "Parámetro 'id' no encontrado o inválido", "text/plain");
                                break;

                            case "/anadir.html":
                                EnviarArchivo(conexionCliente, "FORMATO/anadir.html");
                                break;

                            case "/modificar.html":

                                archivo = "FORMATO/modificar.html";
                                if (parametros.Length > 1)
                                {
                                    string[] parametrosSeparados = parametros.Split('&');
                                    foreach (string parametro in parametrosSeparados)
                                    {
                                        string[] claveValor = parametro.Split('=');
                                        if (claveValor.Length == 2 && claveValor[0] == "id")
                                        {
                                            if (int.TryParse(claveValor[1], out idDetalles))
                                            {
                                                ObjetoGalactico detallesElemento = data.VerDetallesElemento(idDetalles);
                                                EnviarDatosModificar(conexionCliente, detallesElemento, archivo);

                                                return;
                                            }
                                        }
                                    }
                                }
                                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.BadRequest, "Parámetro 'id' no encontrado o inválido", "text/plain");
                                break;

                            case "/filtrarfecha.html":
                                MostrarPorFecha(conexionCliente, parametros);
                                break;

                            case "/atmosfera.html":
                                EnviarAtmosferaHTML(conexionCliente);
                                break;

                            case "/eliminar.html":

                                int id;

                                if (parametros.Length > 1)
                                {
                                    string[] parametrosSeparados = parametros.Split('&');
                                    foreach (string parametro in parametrosSeparados)
                                    {
                                        string[] claveValor = parametro.Split('=');
                                        if (claveValor.Length == 2 && claveValor[0] == "id")
                                        {
                                            if (int.TryParse(claveValor[1], out id))
                                            {
                                                if (data.BorrarElemento(id))
                                                {
                                                    EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, "Elemento eliminado", "text/plain");
                                                }
                                                else
                                                {
                                                    EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.InternalServerError, "No se pudo eliminar el elemento", "text/plain");
                                                }
                                                return;
                                            }
                                            else
                                            {
                                                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.BadRequest, "ID inválido", "text/plain");
                                                return;
                                            }
                                        }
                                    }
                                }


                                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.BadRequest, "No se proporcionó un ID válido en la solicitud", "text/plain");
                                break;


                            case "/procedure.html":
                                List<String> mensajes = data.LanzarProcedimiento();

                                if (mensajes != null && mensajes.Count > 1)
                                {
                                    EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, mensajes[0] + ": " + mensajes[1], "text/plain");
                                }
                                else
                                {
                                    EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.InternalServerError, mensajes[0], "text/plain");
                                }
                                break;


                            case "/reset.html":
                                EnviarHTMLPrincipal(conexionCliente);
                                break;


                            case "/volver.html":
                                EnviarHTMLPrincipal(conexionCliente);
                                break;

                            default:
                                EnviarError404(conexionCliente);
                                break;
                        }
                    }
                    else if (metodo == "POST")
                    {
                        switch (ruta)
                        {
                                case "/elementoanadido":
                                    {
                                        try
                                        {
                                            string formDataString = HttpUtility.UrlDecode(datos.Substring("POST /elementoanadido?".Length));

                                            NameValueCollection formData = HttpUtility.ParseQueryString(formDataString);

                                            Dictionary<string, string> datosFormulario = new Dictionary<string, string>();
                                            foreach (string key in formData.AllKeys)
                                            {
                                                datosFormulario.Add(key, formData[key]);
                                            }

                                            ObjetoGalactico nuevoObjeto = new ObjetoGalactico()
                                            {
                                                Tipo = datosFormulario.ContainsKey("tipo") ? datosFormulario["tipo"] : "",
                                                Nombre = datosFormulario.ContainsKey("nombre") ? datosFormulario["nombre"] : "",
                                                Descubrimiento = datosFormulario.ContainsKey("date") ? DateTime.ParseExact(datosFormulario["date"] + "T00:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture) : DateTime.Now,
                                                Tamano = datosFormulario.ContainsKey("size") ? double.Parse(datosFormulario["size"]) : 0.0,
                                                DistanciaTierra = datosFormulario.ContainsKey("distance") ? double.Parse(datosFormulario["distance"]) : 0.0,
                                                Agua = datosFormulario.ContainsKey("agua"),
                                                Vida = datosFormulario.ContainsKey("vida"),
                                                Atmosfera = datosFormulario.ContainsKey("atmosfera")
                                            };

                                            data.AnadirElemento(nuevoObjeto);

                                            EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, "Elemento añadido", "text/plain");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Error al agregar un nuevo objeto galáctico: " + ex.Message);

                                            string respuestaError = "HTTP/1.1 500 Internal Server Error\r\n\r\nError al procesar la solicitud";
                                            byte[] bytesRespuestaError = Encoding.ASCII.GetBytes(respuestaError);
                                            conexionCliente.Send(bytesRespuestaError);
                                        }
                                    }
                                    break;

                                case "/elementomodificado":
                                    {
                                        try
                                        {
                                            Dictionary<string, string> datosFormulario = new Dictionary<string, string>();

                                            string[] partesAnadir = parametros.Split('&');

                                            foreach (string parte in partesAnadir)
                                            {
                                                string[] campoValor = parte.Split('=');

                                                if (campoValor.Length == 2)
                                                {
                                                    datosFormulario.Add(campoValor[0], campoValor[1]);
                                                }
                                            }

                                            int id = datosFormulario.ContainsKey("Id") ? Int32.Parse(datosFormulario["Id"]) : 0;

                                            ObjetoGalactico nuevoObjeto = new ObjetoGalactico()
                                            {
                                                Id = id,
                                                Tipo = datosFormulario.ContainsKey("tipo") ? datosFormulario["tipo"] : "",
                                                Nombre = datosFormulario.ContainsKey("nombre") ? datosFormulario["nombre"] : "",
                                                Descubrimiento = datosFormulario.ContainsKey("date") ? DateTime.ParseExact(datosFormulario["date"] + "T00:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture) : DateTime.Now,
                                                Tamano = datosFormulario.ContainsKey("size") ? double.Parse(datosFormulario["size"]) : 0.0,
                                                DistanciaTierra = datosFormulario.ContainsKey("distance") ? double.Parse(datosFormulario["distance"]) : 0.0,
                                                Agua = datosFormulario.ContainsKey("agua") && datosFormulario["agua"] == "on",
                                                Vida = datosFormulario.ContainsKey("vida") && datosFormulario["vida"] == "on",
                                                Atmosfera = datosFormulario.ContainsKey("atmosf") && datosFormulario["atmosf"] == "on"
                                            };

                                            data.ModificarElemento(nuevoObjeto.Id, nuevoObjeto);

                                            EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, "Elemento modificado", "text/plain");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Error al modificar un nuevo objeto galáctico: " + ex.Message);

                                            string respuestaError = "HTTP/1.1 500 Internal Server Error\r\n\r\nError al procesar la solicitud";
                                            byte[] bytesRespuestaError = Encoding.ASCII.GetBytes(respuestaError);
                                            conexionCliente.Send(bytesRespuestaError);
                                        }
                                        break;

                                    }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al manejar la solicitud: " + ex.Message);
            }
            finally
            {
                conexionCliente.Shutdown(SocketShutdown.Both);
                conexionCliente.Close();
            }
        }

        private void EnviarHTMLPrincipal(Socket conexionCliente)
        {
            string rutaInterfaceHTML = "FORMATO/interface.html";
            string html;

            try
            {
                html = File.ReadAllText(rutaInterfaceHTML);

                if (ListarElementosHTML().Length > 0) { 
                        html = html.Replace("<!-- Aquí se listarán los elementos galácticos -->", ListarElementosHTML());
                }
                else
                {
                    html = html.Replace("<!-- Aquí se listarán los elementos galácticos -->", "<p>No hay elementos que mostrar</p>");
                }

                html = html.Replace("{{ICONO}}", ImageToBase64("FORMATO/star.ico"));
                html = html.Replace("{{FONDO}}", ImageToBase64("FORMATO/space-one.PNG"));
                string fechaHoyHTML = ObtenerFechaHoyEnFormatoHTML();
                html = html.Replace("{{FECHA_HOY}}", fechaHoyHTML);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo interface.html: " + ex.Message);
                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.InternalServerError, "Error al cargar la interfaz", "text/plain");
                return;
            }

            EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, html, "text/html");
        }

        private void EnviarDetallesConDatos(Socket conexionCliente, ObjetoGalactico detallesElemento, String archivo)
        {
            try
            {
                string contenido = File.ReadAllText(archivo);


                contenido = contenido.Replace("<input type=\"text\" id=\"txtId\" name=\"id\" placeholder=\"Id\" readonly>", "<input type=\"text\" id=\"txtId\" name=\"id\" placeholder=\"Id\" readonly value=\"" + detallesElemento.Id + "\">");
                contenido = contenido.Replace("<input type=\"text\" id=\"txtTipo\" name=\"tipo\" placeholder=\"Tipo\" readonly>", "<input type=\"text\" id=\"txtTipo\" name=\"tipo\" placeholder=\"Tipo\" readonly value=\"" + detallesElemento.Tipo + "\">");
                contenido = contenido.Replace("<input type=\"text\" id=\"txtNombre\" name=\"nombre\" placeholder=\"Nombre\" readonly>", "<input type=\"text\" id=\"txtNombre\" name=\"nombre\" placeholder=\"Nombre\" readonly value=\"" + detallesElemento.Nombre + "\">");
                contenido = contenido.Replace("<input type=\"date\" id=\"dtDate\" name=\"descubrimiento\" readonly>", "<input type=\"date\" id=\"dtDate\" name=\"descubrimiento\" readonly value=\"" + detallesElemento.Descubrimiento.ToString("yyyy-MM-dd") + "\">");
                contenido = contenido.Replace("<input type=\"number\" id=\"txtSize\" name=\"tamano\" placeholder=\"Tamaño\" readonly>", "<input type=\"number\" id=\"txtSize\" name=\"tamano\" placeholder=\"Tamaño\" readonly value=\"" + detallesElemento.Tamano + "\">");
                contenido = contenido.Replace("<input type=\"number\" id=\"txtDistance\" name=\"distance\" placeholder=\"Distancia a la Tierra\" readonly>", "<input type=\"number\" id=\"txtDistance\" name=\"distance\" placeholder=\"Distancia a la Tierra\" readonly value=\"" + detallesElemento.DistanciaTierra + "\">");


                contenido = contenido.Replace("<input type=\"checkbox\" id=\"chbAgua\" name=\"agua\" disabled>", "<input type=\"checkbox\" id=\"chbAgua\" name=\"agua\" disabled" + (detallesElemento.Agua ? " checked" : "") + ">");
                contenido = contenido.Replace("<input type=\"checkbox\" id=\"chbVida\" name=\"vida\" disabled>", "<input type=\"checkbox\" id=\"chbVida\" name=\"vida\" disabled" + (detallesElemento.Vida ? " checked" : "") + ">");
                contenido = contenido.Replace("<input type=\"checkbox\" id=\"chbAtmosfera\" name=\"atmosfera\" disabled>", "<input type=\"checkbox\" id=\"chbAtmosfera\" name=\"atmosfera\" disabled" + (detallesElemento.Atmosfera ? " checked" : "") + ">");

 
                contenido = contenido.Replace("{{ICONO}}", ImageToBase64("FORMATO/star.ico"));
                contenido = contenido.Replace("{{FONDO}}", ImageToBase64("FORMATO/space-one.PNG"));

                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, contenido, "text/html");
            }
            catch (FileNotFoundException)
            {
                EnviarError404(conexionCliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el archivo de detalles con datos: " + ex.Message);
                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.InternalServerError, "Error interno del servidor", "text/plain");
            }
        }

        private void EnviarDatosModificar(Socket conexionCliente, ObjetoGalactico detallesElemento, String archivo)
        {
            try
            {
                string contenido = File.ReadAllText(archivo);

                contenido = contenido.Replace("<input type=\"text\" id=\"txtId\" name=\"Id\" placeholder=\"Id\" readonly>", "<input type=\"text\" id=\"txtId\" name=\"Id\" placeholder=\"Id\" readonly value=\"" + detallesElemento.Id + "\">");
                contenido = contenido.Replace("<input type=\"text\" id=\"txtTipo\" name=\"tipo\" placeholder=\"Tipo\">", "<input type=\"text\" id=\"txtTipo\" name=\"tipo\" placeholder=\"Tipo\" value=\"" + detallesElemento.Tipo + "\">");
                contenido = contenido.Replace("<input type=\"text\" id=\"txtNombre\" name=\"nombre\" placeholder=\"Nombre\">", "<input type=\"text\" id=\"txtNombre\" name=\"nombre\" placeholder=\"Nombre\" value=\"" + detallesElemento.Nombre + "\">");
                contenido = contenido.Replace("<input type=\"date\" id=\"dtDate\" name=\"date\" >", "<input type=\"date\" id=\"dtDate\" name=\"date\" value=\"" + detallesElemento.Descubrimiento.ToString("yyyy-MM-dd") + "\">");
                contenido = contenido.Replace("<input type=\"number\" id=\"txtSize\" name=\"size\" placeholder=\"Tamaño\">", "<input type=\"number\" id=\"txtSize\" name=\"size\" placeholder=\"Tamaño\" value=\"" + detallesElemento.Tamano + "\">");
                contenido = contenido.Replace("<input type=\"number\" id=\"txtDistance\" name=\"distance\" placeholder=\"Distancia a la Tierra\">", "<input type=\"number\" id=\"txtDistance\" name=\"distance\" placeholder=\"Distancia a la Tierra\"  value=\"" + detallesElemento.DistanciaTierra + "\">");


                contenido = contenido.Replace("<input type=\"checkbox\" id=\"chbAgua\" name=\"agua\">", "<input type=\"checkbox\" id=\"chbAgua\" name=\"agua\"" + (detallesElemento.Agua ? " checked" : "") + ">");
                contenido = contenido.Replace("<input type=\"checkbox\" id=\"chbVida\" name=\"vida\">", "<input type=\"checkbox\" id=\"chbVida\" name=\"vida\"" + (detallesElemento.Vida ? " checked" : "") + ">");
                contenido = contenido.Replace("<input type=\"checkbox\" id=\"chbAtmosfera\" name=\"atmosfera\">", "<input type=\"checkbox\" id=\"chbAtmosfera\" name=\"atmosfera\"" + (detallesElemento.Atmosfera ? " checked" : "") + ">");

                contenido = contenido.Replace("{{ICONO}}", ImageToBase64("FORMATO/star.ico"));
                contenido = contenido.Replace("{{FONDO}}", ImageToBase64("FORMATO/space-one.PNG"));

            EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, contenido, "text/html");
            }
            catch (FileNotFoundException)
            {
                EnviarError404(conexionCliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el archivo de detalles con datos: " + ex.Message);
                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.InternalServerError, "Error interno del servidor", "text/plain");
            }
        }

        private void EnviarAtmosferaHTML(Socket conexionCliente)
        {
            string rutaInterfaceHTML = "FORMATO/interface.html";
            string html;
            try
            {
                html = File.ReadAllText(rutaInterfaceHTML);
                if (FiltrarAtmosfera().Length > 0)
                {
                    html = html.Replace("<!-- Aquí se listarán los elementos galácticos -->", FiltrarAtmosfera());
                }
                else
                {
                    html = html.Replace("<!-- Aquí se listarán los elementos galácticos -->", "<p>No hay objetos galácticos con atmósfera.</p>");
                }

               
                html = html.Replace("{{ICONO}}", ImageToBase64("FORMATO/star.ico"));
                html = html.Replace("{{FONDO}}", ImageToBase64("FORMATO/space-one.PNG"));
                string fechaHoyHTML = ObtenerFechaHoyEnFormatoHTML();
                html = html.Replace("{{FECHA_HOY}}", fechaHoyHTML);
        }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo interface.html: " + ex.Message);
           
                EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.InternalServerError, "Error al cargar la interfaz", "text/plain");
                return;
            }

 
            EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, html, "text/html");
        }

        private string FiltrarAtmosfera()
        {
            string html = "";

            List<ObjetoGalactico> objetosGalacticos = data.MostrarAtmosferas();

            foreach (ObjetoGalactico og in objetosGalacticos)
            {
                html += "<li><input type=\"radio\" id=\"" + og.Id + "\" name=\"objeto\" value=\"" + og.Id + "\">" + og.ToString() + "</li><br>";
            }

            return html;
        }

    private void MostrarPorFecha(Socket conexionCliente, string parametros)
    {
        string[] parametrosSeparados = parametros.Split('&');
        DateTime fechaSeleccionada;
        string rutaInterfaceHTML = "FORMATO/interface.html";
        string html = File.ReadAllText(rutaInterfaceHTML);

        foreach (string parametro in parametrosSeparados)
        {
            string[] claveValor = parametro.Split('=');
            if (claveValor.Length == 2 && claveValor[0] == "fecha")
            {
                string fechaString = claveValor[1];
                if (!string.IsNullOrEmpty(fechaString))
                {
                    string fechaFormateada = $"{fechaString}T00:00:00";
                    if (DateTime.TryParseExact(fechaFormateada, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaSeleccionada))
                    {
                        try
                        {

                            if (FiltrarFecha(fechaSeleccionada) != "")
                            {
                                html = html.Replace("<!-- Aquí se listarán los elementos galácticos -->", FiltrarFecha(fechaSeleccionada));
                            }
                            else
                            {
                                html = html.Replace("<!-- Aquí se listarán los elementos galácticos -->", "<p>No hay objetos galácticos posteriores a la fecha seleccionada.</p>");
                            }

                            html = html.Replace("{{ICONO}}", ImageToBase64("FORMATO/star.ico"));
                            html = html.Replace("{{FONDO}}", ImageToBase64("FORMATO/space-one.PNG"));
                            html = html.Replace("{{FECHA_HOY}}", fechaString);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al leer el archivo interface.html: " + ex.Message);

                            EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.InternalServerError, "Error al cargar la interfaz", "text/plain");
                            return;
                        }

                    }

                }
                    
            }
               
        }

        EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, html, "text/html");
    }

    private string FiltrarFecha(DateTime fechaSeleccionada)
    {
        string html = "";

        List<ObjetoGalactico> objetosGalacticos = data.MostrarDesdeFecha(fechaSeleccionada);

        foreach (ObjetoGalactico og in objetosGalacticos)
        {
            html += "<li><input type=\"radio\" id=\"" + og.Id + "\" name=\"objeto\" value=\"" + og.Id + "\">" + og.ToString() + "</li><br>";
        }

        return html;
    }

    private void EnviarArchivo(Socket conexionCliente, string rutaArchivo)
                {
                    try
                    {
                        string contenido = File.ReadAllText(rutaArchivo);

                        contenido = contenido.Replace("{{ICONO}}", ImageToBase64("FORMATO/star.ico"));
                        contenido = contenido.Replace("{{FONDO}}", ImageToBase64("FORMATO/space-one.PNG"));

                        EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.OK, contenido, "text/html");
                    }
                    catch (FileNotFoundException)
                    {
                        EnviarError404(conexionCliente);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al enviar el archivo: " + ex.Message);
                        EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.InternalServerError, "Error interno del servidor", "text/plain");
                    }
                }

        private void EnviarError404(Socket conexionCliente)
        {
            EnviarRespuestaHTTP(conexionCliente, HttpStatusCode.NotFound, "Página no encontrada", "text/plain");
        }

        private void EnviarRespuestaHTTP(Socket conexionCliente, HttpStatusCode statusCode, string contenido, string contentType)
        {
            string response = $"HTTP/1.1 {(int)statusCode} {statusCode.ToString()}\r\nContent-Type: {contentType}\r\nContent-Length: {Encoding.UTF8.GetBytes(contenido).Length}\r\n\r\n{contenido}";
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            conexionCliente.Send(responseBytes);
        }

        private string ListarElementosHTML()
        {
            string html = "";

            foreach (ObjetoGalactico og in data.ListarElementos())
            {
                html += "<li><input type=\"radio\" id=\"" + og.Id + "\" name=\"objeto\" value=\"" + og.Id + "\">" + og.ToString() + "</li><br>";
            }

            return html;
        }

        private string ObtenerFechaHoyEnFormatoHTML()
        {
            string fechaHoy = DateTime.Today.ToString("yyyy-MM-dd");
            return fechaHoy;
        }
                
        // Transforma la imagen para poder insertarla en el HTML
        private string ImageToBase64(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);

            if (imagePath.EndsWith(".ico"))
            {
                base64String = "data:image/x-icon;base64," + base64String;
            }
            else
            {
                base64String = "data:image/png;base64," + base64String;
            }

            return base64String;
        }

        public void Detener()
        {
            if (servidor != null)
            {
                servidor.Close();
                Console.WriteLine("Servidor detenido.");
            }
            else
            {
                Console.WriteLine("El servidor ya está detenido.");
            }
        }
    }
}

