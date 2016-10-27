using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.ABM_Afiliado
{
    public partial class CrearUsuario : Form
    {
        ABM_Afiliado.CrearAfiliado unAfiliado;
        int raiz;
        int numero;
        int flag12;
        int flag13;
        int aleator;
        List<Int64> raizUsuario;
        public CrearUsuario(int alearorio, int raiz, int flag1, int flag2, int aleatorio, List<Int64> raizABMAfiliado)
        {
            InitializeComponent();
           this.raiz = alearorio;
            this.numero = raiz;
            this.flag12 = flag1;
            this.flag13 = flag2;
            this.aleator = aleatorio;
            raizUsuario = raizABMAfiliado;

        }

        private void button_siguiente_Click(object sender, EventArgs e)
        {

            if (!validaciones())
            {
                return;
            }

            Random r = new Random();

            unAfiliado = new CrearAfiliado(textBox1.Text, textBox2.Text, r, this.flag12, this.numero, this.flag13, this.aleator, raizUsuario);
            unAfiliado.ShowDialog();
            this.Hide();
           
        }

        private bool validaciones()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Las contraseñas ingresadas son distintas", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string comando = "select * from [3FG].USUARIOS where USUARIO_NOMBRE = '" + textBox1.Text + "'";
            DataTable dt = (new ConexionSQL()).cargarTablaSQL(comando);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0][5].ToString() == "0")
                {
                    MessageBox.Show("Existe un usuario deshabilitado con ese nombre", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya existe", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
    }
}
