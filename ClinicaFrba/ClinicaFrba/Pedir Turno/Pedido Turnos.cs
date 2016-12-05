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
        private int idEspecialidad;
        private string doctorElegido;
        private int idDoctor;
        private int idAfiliado;

        // Esta query busca todos el ID, nombre y apellido de cada profesional ademas de su especialidad y el ID de la misma
        private string queryDeLoadTable = "SELECT U.APELLIDO AS Apellido, U.NOMBRE AS Nombre, U.ID_USUARIO, E.DESCRIPCION_ESPECIALIDAD AS Especialidad, E.ID_ESPECIALIDAD FROM [3FG].USUARIOS U, [3FG].PROFESIONALES P, [3FG].ESPECIALIDAD_PROFESIONAL EP, [3FG].ESPECIALIDADES E WHERE (U.ID_USUARIO = P.ID_USUARIO) AND (P.ID_USUARIO = EP.ID_USUARIO) AND (EP.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD)";




        // Funcion de inicializacion
        public ABMTurnos(int id)
        {
            // Setteo el id del afiliado
            this.idAfiliado = id;
            InitializeComponent();

            // Cargo el DataGrid
            ConexionSQL.loadDataGrid(queryDeLoadTable,dataGridView1);

            // Escondo las columnas de ID, me viene bien tenerlos a mano pero no quiero que el usuario los vea
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        

        // Funcion de busqueda
        private void button1_Click(object sender, EventArgs e)
        {
            // Agarro la query que use para settear el DataGrid
            string newQuery = this.queryDeLoadTable;

            // Agarro los strings del textBox
            string nombre = textBox1.Text;
            string apellido = textBox2.Text;
            string especialidad = textBox3.Text;

            // Me fijo que casilleros fueron checkeados modifico la primera query en base a ellos
            if (checkBox1.Checked) newQuery += " AND (U.NOMBRE LIKE '%" + nombre + "%')";
            if (checkBox2.Checked) newQuery += " AND (U.APELLIDO LIKE '%" + apellido + "%')";
            if (checkBox3.Checked) newQuery += " AND (E.DESCRIPCION_ESPECIALIDAD LIKE '%" + especialidad + "%')";

            // Vuelvo a cargar el DataGrid
            ConexionSQL.loadDataGrid(newQuery, dataGridView1);
        }



        // Funcion de eleccion de profesional y especialidad por click en el DataGrid
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Me fijo que el usuario no haya clickeado el nombre de la columna
            if (e.RowIndex >= 0)
            {
                // Tomo toda la informacion de la fila que fue clickeada
                object apellido = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                object nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                object idPro = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                object especialidad = dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                object idEsp = dataGridView1.Rows[e.RowIndex].Cells[4].Value;

                // Setteo las variables
                doctorElegido = apellido.ToString();
                this.idDoctor = int.Parse(idPro.ToString());
                this.idEspecialidad = int.Parse(idEsp.ToString());

                // Modifico la ventana para reflejar la eleccion del usuario
                label1.Text = "Profesional Elegido: " + doctorElegido + ", " + nombreBienEscrito(nombre);
                label6.Text = "Especialidad Elegida: " + especialidad.ToString();
            }
        }




       // Funcion para pasar a elegir el turno
        private void button3_Click(object sender, EventArgs e)
        {
            // Si elegi un doctor
            if (doctorElegido != null)
            {
                if(profesionalValido())
                {
                    // Creo una nueva ventana para elegir horario con los datos que necesita
                    Elegir_Horario eh = new Elegir_Horario(doctorElegido, idDoctor, idAfiliado, idEspecialidad);

                    // Escondo esta venta
                    this.Hide();

                    // Y modifico a la nueva ventana para que al cerrarse cierre esta tambien
                    eh.Closed += (s, args) => this.Close();
                    eh.Show();
                }
                else MessageBox.Show("El profesional elegido no tiene un inicio o fin de disponibilidad validos. Disculpe las molestias", "Error", MessageBoxButtons.OK);
            }
            else MessageBox.Show("No ha seleccionado un profesional", "Error", MessageBoxButtons.OK);
        }


        private bool profesionalValido()
        {
            bool ph = true;
            SqlCommand dateEarly = new SqlCommand("SELECT P.INICIO_DISPONIBILIDAD FROM [3FG].PROFESIONALES P WHERE P.ID_USUARIO LIKE '" + idDoctor + "'", new ConexionSQL().conectar());
            if (dateEarly.ExecuteScalar().ToString() == "") { ph = false; }
            SqlCommand dateLate = new SqlCommand("SELECT P.FIN_DISPONIBILIDAD FROM [3FG].PROFESIONALES P WHERE P.ID_USUARIO LIKE '" + idDoctor + "'", new ConexionSQL().conectar());
            if (dateLate.ExecuteScalar().ToString() == "") { ph = false; }
            return ph;
        }

        // Funcion para propositos esteticos nomas
        private string nombreBienEscrito(object nombre)
        {
            string nombreString = nombre.ToString();
            char primerLetra = nombreString[0];
            string elResto = nombreString.Remove(0, 1).ToLower();
            string todoJunto = primerLetra + elResto;
            return todoJunto;
        }

        private void ABMTurnos_Load(object sender, EventArgs e)
        {

        }

    }
}
