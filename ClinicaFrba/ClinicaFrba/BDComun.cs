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
            SqlConnection conexion = new SqlConnection("Data source=.\\SQLSERVER2012; Initial Catalog=GD2C2016;User Id=gd; Password=gd2016");
            conexion.Open();
            return conexion;

        }
    }
}
