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

        public RegistrarAgenda(String username)
        {
            InitializeComponent();
            nombreUsuario = username;

            string query2 = "SELECT ID_USUARIO FROM [3FG].USUARIOS WHERE USUARIO_NOMBRE = '" + nombreUsuario + "'";
            DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
            string idCliente = dt2.Rows[0][0].ToString();
            idPro = Convert.ToInt64(idCliente);

            DataTable dt = (new ConexionSQL()).cargarTablaSQL("SELECT DESCRIPCION_ESPECIALIDAD FROM [3FG].ESPECIALIDAD_PROFESIONAL EP JOIN [3FG].ESPECIALIDADES E ON (E.ID_USUARIO = EP.ID_USUARIO AND E.ID_ESPECIALIDAD = EP.ID_ESPECIALIDAD) WHERE E.ID_USUARIO = '" + idPro);
            comboBoxLunes.DataSource = dt.DefaultView;
            comboBoxLunes.ValueMember = "NOMBRE";
            comboBoxMartes.DataSource = dt.DefaultView;
            comboBoxMartes.ValueMember = "NOMBRE";
            comboBoxMiercoles.DataSource = dt.DefaultView;
            comboBoxMiercoles.ValueMember = "NOMBRE";
            comboBoxJueves.DataSource = dt.DefaultView;
            comboBoxJueves.ValueMember = "NOMBRE";
            comboBoxViernes.DataSource = dt.DefaultView;
            comboBoxViernes.ValueMember = "NOMBRE";
            comboBoxSabado.DataSource = dt.DefaultView;
            comboBoxSabado.ValueMember = "NOMBRE";
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtInicioLunes.Text != null && txtFinLunes.Text != null) {

                Agenda agenda = new Agenda();
               /* agenda.idProfesional = idProf;
                agenda.idEspecialidad = idEspe;*/
            
            }
        }
    }
}
