using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Pedir_Turno
{
    public partial class Elegir_Horario : Form
    {
        private ABMTurnos t;
        private DateTime fechaTemprana;
        private DateTime fechaTardia;

        public Elegir_Horario(ABMTurnos turnos, string doctorElegido)
        {
            InitializeComponent();
            this.t = turnos;
            this.Text = "Elegir turno para Dr." + doctorElegido.ToString();
            this.fechaTemprana = DateTime.Now;
            this.fechaTardia = DateTime.Now.AddYears(1);
            label3.Text = "Primer turno disponible: " + fechaTemprana.ToString("MM/dd/yyyy HH:mm:ss");
            label4.Text = "Ultimo turno disponible: " + fechaTardia.ToString("MM/dd/yyyy HH:mm:ss");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < this.fechaTemprana || dateTimePicker1.Value > this.fechaTardia)
            {
                MessageBox.Show("Elija una fecha valida", "Error");
            }
            else
            {
                DateTime fechaElegida = dateTimePicker1.Value;
                label2.Text = "Horario elegido: " + fechaElegida;
            }
        }
        
    }
}
