using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

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
            if (Usuario.loginUsuario(txtUsuario.Text, getSha256(txtContraseña.Text)) > 0)
            {
                MessageBox.Show("Se ha logueado correctamente.");

                Elegir_Rol.EleccionRol rol = new Elegir_Rol.EleccionRol(txtUsuario.Text);
                
                rol.ShowDialog();


            }
            else
            {   
            
                MessageBox.Show("Usuario y/o contraseña invalidos.");
                /*MessageBox.Show("usuario incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);*/

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
            /*comboBox1.Items.Add("afiliado");
            comboBox1.Items.Add("profesional");
            comboBox1.Items.Add("administrador");*/
        
         }

        public String getSha256(String input)
        {
            SHA256Managed encriptador = new SHA256Managed();
            byte[] inputEnBytes = Encoding.UTF8.GetBytes(input);
            byte[] inputHashBytes = encriptador.ComputeHash(inputEnBytes);
            return BitConverter.ToString(inputHashBytes).Replace("-", String.Empty).ToLower();
        }

    }
}
