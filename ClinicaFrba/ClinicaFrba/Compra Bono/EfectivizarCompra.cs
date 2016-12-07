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
    //EN ESTA VENTANA SE REALIZA LA COMPRA PROPIAMENTE DICHA
    //SE RECIBE COMO PARAMETRO EL ID_USUARIO CARGADO
    public partial class EfectivizarCompra : Form
    {
        private int precioUnidad;
        private int numeroUsuario;
        private int plan;
        private string apellidoYNombre;
        private string detalleDePlan;
        private string queryParaConsulta = "SELECT P.PRECIO_BONO_CONSULTA, P.DESCRIPCION_PLAN, P.ID_PLAN, A.ID_USUARIO, U.APELLIDO + ';' + U.NOMBRE 'APELLIDOYNOMBRE' FROM [3FG].AFILIADOS A JOIN [3FG].PLANES P ON (A.ID_PLAN = P.ID_PLAN) JOIN [3FG].USUARIOS U ON (U.ID_USUARIO = A.ID_USUARIO) WHERE A.ID_USUARIO = ";


        public EfectivizarCompra(int idUsuario)
        {
            //SE CARGAN LOS DATOS NECESARIOS DEL AFILIADO PARA PODER REALIZAR LA COMPRA
            InitializeComponent();
            string query = queryParaConsulta + idUsuario.ToString();
            DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
            precioUnidad = int.Parse(dt.Rows[0][0].ToString());
            plan = int.Parse(dt.Rows[0]["ID_PLAN"].ToString());
            numeroUsuario = int.Parse(dt.Rows[0]["ID_USUARIO"].ToString());
            apellidoYNombre = dt.Rows[0]["APELLIDOYNOMBRE"].ToString();
            detalleDePlan = dt.Rows[0]["DESCRIPCION_PLAN"].ToString();
            label2.Text = "Apellido y Nombre: " + apellidoYNombre;
            label3.Text = detalleDePlan;
            label4.Text = "Costo unitario de Bonos: " + precioUnidad.ToString() + " Pesos";

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
                    //SE REALIZA EL INSERT DE LA COMPRA
                    ConexionSQL unaConexion = new ConexionSQL();
                    DateTime fechaYHora = DateTime.Parse(Program.nuevaFechaSistema()); ;
                    string queryCompra = "INSERT INTO [3FG].COMPRAS (ID_USUARIO, FECHA_COMPRA, CANTIDAD_BONOS, MONTO_PAGADO) OUTPUT INSERTED.ID_COMPRA VALUES (" + numeroUsuario.ToString() + "," + "@Fecha_Compra" + "," + textBox1.Text + "," + precioTotal.ToString() + ")";
                    SqlCommand unaCompra = new SqlCommand(queryCompra);
                    //unaCompra.Connection = unaConexion.getMiConexionSQL();
                    unaCompra.Parameters.Add("@Fecha_Compra", SqlDbType.DateTime, 8).Value = fechaYHora;
                    //unaCompra.ExecuteNonQuery();
                    DataTable dt = unaConexion.cargarTabla(unaCompra);
                    int id_compra = int.Parse(dt.Rows[0][0].ToString());
                    string queryBonos = "INSERT INTO [3FG].BONOS (ID_PLAN,ID_COMPRA) VALUES ( " + plan.ToString() + "," + id_compra.ToString() + ")";
                    //SE REALIZA EL INSERT DE LOS BONOS
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
