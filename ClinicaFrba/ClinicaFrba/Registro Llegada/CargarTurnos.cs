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

namespace ClinicaFrba.Registro_Llegada
{
    public partial class CargarTurnos : Form
    {
        string queryTurnos = "SELECT T.ID_TURNO, T.ID_AFILIADO, T.FECHA_TURNO FROM [3FG].AGENDA A JOIN [3FG].TURNOS T ON (T.ID_AGENDA = A.ID_AGENDA) JOIN [3FG].PROFESIONALES P ON (A.ID_USUARIO = P.ID_USUARIO) WHERE CAST(T.FECHA_TURNO AS DATE) = CAST(@FechaDeHoy AS DATE)";
        private int turnoElegido;
        private int idProfesional;
        private int idTurno = 1;

        public CargarTurnos(int idPro)
        {
            this.idProfesional = idPro;
            InitializeComponent();
            DateTime fechaDeHoy = DateTime.Now;
            SqlCommand unaQuery = new SqlCommand(queryTurnos);
            unaQuery.Connection = new ConexionSQL().conectar();
            unaQuery.Parameters.Add("@FechaDeHoy", SqlDbType.DateTime, 8).Value = fechaDeHoy;
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(unaQuery);
            DataTable dt = new DataTable();
            sqlDataAdap.Fill(dt);
            dataGridView1.DataSource = dt;
            //BDComun.loadDataGrid(queryTurnos, dataGridView1);
            dataGridView1.Columns[2].Visible = false;
        }

        private void CargarTurnos_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (turnoElegido != null)
            {
                EfectivizarLlegada eh = new EfectivizarLlegada(turnoElegido);
                this.Hide();
                eh.Closed += (s, args) => this.Close();
                eh.Show();
            }

            if (turnoElegido == null)
            {
                MessageBox.Show("No ha seleccionado un profesional", "Error", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            object idTurnoElegido = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            object idAfiliado = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            object fecha = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
            this.turnoElegido = Int32.Parse(idTurnoElegido.ToString());
            label1.Text = "Turno elegido: " + idTurno.ToString();
        }
    }
}
