using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClinicaFrba.Compra_Bono
{
    public partial class EfectivizarCompra : Form
    {
        private int precioUnidad;
        private int numeroUsuario;
        private int plan;


        public EfectivizarCompra(int precio, int idPlan, int idUsuario)
        {
            InitializeComponent();
            precioUnidad = precio;
            numeroUsuario = idUsuario;
            plan = idPlan;
        }

        private void EfectivizarCompra_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Debe ingresar una cantidad", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int i;
                int cantidad = int.Parse(textBox1.Text);
                int precioTotal = precioUnidad * (int.Parse(textBox1.Text));
                DialogResult dialogResult = MessageBox.Show("El monto a pagar es de " + precioTotal.ToString() + " , desea realizar la operación?", this.Text, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string queryBonos = "INSERT INTO [3FG].BONOS (ID_PLAN,ID_USUARIO) VALUES ( " + plan.ToString() + "," + numeroUsuario.ToString() + ")";
                    DateTime fechaYHora = DateTime.Now;
                    string queryCompra = "INSERT INTO [3FG].COMPRAS (ID_USUARIO, FECHA_COMPRA, CANTIDAD_BONOS, MONTO_PAGADO) VALUES (" + numeroUsuario.ToString() + "," + "@Fecha_Compra" + "," + textBox1.Text + "," + precioTotal.ToString() + ")";
                    SqlCommand unaCompra = new SqlCommand(queryCompra);
                    unaCompra.Connection = BDComun.obtenerConexion();
                    unaCompra.Parameters.Add("@Fecha_Compra", SqlDbType.DateTime, 8).Value = fechaYHora;
                    unaCompra.ExecuteNonQuery();
                    ConexionSQL unaConexion = new ConexionSQL();
                    //unaConexion.ejecutarComandoSQL(queryCompra);
                    for (i = 0; i < cantidad; i++)
                    {
                        unaConexion.ejecutarComandoSQL(queryBonos);
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }
        }
    }
}
