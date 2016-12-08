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

namespace ClinicaFrba.Pedir_Turno
{
    public partial class Elegir_Horario : Form
    {
        private int idAgenda;
        private int idDoctor;
        private int idEspecialidad;
        private DateTime fechaTemprana;
        private DateTime fechaTardia;
        private DateTime fechaElegida = new DateTime(1000,01,01);
        private int idAfiliado;
        private String diaDeLaSemana;
        private DateTime fechaActual;
        private bool feriado;

        // Funcion de creacion e inicializacion
        public Elegir_Horario(string doctorElegido, int idDoc, int idAfi, int idEsp)
        {
            InitializeComponent();

            // Setteo los id que ya tengo y no van a cambiar
            this.idAfiliado = idAfi;
            this.idEspecialidad = idEsp;

            // Modifico el titulo de la ventana
            this.Text = "Elegir fecha para consulta con Dr." + doctorElegido.ToString();

            // Busco fecha de inicio de la disponibilidad actual del profesional
            SqlCommand dateEarly = new SqlCommand("SELECT P.INICIO_DISPONIBILIDAD FROM [3FG].PROFESIONALES P WHERE P.ID_USUARIO LIKE '" + idDoc + "'", new ConexionSQL().conectar());
            this.fechaTemprana = (DateTime)dateEarly.ExecuteScalar();

            // Busco fecha de fin de la disponibilidad actual del profesional
            SqlCommand dateLate = new SqlCommand("SELECT P.FIN_DISPONIBILIDAD FROM [3FG].PROFESIONALES P WHERE P.ID_USUARIO LIKE '" + idDoc + "'", new ConexionSQL().conectar());
            this.fechaTardia = (DateTime)dateLate.ExecuteScalar();

            // Tomo la fecha actual del sistema
            this.fechaActual = DateTime.Parse(Program.nuevaFechaSistema());

            // Modifico las labels para que el usuario sepa desde donde hasta donde puede elegir fecha
            label3.Text = "Primera fecha disponible: " + primeraFecha().ToString("yyyy-MM-dd");
            label4.Text = "Ultima fecha disponible: " + fechaTardia.ToString("yyyy-MM-dd");
            this.idDoctor = idDoc;
            
            // Indico mediante un label los dias en los que atiende el profesional elegido
            diasDisponibles();

            // Setteo el DateTimePicker con la fecha actual del sistema
            dateTimePicker1.Value = fechaActual;
        }

        // Funcion de creacion de turno
        private void button1_Click(object sender, EventArgs e)
        {
            // Me fijo que el usuario haya elegido fecha
            if (this.fechaElegida.Year != 1000)
            {
                // Abro la conexion
                using (SqlConnection conexion = new ConexionSQL().conectar())
                {
                    // Setteo la query de crear turnos
                    string crearTurno = "INSERT INTO [3FG].TURNOS(ID_AFILIADO,ID_AGENDA,FECHA_TURNO) VALUES(@idAfiliado,@idAgenda,@fechaTurno)";

                    using (SqlCommand queryCrearTurno = new SqlCommand(crearTurno))
                    {
                        // Agrego los valores dinamicamente para evitar SQL Injection
                        queryCrearTurno.Connection = new ConexionSQL().conectar();
                        queryCrearTurno.Parameters.Add("@idAfiliado", SqlDbType.BigInt, 8).Value = idAfiliado;
                        queryCrearTurno.Parameters.Add("@idAgenda", SqlDbType.BigInt, 8).Value = idAgenda;
                        queryCrearTurno.Parameters.Add("@fechaTurno", SqlDbType.DateTime, 8).Value = fechaElegida;

                        // Pruebo de ejecutarlo
                        try
                        {
                            queryCrearTurno.ExecuteNonQuery();
                            MessageBox.Show("Turno creado correctamente", "OK", MessageBoxButtons.OK);
                            this.Close();
                        }

                        // Doy un mensaje por si falla
                        catch
                        {
                            MessageBox.Show("Error al tratar de guardar en database", "Error", MessageBoxButtons.OK);
                        }
                        conexion.Close();
                    }
                }
            }
            else MessageBox.Show("No ha seleccionado un turno", "Error", MessageBoxButtons.OK);
        }


