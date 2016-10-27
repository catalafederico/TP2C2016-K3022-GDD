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
    public partial class ABMafiliados : Form
    {
        ABM_Afiliado.CrearUsuario crearUsuario;
        ModificarAfiliado modificarUsuario;
        EliminarAfiliado eliminiarAfi;
        List<Int64> raices;
        public ABMafiliados()
        {
           
            InitializeComponent();
            raices = new List<Int64>();
            this.llenarLista();
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            crearUsuario = new CrearUsuario(0, 0, 0, 0, 0, raices);
            crearUsuario.ShowDialog();
         
     
        }


        private void llenarLista()
        {
            int i = 0;
            int j = 0;
            /* SELECT COUNT(*) FROM [3FG].USUARIOS u join [3FG].AFILIADOS a on(u.ID_USUARIO=a.ID_USUARIO) WHERE u.TIPO_DE_DOCUMENTO = 'D.N.I' AND u.NUMERO_DOCUMENTO = '52655802';*/
            string query2 = "SELECT RAIZ_AFILIADO FROM [3FG].AFILIADOS";
            DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
            int flag = 0;
            while (flag == 0)
            {

                try
                {
                    string unDato = dt2.Rows[i][j].ToString();
                    raices.Add(Int64.Parse(unDato));
                    i++;
                }
                catch
                {
                    flag = 1;
                }

            }
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            modificarUsuario = new ModificarAfiliado();
            modificarUsuario.ShowDialog();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            eliminiarAfi = new EliminarAfiliado();
            eliminiarAfi.ShowDialog();
        }

    }
}
