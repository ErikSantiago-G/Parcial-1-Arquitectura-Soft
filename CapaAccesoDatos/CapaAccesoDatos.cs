using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace CapaAccesoDatos
{
    public class CapaAccesoDatos
    {
        private SqlConnection conexion;

        public CapaAccesoDatos()
        {
            this.conexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"ParcialCentralRiesgo\";Command Timeout=0;Initial Catalog=BD_CENTRAL_RIESGO");
        }

        public int ConsultarPuntaje(string TipoDoc, string NroDoc)
        {
            int puntaje = 0;

            string select = string.Format(
                "SELECT * FROM CentralRiesgo WHERE TipoDoc = '{0}' AND NroDoc = '{1}' ",
                TipoDoc, NroDoc);

            conexion.Open();

            SqlCommand comando = new SqlCommand(select, conexion);
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                puntaje = reader.GetInt32(2);
            }

            conexion.Close();

            return puntaje;
        }

        public bool registrarPuntaje(string TipoDoc, string NroDoc, int puntaje)
        {
            string insert = string.Format(
                "INSERT INTO CentralRiesgo VALUES ('{0}', '{1}', '{2}')",
                TipoDoc, NroDoc, puntaje);

            conexion.Open();

            SqlCommand comando = new SqlCommand(insert, conexion);

            int filasAfectadas = comando.ExecuteNonQuery();

            conexion.Close();

            if (filasAfectadas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool eliminarPuntaje(string tipoDoc, string nroDoc)
        {
            string delete = string.Format(
                "DELETE FROM CentralRiesgo WHERE TipoDoc = '{0}' AND NroDoc = '{1}'",
                tipoDoc, nroDoc);

            conexion.Open();

            SqlCommand comando = new SqlCommand(delete, conexion);

            int filasAfectadas = comando.ExecuteNonQuery();

            conexion.Close();

            if (filasAfectadas == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}