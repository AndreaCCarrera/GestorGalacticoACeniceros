using Data;
using Model;
using System;
using System.Collections.Generic;


namespace BBusiness
{
    public class Business : IBusiness
    {
        IDObjetoGalactico data = new DXml();


        public List<ObjetoGalactico> ListarElementos()
        {
            if (data.ExisteConexion() == false)
            {
               data.CrearData();
            }

            data.NormalizarData();

            return data.ListarElementos();
            
        }
        public void AnadirElemento(ObjetoGalactico og)
        {
            if (data.ExisteConexion() == false)
            {
                data.CrearData();
            }

            data.NormalizarData();
            data.AnadirElemento(og);
          
        }

        public bool BorrarElemento(int id)
        {
            if (data.ExisteConexion() == false)
            { return false;
            }
         
              return data.BorrarElemento(id);
            
        }
 
        public bool ModificarElemento(int id, ObjetoGalactico nuevo)
        {

            if (data.ExisteConexion() == false)
            {
                return false;
            }
           

            return data.ModificarElemento(id, nuevo);
        }
        public ObjetoGalactico VerDetallesElemento(int id)
        {
            if (data.ExisteConexion() == false)
            {
                return null;
            }

            return data.VerDetallesElemento(id);
           
            
        }
        public List<ObjetoGalactico> MostrarAtmosferas()
        {
            if (data.ExisteConexion() == false)
            {   
                List<ObjetoGalactico> nueva = new List<ObjetoGalactico>();
                return nueva;
            }
            
            return (data.MostrarAtmosferas());

        }
        public List<ObjetoGalactico> MostrarDesdeFecha(DateTime fecha)
        {
            if (data.ExisteConexion() == false)
            {
                List<ObjetoGalactico> nueva = new List<ObjetoGalactico>();
                return nueva;
            }
            else
            {
                return (data.MostrarDesdeFecha(fecha));
            }
        
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

    }
}

