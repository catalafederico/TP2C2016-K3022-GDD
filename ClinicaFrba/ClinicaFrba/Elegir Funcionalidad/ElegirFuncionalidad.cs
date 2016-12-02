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
using ClinicaFrba.Cancelar_Atencion;
using ClinicaFrba.Listados;

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
        Listados.Listados Listados;
        Compra_Bono.EfectivizarCompra unaCompra;
        Compra_Bono.Comprar_Bonos unaCompraAdmin;
        Cancelar_Atencion.CancelarAtencionUsuario cancelacionUsuario;
        Cancelar_Atencion.CancelarAtencionProfesional cancelacionProfesional;
        Registro_Resultado.RegistroResultado registroResultado;
        Registro_Llegada.RegistrarLlegada registroLlegada;
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
            string idUs = dt2.Rows[0][0].ToString();
            idUsuario = Convert.ToInt64(idUs);

         
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
                    if (rol == "Administrativo")
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
               case "Cancelar turno":
                    if (rol == "Afiliado")
                    {
                    cancelacionUsuario = new Cancelar_Atencion.CancelarAtencionUsuario((int)idUsuario);
                    cancelacionUsuario.ShowDialog();
                    }
                    else
                    {
                    cancelacionProfesional = new Cancelar_Atencion.CancelarAtencionProfesional((int)idUsuario);
                    cancelacionProfesional.ShowDialog();
                    }
                    break;
               case "Registrar resultado consulta":
                    registroResultado = new Registro_Resultado.RegistroResultado((int)idUsuario);
                    registroResultado.ShowDialog();
                    break;
                case "Registrar Llegadas":
                    registroLlegada = new Registro_Llegada.RegistrarLlegada();
                    registroLlegada.ShowDialog();
                    break;
                case "Listados Estadisticos":
                    Listados = new Listados.Listados();
                    Listados.ShowDialog();
                    break;

            }
        }

        private void ElegirFuncionalidad_Load(object sender, EventArgs e)
        {

        }
    }
}
