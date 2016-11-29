using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClinicaFrba.ABM_Rol
{
    class CRol
    {


        public static DataTable obtenerFuncionalidades()
        {

            List<CRol> Lista = new List<CRol>();

            using (SqlConnection conexion = BDComun.obtenerConexion())
            {
                
                SqlCommand comando = new SqlCommand(string.Format("SELECT NOMBRE FROM [3FG].FUNCIONALIDADES"), conexion);

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
