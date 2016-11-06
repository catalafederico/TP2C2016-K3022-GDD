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
    public partial class EfectivizarLlegada : Form
    {
        private int idTurno;
        private int idBono;
        string queryBonos = "SELECT B.ID_BONO FROM [3FG].AFILIADOS A JOIN [3FG].TURNOS T ON (A.ID_USUARIO = T.ID_AFILIADO) JOIN [3FG].BONOS B ON (A.ID_USUARIO = B.ID_USUARIO) WHERE T.ID_TURNO = ";

        public EfectivizarLlegada(int turno)
        {
            InitializeComponent();
            idTurno = turno;
            string queryBonosCompleto = queryBonos + turno.ToString();
            BDComun.loadDataGrid(queryBonosCompleto, dataGridView1);
            dataGridView1.Columns[2].Visible = false;


        }

        private void EfectivizarLlegada_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            object bonoElegido = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            this.idBono = Int32.Parse(bonoElegido.ToString());
            label2.Text = "Bono Elegido: " + bonoElegido.ToString();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            string insertar = "INSERT INTO [3FG].RECEPCIONES (ID_TURNO,ID_BONO,FECHA_RECEPCIONES) VALUES (" + idTurno.ToString() + "," + idBono.ToString() + ", @Fecha)";
            SqlCommand unInsert = new SqlCommand(insertar);
            unInsert.Connection = BDComun.obtenerConexion();
            unInsert.Parameters.Add("@Fecha", SqlDbType.DateTime, 8).Value = fechaYHora;
            unInsert.ExecuteNonQuery();


        }
    }
}
