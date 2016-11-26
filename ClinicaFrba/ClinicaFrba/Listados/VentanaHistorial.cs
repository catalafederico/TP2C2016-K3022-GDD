using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaFrba.Dominio;

namespace ClinicaFrba.Listados
{
    public partial class VentanaHistorial : Form
    {
        String planParaFiltrar;
        String especialidadParaFiltrar;
        String semetreElegido;
        String anioElegido;
        public VentanaHistorial(String listado,String plan,String especialidad,String semestre,String anio)
        {
            InitializeComponent();
            label1.Text = listado;
            planParaFiltrar = plan;
            especialidadParaFiltrar = especialidad;
            semetreElegido = semestre;
            anioElegido = anio;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            inicializar();
        }

        private void VentanaHistorial_Load(object sender, EventArgs e)
        {

        }

        private void inicializar()
        {
            if (label1.Text=="ESPECIALIDADES CON MAS CANELACIONES")
            {
                CompletadorDeTablas.hacerQuery("exec [3FG].top5Cancelaciones "+semetreElegido+","+anioElegido+"", ref dataGridView1);
            
            }


            if (label1.Text == "PROFESIONALES CON MAS CONSULTAS")
            {
                /*exec [3FG].top5ProfesionalesConsultadosPorPLan 'Plan Medico 130','Alergología',1,2015*/
                CompletadorDeTablas.hacerQuery("exec [3FG].top5ProfesionalesConsultadosPorPLan '" + planParaFiltrar + "','" + especialidadParaFiltrar + "'," + semetreElegido + "," + anioElegido + "", ref dataGridView1);
                
            }
            if (label1.Text == "AFILIADOS CON MAS BONOS COMPRADOS")
            {
                /*exec [3FG].top5AfiliafsCompradoosConMasBonos 2,2015*/
                CompletadorDeTablas.hacerQuery("exec [3FG].top5AfiliafsCompradoosConMasBonos " + semetreElegido + "," + anioElegido + "", ref dataGridView1);
                
            }

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
