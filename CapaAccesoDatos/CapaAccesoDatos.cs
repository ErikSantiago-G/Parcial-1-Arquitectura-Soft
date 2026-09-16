using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;



namespace CapaAccesoDatos
{
    public class CapaAccesoDatos
    {
        private SqlConnection conexion;

        public ConexionSQlServer() {
            this.conexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"ParcialCentralRiesgo\";Command Timeout=0;Initial Catalog=BD_Central_Riesgo")
        }
        public bool ConsultarPuntaje(string TipoDoc, string NroDoc)
        {
            int puntaje = 0;
            
            string select = string.Format ("Select * FROM CentralRiesgo WHERE TipoDoc = '{0}' AND NroDoc ='{1}' ", TipoDoc, NroDoc);

            conexion.Open();

            SqlCommand comando = new SqlCommand(select, conexion);
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                puntaje = reader.GetInt32(2);

            }
            conexion.Close();
        }
    }
}
