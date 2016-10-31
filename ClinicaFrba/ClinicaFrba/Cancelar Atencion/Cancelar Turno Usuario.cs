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

    public partial class CancelarAtencionUsuario : Form
    {

        private int idTurno=-1;
        private int idUsuario;
        private string turnos = "SELECT T.ID_TURNO, T.FECHA_TURNO AS 'Fecha Turno' FROM [3FG].TURNOS T, [3FG].AFILIADOS A, [3FG].USUARIOS U WHERE T.ID_AGENDA IS NOT NULL AND (T.ID_AFILIADO = A.ID_USUARIO) AND (A.ID_USUARIO = U.ID_USUARIO) AND (T.ID_TURNO NOT IN (SELECT C.ID_TURNO FROM [3FG].CANCELACIONES C))";
        private string profesionales = "SELECT T.ID_TURNO AS ID, U.NOMBRE AS Nombre, U.APELLIDO AS Apellido FROM [3FG].TURNOS T, [3FG].AGENDA A, [3FG].USUARIOS U, [3FG].PROFESIONALES P WHERE T.ID_AGENDA IS NOT NULL AND T.ID_AGENDA = A.ID_AGENDA AND A.ID_USUARIO = P.ID_USUARIO AND P.ID_USUARIO = U.ID_USUARIO AND (T.ID_TURNO NOT IN (SELECT C.ID_TURNO FROM [3FG].CANCELACIONES C))";

        public CancelarAtencionUsuario(int idU)
        {
            this.idUsuario = idU;
            InitializeComponent();
            loadTable(turnos, dataGridView1);
            loadTable(profesionales, dataGridView2);
        }

        private void loadTable(string query, DataGridView dgv)
        {
            using (SqlConnection conexion = BDComun.obtenerConexion())
            {
                query += " AND T.ID_AFILIADO LIKE " + idUsuario;
                SqlCommand comando = new SqlCommand(query, conexion);
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(dataTable);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dataTable;
                dgv.DataSource = bSource;
                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgv.Columns[0].Visible = false;
                foreach (DataGridViewColumn column in dgv.Columns){ column.SortMode = DataGridViewColumnSortMode.NotSortable; }
                conexion.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex <= 2)
            {
                object fechaTurno = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                DateTime fecha = (DateTime)fechaTurno;
                if (fecha.Day != DateTime.Now.Day)
                {
                    object idT = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value;
                    this.idTurno = Int32.Parse(idT.ToString());
                    label1.Text = "Turno a cancelar: " + fecha.ToString("MM/dd/yyyy HH:mm");
                }
                else MessageBox.Show("No se puede borrar un turno en el mismo dia de la consulta", "Error", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (idTurno != -1)
                {
                    using (SqlConnection conexion = BDComun.obtenerConexion())
                    {
                        string crearCancelacion = "INSERT INTO [3FG].CANCELACIONES(ID_TURNO,TIPO_CANCELACION,MOTIVO_CANCELACION) VALUES(@idTurno,'Usuario',@motivoCancelacion)";

                        using (SqlCommand queryCrearCancelacion = new SqlCommand(crearCancelacion))
                        {
                            queryCrearCancelacion.Connection = BDComun.obtenerConexion();
                            queryCrearCancelacion.Parameters.Add("@idTurno", SqlDbType.BigInt, 8).Value = idTurno;
                            queryCrearCancelacion.Parameters.Add("@motivoCancelacion", SqlDbType.VarChar, 250).Value = textBox1.Text;
                            try
                            {
                                queryCrearCancelacion.ExecuteNonQuery();
                                MessageBox.Show("Cancelacion creada correctamente");
                                idTurno = -1;
                                loadTable(turnos, dataGridView1);
                                loadTable(profesionales, dataGridView2);
                                label1.Text = "Turno a cancelar: ";
                            }
                            catch { MessageBox.Show("Error al tratar de guardar en database", "Error", MessageBoxButtons.OK); }
                        }
                    }
                }
                else MessageBox.Show("No ha seleccionado ningun turno a borrar", "Error", MessageBoxButtons.OK);
            }
            else MessageBox.Show("Debe escribir un motivo de cancelacion", "Error", MessageBoxButtons.OK);

        }
    }
}
