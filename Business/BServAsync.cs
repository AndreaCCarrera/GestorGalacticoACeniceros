using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BBusiness
{
    public class BServAsync
    {

        //IDObjetoGalactico data = new DJson();
        DBDSQLServer data = new DBDSQLServer();


        public Task<List<ObjetoGalactico>> ListarElementos()
        {

            if (data.ExisteConexion() == false)
            {
                data.CrearData();
            }

            data.NormalizarData();

            Task<List<ObjetoGalactico>> tarea = new Task<List<ObjetoGalactico>>(() => data.ListarElementos());


            tarea.Start();

            return tarea;

        }
        public Task AnadirElemento(ObjetoGalactico og)
        {
            if (data.ExisteConexion() == false)
            {
                data.CrearData();
            }

            data.NormalizarData();

            Task tarea = Task.Run(() => data.AnadirElemento(og));


            return tarea;

        }

        public Task<bool> BorrarElemento(int id)
        {

            Task<bool> tarea = new Task<bool>(() => data.BorrarElemento(id));

            tarea.Start();

            return tarea;

        }

        public Task<bool> ModificarElemento(int id, ObjetoGalactico nuevo)
        {
            Task<bool> tarea = Task<bool>.Run(() => data.ModificarElemento(id, nuevo));

            return tarea;

        }
        public Task<ObjetoGalactico> VerDetallesElemento(int id)
        {
            Task<ObjetoGalactico> tarea = Task<ObjetoGalactico>.Run(() => VerDetallesElemento(id));

            return tarea;

        }

        public Task<List<ObjetoGalactico>> MostrarAtmosferas()
        {
            Task<List<ObjetoGalactico>> tarea = Task<List<ObjetoGalactico>>.Run(() => data.MostrarAtmosferas());

            return tarea;

        }
        public Task<List<ObjetoGalactico>> MostrarDesdeFecha(DateTime fecha)
        {
            Task<List<ObjetoGalactico>> tarea = Task<List<ObjetoGalactico>>.Run(() => data.MostrarDesdeFecha(fecha));

            return tarea;

        }

        public int UltimaLinea()
        {
            if (data.UltimoId() == 0)
            {
                return 1;
            }
            else
            {
                return data.UltimoId() + 1;
            }

        }

        // Paso intermedio para que vista acceda al método de data
        public List<String> LanzarProcedimiento()
        {
            List<String> mensajes = data.LanzarProcedimiento();

            return mensajes;
        }

    }
}