        // Funcion de eleccion de fecha
        private void button2_Click(object sender, EventArgs e)
        {
            // Verifico si la fecha no esta entre las disponibles
            if (dateTimePicker1.Value.Date < primeraFecha().Date || dateTimePicker1.Value.Date > this.fechaTardia.Date)
            {
                MessageBox.Show("La fecha seleccionada no se encuentra dentro de las disponibles", "Error", MessageBoxButtons.OK);
            }
            else
            {
                if (dateTimePicker1.Value.DayOfWeek != DayOfWeek.Sunday)
                {
                    // Me fijo si el profesional elegido atiende ese dia para esa especialidad
                    if (hayAgenda(switchDias(dateTimePicker1.Value)))
                    {
                        // Si lo hace, busco si hay turnos disponibles para ese dia
                        if (busquedaDeTurnos(dateTimePicker1.Value))
                        {
                            // Aviso al usuario que se ha cambiado el ComboBox con los turnos disponibles ese dia
                            MessageBox.Show("Se ha cargado la lista de turnos disponibles", "Atencion", MessageBoxButtons.OK);
                        }
                        else
                        {
                            // Por las dudas y para mantener consistencia, vacio mi ComboBox. Esto evita que si el usuario
                            // elige una fecha con turnos disponibles y despues otra en la que no los hay, este no pueda elegir
                            // turnos validos de la fecha anterior
                            comboBox1.DataSource = null;
                            MessageBox.Show("No quedan mas turnos disponibles para ese día", "Error", MessageBoxButtons.OK);
                        }
                    }
                    else MessageBox.Show("El profesional elegido no atiende ese día", "Error", MessageBoxButtons.OK);
                }
                else MessageBox.Show("La clinica no está abierta los domingos", "Error", MessageBoxButtons.OK);
            }
        }

        // Se fija cual fecha usar como limite, la actual del sistema o la de inicio de disponibilidad debido a que
        // el turno elegido debe estar dentro de la disponibilidad y ser posterior a la actual
        private DateTime primeraFecha()
        {
            if (fechaTemprana > fechaActual) { return fechaTemprana; }
            else return fechaActual;
        }

        // Busco para que dias tiene agenda el profesional
        private void diasDisponibles() {
            label1.Text = "Los días disponibles del profesional son: [";
            if (hayAgenda("LUNES")) { label1.Text += "Lunes"; };
            if (hayAgenda("MARTES")) { label1.Text += "|Martes"; };
            if (hayAgenda("MIERCOLES")) { label1.Text += "|Miercoles"; };
            if (hayAgenda("JUEVES")) { label1.Text += "|Jueves"; };
            if (hayAgenda("VIERNES")) { label1.Text += "|Viernes"; };
            if (hayAgenda("SABADO")) { label1.Text += "|Sabado"; };
            label1.Text += "]";
        }


        // Me fijo que dia de la semana tiene la fecha que elegi y lo devuelvo
        private string switchDias(DateTime fecha)
        {
            // Este setteo no signfica nada, solo esta porque sino me tira un error de que dia no esta setteado en el return
            string dia="DOMINGO";
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dia = "DOMINGO";
                    break;
                case DayOfWeek.Monday:
                    dia = "LUNES";
                    break;
                case DayOfWeek.Tuesday:
                    dia = "MARTES";
                    break;
                case DayOfWeek.Wednesday:
                    dia = "MIERCOLES";
                    break;
                case DayOfWeek.Thursday:
                    dia = "JUEVES";
                    break;
                case DayOfWeek.Friday:
                    dia = "VIERNES";
                    break;
                case DayOfWeek.Saturday:
                    dia = "SABADO";
                    break;
            }
            this.diaDeLaSemana = dia;
            return dia;
        }

