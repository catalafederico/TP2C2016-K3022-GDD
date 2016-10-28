using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Registrar_Agenda_Medico
{
    public partial class RegistrarAgenda : Form
    {
        string nombreUsuario;
        Int64 idPro;

        public RegistrarAgenda(String username,Int64 idProfe)
        {
            InitializeComponent();
            nombreUsuario = username;
            idPro = idProfe;

            /*string query2 = "SELECT ID_USUARIO FROM [3FG].USUARIOS WHERE USUARIO_NOMBRE = '" + nombreUsuario + "'";
            DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
            string idProf = dt2.Rows[0][0].ToString();
            idPro = Convert.ToInt64(idProf);*/

            DataTable dt = (new ConexionSQL()).cargarTablaSQL("SELECT DESCRIPCION_ESPECIALIDAD FROM [3FG].ESPECIALIDAD_PROFESIONAL EP JOIN [3FG].ESPECIALIDADES E ON (E.ID_ESPECIALIDAD = EP.ID_ESPECIALIDAD) WHERE EP.ID_USUARIO = '" + idPro + "'");
            comboBoxLunes.DataSource = dt.DefaultView;
            comboBoxLunes.ValueMember = "DESCRIPCION_ESPECIALIDAD";
            comboBoxMartes.DataSource = dt.DefaultView;
            comboBoxMartes.ValueMember = "DESCRIPCION_ESPECIALIDAD";
            comboBoxMiercoles.DataSource = dt.DefaultView;
            comboBoxMiercoles.ValueMember = "DESCRIPCION_ESPECIALIDAD";
            comboBoxJueves.DataSource = dt.DefaultView;
            comboBoxJueves.ValueMember = "DESCRIPCION_ESPECIALIDAD";
            comboBoxViernes.DataSource = dt.DefaultView;
            comboBoxViernes.ValueMember = "DESCRIPCION_ESPECIALIDAD";
            comboBoxSabado.DataSource = dt.DefaultView;
            comboBoxSabado.ValueMember = "DESCRIPCION_ESPECIALIDAD";
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtInicioLunes.Text != null && txtFinLunes.Text != null) {

                Agenda agenda = new Agenda();
                agenda.idProfesional = idPro;
                string query2 = "SELECT ID_ESPECIALIDAD FROM [3FG].ESPECIALIDADES WHERE DESCRIPCION_ESPECIALIDAD = '" + comboBoxLunes.Text + "'";
                DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
                string idCliente = dt2.Rows[0][0].ToString();
                Int64 idEspe = Convert.ToInt64(idCliente);
                agenda.idEspecialidad = idEspe;
                agenda.dia = "Lunes";
                agenda.horaInicio = txtInicioLunes.Text;
                agenda.horaFin = txtFinLunes.Text;
                int resultado = AgendaDAL.agregarAgenda(agenda);
            
            }

            if (txtInicioMartes.Text != null && txtFinMartes.Text != null)
            {

                Agenda agenda = new Agenda();
                agenda.idProfesional = idPro;
                string query2 = "SELECT ID_ESPECIALIDAD FROM [3FG].ESPECIALIDADES WHERE DESCRIPCION_ESPECIALIDAD = '" + comboBoxMartes.Text + "'";
                DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
                string idCliente = dt2.Rows[0][0].ToString();
                Int64 idEspe = Convert.ToInt64(idCliente);
                agenda.idEspecialidad = idEspe;
                agenda.dia = "Martes";
                agenda.horaInicio = txtInicioMartes.Text;
                agenda.horaFin = txtFinMartes.Text;
                int resultado = AgendaDAL.agregarAgenda(agenda);

            }

            if (txtInicioMiercoles.Text != null && txtFinMiercoles.Text != null)
            {

                Agenda agenda = new Agenda();
                agenda.idProfesional = idPro;
                string query2 = "SELECT ID_ESPECIALIDAD FROM [3FG].ESPECIALIDADES WHERE DESCRIPCION_ESPECIALIDAD = '" + comboBoxMiercoles.Text + "'";
                DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
                string idCliente = dt2.Rows[0][0].ToString();
                Int64 idEspe = Convert.ToInt64(idCliente);
                agenda.idEspecialidad = idEspe;
                agenda.dia = "Miercoles";
                agenda.horaInicio = txtInicioMiercoles.Text;
                agenda.horaFin = txtFinMiercoles.Text;
                int resultado = AgendaDAL.agregarAgenda(agenda);

            }

            if (txtInicioJueves.Text != null && txtFinJueves.Text != null)
            {

                Agenda agenda = new Agenda();
                agenda.idProfesional = idPro;
                string query2 = "SELECT ID_ESPECIALIDAD FROM [3FG].ESPECIALIDADES WHERE DESCRIPCION_ESPECIALIDAD = '" + comboBoxJueves.Text + "'";
                DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
                string idCliente = dt2.Rows[0][0].ToString();
                Int64 idEspe = Convert.ToInt64(idCliente);
                agenda.idEspecialidad = idEspe;
                agenda.dia = "Jueves";
                agenda.horaInicio = txtInicioJueves.Text;
                agenda.horaFin = txtFinJueves.Text;
                int resultado = AgendaDAL.agregarAgenda(agenda);

            }

            if (txtInicioViernes.Text != null && txtFinViernes.Text != null)
            {

                Agenda agenda = new Agenda();
                agenda.idProfesional = idPro;
                string query2 = "SELECT ID_ESPECIALIDAD FROM [3FG].ESPECIALIDADES WHERE DESCRIPCION_ESPECIALIDAD = '" + comboBoxViernes.Text + "'";
                DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
                string idCliente = dt2.Rows[0][0].ToString();
                Int64 idEspe = Convert.ToInt64(idCliente);
                agenda.idEspecialidad = idEspe;
                agenda.dia = "Viernes";
                agenda.horaInicio = txtInicioViernes.Text;
                agenda.horaFin = txtFinViernes.Text;
                int resultado = AgendaDAL.agregarAgenda(agenda);

            }

            if (txtInicioSabado.Text != null && txtFinSabado.Text != null)
            {

                Agenda agenda = new Agenda();
                agenda.idProfesional = idPro;
                string query2 = "SELECT ID_ESPECIALIDAD FROM [3FG].ESPECIALIDADES WHERE DESCRIPCION_ESPECIALIDAD = '" + comboBoxSabado.Text + "'";
                DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
                string idCliente = dt2.Rows[0][0].ToString();
                Int64 idEspe = Convert.ToInt64(idCliente);
                agenda.idEspecialidad = idEspe;
                agenda.dia = "Sabado";
                agenda.horaInicio = txtInicioSabado.Text;
                agenda.horaFin = txtFinSabado.Text;
                int resultado = AgendaDAL.agregarAgenda(agenda);

            }

            if (txtInicioLunes.Text != null && txtFinLunes.Text != null && txtInicioMartes.Text != null && txtFinMartes.Text != null && txtInicioMiercoles.Text != null && txtFinMiercoles.Text != null && txtInicioJueves.Text != null && txtFinJueves.Text != null && txtInicioViernes.Text != null && txtFinViernes.Text != null && txtInicioSabado.Text != null && txtFinSabado.Text != null) {
                MessageBox.Show("No hay ningun horario completado", "No se agrego ninguna agenda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
