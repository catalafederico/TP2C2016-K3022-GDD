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

        public static int loginUsuario(String nombre, String contraseña)
        {
            SqlConnection unaConexion;
            int resultado = -1;
            unaConexion = new SqlConnection("Data Source=DESKTOP-T5SL9S1;Initial Catalog=GD2C2016;Integrated Security=True");
            unaConexion.Open();
            SqlCommand unaQuery = new SqlCommand(string.Format("select *from [3FG].USUARIOS where USUARIO_NOMBRE ='{0}' and PASSWORD ='{1}'", nombre, contraseña), unaConexion);


            SqlDataReader reader = unaQuery.ExecuteReader();


            while (reader.Read())
            {
                resultado = 50;
            }
            unaConexion.Close();
            return resultado;
        }
    }
}
