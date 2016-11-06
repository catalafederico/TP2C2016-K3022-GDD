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
        //AQUI SE DETALLA EL CODIGO DE ESTA VENTANA. DICHA VENTANA SOLO SE UTILIZA POR EL ADMINISTRADOR PARA INGRESAR EL NUMERO DE AFILIADO (AUNQUE POR EL MOMENTO ES SOLAMENTE EL ID_USUARIO)
        //ASI QUE EN EL CASO DE QUE EL USUARIO LOGGEADO SEA AFILIADO, DIRECTAMENTE PASARA A LA VENTANA EFECTIVIZAR COMPRA
        private string queryParaConsulta = "SELECT P.PRECIO_BONO_CONSULTA, P.ID_PLAN, A.ID_USUARIO FROM [3FG].AFILIADOS A JOIN [3FG].PLANES P ON (A.ID_PLAN = P.ID_PLAN) WHERE A.ID_USUARIO = ";

        public Comprar_Bonos()
        {
            InitializeComponent();
        }

        private void Comprar_Bonos_Load(object sender, EventArgs e)
        {

        }
        //ESTE BOTON AL ACCIONARSE SE TOMA EL VALOR DEL TEXTBOX, QUE TIENE EL ID_USUARIO Y LO SE LO ENVIA A LA VENTANA EFECTIVIZAR COMPRA
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
                    /*int precio = int.Parse(dt.Rows[0][0].ToString());
                    int plan = int.Parse(dt.Rows[0]["ID_PLAN"].ToString());
                    int numeroDeUsuario = int.Parse(dt.Rows[0]["ID_USUARIO"].ToString());*/
                    EfectivizarCompra compra = new EfectivizarCompra(int.Parse(textBox1.Text));
                    this.Hide();
                    compra.Closed += (s, args) => this.Close();
                    compra.Show();
                }
            }
        }
    }
}
