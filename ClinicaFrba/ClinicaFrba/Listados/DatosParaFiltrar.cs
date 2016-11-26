using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Listados
{
    public partial class DatosParaFiltrar : Form
    {
        String listadoAPlasmarEnDataGrid;
        String semestreElegido;
        String anioElegido;
        public DatosParaFiltrar(String listado,String semestre,String anio)
        {
            InitializeComponent();
            semestreElegido = semestre;
            anioElegido = anio;
            string query = "select DESCRIPCION_PLAN from [3FG].PLANES";
                DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                comboBox_PLAN.DataSource = dt.DefaultView;
                comboBox_PLAN.ValueMember = "DESCRIPCION_PLAN";


                string query1 = "select DESCRIPCION_ESPECIALIDAD from [3FG].ESPECIALIDADES";
                DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query1);
                comboBox_ESPECIALIDAD.DataSource = dt2.DefaultView;
                comboBox_ESPECIALIDAD.ValueMember = "DESCRIPCION_ESPECIALIDAD";
                listadoAPlasmarEnDataGrid = listado;

            
        }

        private void DatosParaFiltrar_Load(object sender, EventArgs e)
        {

        }

        private void comboBox_PLAN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_ESPECIALIDAD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VentanaHistorial ventana = new VentanaHistorial(listadoAPlasmarEnDataGrid, comboBox_PLAN.Text, comboBox_ESPECIALIDAD.Text, semestreElegido, anioElegido);
            ventana.ShowDialog();
            this.Hide();
        }
    }
}
