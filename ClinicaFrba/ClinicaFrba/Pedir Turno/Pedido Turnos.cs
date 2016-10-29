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
        private int id;
        private Boolean searchName;
        private Boolean searchSurname;
        private Boolean searchSpecialty;
        private string queryDeLoadTable = "SELECT U.APELLIDO AS Apellido, U.NOMBRE AS Profesional, U.ID_USUARIO, E.DESCRIPCION_ESPECIALIDAD AS Especialidad FROM [3FG].USUARIOS U, [3FG].PROFESIONALES P, [3FG].ESPECIALIDAD_PROFESIONAL EP, [3FG].ESPECIALIDADES E WHERE (U.ID_USUARIO = P.ID_USUARIO) AND (P.ID_USUARIO = EP.ID_USUARIO) AND (EP.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD)";

        public ABMTurnos()
        {
            InitializeComponent();
            loadTable(queryDeLoadTable);
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
                dataGridView1.Columns[2].Visible = false;
                conexion.Close();
            }    
        }

        private void ABMTurnos_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (doctorElegido != null)
            {
                Elegir_Horario eh = new Elegir_Horario(this, doctorElegido, id);
                this.Hide();
                eh.Closed += (s, args) => this.Close();
                eh.Show();
            }

            if (doctorElegido == null)
            {
                MessageBox.Show("No ha seleccionado un profesional", "Error", MessageBoxButtons.OK);
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex < 2)
            {
                object apellido = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                object nombre = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value;
                object id = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex+2].Value;
                doctorElegido = apellido.ToString();
                this.id = Int32.Parse(id.ToString());
                label1.Text = "Profesional Elegido: " + doctorElegido + ", " + nombreBienEscrito(nombre) ;
            }
        }

        private string nombreBienEscrito(object nombre)
        {
            string nombreString = nombre.ToString();
            char primerLetra = nombreString[0];
            string elResto = nombreString.Remove(0, 1).ToLower();
            string todoJunto = primerLetra + elResto;
            return todoJunto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newQuery = this.queryDeLoadTable;
            string nombre = textBox1.Text;
            string apellido = textBox2.Text;
            string especialidad = textBox3.Text;
            if (searchName) newQuery += " AND (U.NOMBRE LIKE '%" + nombre + "%')";
            if (searchSurname) newQuery += " AND (U.APELLIDO LIKE '%" + apellido + "%')";
            if (searchSpecialty) newQuery += " AND (E.DESCRIPCION_ESPECIALIDAD LIKE '%" + especialidad + "%')";
            loadTable(newQuery);
        }

       
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) searchName = true;
            else searchName = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) searchSurname = true;
            else searchSurname = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) searchSpecialty = true;
            else searchSpecialty = false;
        }
    }
}
