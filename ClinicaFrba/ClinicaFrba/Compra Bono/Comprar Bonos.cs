using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Compra_Bono
{
    public partial class Comprar_Bonos : Form
    {
        private string queryParaConsulta = "SELECT P.PRECIO_BONO_CONSULTA, P.ID_PLAN, A.ID_USUARIO FROM [3FG].AFILIADOS A JOIN [3FG].PLANES P ON (A.ID_PLAN = P.ID_PLAN) WHERE A.ID_USUARIO = ";

        public Comprar_Bonos()
        {
            InitializeComponent();
        }

        private void Comprar_Bonos_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = queryParaConsulta + textBox1.Text;

            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Debe ingresar un numero de afiliado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("El numero de afiliado ingresado es incorrecto", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int precio = int.Parse(dt.Rows[0][0].ToString());
                    int plan = int.Parse(dt.Rows[0]["ID_PLAN"].ToString());
                    int numeroDeUsuario = int.Parse(dt.Rows[0]["ID_USUARIO"].ToString());
                    EfectivizarCompra compra = new EfectivizarCompra(precio, plan, numeroDeUsuario);
                    this.Hide();
                    compra.Closed += (s, args) => this.Close();
                    compra.Show();
                }
            }
        }
    }
}
