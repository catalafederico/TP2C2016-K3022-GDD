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
    public partial class ModificarAfiliado : Form
    {
        ConsultarHistorial consultarHistorial;
        DatoAModificar unDato;    
        public ModificarAfiliado()
        {
            InitializeComponent();
            comboBox1.Items.Add("D.N.I");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonHistorial_Click(object sender, EventArgs e)
        {
            consultarHistorial = new ConsultarHistorial();
            consultarHistorial.ShowDialog();
        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            if (!validacionesAfiliado())
            {
                return;
            }

            unDato = new DatoAModificar(textBox1.Text);
            unDato.ShowDialog();
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

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
