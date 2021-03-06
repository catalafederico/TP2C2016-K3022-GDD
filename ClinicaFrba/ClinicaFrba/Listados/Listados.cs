﻿using System;
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
    public partial class Listados : Form
    {
        public Listados()
        {
            InitializeComponent();
            comboBox1.Items.Add("ESPECIALIDADES CON MAS CANELACIONES");
            comboBox1.Items.Add("PROFESIONALES CON MAS CONSULTAS");
            comboBox1.Items.Add("AFILIADOS CON MAS BONOS COMPRADOS");
            comboBox1.Items.Add("PROFESIONALES CON MENOS HORAS TRABAJADAS");
            comboBox1.Items.Add("ESPECIALIDADES CON MAS BONO DE CONSULTA UTILIZADOS");
            /*exec [3FG].top5EspecialidadesConMasBonosUtilizados 1,2015*/
            comboBox_SEMESTRE.Items.Add("1");
            comboBox_SEMESTRE.Items.Add("2");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool validacionesCliente()
        {


            if (comboBox1.Text =="")
            {
                MessageBox.Show("No se ha seleccionado ningun listado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (comboBox_SEMESTRE.Text=="")
            {
                MessageBox.Show("No se elegir el semestre", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (textBox_AÑO.Text=="")
            {
                MessageBox.Show("No se ha Elegido el año", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            return true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validacionesCliente())
            {
                return;
            }
            if (comboBox1.Text == "ESPECIALIDADES CON MAS CANELACIONES")
            {
                VentanaHistorial ventana = new VentanaHistorial(comboBox1.Text, null, null, comboBox_SEMESTRE.Text,textBox_AÑO.Text);
                ventana.ShowDialog();
             
            }

            if (comboBox1.Text == "PROFESIONALES CON MAS CONSULTAS")
            {
                DatosParaFiltrar ventana = new DatosParaFiltrar(comboBox1.Text, comboBox_SEMESTRE.Text, textBox_AÑO.Text);
                ventana.ShowDialog();
             
            }
            if (comboBox1.Text == "AFILIADOS CON MAS BONOS COMPRADOS")
            {
                VentanaHistorial ventana = new VentanaHistorial(comboBox1.Text, null, null, comboBox_SEMESTRE.Text, textBox_AÑO.Text);
                ventana.ShowDialog();
               
            }
            if (comboBox1.Text == "ESPECIALIDADES CON MAS BONO DE CONSULTA UTILIZADOS")
            {
                VentanaHistorial ventana = new VentanaHistorial(comboBox1.Text, null, null, comboBox_SEMESTRE.Text, textBox_AÑO.Text);
                ventana.ShowDialog();
                
            }
            if (comboBox1.Text == "PROFESIONALES CON MENOS HORAS TRABAJADAS")
            {
                DatosParaFiltrar ventana = new DatosParaFiltrar(comboBox1.Text, comboBox_SEMESTRE.Text, textBox_AÑO.Text);
                ventana.ShowDialog();
              
            }

            
        }

        private void comboBox_SEMESTRE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
