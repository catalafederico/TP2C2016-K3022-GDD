using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using ClinicaFrba.ABM_Rol;


namespace ClinicaFrba.AbmRol
{
    public partial class ABMROL : Form
    {

        ABM_Rol.CrearRol crearRol;
        ABM_Rol.ElegirRol elegirRol;
        string rol;

        public ABMROL(String rolPasado)
        {
            InitializeComponent();
            rol = rolPasado;
        }

        private void ABMROL_Load(object sender, EventArgs e)
        {

        }

        private void button_crear_Click(object sender, EventArgs e)
        {
            crearRol = new CrearRol();
            crearRol.ShowDialog();
            this.Hide();



        }

        private void button_eliminar_Click(object sender, EventArgs e)
        {
            elegirRol = new ElegirRol("Eliminar Rol", rol);
            elegirRol.ShowDialog();
        }

        private void button_modificar_Click(object sender, EventArgs e)
        {
            elegirRol = new ElegirRol("Modificar Rol", rol);
            elegirRol.ShowDialog();
        }
    }
}
