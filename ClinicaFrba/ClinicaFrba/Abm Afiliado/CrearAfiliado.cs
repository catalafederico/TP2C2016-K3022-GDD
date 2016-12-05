using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaFrba.Dominio;
using ClinicaFrba.ABM_Afiliado;
namespace ClinicaFrba.ABM_Afiliado
{
    public partial class CrearAfiliado : Form
    {
        Random r = new Random();
        int flagnumerito;
        int raiz;
        int flagNuevo;
        int aleatorio;
        NewUsuario Usuario;
        List<Int64> raizAfiliado;
        public CrearAfiliado(String nombre, String Contraseña, Random unr, int flag, int unaRaiz, int otroFlag, int aleator, List<Int64> raizUsuario)
        {
            InitializeComponent();
            Usuario = new NewUsuario(nombre, Contraseña);
            DataTable dt = (new ConexionSQL()).cargarTablaSQL("select DESCRIPCION_PLAN from [3FG].PLANES");
            comboBox2.DataSource = dt.DefaultView;
            comboBox2.ValueMember = "DESCRIPCION_PLAN";
            comboBox3.Items.Add("SOLTERO");
            comboBox3.Items.Add("CASADO");
            comboBox3.Items.Add("VIUDO");
            comboBox3.Items.Add("CONCUBINATO");
            comboBox3.Items.Add("DIVORCIADO");
            comboBox1.Items.Add("D.N.I");
            r = unr;
            flagnumerito = flag;
            raiz = unaRaiz;
            flagNuevo = otroFlag;
            this.aleatorio = aleator;
            raizAfiliado = raizUsuario;
        }

        private void textBoxSexo_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!validacionesCliente())
            {
                return;
            }


            Usuario.afiNombre = textBoxNombre.Text;
            Usuario.afiApellido = textBoxApellido.Text;
            Usuario.sexo = textBoxSexo.Text;
            Usuario.afiTipoDocumento = comboBox1.Text;
            Usuario.afiNumeroDocumento = Convert.ToInt64(textBoxNumeroDoc.Text);
            Usuario.afiPlan = comboBox2.Text;
            Usuario.afiFechaNac = textBoxFechNacim.Text;
            Usuario.estadoCivil = comboBox3.Text;
            Usuario.cantidadFamiliares = Convert.ToInt64(textBoxFamiliares.Text);

            Usuario.mail = textBoxMail.Text;
            Usuario.telefono = Convert.ToInt64(textBoxTelefono.Text);
            Usuario.DireccionCompleta = textBoxDirCompleta.Text;


            /*INSERT INTO [3FG].USUARIOS(USUARIO_NOMBRE,CONTRASEÑA,NOMBRE,APELLIDO,TIPO_DE_DOCUMENTO,NUMERO_DOCUMENTO,TELEFONO,DIRECCION,MAIL,FECHA_NACIMIENTO,SEXO)
VALUES ('pepita',(SELECT SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA2_256','lora')),3,250) ),'luciana','ortman','D.N.I',123123123,46465237,'rivadavia 2345','luciana@gamil.com',(SELECT convert(datetime, '23/10/2016', 103)),'M')
*/
            string crearAfiliado = "INSERT INTO [3FG].USUARIOS(USUARIO_NOMBRE,CONTRASEÑA,NOMBRE,APELLIDO,TIPO_DE_DOCUMENTO,NUMERO_DOCUMENTO,TELEFONO,DIRECCION,MAIL,FECHA_NACIMIENTO,SEXO)VALUES('" + Usuario.username + "', (SELECT SUBSTRING(master.dbo.fn_varbintohexstr(HASHBYTES('SHA2_256','" + Usuario.password + "')),3,250) ),'" + Usuario.afiNombre + "','" + Usuario.afiApellido + "','" + Usuario.afiTipoDocumento + "'," + Usuario.afiNumeroDocumento + "," + Usuario.telefono + ",'" + Usuario.DireccionCompleta + "','" + Usuario.mail + "',(SELECT convert(datetime, '" + Usuario.afiFechaNac + "', 103)),'" + Usuario.sexo + "')";
            (new ConexionSQL()).ejecutarComandoSQL(crearAfiliado);

            /*falta agregar esto*/



            if (flagNuevo == 0)
            {
                aleatorio = r.Next(99999);
                int flag = 0;
                while (flag == 0)
                {
                    if (raizAfiliado.Contains(aleatorio))
                    {
                        aleatorio = r.Next(999999999);
                    }
                    else
                    {
                        raizAfiliado.Add(aleatorio);
                        flag = 1;

                    }
                }

            }


            if (flagnumerito == 0)
            {
                raiz = 00;
                raiz++;
            }
            else
            {
                raiz++;
            }




            string crearAfiliado1 = "insert into [3FG].AFILIADOS(ID_USUARIO,ID_PLAN,ESTADO_CIVIL,CANT_FAMILIARES,RAIZ_AFILIADO,NUMERO_FAMILIA) values((select ID_USUARIO from [3FG].USUARIOS where USUARIO_NOMBRE='" + Usuario.username + "'),(select ID_PLAN from [3FG].PLANES where DESCRIPCION_PLAN ='" + Usuario.afiPlan + "'),'" + Usuario.estadoCivil + "','" + Usuario.cantidadFamiliares + "','" + aleatorio + "','" + raiz + "')";
            (new ConexionSQL()).ejecutarComandoSQL(crearAfiliado1);


