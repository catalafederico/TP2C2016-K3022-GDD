using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba
{
    public partial class IniciarSesion : Form
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (Usuario.loginUsuario(txtUsuario.Text, txtContraseña.Text) > 0)
            {
                if (comboBox1.Text.Equals("afiliado"))
                {
                    MessageBox.Show("usuario afiliado ingreso correctamente");
                    this.Hide();
                    this.Close();
                }

                if (comboBox1.Text.Equals("profesional"))
                {
                    MessageBox.Show("usuario profesional ingreso correctamente");
                    this.Hide();
                }

                if (comboBox1.Text.Equals("administrador"))
                {
                    MessageBox.Show("usuario administrador ingreso correctamente");
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("usuario incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void IniciarSesion_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("afiliado");
            comboBox1.Items.Add("profesional");
            comboBox1.Items.Add("administrador");
        }
    }
}
