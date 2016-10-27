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
    public partial class EliminarAfiliado : Form
    {
        public EliminarAfiliado()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!validacionesAfiliado())
            {
                return;
            }


            if ((MessageBox.Show("¿Realmente desea modificar al Afiliado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {

                string comando = "update [3FG].USUARIOS set HABILITADO =0 where NUMERO_DOCUMENTO='" + Int64.Parse(textBox1.Text) + "'";
                (new ConexionSQL()).ejecutarComandoSQL(comando);
                MessageBox.Show("Afiliado Eliminado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);

                this.Close();
            }
            else
            {
                this.Close();
            }

        }
        private bool validacionesAfiliado()
        {

            /* SELECT COUNT(*) FROM [3FG].USUARIOS u join [3FG].AFILIADOS a on(u.ID_USUARIO=a.ID_USUARIO) WHERE u.TIPO_DE_DOCUMENTO = 'D.N.I' AND u.NUMERO_DOCUMENTO = '52655802';*/
            string query2 = "SELECT COUNT(*) FROM [3FG].USUARIOS u join [3FG].AFILIADOS a on(u.ID_USUARIO=a.ID_USUARIO) WHERE u.TIPO_DE_DOCUMENTO ='" + comboBox1.Text + "'  AND NUMERO_DOCUMENTO = '" + textBox1.Text + "'";
            DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
            string cantidad = dt2.Rows[0][0].ToString();
            if (cantidad == "0")
            {
                MessageBox.Show("Documento Invalido,No Se Encuantra Registrado En El Sistema", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
