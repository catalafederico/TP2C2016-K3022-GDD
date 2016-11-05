using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace ClinicaFrba
{
    public class BDComun
    {
        public static SqlConnection obtenerConexion()
        {
            SqlConnection conexion = new SqlConnection("Data Source=.\\SQLSERVER2012;Initial Catalog=GD2C2016;Integrated Security=True");
            conexion.Open();
            return conexion;
        }

        public static void loadDataGrid(string query, DataGridView dgv)
        {
            using (SqlConnection conexion = BDComun.obtenerConexion())
            {
                SqlCommand comando = new SqlCommand(query, conexion);
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(dataTable);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dataTable;
                dgv.DataSource = bSource;
                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                conexion.Close();
            }
        }
    }
}
