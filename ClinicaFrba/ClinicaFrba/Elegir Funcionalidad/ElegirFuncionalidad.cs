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
        Registrar_Agenda_Medico.RegistrarAgenda agenda;
        /*
        ABM_Rol.ABMRol abmRol;
        ABM_Visibilidad.ABMVisibilidad abmVis;
        ABM_Usuario.ABMUsuario abmUsuario;
        ABM_Usuario.Modificar_Usuario mUsuario;
        */
        Int64 idCli;
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
            idCli = Convert.ToInt64(idCliente);

            //Verifica si puede seguir comprando o no
            /* ver despues por el tema de las compras*/
            if (rolPasado == "Cliente")
            {
                string query5 = "SELECT (SELECT COUNT(*) FROM GDD_15.OFERTAS WHERE N_ID_CLIENTE = '" + idCli + "' AND C_GANADOR = 'SI') + (SELECT COUNT(*) FROM GDD_15.COMPRAS WHERE N_ID_CLIENTE = '" + idCli + "') - (SELECT COUNT(*) FROM GDD_15.CALIFICACIONES WHERE N_ID_CLIENTE = '" + idCli + "')";
                DataTable dt5 = (new ConexionSQL()).cargarTablaSQL(query5);
                string comprasSinCalif = dt5.Rows[0][0].ToString();
                Int32 cantComprasSinCalif = Convert.ToInt32(comprasSinCalif);

                string query3 = "SELECT N_COMPRA_HABILITADA FROM GDD_15.CLIENTES WHERE N_ID_USUARIO = '" + idCli + "'";
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
                        string query7 = "UPDATE GDD_15.CLIENTES SET N_COMPRA_HABILITADA = '0' WHERE N_ID_USUARIO = '" + idCli + "'";
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
                    unTurno = new ABMTurnos();
                    unTurno.ShowDialog();
                    break;
                case "Registrar agenda del profesional":
                    agenda = new Registrar_Agenda_Medico.RegistrarAgenda(nombreUsuario,idCli);
                    agenda.ShowDialog();
                    break;
                    /*
                case "ABM de Usuarios":
                    if (rol == "Administrativo")
                    {
                        abmUsuario = new ABM_Usuario.ABMUsuario();
                        abmUsuario.ShowDialog();
                    }
                    else if (rol == "Empresa" || rol == "Cliente")
                    {
                        mUsuario = new ABM_Usuario.Modificar_Usuario(rol, nombreUsuario);
                        mUsuario.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se puede modificar un usuario del rol: " + rol);
                    }
                    break;
                case "ABM de Rubro":
                    MessageBox.Show("Funcionalidad no requerida");
                    break;
                case "ABM de visibilidad de publicación":
                    abmVis = new ABM_Visibilidad.ABMVisibilidad();
                    abmVis.ShowDialog();
                    break;
                case "Generar Publicación":
                    Generar_Publicación.ElegirAccion elegirAccion = new Generar_Publicación.ElegirAccion(nombreUsuario);
                    elegirAccion.ShowDialog();
                    break;
                case "Comprar/Ofertar":
                    string query6 = "SELECT N_COMPRA_HABILITADA FROM GDD_15.CLIENTES WHERE N_ID_USUARIO = '" + idCli + "'";
                    DataTable dt6 = (new ConexionSQL()).cargarTablaSQL(query6);
                    string compraHabilitada = dt6.Rows[0][0].ToString();
                    if (compraHabilitada == "1")
                    {
                        string query5 = "SELECT (SELECT COUNT(*) FROM GDD_15.OFERTAS WHERE N_ID_CLIENTE = '" + idCli + "' AND C_GANADOR = 'SI') + (SELECT COUNT(*) FROM GDD_15.COMPRAS WHERE N_ID_CLIENTE = '" + idCli + "') - (SELECT COUNT(*) FROM GDD_15.CALIFICACIONES WHERE N_ID_CLIENTE = '" + idCli + "')";
                        DataTable dt5 = (new ConexionSQL()).cargarTablaSQL(query5);
                        string comprasSinCalif = dt5.Rows[0][0].ToString();
                        Int32 cantComprasSinCalif = Convert.ToInt32(comprasSinCalif);
                        if (cantComprasSinCalif < 4)
                        {
                            ComprarOfertar.ElegirTipo elegirTipo = new ComprarOfertar.ElegirTipo(nombreUsuario);
                            elegirTipo.ShowDialog();
                        }
                        else
                        {
                            string query7 = "UPDATE GDD_15.CLIENTES SET N_COMPRA_HABILITADA = '0' WHERE N_ID_USUARIO = '" + idCli + "'";
                            DataTable dt7 = (new ConexionSQL()).cargarTablaSQL(query7);
                            MessageBox.Show("Como tiene más de 3 publicaciones (" + cantComprasSinCalif + ") sin calificar no puede realizar compras u ofertas hasta que califique todas sus publicaciones", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe calificar todas sus publicaciones para realizar una compra u oferta", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "Historial":
                    string query3 = "SELECT (SELECT COUNT(*) CUENTA FROM GDD_15.CLIENTES CL JOIN GDD_15.COMPRAS CO ON (CL.N_ID_USUARIO = CO.N_ID_CLIENTE) WHERE CL.N_ID_USUARIO = '" + idCli + "') + (SELECT COUNT(*) CUENTA FROM GDD_15.CLIENTES CL JOIN GDD_15.OFERTAS O ON (CL.N_ID_USUARIO = O.N_ID_CLIENTE) WHERE CL.N_ID_USUARIO = '" + idCli + "')";
                    DataTable dt3 = (new ConexionSQL()).cargarTablaSQL(query3);
                    string cantidadOperaciones = dt3.Rows[0][0].ToString();
                    if (cantidadOperaciones != "0")
                    {
                        Historial_Cliente.Historial historial = new Historial_Cliente.Historial(nombreUsuario);
                        historial.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No hay operaciones en el historial", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "Calificar al Vendedor":
                    Calificar.ElegirRealizar realizar = new Calificar.ElegirRealizar(idCli);
                    realizar.ShowDialog();
                    break;
                case "Consulta de facturas":
                    string query4 = "SELECT COUNT([Código Factura]) FROM (SELECT F.N_ID_FACTURA 'Código Factura', N_ID_ITEM 'Código Item', CASE WHEN FI.N_ID_OFERTA IS NULL AND FI.N_ID_COMPRA IS NULL AND FI.C_VISIBILIDAD IS NOT NULL THEN 'Comisión por tipo de visibilidad' WHEN FI.N_ID_OFERTA IS NOT NULL OR FI.N_ID_COMPRA IS NOT NULL AND FI.C_VISIBILIDAD IS NULL THEN 'Comisión por venta'  WHEN FI.N_ID_OFERTA IS NOT NULL OR FI.N_ID_COMPRA IS NOT NULL AND FI.C_VISIBILIDAD IS NOT NULL THEN 'Comisión por envío' END AS 'Detalle', N_MONTO 'Monto Item ($)', F.F_ALTA 'Fecha Alta' FROM GDD_15.PUBLICACIONES P JOIN GDD_15.FACTURAS F ON (P.N_ID_PUBLICACION = F.N_ID_PUBLICACION) JOIN GDD_15.FACTURAS_ITEMS FI ON (F.N_ID_FACTURA = FI.N_ID_FACTURA) WHERE N_ID_USUARIO = '" + idCli + "') SQ ";
                    DataTable dt4 = (new ConexionSQL()).cargarTablaSQL(query4);
                    string cantidadFacturas = dt4.Rows[0][0].ToString();
                    if (cantidadFacturas != "0")
                    {
                        Facturas.Facturas facturas = new Facturas.Facturas(idCli);
                        facturas.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No hay facturas para mostrar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "Listado Estadístico":

                    Listado_Estadistico.Seleccionar selec = new Listado_Estadistico.Seleccionar();
                    selec.ShowDialog();
                    break;
                default:
                    MessageBox.Show("Elija una funcionalidad de las indicadas", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;*/
            }
        }

        private void ElegirFuncionalidad_Load(object sender, EventArgs e)
        {

        }
    }
}
