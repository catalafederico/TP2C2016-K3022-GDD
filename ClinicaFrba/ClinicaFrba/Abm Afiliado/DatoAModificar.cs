using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.ABM_Afiliado
{
    public partial class DatoAModificar : Form
    {
        String unDato;
        public DatoAModificar(String Numero)
        {
            InitializeComponent();
            comboBox1.Items.Add("Direccion");
            comboBox1.Items.Add("Telefono");
            comboBox1.Items.Add("Mail");
            comboBox1.Items.Add("Plan Medico");
            unDato = Numero;
        }

        private void DatoAModificar_Load(object sender, EventArgs e)
        {

        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {



            if (!validacionesCliente())
            {
                return;
            }


            if ((MessageBox.Show("¿Realmente desea modificar al Afiliado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                modificarAfiliado(comboBox1.Text, textBox1.Text);

                this.Close();

            }
        }


        private void modificarAfiliado(String comboBox, String texto)
        {
            /* update [3FG].USUARIOS set DIRECCION ='lala' where ID_USUARIO= 2 */

            if (comboBox == "Direccion")
            {
                string comando = "update [3FG].USUARIOS set DIRECCION ='" + texto + "' where NUMERO_DOCUMENTO='" + Int64.Parse(unDato) + "'";
                (new ConexionSQL()).ejecutarComandoSQL(comando);
                MessageBox.Show("Afiliado modificado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
            }

            if (comboBox == "Telefono")
            {
                string comando = "update [3FG].USUARIOS set TELEFONO ='" + Int64.Parse(texto) + "' where NUMERO_DOCUMENTO='" + Int64.Parse(unDato) + "'";
                (new ConexionSQL()).ejecutarComandoSQL(comando);
                MessageBox.Show("Afiliado modificado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
            }

            if (comboBox == "Mail")
            {
                string comando = "update [3FG].USUARIOS set Mail ='" + Int64.Parse(texto) + "' where NUMERO_DOCUMENTO='" + Int64.Parse(unDato) + "'";
                (new ConexionSQL()).ejecutarComandoSQL(comando);
                MessageBox.Show("Afiliado modificado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
            }

            if (comboBox == "Plan Medico")
            {

                if (!verificarPlanes())
                {
                    return;
                }

                ModificarPlan planAModificar = new ModificarPlan(comboBox1.Text, textBox1.Text, unDato);
                planAModificar.ShowDialog();


            }



            /*

              string comando = "DELETE FROM [3FG].FUNCIONALIDADES_ROL WHERE ID_ROL = '" + idRol + "'";
              (new ConexionSQL()).ejecutarComandoSQL(comando);

              string comando2 = "INSERT INTO [3FG].FUNCIONALIDADES_ROL(ID_ROL, ID_FUNCIONALIDAD) SELECT tablaRol.ID_ROL,tablaFuncionalidad.ID_FUNCIONALIDAD FROM [3FG].ROLES  tablaRol, [3FG].FUNCIONALIDADES tablaFuncionalidad WHERE tablaRol.NOMBRE_ROL = '" + rol + "' AND tablaFuncionalidad.NOMBRE IN (";

              foreach (Funcionalidades elemento in chkListaFuncionalidades.CheckedItems)
              {
                  comando2 = comando2 + " '" + elemento.Descripcion + "',";
              }
              comando2 = comando2.Substring(0, comando2.Length - 1);
              comando2 = comando2 + ")";

              (new ConexionSQL()).ejecutarComandoSQL(comando2);

              string comando5 = "UPDATE [3FG].ROLES SET NOMBRE_ROL = '" + txtNombreRol.Text + "' WHERE ID_ROL = '" + idRol + "'";
              (new ConexionSQL()).ejecutarComandoSQL(comando5);

              if (estadoAnterior == true && chkHabilitado.Checked == true)
              {

              }
              else if (estadoAnterior == true && chkHabilitado.Checked == false)
              {
                  string comando3 = "UPDATE [3FG].ROLES SET HABILITADO = 0 WHERE NOMBRE_ROL = '" + rol + "'";
                  (new ConexionSQL()).ejecutarComandoSQL(comando3);
              }
              else if (estadoAnterior == false && chkHabilitado.Checked == true)
              {
                  string comando4 = "UPDATE [3FG].ROLES SET HABILITADO = 1 WHERE NOMBRE_ROL = '" + rol + "'";
                  (new ConexionSQL()).ejecutarComandoSQL(comando4);
              }
              else
              {

              }

              */
        }

        private bool verificarPlanes()
        {

            /*SELECT COUNT(*) FROM [3FG].PLANES WHERE DESCRIPCION_PLAN = 'plan caro'*/
            string query2 = "SELECT COUNT(*) FROM [3FG].PLANES WHERE DESCRIPCION_PLAN = '" + textBox1.Text + "'";
            DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
            string cantidad = dt2.Rows[0][0].ToString();
            if (cantidad == "0")
            {
                MessageBox.Show("Ese plan no se Encuentra en el sistema", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private bool validacionesDato()
        {

            /* SELECT COUNT(*) FROM [3FG].USUARIOS u join [3FG].AFILIADOS a on(u.ID_USUARIO=a.ID_USUARIO) WHERE u.TIPO_DE_DOCUMENTO = 'D.N.I' AND u.NUMERO_DOCUMENTO = '52655802';*/

            if (textBox1.Text == null)
            {
                MessageBox.Show("Debe Agregar el dato para modificar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (comboBox1.Text == null)
            {
                MessageBox.Show("Debe Elegir el Dato a Modificar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private bool validacionesCliente()
        {
         

            if (comboBox1.Text=="")
            {
                MessageBox.Show("No ha completado que desea modificar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }



            if (textBox1.Text =="")
            {
                MessageBox.Show("No se agrego el nuevo dato", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

           

            return true;



        }



    }
}
