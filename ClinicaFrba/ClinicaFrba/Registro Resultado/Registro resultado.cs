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

namespace ClinicaFrba.Registro_Resultado
{
    public partial class RegistroResultado : Form
    {
        private DateTime fechaAtencion;
        private int idProfesional;
        private int idRecepcion;
        // Setteo todo el DataGrid con los turnos que tengan una recepcion confirmada y no un resultado
        private static string queryDeAtenciones = @"SELECT R.ID_RECEPCION, T.ID_TURNO, T.FECHA_TURNO AS 'Fecha Turno' 
                                                    FROM [3FG].TURNOS T, [3FG].RECEPCIONES R, [3FG].AGENDA A, [3FG].USUARIOS U, [3FG].PROFESIONALES P
                                                    WHERE R.ID_TURNO = T.ID_TURNO
                                                    AND T.ID_AGENDA = A.ID_AGENDA
                                                    AND A.ID_USUARIO = P.ID_USUARIO
                                                    AND P.ID_USUARIO = U.ID_USUARIO
                                                    AND (R.ID_RECEPCION NOT IN (SELECT AM.ID_RECEPCION FROM [3FG].ATENCIONES_MEDICAS AM))";



        // Funcion de inicializacion
        public RegistroResultado(int idP)
        {
            // Setteo el id del profesional
            this.idProfesional = idP;

            // Le agrego a la query que los turnos sean del profesional
            queryDeAtenciones += " AND A.ID_USUARIO = " + this.idProfesional + " ORDER BY T.FECHA_TURNO";
            InitializeComponent();

            // Cargo el Datagrid
            ConexionSQL.loadDataGrid(queryDeAtenciones, dataGridView1);

            // Escondo las columnas de id, las necesito pero no quiero que el usuario las vea
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }




        // Funcion de eleccion de turno por click
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Me fijo que el usuario no clickee el nombre de una columna
            if(e.RowIndex >= 0)
            {
                // Tomo los valores que voy a usar de cada columna
                object fechaElegida = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                object recepcion = dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                if (existeAfiliado((DateTime)fechaElegida))
                {
                    // Los uso para settear las variables que necesito
                    this.fechaAtencion = (DateTime)fechaElegida;
                    this.idRecepcion = int.Parse(recepcion.ToString());

                    // Muestro en la ventana la eleccion del usuario
                    label3.Text = "Fecha atencion: " + fechaAtencion.ToString("dd/MM/yyyy");
                    label4.Text = "Hora atencion: " + fechaAtencion.ToString("hh:mm");
                    mostrarAfiliado();
                }
                else MessageBox.Show("No se encontro ningun usuario para el turno elegido. Comuniquese con soporte para más información", "Error", MessageBoxButtons.OK);
            }
        }

        private bool existeAfiliado(DateTime fechaElegida)
        {
            SqlCommand numeroUsuario = new SqlCommand("SELECT COUNT(*) FROM [3FG].TURNOS T, [3FG].AFILIADOS A, [3FG].USUARIOS U WHERE T.ID_AFILIADO = A.ID_USUARIO AND A.ID_USUARIO = U.ID_USUARIO AND T.FECHA_TURNO = '" + fechaElegida.ToString("dd/MM/yyyy hh:mm:ss.fff") + "'", new ConexionSQL().conectar());
            int deberiaSerMayorACero = (int) numeroUsuario.ExecuteScalar();
            return (deberiaSerMayorACero > 0);
        }

        // Funcion que agrega el nombre del paciente del turno elegido a la ventana
        private void mostrarAfiliado()
        {
            string queryObtencion = "SELECT top 1 U.NOMBRE, U.APELLIDO FROM [3FG].TURNOS T, [3FG].AFILIADOS A, [3FG].USUARIOS U WHERE T.ID_AFILIADO = A.ID_USUARIO AND A.ID_USUARIO = U.ID_USUARIO AND T.FECHA_TURNO = '" + this.fechaAtencion.ToString("dd/MM/yyyy hh:mm:ss.fff") + "'";
                Dictionary<string, string> diccionario = new Dictionary<string, string>();
                using (SqlConnection conexion = new ConexionSQL().conectar())
                {
                    using (SqlCommand command = new SqlCommand(queryObtencion, conexion))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                diccionario.Add(reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                }
                label2.Text = "Afiliado: " + nombreBienEscrito(diccionario.FirstOrDefault().Key) + " " + diccionario.FirstOrDefault().Value;
            
        }


        // Funcion para propositos esteticos nomas
        private string nombreBienEscrito(object nombre)
        {
            string nombreString = nombre.ToString();
            char primerLetra = nombreString[0];
            string elResto = nombreString.Remove(0, 1).ToLower();
            string todoJunto = primerLetra + elResto;
            return todoJunto;
        }


        // Funcion de confirmacion de turno
        private void button1_Click(object sender, EventArgs e)
        {
            // Se fija que el profesional de consentimiento de que el horario y fecha son los correctos
            if (checkBox1.Checked)
            {
                IngresarSintomasYDiagnostico eh = new IngresarSintomasYDiagnostico(idRecepcion);
                this.Hide();
                eh.Closed += (s, args) => this.Close();
                eh.ShowDialog();
            }
            else MessageBox.Show("No puede ingresar el resultado de la consulta sin confirmar la fecha y hora del turno elegido", "Error", MessageBoxButtons.OK);
        }

        

    }
}