            /* insert into [3FG].ROLES_USUARIO(ID_USUARIO,ID_ROL)values((select a.ID_USUARIO from [3FG].AFILIADOS a join[3FG].USUARIOS u on (a.ID_USUARIO=u.ID_USUARIO) and u.NUMERO_DOCUMENTO=1233456),2)*/
            string AsignarRol = "insert into [3FG].ROLES_USUARIO(ID_USUARIO,ID_ROL)values((select a.ID_USUARIO from [3FG].AFILIADOS a join[3FG].USUARIOS u on (a.ID_USUARIO=u.ID_USUARIO) and u.NUMERO_DOCUMENTO= '" + Usuario.afiNumeroDocumento + "'),2)";
            (new ConexionSQL()).ejecutarComandoSQL(AsignarRol);


            /* insert into [3FG].AFILIADOS(ID_USUARIO,ID_PLAN,ESTADO_CIVIL,CANT_FAMILIARES,RAIZ_AFILIADO,NUMERO_FAMILIA)
 values((select ID_USUARIO from [3FG].USUARIOS where USUARIO_NOMBRE= 'pepita'),(select ID_PLAN from [3FG].PLANES where DESCRIPCION_PLAN ='plan medico corriente'),'casada',5,(select max(RAIZ_AFILIADO) from [3FG].AFILIADOS),01)
*/
            if (Convert.ToInt32(textBoxFamiliares.Text) > 0)
            {
                if ((MessageBox.Show("¿Desea añadir a un familiar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {

                    MessageBox.Show("Afiliado anterior Agregado con exito, agregue al nuevo afiliado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
                    ABM_Afiliado.CrearUsuario crearUsuario = new CrearUsuario(aleatorio, raiz, 1, 1, aleatorio, raizAfiliado);
                    crearUsuario.ShowDialog();

                    this.Hide();
                    /*modificarRol(rolPasado);
                    MessageBox.Show("Rol " + rolPasado + " modificado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                    form.Close();*/
                }
                else
                {
                    MessageBox.Show("Afiliado Agreagado Con Exito", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Hide();
                }


            }
            else
            {
                MessageBox.Show("Afiliado Agreagado Con Exito", this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Hide();
            }





        }
        private bool validacionesCliente()
        {
            if (textBoxSexo.Text == "" || textBoxNombre.Text == "" || textBoxApellido.Text == "" || comboBox1.Text == "" || textBoxNumeroDoc.Text == "" || textBoxMail.Text == "" || textBoxDirCompleta.Text == "" || textBoxTelefono.Text == "" || comboBox2.Text == "" || textBoxFechNacim.Text == "" || comboBox3.Text == "" || textBoxFamiliares.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos obligatorios", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }



            if (textBoxNombre.TextLength > 100)
            {
                MessageBox.Show("El nombre no debe superar los 100 caractéres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (textBoxSexo.TextLength > 20)
            {
                MessageBox.Show("El nombre no debe superar los 20 caractéres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!textBoxNombre.Text.All(Char.IsLetter))
            {
                MessageBox.Show("Sólo se admiten letras en el nombre", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!textBoxApellido.Text.All(Char.IsLetter))
            {
                MessageBox.Show("Sólo se admiten letras en el apellido", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }



            if (textBoxApellido.TextLength > 50)
            {
                MessageBox.Show("El apellido no debe superar los 50 caractéres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBoxNumeroDoc.TextLength > 50)
            {
                MessageBox.Show("El documento no debe superar los 50 caractéres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBoxMail.TextLength > 100)
            {
                MessageBox.Show("El mail no debe superar los 100 caractéres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBoxTelefono.TextLength > 100)
            {
                MessageBox.Show("El telefono no debe superar los 100 caractéres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int numDoc;

            try
            {
                numDoc = Convert.ToInt32(textBoxNumeroDoc.Text);
            }
            catch
            {
                MessageBox.Show("El número de documento debe ser un entero menor a 2147483647", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numDoc < 1)
            {
                MessageBox.Show("El número de documento debe ser mayor o igual a 1", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!(textBoxFechNacim.Text.Contains("/")))
            {
                MessageBox.Show("formato invalido", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBoxMail.TextLength > 50)
            {
                MessageBox.Show("El mail no debe superar los 50 caractéres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!(textBoxMail.Text.Contains("@")))
            {
                MessageBox.Show("El mail debe contener el carácter @", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Int64 numTel;
            try
            {
                numTel = Convert.ToInt64(textBoxTelefono.Text);
            }
            catch
            {
                MessageBox.Show("El número de téléfono debe ser un entero menor a 9223372036854775807", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numTel < 1)
            {
                MessageBox.Show("El número de teléfono debe ser mayor o igual a 1", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBoxDirCompleta.TextLength > 100)
            {
                MessageBox.Show("La calle no debe superar los 100 caractéres", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }







            DateTime diaDeHoy = DateTime.Today;

            if (diaDeHoy <= DateTime.Parse(textBoxFechNacim.Text))
            {
                MessageBox.Show("La fecha de nacimiento tiene que ser anterior al día de hoy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string query2 = "SELECT COUNT(*) FROM [3FG].USUARIOS WHERE TIPO_DE_DOCUMENTO = '" + comboBox1.Text + "'  AND NUMERO_DOCUMENTO = '" + textBoxNumeroDoc.Text + "'";
            DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
            string cantidad = dt2.Rows[0][0].ToString();
            if (cantidad != "0")
            {
                MessageBox.Show("Ya existe ese tipo y número de documento", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;



        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CrearAfiliado_Load(object sender, EventArgs e)
        {

        }
    }
}
