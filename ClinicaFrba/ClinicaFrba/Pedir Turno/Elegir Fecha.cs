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
        private int idAgenda;
        private int idDoctor;
        private DateTime fechaTemprana;
        private DateTime fechaTardia;
        private DateTime fechaElegida = new DateTime(1000,01,01);
        private int idAfiliado;
        private String diaDeLaSemana;

        public Elegir_Horario(string doctorElegido, int idDoc, int idAfi)
        {
            InitializeComponent();
            this.idAfiliado = idAfi;
            this.Text = "Elegir fecha para consulta con Dr." + doctorElegido.ToString();
            SqlCommand dateEarly = new SqlCommand("SELECT P.INICIO_DISPONIBILIDAD FROM [3FG].PROFESIONALES P WHERE P.ID_USUARIO LIKE '" + idDoc + "'", BDComun.obtenerConexion());
            this.fechaTemprana = (DateTime)dateEarly.ExecuteScalar();
            SqlCommand dateLate = new SqlCommand("SELECT P.FIN_DISPONIBILIDAD FROM [3FG].PROFESIONALES P WHERE P.ID_USUARIO LIKE '" + idDoc + "'", BDComun.obtenerConexion());
            this.fechaTardia = (DateTime)dateLate.ExecuteScalar();
            label3.Text = "Primera fecha disponible: " + fechaTemprana.ToString("yyyy-MM-dd");
            label4.Text = "Ultima fecha disponible: " + fechaTardia.ToString("yyyy-MM-dd");
            this.idDoctor = idDoc;
        }

        private bool hayAgenda()
        {
            switch (fechaElegida.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    this.diaDeLaSemana = "DOMINGO";
                    break;
                case DayOfWeek.Monday:
                    this.diaDeLaSemana = "LUNES";
                    break;
                case DayOfWeek.Tuesday:
                    this.diaDeLaSemana = "MARTES";
                    break;
                case DayOfWeek.Wednesday:
                    this.diaDeLaSemana = "MIERCOLES";
                    break;
                case DayOfWeek.Thursday:
                    this.diaDeLaSemana = "JUEVES";
                    break;
                case DayOfWeek.Friday:
                    this.diaDeLaSemana = "VIERNES";
                    break;
                case DayOfWeek.Saturday:
                    this.diaDeLaSemana = "SABADO";
                    break;
            }
            SqlCommand queryHayAgenda = new SqlCommand("SELECT COUNT(*) FROM [3FG].PROFESIONALES P, [3FG].AGENDA A WHERE (P.ID_USUARIO LIKE '" + idDoctor + @"') 
                                                        AND (P.ID_USUARIO = A.ID_USUARIO) AND (A.DIA_ATENCION = '" + this.diaDeLaSemana +"')", BDComun.obtenerConexion());
            if ((int)queryHayAgenda.ExecuteScalar() > 0)
            {
                return true;
            }
            else return false;
        }

        private int buscarAgenda()
        {
            SqlCommand queryBuscarAgenda = new SqlCommand("SELECT A.ID_AGENDA FROM [3FG].PROFESIONALES P, [3FG].AGENDA A WHERE (P.ID_USUARIO LIKE '" + idDoctor + @"') 
                                                           AND (P.ID_USUARIO = A.ID_USUARIO) AND (A.DIA_ATENCION = '" + this.diaDeLaSemana + "')", BDComun.obtenerConexion());
            return int.Parse(queryBuscarAgenda.ExecuteScalar().ToString());
        }

       private void button1_Click(object sender, EventArgs e)
        {
            if (this.fechaElegida.Year != 1000)
            {
                ElegirHora eh = new ElegirHora(idAgenda, idAfiliado, fechaElegida);
                this.Hide();
                eh.Closed += (s, args) => this.Close();
                eh.Show();
            }

            if (this.fechaElegida.Year == 1000)
            {
                MessageBox.Show("No ha seleccionado una fecha", "Error", MessageBoxButtons.OK);
            }
        }

       private void button2_Click(object sender, EventArgs e)
       {
           if (dateTimePicker1.Value.Date < this.fechaTemprana.Date || dateTimePicker1.Value.Date > this.fechaTardia.Date)
           {
               MessageBox.Show("La fecha seleccionada no se encuentra dentro de las disponibles", "Error", MessageBoxButtons.OK);
           }
           else
           {
               this.fechaElegida = dateTimePicker1.Value;
               if (hayAgenda())
               {
                   this.idAgenda = buscarAgenda();
                   label2.Text = "Fecha elegida: " + this.fechaElegida.Date.ToString("yyyy-MM-dd");
               }
               else MessageBox.Show("El profesional elegido no atiende ese día", "Error", MessageBoxButtons.OK);
           }
       }

    }
}
