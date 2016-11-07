using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaFrba.ABM_Rol;
using ClinicaFrba.Pedir_Turno;

namespace ClinicaFrba.Eleccion_Funcionalidad
{
    public partial class ElegirFuncionalidad : Form
    {

        string funcionalidad;
        string nombreUsuario;
        string rol;
        AbmRol.ABMROL abmRol;
        ABM_Afiliado.ABMafiliados abmAfiliado;
        ABMTurnos unTurno;
        Compra_Bono.EfectivizarCompra unaCompra;
        Compra_Bono.Comprar_Bonos unaCompraAdmin;
        
        Registrar_Agenda_Medico.RegistrarAgenda agenda;
      
        Int64 idUsuario;

        public ElegirFuncionalidad(String rolPasado, String username)
        {
            InitializeComponent();
            rol = rolPasado;
            nombreUsuario = username;
            DataTable dt = (new ConexionSQL()).cargarTablaSQL("SELECT F.NOMBRE FROM [3FG].FUNCIONALIDADES_ROL FR JOIN [3FG].ROLES R ON (R.ID_ROL = FR.ID_ROL) JOIN [3FG].FUNCIONALIDADES F ON (F.ID_FUNCIONALIDAD = FR.ID_FUNCIONALIDAD) WHERE R.NOMBRE_ROL = '" + rolPasado + "' AND R.HABILITADO = 1");
            comboBox_funcionalidades.DataSource = dt.DefaultView;
            comboBox_funcionalidades.ValueMember = "NOMBRE";

            string query2 = "SELECT ID_USUARIO FROM [3FG].USUARIOS WHERE USUARIO_NOMBRE = '" + nombreUsuario + "'";
            DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
            string idCliente = dt2.Rows[0][0].ToString();
            idUsuario = Convert.ToInt64(idCliente);

            //Verifica si puede seguir comprando o no
            /* ver despues por el tema de las compras*/
            if (rolPasado == "Cliente")
            {
                string query5 = "SELECT (SELECT COUNT(*) FROM GDD_15.OFERTAS WHERE N_ID_CLIENTE = '" + idUsuario + "' AND C_GANADOR = 'SI') + (SELECT COUNT(*) FROM GDD_15.COMPRAS WHERE N_ID_CLIENTE = '" + idUsuario + "') - (SELECT COUNT(*) FROM GDD_15.CALIFICACIONES WHERE N_ID_CLIENTE = '" + idUsuario + "')";
                DataTable dt5 = (new ConexionSQL()).cargarTablaSQL(query5);
                string comprasSinCalif = dt5.Rows[0][0].ToString();
                Int32 cantComprasSinCalif = Convert.ToInt32(comprasSinCalif);

                string query3 = "SELECT N_COMPRA_HABILITADA FROM GDD_15.CLIENTES WHERE N_ID_USUARIO = '" + idUsuario + "'";
                DataTable dt3 = (new ConexionSQL()).cargarTablaSQL(query3);
                string compraHabilitada = dt3.Rows[0][0].ToString();

                if (compraHabilitada == "1")
                {
                    if (cantComprasSinCalif < 4)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Como tiene más de 3 publicaciones (" + cantComprasSinCalif + ") sin calificar no puede realizar compras u ofertas hasta que califique todas sus publicaciones", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string query7 = "UPDATE GDD_15.CLIENTES SET N_COMPRA_HABILITADA = '0' WHERE N_ID_USUARIO = '" + idUsuario + "'";
                        DataTable dt7 = (new ConexionSQL()).cargarTablaSQL(query7);
                    }
                }
            }
        }

        private void comboBox_funcionalidades_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_seleccionar_funcionalidad_Click(object sender, EventArgs e)
        {
            funcionalidad = comboBox_funcionalidades.Text;

            switch (funcionalidad)
            {
                case "ABM de Rol":
                    abmRol = new AbmRol.ABMROL(rol);
                    abmRol.ShowDialog();
                    break;

                case "ABM de Afiliado":
                    if (rol == "Administrativo" || rol == "Administrador general")
                    {
                        abmAfiliado = new ABM_Afiliado.ABMafiliados();
                        abmAfiliado.ShowDialog();
                    }

                    else
                    {
                        MessageBox.Show("No se puede modificar un Afiliado con el rol: " + rol);
                    }
                    break;
                case"Solicitar turno":
                    unTurno = new ABMTurnos((int) idUsuario);
                    unTurno.ShowDialog();
                    break;
                case "Registrar agenda del profesional":
                    agenda = new Registrar_Agenda_Medico.RegistrarAgenda(idUsuario);
                    agenda.ShowDialog();
                    break;
               case "Comprar Bonos":
                    if (rol == "Administrativo" || rol == "Administrador general")
                    {
                        unaCompraAdmin = new Compra_Bono.Comprar_Bonos();
                        unaCompraAdmin.ShowDialog();
                    }
                    else
                    {
                        unaCompra = new Compra_Bono.EfectivizarCompra((int)idUsuario);
                        unaCompra.ShowDialog();
                    }
                    break;
            }
        }

        private void ElegirFuncionalidad_Load(object sender, EventArgs e)
        {

        }
    }
}
