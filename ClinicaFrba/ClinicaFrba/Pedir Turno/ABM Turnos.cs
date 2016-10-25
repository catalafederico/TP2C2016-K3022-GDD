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
        private string doctorElegido;

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
            loadTable("SELECT TOP 100 NOMBRE FROM [3FG].USUARIOS");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = textBox1.Text;
            string busqueda = "SELECT NOMBRE FROM [3FG].USUARIOS WHERE NOMBRE LIKE '%" + terminoBusqueda + "%'";
            loadTable(busqueda);
        }

        private void ABMTurnos_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (doctorElegido != null)
            {
                Elegir_Horario eh = new Elegir_Horario(this, doctorElegido);
                this.Hide();
                eh.Closed += (s, args) => this.Close();
                eh.Show();
            }

            if (doctorElegido == null)
            {
                MessageBox.Show("No ha seleccionado un profesional", "Error");
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                object click = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                doctorElegido = click.ToString();
                label1.Text = "Profesional Elegido: " + doctorElegido;
            }
        }

    }
}
