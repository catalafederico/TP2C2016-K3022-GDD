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
    public partial class ModificarPlan : Form
    {
        String opcion;
        String PLanElegido;
        String Documento;
        public ModificarPlan(String comboBox, String plan, String doc)
        {
            InitializeComponent();
            opcion = comboBox;
            PLanElegido = plan;
            Documento = doc;
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            if (!verificarMotivo())
            {
                return;
            }


            /* update [3FG].AFILIADOS set ID_PLAN = (select ID_PLAN from [3FG].PLANES where DESCRIPCION_PLAN = 'plan caro') where(select  ID_USUARIO from [3FG].USUARIOS where NUMERO_DOCUMENTO =334444) = [3FG].AFILIADOS.ID_USUARIO*/
            string comando = " update [3FG].AFILIADOS set ID_PLAN = (select ID_PLAN from [3FG].PLANES where DESCRIPCION_PLAN = '" + PLanElegido + "') where(select  ID_USUARIO from [3FG].USUARIOS where NUMERO_DOCUMENTO ='" + Int64.Parse(Documento) + "')= [3FG].AFILIADOS.ID_USUARIO";
            (new ConexionSQL()).ejecutarComandoSQL(comando);
            /*insert into [3FG].HISTORIAL_CAMBIOS_PLAN(ID_USUARIO,MOTIVO_CAMBIO_PLAN,FECHA_MODIFICACION) values((select ID_USUARIO from [3FG].AFILIADOS a where (select  ID_USUARIO from [3FG].USUARIOS where NUMERO_DOCUMENTO =334444) =a.ID_USUARIO) ,'se me canto cambiar',GETDATE())*/
            string comando1 = " insert into [3FG].HISTORIAL_CAMBIOS_PLAN(ID_USUARIO,MOTIVO_CAMBIO_PLAN,FECHA_MODIFICACION) values((select ID_USUARIO from [3FG].AFILIADOS a where (select  ID_USUARIO from [3FG].USUARIOS where NUMERO_DOCUMENTO ='" + Int64.Parse(Documento) + "') =a.ID_USUARIO) ,'" + textBox1.Text + "',GETDATE()) ";
            (new ConexionSQL()).ejecutarComandoSQL(comando1);
            MessageBox.Show("plan modificado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
            this.Hide();
        }
        private bool verificarMotivo()
        {

            /* SELECT COUNT(*) FROM [3FG].USUARIOS u join [3FG].AFILIADOS a on(u.ID_USUARIO=a.ID_USUARIO) WHERE u.TIPO_DE_DOCUMENTO = 'D.N.I' AND u.NUMERO_DOCUMENTO = '52655802';*/

            if (textBox1.Text == "")
            {
                MessageBox.Show("Debe Agregar el motivo de la modificacion del plan", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            return true;
        }

        private void ModificarPlan_Load(object sender, EventArgs e)
        {

        }

    }
}
