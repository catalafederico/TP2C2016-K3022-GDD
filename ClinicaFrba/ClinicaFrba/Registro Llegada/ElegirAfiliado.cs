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

namespace ClinicaFrba.Registro_Llegada
{
    public partial class ElegirAfiliado : Form
    {
        private int id_bono;
        private int turno;
        private int numeroAfiliado;
        private string queryDeAfiliados = "select U.APELLIDO, U.NOMBRE, A1.RAIZ_AFILIADO,A1.NUMERO_FAMILIA from [3FG].AFILIADOS A1 join [3FG].USUARIOS U on (A1.ID_USUARIO = U.ID_USUARIO) where RAIZ_AFILIADO = (select A.RAIZ_AFILIADO from [3FG].AFILIADOS A join [3FG].TURNOS T on (A.ID_USUARIO = T.ID_AFILIADO) where T.ID_TURNO = @Turno)";
        private string queryDeBonos = "select B.ID_BONO,B.ID_COMPRA,B.ID_PLAN from [3FG].BONOS B JOIN [3FG].COMPRAS C ON (B.ID_COMPRA = C.ID_COMPRA) JOIN [3FG].AFILIADOS A ON (A.ID_USUARIO = C.ID_USUARIO) WHERE A.RAIZ_AFILIADO = @NumeroAfiliado AND NUMERO_CONSULTA IS NULL";
        
        //Se cargan los afiliados que pueden hacer uso de dicho turno
        public ElegirAfiliado(int id_Turno)
        {
            turno = id_Turno;
            InitializeComponent();
            SqlCommand comandoAfiliados = new SqlCommand(queryDeAfiliados);
            comandoAfiliados.Parameters.Add("@Turno", SqlDbType.Int).Value = id_Turno;
            ConexionSQL.loadDataGridConSqlCommand(comandoAfiliados, dataGridView1);
        }



        private void ElegirAfiliado_Load(object sender, EventArgs e)
        {

        }

        //Se selecciona el afiliado que hara uso del turno
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Tomo toda la informacion de la fila que fue clickeada
                object apellido = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                object nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                object raiz = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                object nroFamilia = dataGridView1.Rows[e.RowIndex].Cells[3].Value;

                // Setteo las variables
                numeroAfiliado = int.Parse(raiz.ToString());
                string apellidoAfi = apellido.ToString();
                string nombreAfi = nombre.ToString();

                // Modifico la ventana para reflejar la eleccion del usuario
                label1.Text = "Afiliado Seleccionado: " + apellidoAfi + "; "+ nombreAfi;
            }
        }


        //Se buscan los bonos que tiene disponible dicho usuario para poder realizar la consulta (Con esto incluimos los bonos comprados por miembros de la misma familia)
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand comandoBonos = new SqlCommand(queryDeBonos);
            comandoBonos.Parameters.Add("@NumeroAfiliado", SqlDbType.Int).Value = numeroAfiliado;
            ConexionSQL.loadDataGridConSqlCommand(comandoBonos, dataGridView2);
        }


        //Se selecciona un bono
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Tomo toda la informacion de la fila que fue clickeada
                object bono = dataGridView2.Rows[e.RowIndex].Cells[0].Value;
                object compra = dataGridView2.Rows[e.RowIndex].Cells[1].Value;
                object plan = dataGridView2.Rows[e.RowIndex].Cells[2].Value;

                // Setteo las variables
                id_bono = int.Parse(bono.ToString());

                // Modifico la ventana para reflejar la eleccion del usuario
                label2.Text = "Bono Seleccionado: " + bono.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Si elegi un bono
            if (id_bono != null)
            {
                ConexionSQL unaConexion = new ConexionSQL();
                DateTime fechaYHora = DateTime.Now;
                string insertDeRecepcion = "INSERT INTO [3FG].RECEPCIONES (ID_TURNO, ID_BONO,FECHA_RECEPCIONES) VALUES (@Turno,@Bono,@Fecha)";
                SqlCommand unaLlegada = new SqlCommand(insertDeRecepcion);
                unaLlegada.Parameters.Add("@Turno", SqlDbType.Int).Value = turno;
                unaLlegada.Parameters.Add("@Bono", SqlDbType.Int).Value = id_bono;
                unaLlegada.Parameters.Add("@Fecha", SqlDbType.DateTime, 8).Value = fechaYHora;
                unaConexion.ejecutarComando(unaLlegada);
                MessageBox.Show("Se ha registrado la llegada a la consulta", "Operacion Exitosa", MessageBoxButtons.OK);
                // Creo una nueva ventana para elegir horario con los datos que necesita
                
                RegistrarLlegada eh = new RegistrarLlegada();

                // Escondo esta venta
                this.Hide();

                // La cierro y abro una nueva ventana de RegistroLlegada
                eh.Closed += (s, args) => this.Close();
                eh.Show();
            }
            else MessageBox.Show("No ha seleccionado un Turno", "Error", MessageBoxButtons.OK);
        }


    }
}
