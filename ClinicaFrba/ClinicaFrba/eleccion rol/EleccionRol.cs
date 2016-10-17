using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaFrba.Eleccion_Funcionalidad;


namespace ClinicaFrba.Elegir_Rol
{
    public partial class EleccionRol : Form
    {

        ElegirFuncionalidad funcionalidades;
        String rol;
        String nombreUsuario;

        public EleccionRol(String username)
        {
            InitializeComponent();
            /*
            DataTable dt = Rol.buscarRolesDeUsuario(usuario);
            comboBoxRoles.DataSource = dt.DefaultView;
            comboBoxRoles.ValueMember = "NOMBRE_ROL";*/



            nombreUsuario = username;
            DataTable dt = (new ConexionSQL()).cargarTablaSQL("SELECT R.NOMBRE_ROL FROM [3FG].ROLES_USUARIO RU JOIN [3FG].USUARIOS U ON (U.ID_USUARIO = RU.ID_USUARIO) JOIN [3FG].ROLES R ON (R.ID_ROL = RU.ID_ROL) WHERE U.USUARIO_NOMBRE = '" + username + "' AND R.HABILITADO = 1 AND U.HABILITADO = 1");
            comboBoxRoles.DataSource = dt.DefaultView;
            comboBoxRoles.ValueMember = "NOMBRE_ROL";

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {


            rol = comboBoxRoles.Text;
            if (rol == "Afiliado" || rol == "Profesional" || rol == "Administrativo")
            {
                funcionalidades = new ElegirFuncionalidad(rol, nombreUsuario);
                funcionalidades.ShowDialog();
            }
            else
            {
                funcionalidades = new ElegirFuncionalidad(rol, nombreUsuario);
                funcionalidades.ShowDialog();
            }



            /*if (comboBoxRoles.Text.Equals("Afiliado"))
            {
              
                ComboBoxGeneral unC = new ComboBoxGeneral(2);
                unC.ShowDialog();
                this.Hide();
            }else

            if (comboBoxRoles.Text.Equals("Profesional"))
            {
                ComboBoxGeneral unC = new ComboBoxGeneral(1);
                unC.ShowDialog();
                this.Hide();
              
            }else

            if (comboBoxRoles.Text.Equals("Administrativo"))
            {
                ComboBoxGeneral unC = new ComboBoxGeneral(1);
                unC.ShowDialog();
                this.Hide();
            }else


            if (comboBoxRoles.Text.Equals("Administrador general"))
            {
                ComboBoxGeneral unC = new ComboBoxGeneral(4);
                unC.ShowDialog();
                this.Hide();
            }*/


        }

        private void comboBoxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EleccionRol_Load(object sender, EventArgs e)
        {

        }
    }

}
