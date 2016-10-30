using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Registrar_Agenda_Medico
{
    public partial class RegistrarAgenda : Form
    {
        string nombreUsuario;
        Int64 idPro;

        public RegistrarAgenda(String username, Int64 idProfe)
        {
            InitializeComponent();
            nombreUsuario = username;
            idPro = idProfe;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAñadirRango_Click(object sender, EventArgs e)
        {
            AñadirRango nuevoRango = new AñadirRango(idPro);
            nuevoRango.ShowDialog();
        }
    }
}
