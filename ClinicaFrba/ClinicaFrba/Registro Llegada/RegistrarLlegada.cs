﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClinicaFrba.Registro_Llegada
{
    public partial class RegistrarLlegada : Form
    {
        private int idEspecialidad;
        private string doctorElegido;
        private int idDoctor;

        // Esta query busca todos el ID, nombre y apellido de cada profesional ademas de su especialidad y el ID de la misma
        private string queryDeLoadTable = "SELECT U.APELLIDO AS Apellido, U.NOMBRE AS Nombre, U.ID_USUARIO, E.DESCRIPCION_ESPECIALIDAD AS Especialidad, E.ID_ESPECIALIDAD FROM [3FG].USUARIOS U, [3FG].PROFESIONALES P, [3FG].ESPECIALIDAD_PROFESIONAL EP, [3FG].ESPECIALIDADES E WHERE (U.ID_USUARIO = P.ID_USUARIO) AND (P.ID_USUARIO = EP.ID_USUARIO) AND (EP.ID_ESPECIALIDAD = E.ID_ESPECIALIDAD) AND U.HABILITADO = 1";

        public RegistrarLlegada()
        {
            
            InitializeComponent();

            // Cargo el DataGrid
            ConexionSQL.loadDataGrid(queryDeLoadTable, dataGridView1);

            // Escondo las columnas de ID, me viene bien tenerlos a mano pero no quiero que el usuario los vea
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

       /* private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            // Agarro la query que use para settear el DataGrid
            string newQuery = this.queryDeLoadTable;

            // Agarro los strings del textBox
            string nombre = textBox1.Text;
            string apellido = textBox2.Text;
            string especialidad = textBox3.Text;

            // Me fijo que casilleros fueron checkeados modifico la primera query en base a ellos
            if (checkBox1.Checked) newQuery += " AND (U.NOMBRE LIKE '%" + nombre + "%')";
            if (checkBox2.Checked) newQuery += " AND (U.APELLIDO LIKE '%" + apellido + "%')";
            if (checkBox3.Checked) newQuery += " AND (E.DESCRIPCION_ESPECIALIDAD LIKE '%" + especialidad + "%')";

            // Vuelvo a cargar el DataGrid
            ConexionSQL.loadDataGrid(newQuery, dataGridView1);
        }



        // Funcion para propositos esteticos nomas
        private string nombreBienEscrito(object nombre)
        {
            string nombreString = nombre.ToString();
            char primerLetra = nombreString[0];
            string elResto = nombreString.Remove(0, 1).ToLower();
            string todoJunto = primerLetra + elResto;
            return todoJunto;
        }

        // Funcion de eleccion de profesional y especialidad por click en el DataGrid
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Me fijo que el usuario no haya clickeado el nombre de la columna
            if (e.RowIndex >= 0)
            {
                // Tomo toda la informacion de la fila que fue clickeada
                object apellido = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                object nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                object idPro = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                object especialidad = dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                object idEsp = dataGridView1.Rows[e.RowIndex].Cells[4].Value;

                // Setteo las variables
                doctorElegido = apellido.ToString();
                this.idDoctor = int.Parse(idPro.ToString());
                this.idEspecialidad = int.Parse(idEsp.ToString());

                // Modifico la ventana para reflejar la eleccion del usuario
                label4.Text = "Profesional Elegido: " + doctorElegido + ", " + nombreBienEscrito(nombre);
                label5.Text = "Especialidad Elegida: " + especialidad.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Si elegi un doctor
            if (doctorElegido != null)
            {
                // Creo una nueva ventana para elegir horario con los datos que necesita
                ElegirTurno eh = new ElegirTurno(idDoctor,idEspecialidad);

                // Escondo esta venta
                this.Hide();

                // Y modifico a la nueva ventana para que al cerrarse cierre esta tambien
                eh.Closed += (s, args) => this.Close();
                eh.ShowDialog();
            }
            else MessageBox.Show("No ha seleccionado un profesional", "Error", MessageBoxButtons.OK);
        
        }

        private void RegistrarLlegada_Load(object sender, EventArgs e)
        {

        }

     
    }
}
