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

namespace ClinicaFrba.Cancelar_Atencion
{

    public partial class CancelarAtencion : Form
    {

        private int id=-1;
        private string turnos = "SELECT T.FECHA_TURNO AS Fecha_Turno, T.ID_TURNO, U.APELLIDO AS Apellido, U.NOMBRE AS Nombre FROM [3FG].TURNOS T, [3FG].PROFESIONALES P, [3FG].USUARIOS U, [3FG].AGENDA A WHERE T.ID_AGENDA IS NOT NULL AND (T.ID_AGENDA = A.ID_AGENDA) AND (A.ID_USUARIO = P.ID_USUARIO) AND (P.ID_USUARIO = U.ID_USUARIO) AND (T.ID_TURNO NOT IN (SELECT C.ID_TURNO FROM [3FG].CANCELACIONES C))";

        public CancelarAtencion()
        {
            InitializeComponent();
            loadTable(turnos);
        }

        private void loadTable(string query)
        {
            using (SqlConnection conexion = BDComun.obtenerConexion())
            {
                SqlCommand comando = new SqlCommand(query, conexion);
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(dataTable);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dataTable;
                dataGridView1.DataSource = bSource;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.Columns[1].Visible = false;
                conexion.Close();
            }
        }

        private string nombreBienEscrito(object nombre)
        {
            string nombreString = nombre.ToString();
            char primerLetra = nombreString[0];
            string elResto = nombreString.Remove(0, 1).ToLower();
            string todoJunto = primerLetra + elResto;
            return todoJunto;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex <= 2)
            {
                object fechaTurno = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                DateTime fecha = (DateTime)fechaTurno;
                if (fecha.Day != DateTime.Now.Day)
                {
                    object idT = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value;
                    object apellido = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value;
                    object nombre = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value;
                    this.id = Int32.Parse(idT.ToString());
                    label1.Text = "Turno a cancelar: " + fecha.ToString("MM/dd/yyyy HH:mm");
                    label2.Text = "con profesional: " + apellido + ", " + nombreBienEscrito(nombre);
                }
                else MessageBox.Show("No se puede borrar un turno en el mismo dia de la consulta", "Error", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (id != -1)
                {
                    using (SqlConnection conexion = BDComun.obtenerConexion())
                    {
                        string crearCancelacion = "INSERT INTO [3FG].CANCELACIONES(ID_TURNO,TIPO_CANCELACION,MOTIVO_CANCELACION) VALUES(@idTurno,'Usuario',@motivoCancelacion)";

                        using (SqlCommand queryCrearCancelacion = new SqlCommand(crearCancelacion))
                        {
                            queryCrearCancelacion.Connection = BDComun.obtenerConexion();
                            queryCrearCancelacion.Parameters.Add("@idTurno", SqlDbType.BigInt, 8).Value = id;
                            queryCrearCancelacion.Parameters.Add("@motivoCancelacion", SqlDbType.VarChar, 250).Value = textBox1.Text;
                            try
                            {
                                queryCrearCancelacion.ExecuteNonQuery();
                                MessageBox.Show("Cancelacion creada correctamente");
                            }
                            catch { MessageBox.Show("Error al tratar de guardar en database", "Error", MessageBoxButtons.OK); }
                        }
                    }
                }
                else MessageBox.Show("No ha seleccionado ningun turno a borrar", "Error", MessageBoxButtons.OK);
            }
            else MessageBox.Show("Debe escribir un motivo de cancelacion", "Error", MessageBoxButtons.OK);
            loadTable(turnos);
        }
    }
}
