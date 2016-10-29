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

namespace ClinicaFrba.Pedir_Turno
{
    public partial class Elegir_Horario : Form
    {
        private ABMTurnos t;
        private DateTime fechaTemprana;
        private DateTime fechaTardia;
        private DateTime fechaElegida;
        private int idAfiliado;

        public Elegir_Horario(ABMTurnos turnos, string doctorElegido, int id)
        {
            InitializeComponent();
            this.idAfiliado = id;
            this.t = turnos;
            this.Text = "Elegir turno para Dr." + doctorElegido.ToString();
            this.fechaTemprana = DateTime.Now;
            SqlCommand dateLate = new SqlCommand("SELECT P.FIN_DISPONIBILIDAD FROM [3FG].PROFESIONALES P WHERE P.ID_USUARIO LIKE '" + id + "'", BDComun.obtenerConexion());
            this.fechaTardia = (DateTime) dateLate.ExecuteScalar();
            label3.Text = "Primer turno disponible: " + fechaTemprana.ToString("MM/dd/yyyy HH:mm:ss");
            label4.Text = "Ultimo turno disponible: " + fechaTardia.ToString("MM/dd/yyyy HH:mm:ss");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < this.fechaTemprana || dateTimePicker1.Value > this.fechaTardia)
            {
                MessageBox.Show("La fecha seleccionada no se encuentra dentro de las disponibles","Error",MessageBoxButtons.OK);
            }


            else
            {
                this.fechaElegida = dateTimePicker1.Value;
                label2.Text = "Horario elegido: " + this.fechaElegida;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = BDComun.obtenerConexion())
            {
                string crearTurno = "INSERT INTO [3FG].TURNOS(ID_AFILIADO,FECHA_TURNO) VALUES(@idAfiliado,@fechaTurno)";

                using (SqlCommand queryCrearTurno = new SqlCommand(crearTurno))
                {
                    queryCrearTurno.Connection = BDComun.obtenerConexion();
                    //Esta mal le pasa el del medico, no el del afiliado. Despues lo arreglo
                    queryCrearTurno.Parameters.Add("@idAfiliado", SqlDbType.BigInt, 8).Value = idAfiliado;
                    queryCrearTurno.Parameters.Add("@fechaTurno", SqlDbType.DateTime, 8).Value = fechaElegida;
                    try
                    {
                        queryCrearTurno.ExecuteNonQuery();
                        MessageBox.Show("Turno creado correctamente");
                        //Agrego esto para que no se llene de turnos basura mientras pruebo
                        //using (SqlCommand command = new SqlCommand("DELETE FROM [3FG].TURNOS WHERE ID_AFILIADO = '" + idAfiliado + "'", conexion)) command.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Error al tratar de guardar en database", "Error", MessageBoxButtons.OK);
                    }
                    conexion.Close();
                }
            }
            
        }
        
    }
}
