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
    public partial class CancelarAtencionProfesional : Form
    {
     
        // Setteo las fechas temprana y tardia con una fecha imposible (MonthCalendar no puede marcar fechas tan pasadas) para poder fijarme si el usuario eligio fecha o no
        private DateTime fechaTemprana = new DateTime(1000, 1, 01);
        private DateTime fechaTardia = new DateTime(1000, 1, 01);

        /* Esta query la uso como base para llenar el DataGridView de turnos. Linkea Turnos
          Agenda, Profesionales y Usuarios y me devuelve todos los turnos que tienen una agenda
          linkeada a un profesional y que no hayan sido ya cancelados*/
        private string turnos = "SELECT T.ID_TURNO, T.FECHA_TURNO AS 'Turnos a borrar' FROM [3FG].TURNOS T, [3FG].PROFESIONALES P, [3FG].USUARIOS U, [3FG].AGENDA A WHERE T.ID_AGENDA IS NOT NULL AND (T.ID_AGENDA = A.ID_AGENDA) AND (A.ID_USUARIO = U.ID_USUARIO) AND (U.ID_USUARIO = P.ID_USUARIO) AND (T.ID_TURNO NOT IN (SELECT C.ID_TURNO FROM [3FG].CANCELACIONES C))";

        public CancelarAtencionProfesional(int idU)
        {
            InitializeComponent();
            // Cambio la query original para agregarle que solo me traiga los turnos del profesional indicado
            this.turnos += " AND (P.ID_USUARIO = " + idU + ") ";
        }

        //Funcion generica de llenado de DataGridView
        private void loadTable(string query)
        {
            using (SqlConnection conexion = BDComun.obtenerConexion())
            {
                //Aca recibe la query como dato y la usa para llenar el DataGridView
                SqlCommand comando = new SqlCommand(query, conexion);
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(dataTable);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dataTable;
                dataGridView1.DataSource = bSource;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                /* Aca esconde la columna del ID de cada turno.
                 * No quiero que el usuario la vea pero quiero los guardar ID en algun lugar
                 * facil de encontrar y linkeadas al resto de los datos del turno */
                dataGridView1.Columns[0].Visible = false;
                conexion.Close();
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Setteo las fechas temprana y tardia con los valores que fueron marcados en el MonthCalendar
            this.fechaTemprana = monthCalendar1.SelectionRange.Start;
            this.fechaTardia = monthCalendar1.SelectionRange.End;

            // Updateo los labels para dejarle saber al usuario que intervalo que intervalo selecciono
            label2.Text = "Desde: " + fechaTemprana.Date.ToShortDateString() + "  hasta: " + fechaTardia.Date.ToShortDateString();
            label4.Text = "Fecha elegida: " + fechaTemprana.Date.ToShortDateString();
        }

        // Validaciones para el llenado de tablas
        private void cargaDeTablaYFechas()
        {
            // Me fijo si el usuario marco un rango de fecha
            if (fechaTemprana.Year != 1000 || fechaTardia.Year != 1000)
                    {
                        // Me fijo que no haya marcado ambas opciones de cancelacion
                        if (checkBox1.Checked && checkBox2.Checked)
                        {
                            MessageBox.Show("Solo puede seleccionar un metodo de cancelacion", "Error", MessageBoxButtons.OK);
                        }

                        // Me fijo que haya marcado alguna opcion de cancelacion
                        if (!checkBox1.Checked && !checkBox2.Checked)
                        {
                            MessageBox.Show("Debe seleccionar un metodo de cancelacion", "Error", MessageBoxButtons.OK);
                        }

                        // Si marco la opcion de cancelacion por rango de fechas...
                        if (checkBox1.Checked && !checkBox2.Checked)
                        {
                            // Cargo la DataGridView con la query "turnos" y agregandole como condicion
                            // que los turnos obtenidos se encuentren entre las primera y ultima fecha
                            loadTable(turnos + "AND CONVERT(DATE,T.FECHA_TURNO) BETWEEN CONVERT(DATE,'" + this.fechaTemprana.Date.ToString("yyyy-MM-dd") + "') AND CONVERT(DATE,'" + this.fechaTardia.Date.ToString("yyyy-MM-dd") + "')");
                        }
                        // Si marco la opcion de cancelacion por una sola fecha...
                        if (checkBox2.Checked && !checkBox1.Checked)
                        {
                            // Cargo la DataGridView con la query "turnos" y agregandole como condicion
                            // que los turnos obtenidos se encuentren en la fecha marcada. En este caso,
                            // fechaTemprana y fechaTardia son iguales asi que es indistinto cual uso
                            loadTable(turnos + "AND CONVERT(DATE,T.FECHA_TURNO) = CONVERT(DATE,'" + this.fechaTemprana.Date.ToString("yyyy-MM-dd") + "')");
                        }                    
                    }
            else MessageBox.Show("Seleccione una fecha o rango de fechas", "Error", MessageBoxButtons.OK);
        }

        //wow such (void) long function
        private void button1_Click_1(object sender, EventArgs e)
        {
            cargaDeTablaYFechas();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            int contador = 0;
            // Me fijo que haya escrito un motivo de cancelacion
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Y para cada turno en el DataGrid...
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Lo cancelo
                    cancelar(int.Parse(dataGridView1.Rows[row.Index].Cells[0].Value.ToString()));
                    /* Esta linea de aqui arriba es un poquito rara asi que explico lo que hace:
                     * 
                     *   "dataGridView1.Rows[row.Index]" me da la fila actual.
                     *   ".Cells[0].Value" me devuelve el contenido de la primera columna en esa fila,
                     *      si hacemos memoria la primera columna tiene los ID de cada turno
                     *   
                     * Esta parte es la mas rara pero todo lo que explique arriba devuelve un objeto tipo object
                     * lo cual no es muy bueno. Quiero pasarlo a int pero no hay funcion de pasaje directo de
                     * object a int asi que primero lo paso a string y despues a int. Es un quilombo, lo se, pero
                     * me parecio mejor tenerlo en int a precio de hacer esta linea un poco mas fea */

                    // Este contadorsito me dice cuantas cancelaciones hice
                    contador++;
                }
                if (contador > 0)
                {
                    // Si todo salio bien llegamos aqui
                    MessageBox.Show("Cancelaciones creadas correctamente, " + contador.ToString() + " turnos cancelados");
                    // Vuelvo a cargar la tabla para mostrar que todos los turnos fueron cancelados
                    cargaDeTablaYFechas();
                }
                // Me fijo que el usuario agarro fechas con turnos
                else MessageBox.Show("No hay turnos guardados en las fechas marcadas", "Error", MessageBoxButtons.OK);                
            }
            else MessageBox.Show("Debe escribir un motivo de cancelacion", "Error", MessageBoxButtons.OK);
        }

        // Funcion de cancelacion de turnos
        private void cancelar(int idTurno)
        {
            using (SqlConnection conexion = BDComun.obtenerConexion())
            {
                // La creo asi para cuidarme de SQL Injection
                string crearCancelacion = "INSERT INTO [3FG].CANCELACIONES(ID_TURNO,TIPO_CANCELACION,MOTIVO_CANCELACION) VALUES(@idTurno,'Profesional',@motivoCancelacion)";
                using (SqlCommand queryCrearCancelacion = new SqlCommand(crearCancelacion))
                {
                    queryCrearCancelacion.Connection = conexion;

                    // Le digo con que valor reemplazar a cada variable
                    queryCrearCancelacion.Parameters.Add("@idTurno", SqlDbType.BigInt, 8).Value = idTurno;
                    queryCrearCancelacion.Parameters.Add("@motivoCancelacion", SqlDbType.VarChar, 250).Value = textBox1.Text;

                    // Cruzo los dedos y espero que pueda ejecutarla unas 700 veces :^)
                    try { queryCrearCancelacion.ExecuteNonQuery(); }
                    catch { MessageBox.Show("Error al tratar de guardar en database", "Error", MessageBoxButtons.OK); }
                }
            }
        }

        
    }
}
