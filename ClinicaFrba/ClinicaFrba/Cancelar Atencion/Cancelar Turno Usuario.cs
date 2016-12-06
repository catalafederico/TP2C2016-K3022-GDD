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
        // Setteo el id del turno en -1 para saber si el usuario eligio o no un turno
        private int idTurno=-1;
        private int idUsuario;

        // Setteo la fecha actual del sistema
        private static DateTime fechaActual = DateTime.Parse(Program.nuevaFechaSistema());

        // Query que devuelve los turnos que no han sido cancelados, no tienen recepcion todavia y son posteriores a la fecha actual del sistema
        private string turnos = @"SELECT T.ID_TURNO, T.FECHA_TURNO AS 'Fecha Turno'
                                  FROM [3FG].TURNOS T, [3FG].AFILIADOS A, [3FG].USUARIOS U
                                  WHERE T.ID_AGENDA IS NOT NULL
                                  AND (T.ID_AFILIADO = A.ID_USUARIO)
                                  AND (A.ID_USUARIO = U.ID_USUARIO)
                                  AND (T.ID_TURNO NOT IN (SELECT C.ID_TURNO FROM [3FG].CANCELACIONES C))
                                  AND (T.ID_TURNO NOT IN (SELECT R.ID_TURNO FROM [3FG].RECEPCIONES R))
                                  AND T.FECHA_TURNO > '" + fechaActual.ToString("yyyy-dd-MM HH:mm:ss") + "'";
        


        // Funcion de inicializacion
        public CancelarAtencionUsuario(int idU)
        {
            // Setteo el id del usuario
            this.idUsuario = idU;
            InitializeComponent();

            // Le agrego a la query que solo busque los turnos de este usuario
            turnos += "AND A.ID_USUARIO = " + idUsuario.ToString();

            // Cargo el DataGrid
            ConexionSQL.loadDataGrid(turnos, dataGridView1);

            // Escondo la columna que tiene el ID del turno
            dataGridView1.Columns[0].Visible = false;
        }




        // Funcion de eleccion por click en fila del DataGrid
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex < 2)
            {
                // Tomo la fecha de la fila clickeada
                object fechaTurno = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                DateTime fecha = (DateTime)fechaTurno;

                // Si la fecha del turno no es la actual de la app
                if (fecha.Day != DateTime.Parse(Program.nuevaFechaSistema()).Day)
                {
                    // Setteo el id del turno elegido
                    object idT = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value;
                    this.idTurno = Int32.Parse(idT.ToString());

                    // Modifico la ventana para reflejar la eleccion del usuario
                    label1.Text = "Turno a cancelar: " + fecha.ToString("MM/dd/yyyy HH:mm");
                }
                // Las cancelaciones por usuario deben hacerse al menos un dia antes, si no es asi no se pueden cancelar
                else MessageBox.Show("No se puede borrar un turno en el mismo dia de la consulta", "Error", MessageBoxButtons.OK);
            }
        }



        // Funcion de creacion de la cancelacion
        private void button1_Click(object sender, EventArgs e)
        {
            // Me fijo que haya escrito un motivo
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Me fijo que haya elegido un turno
                if (idTurno != -1)
                {
                    // Abro la conexion
                    using (SqlConnection conexion = new ConexionSQL().conectar())
                    {
                        // Creo la query
                        string crearCancelacion = "INSERT INTO [3FG].CANCELACIONES(ID_TURNO,TIPO_CANCELACION,MOTIVO_CANCELACION) VALUES(@idTurno,'Usuario',@motivoCancelacion)";

                        // Lleno los campos de manera dinamica para evitar SQL Injection
                        using (SqlCommand queryCrearCancelacion = new SqlCommand(crearCancelacion))
                        {
                            queryCrearCancelacion.Connection = new ConexionSQL().conectar();
                            queryCrearCancelacion.Parameters.Add("@idTurno", SqlDbType.BigInt, 8).Value = idTurno;
                            queryCrearCancelacion.Parameters.Add("@motivoCancelacion", SqlDbType.VarChar, 250).Value = textBox1.Text;

                            // Trato de crear la cancelacion
                            try
                            {
                                queryCrearCancelacion.ExecuteNonQuery();
                                MessageBox.Show("Cancelacion creada correctamente");
                                idTurno = -1;
                                ConexionSQL.loadDataGrid(turnos, dataGridView1);
                                label1.Text = "Turno a cancelar: ";
                            }
                            // Por si falla
                            catch { MessageBox.Show("Error al tratar de guardar en database", "Error", MessageBoxButtons.OK); }
                        }
                    }
                }
                else MessageBox.Show("No ha seleccionado ningun turno a borrar", "Error", MessageBoxButtons.OK);
            }
            else MessageBox.Show("Debe escribir un motivo de cancelacion", "Error", MessageBoxButtons.OK);
        }



        // Funcion para salir
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }


}
