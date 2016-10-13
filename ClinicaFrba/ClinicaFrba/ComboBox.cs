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


namespace ClinicaFrba
{
    class ComboBox
    {
        public static DataTable buscarFuncionalidadDeRol(int numero)
        {


            List<ComboBox> Lista = new List<ComboBox>();

            using (SqlConnection conexion = BDComun.obtenerConexion())
            {

                SqlCommand comando = new SqlCommand(string.Format("select NOMBRE from [3FG].FUNCIONALIDADES F join [3FG].FUNCIONALIDADES_ROL R on(f.ID_FUNCIONALIDAD = R.ID_FUNCIONALIDAD) join [3FG].ROLES L on(R.ID_ROL =L.ID_ROL) where L.ID_ROL={0}", numero), conexion);

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