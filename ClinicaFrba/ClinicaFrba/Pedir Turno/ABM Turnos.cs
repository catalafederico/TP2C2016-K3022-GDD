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

namespace ClinicaFrba.Pedir_Turno
{
    public partial class ABMTurnos : Form
    {
        public ABMTurnos()
        {
            InitializeComponent();
        }

        private void loadTable(string query)
        {
            using (SqlConnection conexion = BDComun.obtenerConexion())
            {
                SqlCommand comando = new SqlCommand(query, conexion);
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(dataTable);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dataTable;
                dataGridView1.DataSource = bSource;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                conexion.Close();
            }    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadTable("SELECT NOMBRE FROM [3FG].USUARIOS");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = textBox1.Text;
            string busqueda = "SELECT NOMBRE FROM [3FG].USUARIOS WHERE NOMBRE LIKE '%" + terminoBusqueda + "%'";
            loadTable(busqueda);
        }

    }
}
