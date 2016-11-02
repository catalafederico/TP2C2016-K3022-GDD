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
    public partial class ElegirHora : Form
    {
        private int idAgenda;
        private int idAfiliado;
        private TimeSpan horarioMinimo;
        private TimeSpan horarioMaximo;
        private DateTime fechaElegida;
        private bool eligio = false;

        public ElegirHora(int idAg, int idAf, DateTime fecha)
        {
            this.idAgenda = idAg;
            this.idAfiliado = idAf;
            this.fechaElegida = fecha;
            SqlCommand queryBuscarMinimo = new SqlCommand("SELECT INICIO_ATENCION FROM [3FG].AGENDA WHERE ID_AGENDA = " + this.idAgenda.ToString(), BDComun.obtenerConexion());
            this.horarioMinimo = TimeSpan.Parse(queryBuscarMinimo.ExecuteScalar().ToString());
            SqlCommand queryBuscarMaximo = new SqlCommand("SELECT FIN_ATENCION FROM [3FG].AGENDA WHERE ID_AGENDA = " + this.idAgenda.ToString(), BDComun.obtenerConexion());
            this.horarioMaximo = TimeSpan.Parse(queryBuscarMaximo.ExecuteScalar().ToString());
            InitializeComponent();
            label4.Text = "Primer turno: " + horarioMinimo.ToString();
            label5.Text = "Ultimo turno: " + horarioMaximo.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan horarioAElegir = new TimeSpan((int) numericUpDown1.Value,(int) numericUpDown2.Value, 0);
            if (horarioAElegir > horarioMaximo || horarioAElegir < horarioMinimo)
            { 
                MessageBox.Show("Horario no disponible", "Error", MessageBoxButtons.OK);
            }
            else
            {
                verificacionYPedidoDeTurno(horarioAElegir);
                eligio = true;
            }
        }

        private void verificacionYPedidoDeTurno(TimeSpan horarioAElegir)
        {
            DateTime posibleFecha = fechaElegida.Date + horarioAElegir;
            SqlCommand disponibilidad = new SqlCommand(@"SELECT COUNT(*) FROM [3FG].TURNOS T, [3FG].AGENDA A
                                                         WHERE (T.ID_AGENDA = " + this.idAgenda.ToString() + @") AND 
                                                         CONVERT(varchar, T.FECHA_TURNO, 108) = CONVERT(varchar,'" + posibleFecha.ToString("hh:mm:ss") + @"', 108)
                                                         AND CONVERT(DATE,T.FECHA_TURNO) = CONVERT(DATE,'" + posibleFecha.Date.ToString("yyyy-MM-dd") + "') ", BDComun.obtenerConexion());
            int turnosPisados = (int)disponibilidad.ExecuteScalar();
            if (turnosPisados > 0)
            {
                MessageBox.Show("Ya hay un turno en ese horario", "Error", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Puede pedir un turno en este horario", "OK", MessageBoxButtons.OK);
                this.fechaElegida = posibleFecha;
                label3.Text = "Turno a pedir: " + posibleFecha.ToString("yyyy-MM-dd hh-mm");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (eligio)
            {
                using (SqlConnection conexion = BDComun.obtenerConexion())
                {
                    string crearTurno = "INSERT INTO [3FG].TURNOS(ID_AFILIADO,ID_AGENDA,FECHA_TURNO) VALUES((Select ID_USUARIO from [3FG].USUARIOS where ID_USUARIO = " + idAfiliado + "),@idAgenda,@fechaTurno)";

                    using (SqlCommand queryCrearTurno = new SqlCommand(crearTurno))
                    {
                        queryCrearTurno.Connection = BDComun.obtenerConexion();
                        queryCrearTurno.Parameters.Add("@idAgenda", SqlDbType.BigInt, 8).Value = idAgenda;
                        queryCrearTurno.Parameters.Add("@fechaTurno", SqlDbType.DateTime, 8).Value = fechaElegida;
                        try
                        {
                            queryCrearTurno.ExecuteNonQuery();
                            MessageBox.Show("Turno creado correctamente", "OK", MessageBoxButtons.OK);
                        }
                        catch
                        {
                            MessageBox.Show("Error al tratar de guardar en database", "Error", MessageBoxButtons.OK);
                        }
                        conexion.Close();
                    }
                }
            }
            else MessageBox.Show("Verifique la disponibilidad del turno antes de confirmarlo", "Error", MessageBoxButtons.OK);
        }
            
    }
    
}
