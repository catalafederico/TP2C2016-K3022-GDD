using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.AbmRol
{
    public partial class ABMROL : Form
    {
        public ABMROL()
        {
            InitializeComponent();
        }

        private void ABMROL_Load(object sender, EventArgs e)
        {

        }

        private void button_crear_Click(object sender, EventArgs e)
        {
            ABM_Rol.CrearRol  cr = new ABM_Rol.CrearRol();
            cr.ShowDialog();
            this.Hide();



        }
    }
}
