using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClinicaFrba
{
    public class BDComun
    {
        public static SqlConnection obtenerConexion()
        {
            SqlConnection conexion = new SqlConnection("Data Source=FEDE-PC\\SQLSERVER2012;Initial Catalog=GD2C2016;Integrated Security=True");
            conexion.Open();
            return conexion;
        }
    }
}
