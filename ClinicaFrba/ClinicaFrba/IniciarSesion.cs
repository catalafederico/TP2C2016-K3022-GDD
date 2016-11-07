using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;


using ClinicaFrba.dominio;
using ClinicaFrba.Eleccion_Funcionalidad;

namespace ClinicaFrba
{
    public partial class IniciarSesion : Form
    {
        private string username, password;
        Elegir_Rol.EleccionRol eleccion;
        ElegirFuncionalidad funcionalidades;
        public IniciarSesion()
        {
            InitializeComponent();

            DateTime fecha = DateTime.Parse(Program.nuevaFechaSistema());
            labelDia.Text = fecha.Day.ToString() + "/" + fecha.Month + "/" + fecha.Year;

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
         
            username = txtUsuario.Text;
            password = txtContraseña.Text;

            if (username == null || password == null || password == "" || username == "")
            {
                MessageBox.Show("Debe ingresar su nombre de usuario y contraseña", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "SELECT CONTRASEÑA FROM [3FG].USUARIOS WHERE USUARIO_NOMBRE = '" + username + "'";
                DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Nombre de usuario incorrecto", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    String passwordSistema = dt.Rows[0][0].ToString();
                    if (getSha256(password) == passwordSistema)
                    {
                        string resetearIntentos = "UPDATE [3FG].USUARIOS SET CANT_INTENTOS = 0 WHERE USUARIO_NOMBRE = '" + username + "'";
                        (new ConexionSQL()).ejecutarComandoSQL(resetearIntentos);
                        string query5 = "SELECT HABILITADO FROM [3FG].USUARIOS WHERE USUARIO_NOMBRE = '" + username + "'";
                        DataTable dt5 = (new ConexionSQL()).cargarTablaSQL(query5);
                        string habilitado = dt5.Rows[0][0].ToString();
                        if (habilitado == "1")
                        {
                            string query2 = "SELECT COUNT(*) FROM [3FG].ROLES_USUARIO ROLES JOIN [3FG].USUARIOS USUARIOS ON (USUARIOS.ID_USUARIO = ROLES.ID_USUARIO) WHERE USUARIOS.USUARIO_NOMBRE = '" + username + "' AND USUARIOS.HABILITADO = 1";
                            DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
                            string cantidadRoles = dt2.Rows[0][0].ToString();
                            if (cantidadRoles == "1")
                            {
                                 
                                DataTable dt3 = (new ConexionSQL()).cargarTablaSQL("SELECT R.NOMBRE_ROL FROM [3FG].ROLES_USUARIO RU JOIN [3FG].USUARIOS U ON (U.ID_USUARIO = RU.ID_USUARIO) JOIN [3FG].ROLES R ON (R.ID_ROL = RU.ID_ROL) WHERE U.USUARIO_NOMBRE ='" + username + "'AND R.HABILITADO = 1 AND U.HABILITADO = 1");
                                string rol = dt3.Rows[0][0].ToString();
                                funcionalidades = new ElegirFuncionalidad(rol, username);
                                funcionalidades.ShowDialog();
                            }
                            else if (cantidadRoles == "0")
                            {
                                MessageBox.Show("El usuario no tiene roles habilitados", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                eleccion = new Elegir_Rol.EleccionRol(username);
                                eleccion.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("El usuario no esta habilitado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        string query4 = "SELECT CANT_INTENTOS FROM [3FG].USUARIOS WHERE USUARIO_NOMBRE = '" + username + "'";
                        DataTable dt4 = (new ConexionSQL()).cargarTablaSQL(query4);
                        string cantidadIntentos = dt4.Rows[0][0].ToString();
                        string texto = "";
                        if (cantidadIntentos == "0")
                        {
                            cantidadIntentos = "1";
                            texto = "";
                        }
                        else if (cantidadIntentos == "1")
                        {
                            cantidadIntentos = "2";
                            texto = "";
                        }
                        else if (cantidadIntentos == "2")
                        {
                            cantidadIntentos = "3";
                            texto = ": Usuario inhabilitado";
                            string inhabilitarUsuario = "UPDATE [3FG].USUARIOS SET HABILITADO = 0 WHERE USUARIO_NOMBRE = '" + username + "'";
                            (new ConexionSQL()).ejecutarComandoSQL(inhabilitarUsuario);

                        }
                        string sumarIntento = "UPDATE [3FG].USUARIOS SET CANT_INTENTOS = '" + cantidadIntentos + "' WHERE USUARIO_NOMBRE = '" + username + "'";
                        (new ConexionSQL()).ejecutarComandoSQL(sumarIntento);
                        MessageBox.Show("Contraseña incorrecta" + texto, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void IniciarSesion_Load(object sender, EventArgs e)
        {
        
         }

        public String getSha256(String input)
        {
            SHA256Managed encriptador = new SHA256Managed();
            byte[] inputEnBytes = Encoding.UTF8.GetBytes(input);
            byte[] inputHashBytes = encriptador.ComputeHash(inputEnBytes);
            return BitConverter.ToString(inputHashBytes).Replace("-", String.Empty).ToLower();
        }

    }
}
