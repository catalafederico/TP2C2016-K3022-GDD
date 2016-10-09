using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Elegir_Rol
{
    public partial class EleccionRol : Form
    {
        public EleccionRol(String usuario)
        {
            InitializeComponent();

            DataTable dt = Rol.buscarRolesDeUsuario(usuario);
            comboBoxRoles.DataSource = dt.DefaultView;
            comboBoxRoles.ValueMember = "NOMBRE_ROL";

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
