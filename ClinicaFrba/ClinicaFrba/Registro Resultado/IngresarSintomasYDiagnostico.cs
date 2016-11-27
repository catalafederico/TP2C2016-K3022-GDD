using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClinicaFrba.Registro_Resultado
{
    public partial class IngresarSintomasYDiagnostico : Form
    {
        private int idRecepcion;

        

        // Funcion de inicializacion
        public IngresarSintomasYDiagnostico(int idRec)
        {
            // Setteo el id de la recepcion recibida
            this.idRecepcion = idRec;
            InitializeComponent();
        }



        // Funcion de creacion de resultado
        private void button1_Click(object sender, EventArgs e)
        {
            // Me fijo que el profesional ingrese sintomas
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Me fijo que el profesional ingrese un diagnostico
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    // Delego en esta funcion
                    crearResultado();
                }
                else MessageBox.Show("Debe ingresar su diagnostico", "Error", MessageBoxButtons.OK);
            }
            else MessageBox.Show("Debe ingresar los sintomas del paciente", "Error", MessageBoxButtons.OK);
        }



        // Verdadera funcion de creacion de resultado
        private void crearResultado()
        {
            // Abro la conexion
            using (SqlConnection conexion = new ConexionSQL().conectar())
            {
                // Creo que la query de insercion en la tabla
                string crearResultado = "INSERT INTO [3FG].ATENCIONES_MEDICAS(ID_RECEPCION,FECHA_ATENCION,SINTOMAS,DIAGNOSTICO) VALUES (@idRecepcion,@fechaAtencion,@sintomas,@diagnostico)";

                // Lleno los datos de la query dinamicamente para evitar SQL Injection
                using (SqlCommand queryCrearResultado = new SqlCommand(crearResultado))
                {
                    queryCrearResultado.Connection = conexion;
                    queryCrearResultado.Parameters.Add("@idRecepcion", SqlDbType.BigInt, 8).Value = this.idRecepcion;
                    queryCrearResultado.Parameters.Add("@fechaAtencion", SqlDbType.DateTime, 8).Value = DateTime.Now;

                    //Estos dos valores los tomo directamente del textBox
                    queryCrearResultado.Parameters.Add("@sintomas", SqlDbType.VarChar, 250).Value = textBox1.Text;
                    queryCrearResultado.Parameters.Add("@diagnostico", SqlDbType.VarChar, 250).Value = textBox2.Text;

                    // Trato de ejecutar la query
                    try
                    {
                        queryCrearResultado.ExecuteNonQuery();
                        MessageBox.Show("Resultado de atencion creado correctamente", "OK", MessageBoxButtons.OK);
                        this.Close();
                    }

                    // Por si falla
                    catch
                    {
                        MessageBox.Show("Error al tratar de guardar en database", "Error", MessageBoxButtons.OK);
                    }
                    conexion.Close();
                }
            }
        }
    }
}
