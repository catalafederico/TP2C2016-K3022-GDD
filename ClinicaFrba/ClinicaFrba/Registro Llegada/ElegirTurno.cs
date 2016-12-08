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
    public partial class ElegirTurno : Form
    {
        //Esta es la query con la que se cargara la tabla. Se buscan todos los turnos del dia en cuestion, para dicho profesional, en dicha especialidad, junto con el afiliado que solicito el turno, excluyendo turnos cancelados, turnos ya concurridos y afiliados inhabilitados.
        private string queryDeBusquedaTurnos = "select T.ID_TURNO, T.FECHA_TURNO, T.ID_AFILIADO,T.ID_AGENDA, U.APELLIDO, U.NOMBRE, AF.RAIZ_AFILIADO, AF.NUMERO_FAMILIA from [3FG].AGENDA A JOIN [3FG].ESPECIALIDAD_PROFESIONAL E ON (A.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD) JOIN [3FG].PROFESIONALES P ON (P.ID_USUARIO = E.ID_USUARIO) JOIN [3FG].TURNOS T ON (A.ID_AGENDA = T.ID_AGENDA) JOIN [3FG].USUARIOS U ON (U.ID_USUARIO = T.ID_AFILIADO) JOIN [3FG].AFILIADOS AF ON (AF.ID_USUARIO = U.ID_USUARIO) WHERE P.ID_USUARIO = @id_Profesional AND E.ID_ESPECIALIDAD = @id_especialidad AND T.ID_TURNO NOT IN (SELECT ID_TURNO FROM [3FG].RECEPCIONES) AND T.ID_TURNO NOT IN (SELECT C.ID_TURNO FROM [3FG].CANCELACIONES C) AND CONVERT(date, T.FECHA_TURNO) = CONVERT(date, @Fecha) AND U.HABILITADO = 1";
        private int id_turno = -1;
        private int id_afiliado;
        private int raizAfiliado;
        private string apellidoDeAfiliado;
        private string nombreDeAfiliado;


        //Se cargan los datos de todos los turnos del dia con el profesional y la especialidad elegida
        public ElegirTurno(int id_profesional, int id_especilidad)
        {
            InitializeComponent();
            DateTime fechaYHora = DateTime.Parse(Program.nuevaFechaSistema());
            SqlCommand comando = new SqlCommand(queryDeBusquedaTurnos);
            comando.Parameters.Add("@id_Profesional", SqlDbType.Int).Value = id_profesional;
            comando.Parameters.Add("@id_especialidad", SqlDbType.Int).Value = id_especilidad;
            comando.Parameters.Add("@Fecha", SqlDbType.DateTime, 8).Value = fechaYHora;
            ConexionSQL.loadDataGridConSqlCommand(comando, dataGridView1);
        }

        private void ElegirTurno_Load(object sender, EventArgs e)
        {

        }

        //Pasamos a la ventana de eleccion del afiliado que hara uso del turno
        private void button1_Click(object sender, EventArgs e)
        {
            // Si elegi un Turno
            if (id_turno != -1)
            {
                // Creo una nueva ventana para elegir horario con los datos que necesita
                ElegirAfiliado eh = new ElegirAfiliado(id_turno, id_afiliado, raizAfiliado,apellidoDeAfiliado,nombreDeAfiliado);

                // Escondo esta venta
                this.Hide();

                // Y modifico a la nueva ventana para que al cerrarse cierre esta tambien
                eh.Closed += (s, args) => this.Close();
                eh.Show();
            }
            else MessageBox.Show("No ha seleccionado un Turno", "Error", MessageBoxButtons.OK);
        }


        //Se selecciona un turno
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Me fijo que el usuario no haya clickeado el nombre de la columna
            if (e.RowIndex >= 0)
            {
                // Tomo toda la informacion de la fila que fue clickeada
                object turno = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                object fecha = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                object afiliado = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                object agenda = dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                object apellido = dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                object nombre = dataGridView1.Rows[e.RowIndex].Cells[5].Value;
                object raiz = dataGridView1.Rows[e.RowIndex].Cells[6].Value;
                object nroFamilia = dataGridView1.Rows[e.RowIndex].Cells[7].Value;

                // Setteo las variables
                id_turno = int.Parse(turno.ToString());
                id_afiliado = int.Parse(afiliado.ToString());
                raizAfiliado = int.Parse(raiz.ToString());
                apellidoDeAfiliado = apellido.ToString();
                nombreDeAfiliado = nombre.ToString();

                // Modifico la ventana para reflejar la eleccion del usuario
                label1.Text = "Turno Elegido: " + id_turno.ToString();
                label3.Text = "Apellido del Afiliado: " + apellido.ToString();
                label4.Text = "Nombre del Afiliado: " + nombre.ToString();
            }
        }
    }
}
