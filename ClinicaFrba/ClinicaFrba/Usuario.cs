using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClinicaFrba
{
    class Usuario
    {

        public static int loginUsuario(String nombre, String contraseñaEncriptada)
        {
            int resultado = -1;

            SqlConnection conexion = new ConexionSQL().conectar();

            SqlCommand unaQuery = new SqlCommand(string.Format("SELECT * FROM [3FG].USUARIOS WHERE USUARIO_NOMBRE ='{0}' AND CONTRASEÑA = '{1}'", nombre, contraseñaEncriptada), conexion);

            SqlDataReader reader = unaQuery.ExecuteReader();


            while (reader.Read())
            {
                resultado = 50;
            }

            conexion.Close();

            return resultado;

        }

    }
}
