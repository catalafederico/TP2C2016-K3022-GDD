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
        private int idDoctor;
        private int idAfiliado;
        private string queryDeLoadTable = "SELECT U.APELLIDO AS Apellido, U.NOMBRE AS Nombre, U.ID_USUARIO, E.DESCRIPCION_ESPECIALIDAD AS Especialidad FROM [3FG].USUARIOS U, [3FG].PROFESIONALES P, [3FG].ESPECIALIDAD_PROFESIONAL EP, [3FG].ESPECIALIDADES E WHERE (U.ID_USUARIO = P.ID_USUARIO) AND (P.ID_USUARIO = EP.ID_USUARIO) AND (EP.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD)";

        public ABMTurnos(int id)
        {
            this.idAfiliado = id;
            InitializeComponent();
            BDComun.loadDataGrid(queryDeLoadTable,dataGridView1);
            dataGridView1.Columns[2].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (doctorElegido != null)
            {
                Elegir_Horario eh = new Elegir_Horario(doctorElegido, idDoctor, idAfiliado);
                this.Hide();
                eh.Closed += (s, args) => this.Close();
                eh.Show();
            }

            if (doctorElegido == null)
            {
                MessageBox.Show("No ha seleccionado un profesional", "Error", MessageBoxButtons.OK);
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
            if (checkBox1.Checked) newQuery += " AND (U.NOMBRE LIKE '%" + nombre + "%')";
            if (checkBox2.Checked) newQuery += " AND (U.APELLIDO LIKE '%" + apellido + "%')";
            if (checkBox3.Checked) newQuery += " AND (E.DESCRIPCION_ESPECIALIDAD LIKE '%" + especialidad + "%')";
            BDComun.loadDataGrid(newQuery, dataGridView1);
        }

 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            object apellido = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            object nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            object id = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
            doctorElegido = apellido.ToString();
            this.idDoctor = Int32.Parse(id.ToString());
            label1.Text = "Profesional Elegido: " + doctorElegido + ", " + nombreBienEscrito(nombre);
        }
    }
}
