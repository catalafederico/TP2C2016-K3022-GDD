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
        private int id_bono = -1;
        private int turno;
        private string queryDeBonos = "select B.ID_BONO 'NUMERO DE BONO',B.ID_COMPRA,B.ID_PLAN, C.FECHA_COMPRA, P.DESCRIPCION_PLAN from [3FG].BONOS B JOIN [3FG].COMPRAS C ON (B.ID_COMPRA = C.ID_COMPRA) JOIN [3FG].PLANES P ON (P.ID_PLAN = B.ID_PLAN) JOIN [3FG].AFILIADOS A ON (A.ID_USUARIO = C.ID_USUARIO) WHERE A.RAIZ_AFILIADO = @NumeroAfiliado AND NUMERO_CONSULTA IS NULL AND B.ID_PLAN IN (SELECT A1.ID_PLAN FROM [3FG].AFILIADOS A1 WHERE A1.ID_USUARIO = @Id_Usuario)";
        
        //Se cargan los bonos que puede usar el afiliado seleccionado, que son los que adquirio él o algun miembro de su familia, siemprey cuando, sea con el mismo plan que posee.
        public ElegirAfiliado(int id_Turno, int id_afiliado, int raizAfiliado, string apellido, string nombre)
        {
            turno = id_Turno;
            InitializeComponent();
            SqlCommand comandoBonos = new SqlCommand(queryDeBonos);
            comandoBonos.Parameters.Add("@NumeroAfiliado", SqlDbType.Int).Value = raizAfiliado;
            comandoBonos.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = id_afiliado;
            ConexionSQL.loadDataGridConSqlCommand(comandoBonos, dataGridView2);
            label1.Text = "Afiliado seleccionado: " + apellido.ToString() + ", " + nombre.ToString();
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Visible = false;
        }



        private void ElegirAfiliado_Load(object sender, EventArgs e)
        {

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
                object fecha = dataGridView2.Rows[e.RowIndex].Cells[3].Value;
                object descripcion = dataGridView2.Rows[e.RowIndex].Cells[4].Value;

                // Setteo las variables
                id_bono = int.Parse(bono.ToString());

                // Modifico la ventana para reflejar la eleccion del usuario
                label2.Text = "Bono Seleccionado: " + bono.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Si elegi un bono
            if (id_bono != (-1))
            {
                ConexionSQL unaConexion = new ConexionSQL();
                DateTime actual = DateTime.Now;
                DateTime fechaYHora = DateTime.Parse(Program.nuevaFechaSistema());
                TimeSpan ts = new TimeSpan(actual.Hour,actual.Minute,actual.Second);
                fechaYHora = fechaYHora.Date + ts;
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
                eh.ShowDialog();
            }
            else MessageBox.Show("No ha seleccionado un Bono", "Error", MessageBoxButtons.OK);
        }


    }
}
