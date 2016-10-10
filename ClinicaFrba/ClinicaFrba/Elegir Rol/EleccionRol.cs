﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Elegir_Rol
{
    public partial class EleccionRol : Form
    {
        public EleccionRol(String usuario)
        {
            InitializeComponent();

            DataTable dt = Rol.buscarRolesDeUsuario(usuario);
            comboBoxRoles.DataSource = dt.DefaultView;
            comboBoxRoles.ValueMember = "NOMBRE_ROL";

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (comboBoxRoles.Text.Equals("Afiliado"))
            {
              
                ComboBoxGeneral unC = new ComboBoxGeneral(2);
                unC.ShowDialog();
                this.Hide();
            }else

            if (comboBoxRoles.Text.Equals("Profesional"))
            {
                ComboBoxGeneral unC = new ComboBoxGeneral(1);
                unC.ShowDialog();
                this.Hide();
              
            }else

            if (comboBoxRoles.Text.Equals("Administrativo"))
            {
                ComboBoxGeneral unC = new ComboBoxGeneral(1);
                unC.ShowDialog();
                this.Hide();
            }else


            if (comboBoxRoles.Text.Equals("Administrador general"))
            {
                ComboBoxGeneral unC = new ComboBoxGeneral(4);
                unC.ShowDialog();
                this.Hide();
            }


        }

        private void comboBoxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EleccionRol_Load(object sender, EventArgs e)
        {

        }
    }

}

/*
 
 namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class ElegirRol : Form
    {
        String elegirFormato;
        ABM_Rol.ModificarRol modificarRol;
        string rol;

        public ElegirRol(String formato, string rolPasado)
        {
            elegirFormato = formato;
            InitializeComponent();
            label1.Text = formato;
            buttonGuardar.Text = formato;
            rol = rolPasado;

            if (elegirFormato == "Eliminar Rol")
            {
                string query = "SELECT C_ROL FROM GDD_15.ROLES WHERE N_HABILITADO = 1";
                DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                comboBoxRol.DataSource = dt.DefaultView;
                comboBoxRol.ValueMember = "C_ROL";
            }
            else if (elegirFormato == "Modificar Rol")
            {
                string query = "SELECT C_ROL FROM GDD_15.ROLES";
                DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                comboBoxRol.DataSource = dt.DefaultView;
                comboBoxRol.ValueMember = "C_ROL";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (rol == comboBoxRol.Text)
            {
                MessageBox.Show("No se puede modificar o eliminar el rol utilizado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (elegirFormato == "Eliminar Rol")
            {
                if ((MessageBox.Show("¿Realmente desea dar de baja el rol " + comboBoxRol.Text + "?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    string query = "UPDATE GDD_15.ROLES SET N_HABILITADO = 0 WHERE C_ROL = '" + comboBoxRol.Text + "'";
                    DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                    MessageBox.Show("Rol " + comboBoxRol.Text + " eliminado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                }
                else
                {
                    return;
                }
            } 
            else if (elegirFormato == "Modificar Rol")
            {
                modificarRol = new ABM_Rol.ModificarRol(comboBoxRol.Text,this);
                modificarRol.ShowDialog();
            }
        }

        private void btFiltro_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No implementado por ahora", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ElegirRol_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

 
 */
