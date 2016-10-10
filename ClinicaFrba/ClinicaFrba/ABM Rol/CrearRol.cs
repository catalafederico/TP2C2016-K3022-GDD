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

namespace ClinicaFrba.ABM_Rol
{
    public partial class CrearRol : Form
    {

        string nombreRol;


        public CrearRol()
        {
            InitializeComponent();
            DataTable dt = CRol.obtenerFuncionalidades();
            checkedListBox1.DataSource = dt.DefaultView;
            checkedListBox1.ValueMember = "NOMBRE";
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CrearRol_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            nombreRol = textBox1.Text.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
            {
                checkedListBox1.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals(string.Empty))
            {
                MessageBox.Show("Falta agregar nombre", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkedListBox1.CheckedIndices.Count == 0)
            {
                MessageBox.Show("Falta elegir funcionalidades", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            };

            if (textBox1.Text.Length >= 100)
            {
                MessageBox.Show("El nombre de rol debe tener menos de 100 caracteres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!textBox1.Text.All(Char.IsLetter))
            {
                MessageBox.Show("Sólo se admiten letras en el nombre del rol", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (validacionNombreExistente())
            {
                return;
            }

            agregarRol();

            MessageBox.Show("Rol " + textBox1.Text + " agregado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
            this.Close();
        }



        private void agregarRol()
        {
            string agregarRol = "INSERT INTO [3FG].ROLES(NOMBRE_ROL) VALUES('" + nombreRol + "')";
            (new ConexionSQL()).ejecutarComandoSQL(agregarRol);

            DataTable tblfunc = new DataTable("func");
            tblfunc.Columns.Add("Id", typeof(int));

            string comando = "INSERT INTO [3FG].FUNCIONALIDADES_ROLES(ID_ROL, ID_FUNCIONALIDAD) SELECT [3FG].ROLES.ID_ROL, [3FG].FUNCIONALIDADES.ID_FUNCIONALIDAD FROM [3FG].ROLES  tablaRol,[3FG].FUNCIONALIDADES tablaFuncionalidad WHERE tablaRol.NOMBRE_ROL = '" + nombreRol + "' AND tablaFuncionalidad.NOMBRE in (";

            foreach (dominio.Funcionalidades elemento in checkedListBox1.CheckedItems)
            {
                comando = comando + " '" + elemento.Descripcion + "',";
            }
            comando = comando.Substring(0, comando.Length - 1);
            comando = comando + ")";

            (new ConexionSQL()).ejecutarComandoSQL(comando);

        }


        private bool validacionNombreExistente()
        {
            string comando = "SELECT * FROM  [3FG].ROLES WHERE C_ROL = '" + textBox1.Text + "'";
            DataTable dt = (new ConexionSQL()).cargarTablaSQL(comando);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0][2].ToString() == "0")
                {
                    MessageBox.Show("Existe un rol deshabilitado con ese nombre", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
                else
                {
                    MessageBox.Show("El nombre de rol ya existe", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }








    }
}
