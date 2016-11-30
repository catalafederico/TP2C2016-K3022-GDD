using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClinicaFrba.Elegir_Rol
{
    class Rol
    {
        public static DataTable buscarRolesDeUsuario(String usuario) {

            List<Rol> Lista = new List<Rol>();

            using (SqlConnection conexion = new ConexionSQL().conectar())
            {

                SqlCommand comando = new SqlCommand(string.Format("SELECT NOMBRE_ROL FROM [3FG].ROLES R JOIN [3FG].ROLES_USUARIO RU ON (R.ID_ROL = RU.ID_ROL) JOIN [3FG].USUARIOS U ON (RU.ID_USUARIO = U.ID_USUARIO) WHERE USUARIO_NOMBRE = '{0}' AND R.HABILITADO = 1", usuario),conexion);

                /*SqlDataReader reader = comando.ExecuteReader();*/

                DataTable ds = new DataTable();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                
                dataAdapter.Fill(ds);

                conexion.Close();

                return ds;

            }
        
        }
    }
}