        // Funcion de checkeo de agenda
        private bool hayAgenda(string fecha)
        {
            // Me fijo si el profesional atiende ese dia para la especialidad que elegi
            SqlCommand queryHayAgenda = new SqlCommand("SELECT COUNT(*) FROM [3FG].PROFESIONALES P, [3FG].AGENDA A WHERE (P.ID_USUARIO LIKE '" + idDoctor + @"') 
                                                        AND (P.ID_USUARIO = A.ID_USUARIO) AND (A.DIA_ATENCION = '" + fecha + @"')
                                                        AND (A.ID_ESPECIALIDAD = " + this.idEspecialidad.ToString() + ")", new ConexionSQL().conectar());
            if ((int)queryHayAgenda.ExecuteScalar() > 0)
            {
                return true;
            }
            else return false;
        }


        // Funcion de obtencion de agenda
        private int buscarAgenda()
        {   
            // Busco y devuelvo el id de las agendas del profesional para la especialidad elegida
            SqlCommand queryBuscarAgenda = new SqlCommand("SELECT A.ID_AGENDA FROM [3FG].PROFESIONALES P, [3FG].AGENDA A WHERE (P.ID_USUARIO LIKE '" + idDoctor + @"') 
                                                           AND (P.ID_USUARIO = A.ID_USUARIO) AND (A.DIA_ATENCION = '" + this.diaDeLaSemana + @"')
                                                           AND (A.ID_ESPECIALIDAD = " + this.idEspecialidad.ToString() + ")", new ConexionSQL().conectar());
            return int.Parse(queryBuscarAgenda.ExecuteScalar().ToString());
        }



       // Funcion de busqueda de turnos
       private bool busquedaDeTurnos(DateTime fecha)
       {
           // Busco la agenda del dia marcado
           int idAgendaTemporaria = buscarAgenda();

           // Busco el inicio de la disponibilidad para esa agenda
           SqlCommand queryBuscarMinimo = new SqlCommand("SELECT INICIO_ATENCION FROM [3FG].AGENDA WHERE ID_AGENDA = " + idAgendaTemporaria.ToString(), new ConexionSQL().conectar());
           TimeSpan horarioMinimo = TimeSpan.Parse(queryBuscarMinimo.ExecuteScalar().ToString());

           // Busco el fin de la disponibilidad para esa agenda
           SqlCommand queryBuscarMaximo = new SqlCommand("SELECT FIN_ATENCION FROM [3FG].AGENDA WHERE ID_AGENDA = " + idAgendaTemporaria.ToString(), new ConexionSQL().conectar());
           TimeSpan horarioMaximo = TimeSpan.Parse(queryBuscarMaximo.ExecuteScalar().ToString());

           // Setteo el turno actual (empiezo con el primero posible del dia) y el turno final del dia
           DateTime ultimoTurno = fecha.Date + horarioMaximo;
           DateTime actualTurno = fecha.Date + horarioMinimo;

           // Voy a retornar este bool para informar que ocurrio con la operacion
           bool encontroTurno = false;

           // Itero el siguiente comportamiento hasta que llegue al turno final del dia o elija uno el usuario
           while (actualTurno < ultimoTurno && !encontroTurno)
           {

               /* Esta hermosa query del infierno busca la cantidad de turnos en todas las agendas
                * del profesional que voy a estar pisando con el nuevo turno que quiero crear en el
                * caso de que lo cree con el DateTime actualTurno*/
               SqlCommand disponibilidad = new SqlCommand(@"SELECT COUNT(*) FROM [3FG].TURNOS T,
                                                        (SELECT A.ID_AGENDA FROM [3FG].AGENDA A,
                                                        [3FG].PROFESIONALES P WHERE A.ID_USUARIO = P.ID_USUARIO
                                                        AND P.ID_USUARIO = " + this.idDoctor.ToString() + @") AP
                                                        WHERE T.ID_AGENDA = AP.ID_AGENDA AND
                                                        T.FECHA_TURNO = '" + actualTurno.ToString("yyyy-dd-MM HH:mm:ss") + @"'
                                                        AND (T.ID_TURNO NOT IN (SELECT C.ID_TURNO FROM [3FG].CANCELACIONES C))", new ConexionSQL().conectar());

               // Salvo ese valor en una variable
               int turnoPisados = (int)disponibilidad.ExecuteScalar();

               /* Y me fijo si es mayor a 0, en este caso ya existen turnos en este DateTime y paso al siguiente turno
                * posible que esta 30 minutos mas adelante*/
               if (turnoPisados > 0) { actualTurno = actualTurno.AddMinutes(30); }

               // Si no es mayor a 0, empiezo a llenar un diccionario con los horarios disponibles de ese dia
               else
               {
                   this.idAgenda = idAgendaTemporaria;
                   Dictionary<string, DateTime> turnos = new Dictionary<string, DateTime>();
                   while (actualTurno < ultimoTurno)
                   {
                       // Me sigo fijando si los turnos estan disponbiles o no
                       if (turnoPisados == 0)
                       {
                           turnos.Add(actualTurno.ToString("HH:mm"), actualTurno);
                       }
                       actualTurno = actualTurno.AddMinutes(30);

                       // No queria repetir la query aca pero tratando de settear la query como un string hacia que el valor
                       // de actualTurno no cambiara en la query aun cuando cambiaba afuera asi que no tuve otra opcion que copiarla aca
                       disponibilidad = new SqlCommand(@"SELECT COUNT(*) FROM [3FG].TURNOS T,
                                                       (SELECT A.ID_AGENDA FROM [3FG].AGENDA A,
                                                       [3FG].PROFESIONALES P WHERE A.ID_USUARIO = P.ID_USUARIO
                                                       AND P.ID_USUARIO = " + this.idDoctor.ToString() + @") AP
                                                       WHERE T.ID_AGENDA = AP.ID_AGENDA AND
                                                       T.FECHA_TURNO = '" + actualTurno.ToString("yyyy-dd-MM HH:mm:ss") + @"'
                                                       AND (T.ID_TURNO NOT IN (SELECT C.ID_TURNO FROM [3FG].CANCELACIONES C))", new ConexionSQL().conectar());
                       turnoPisados = (int)disponibilidad.ExecuteScalar();
                  }

                  // Asigno mi nuevo diccionario como BindingSource de comboBox1 para que tome esos valores
                  comboBox1.DataSource = new BindingSource(turnos, null);
                  comboBox1.DisplayMember = "Key";
                  comboBox1.ValueMember = "Value";

                  // Cambio el valor para salir del loop
                  encontroTurno = true;
              }
          }
           return encontroTurno;
       }

       // Setteo el turno elegido y muestro con la label que turno eligio
       private void button3_Click(object sender, EventArgs e)
       {
           if (comboBox1.SelectedValue != null)
           {
               this.fechaElegida = (DateTime)comboBox1.SelectedValue;
               label2.Text = "Turno elegido: " + fechaElegida.ToString("yyyy-dd-MM HH:mm");
           }
           else MessageBox.Show("No ha elegido una fecha ni horario valido para un turno", "Error", MessageBoxButtons.OK);
       }

       private void Elegir_Horario_Load(object sender, EventArgs e)
       {

       }
    
    }
}
