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
    public partial class AñadirRango : Form
    {
        Int64 idPro;

        public AñadirRango(Int64 idProfe)
        {
            InitializeComponent();
            idPro = idProfe;

            DataTable dt1 = (new ConexionSQL()).cargarTablaSQL("SELECT DESCRIPCION_ESPECIALIDAD FROM [3FG].ESPECIALIDAD_PROFESIONAL EP JOIN [3FG].ESPECIALIDADES E ON (E.ID_ESPECIALIDAD = EP.ID_ESPECIALIDAD) WHERE EP.ID_USUARIO = '" + idPro + "'");
            comboBoxEspecialidades.DataSource = dt1.DefaultView;
            comboBoxEspecialidades.ValueMember = "DESCRIPCION_ESPECIALIDAD";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
