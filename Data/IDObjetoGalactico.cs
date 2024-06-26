using Model;
using System;
using System.Collections.Generic;
namespace Data
{
    public interface IDObjetoGalactico
    {
        List<ObjetoGalactico> ListarElementos();
        void AnadirElemento(ObjetoGalactico og);
        bool BorrarElemento(int id);
        bool ModificarElemento(int id, ObjetoGalactico nuevo);
        ObjetoGalactico VerDetallesElemento(int id);
        List<ObjetoGalactico> MostrarAtmosferas();
        List<ObjetoGalactico> MostrarDesdeFecha(DateTime fecha);
        int UltimoId();
        bool ExisteConexion();
        bool CrearData();
        void NormalizarData();


    }
}
