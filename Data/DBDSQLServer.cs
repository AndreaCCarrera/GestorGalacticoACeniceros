using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class DBDSQLServer : IDObjetoGalactico
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\BDObjetosGalacticos.mdf;Integrated Security=True;Connect Timeout=30";
        public List<ObjetoGalactico> ListarElementos()
        {
            List<ObjetoGalactico> objetosEspaciales = new List<ObjetoGalactico>();

            string queryString = "SELECT * FROM ObjetoGalactico";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ObjetoGalactico objeto = new ObjetoGalactico
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Tipo = Convert.ToString(reader["Tipo"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Descubrimiento = Convert.ToDateTime(reader["Descubrimiento"]),
                            Tamano = Convert.ToSingle(reader["Tamano"]),
                            DistanciaTierra = Convert.ToSingle(reader["DistanciaTierra"]),
                            Agua = Convert.ToBoolean(reader["Agua"]),
                            Vida = Convert.ToBoolean(reader["Vida"]),
                            Atmosfera = Convert.ToBoolean(reader["Atmosfera"])
                        };
                        objetosEspaciales.Add(objeto);
                    }
                }
            }
            return objetosEspaciales;
        }

        public void AnadirElemento(ObjetoGalactico og)
        {
            int ultimoId = UltimoId();

            og.Id = ultimoId;

            string queryString = "INSERT INTO ObjetoGalactico (Id, Tipo, Nombre, Descubrimiento, Tamano, DistanciaTierra, Agua, Vida, Atmosfera) " +
                                 "VALUES (@Id, @Tipo, @Nombre, @Descubrimiento, @Tamano, @DistanciaTierra, @Agua, @Vida, @Atmosfera)";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                cmd.Parameters.AddWithValue("@Id", og.Id);
                cmd.Parameters.AddWithValue("@Tipo", og.Tipo);
                cmd.Parameters.AddWithValue("@Nombre", og.Nombre);
                cmd.Parameters.AddWithValue("@Descubrimiento", og.Descubrimiento);
                cmd.Parameters.AddWithValue("@Tamano", og.Tamano);
                cmd.Parameters.AddWithValue("@DistanciaTierra", og.DistanciaTierra);
                cmd.Parameters.AddWithValue("@Agua", og.Agua);
                cmd.Parameters.AddWithValue("@Vida", og.Vida);
                cmd.Parameters.AddWithValue("@Atmosfera", og.Atmosfera);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool BorrarElemento(int id)
        {
            string queryString = "DELETE FROM ObjetoGalactico WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool ModificarElemento(int id, ObjetoGalactico nuevo)
        {
            string queryString = "UPDATE ObjetoGalactico SET Tipo = @Tipo, Nombre = @Nombre, Descubrimiento = @Descubrimiento, Tamano = @Tamano, " +
                                 "DistanciaTierra = @DistanciaTierra, Agua = @Agua, Vida = @Vida, Atmosfera = @Atmosfera WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Tipo", nuevo.Tipo);
                cmd.Parameters.AddWithValue("@Nombre", nuevo.Nombre);
                cmd.Parameters.AddWithValue("@Descubrimiento", nuevo.Descubrimiento);
                cmd.Parameters.AddWithValue("@Tamano", nuevo.Tamano);
                cmd.Parameters.AddWithValue("@DistanciaTierra", nuevo.DistanciaTierra);
                cmd.Parameters.AddWithValue("@Agua", nuevo.Agua);
                cmd.Parameters.AddWithValue("@Vida", nuevo.Vida);
                cmd.Parameters.AddWithValue("@Atmosfera", nuevo.Atmosfera);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public ObjetoGalactico VerDetallesElemento(int id)
        {
            ObjetoGalactico objetoGalactico = null;

            string queryString = "SELECT * FROM ObjetoGalactico WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        objetoGalactico = new ObjetoGalactico
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Tipo = Convert.ToString(reader["Tipo"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Descubrimiento = Convert.ToDateTime(reader["Descubrimiento"]),
                            Tamano = Convert.ToSingle(reader["Tamano"]),
                            DistanciaTierra = Convert.ToSingle(reader["DistanciaTierra"]),
                            Agua = Convert.ToBoolean(reader["Agua"]),
                            Vida = Convert.ToBoolean(reader["Vida"]),
                            Atmosfera = Convert.ToBoolean(reader["Atmosfera"])
                        };
                    }
                }
            }

            return objetoGalactico;
        }
        public List<ObjetoGalactico> MostrarAtmosferas()
        {

            List<ObjetoGalactico> nuevaLista = new List<ObjetoGalactico>();

            string queryString = "SELECT * FROM ObjetoGalactico WHERE Atmosfera = 1";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ObjetoGalactico objeto = new ObjetoGalactico
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Tipo = Convert.ToString(reader["Tipo"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Descubrimiento = Convert.ToDateTime(reader["Descubrimiento"]),
                            Tamano = Convert.ToSingle(reader["Tamano"]),
                            DistanciaTierra = Convert.ToSingle(reader["DistanciaTierra"]),
                            Agua = Convert.ToBoolean(reader["Agua"]),
                            Vida = Convert.ToBoolean(reader["Vida"]),
                            Atmosfera = Convert.ToBoolean(reader["Atmosfera"])
                        };
                        nuevaLista.Add(objeto);
                    }
                }
            }


            return nuevaLista;
        }

        public List<ObjetoGalactico> MostrarDesdeFecha(DateTime fecha)
        {
            List<ObjetoGalactico> nuevaLista = new List<ObjetoGalactico>();

            string queryString = "SELECT * FROM ObjetoGalactico WHERE Descubrimiento < @Fecha";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                cmd.Parameters.AddWithValue("@Fecha", fecha);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ObjetoGalactico objeto = new ObjetoGalactico
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Tipo = Convert.ToString(reader["Tipo"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Descubrimiento = Convert.ToDateTime(reader["Descubrimiento"]),
                            Tamano = Convert.ToSingle(reader["Tamano"]),
                            DistanciaTierra = Convert.ToSingle(reader["DistanciaTierra"]),
                            Agua = Convert.ToBoolean(reader["Agua"]),
                            Vida = Convert.ToBoolean(reader["Vida"]),
                            Atmosfera = Convert.ToBoolean(reader["Atmosfera"])
                        };
                        nuevaLista.Add(objeto);
                    }
                }
            }

            return nuevaLista;
        }
        public int UltimoId()
        {
            int ultimoId = 0;

            string queryString = "SELECT MAX(Id) FROM ObjetoGalactico";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    ultimoId = Convert.ToInt32(result);
                }
            }

            return ultimoId + 1;
        }
        public bool ExisteConexion()
        {
            bool respuesta;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    respuesta = true;
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool CrearData()
        {
            Boolean respuesta;
            string connectionStringLocalDB = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\BDObjetosGalacticos.mdf;Integrated Security=True;Connect Timeout=30";


            try
            {
              
                bool existeBD;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM sys.databases WHERE name = 'BDObjetosGalacticos'", connection);
                    int count = (int)command.ExecuteScalar();

                    existeBD = (count > 0);
                }

               
                if (!existeBD)
                {
                    using (SqlConnection connection = new SqlConnection(connectionStringLocalDB))
                    {
                        connection.Open();
                        SqlCommand createDatabaseCommand = new SqlCommand("CREATE DATABASE BDObjetosGalacticos", connection);
                        createDatabaseCommand.ExecuteNonQuery();
                    }
                    respuesta = true;
                }
                else
                {
                    respuesta = true; 
                }
            }
            catch (Exception)
            {
                respuesta = false;
            }
            return respuesta;
        }

        public void NormalizarData()
        {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ObjetoGalactico'", connection);
                    int count = (int)command.ExecuteScalar();

                    if (count == 0)
                    {
                        SqlCommand createTableCommand = new SqlCommand(
                            @"CREATE TABLE [dbo].[ObjetoGalactico] (
                        [Id]              INT          NOT NULL,
                        [Tipo]            VARCHAR (30) NOT NULL,
                        [Nombre]          VARCHAR (30) NOT NULL,
                        [Descubrimiento]  DATE         NOT NULL,
                        [Tamano]          FLOAT        NOT NULL,
                        [DistanciaTierra] FLOAT        NOT NULL,
                        [Agua]            BIT          NOT NULL,
                        [Vida]            BIT          NOT NULL,
                        [Atmosfera]       BIT          NOT NULL,
                        PRIMARY KEY CLUSTERED ([Id] ASC)
                    )", connection);

                        createTableCommand.ExecuteNonQuery();
                    }
                }
           
        }


        // Lanzar procedimiento almacenado
        public List<String> LanzarProcedimiento()
        {
            List<String> mensajes = new List<String>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("PruebaProcedimientoAlmacenado", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@param", "Contar elementos");
                        cmd.Parameters.AddWithValue("@paramNombreTabla", "ObjetoGalactico");
                        cmd.Parameters.Add("@param_salida", SqlDbType.VarChar, 150).Direction = ParameterDirection.Output;

                        SqlParameter paramData = cmd.Parameters.Add("@param_data", SqlDbType.Int);
                        paramData.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();

                        string respuesta = cmd.Parameters["@param_salida"].Value.ToString();
                        int count = Convert.ToInt32(cmd.Parameters["@param_data"].Value);

                        mensajes.Add(respuesta);
                        mensajes.Add(count.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                mensajes.Add("Error al lanzar el procedimiento: " + ex.Message);
            }

            return mensajes;
        }

    }

}