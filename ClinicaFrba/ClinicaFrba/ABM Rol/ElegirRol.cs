using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.ABM_Rol
{
    public partial class ElegirRol : Form
    {


        String elegirFormato;
        ModificarRol modificarRol;
        string rol;
        public ElegirRol(String formato, string rolPasado)
        {
            elegirFormato = formato;
            InitializeComponent();
            label1.Text = formato;
            buttonGuardar.Text = formato;
            rol = rolPasado;

            if (elegirFormato == "Eliminar Rol")
            {
                string query = "SELECT NOMBRE_ROL FROM [3FG].ROLES WHERE HABILITADO = 1";
                DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                comboBoxRol.DataSource = dt.DefaultView;
                comboBoxRol.ValueMember = "NOMBRE_ROL";
            }
            else if (elegirFormato == "Modificar Rol")
            {
                string query = "SELECT NOMBRE_ROL FROM [3FG].ROLES";
                DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                comboBoxRol.DataSource = dt.DefaultView;
                comboBoxRol.ValueMember = "NOMBRE_ROL";
            }




        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ElegirRol_Load(object sender, EventArgs e)
        {

        }

        private void buttonGuardar_Click_1(object sender, EventArgs e)
        {
            if (rol == comboBoxRol.Text)
            {
                MessageBox.Show("No se puede modificar o eliminar el rol utilizado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (elegirFormato == "Eliminar Rol")
            {
                if ((MessageBox.Show("¿Realmente desea dar de baja el rol " + comboBoxRol.Text + "?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    string query = "UPDATE [3FG].ROLES SET HABILITADO = 0 WHERE NOMBRE_ROL = '" + comboBoxRol.Text + "'";
                    DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                    MessageBox.Show("Rol " + comboBoxRol.Text + " eliminado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else if (elegirFormato == "Modificar Rol")
            {
                modificarRol = new ModificarRol(comboBoxRol.Text, this);
                modificarRol.ShowDialog();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
