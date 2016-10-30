using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Registrar_Agenda_Medico
{
    public partial class RegistrarAgenda : Form
    {
        Int64 idPro;

        public RegistrarAgenda(Int64 idProfe)
        {
            InitializeComponent();
            idPro = idProfe;

            cargarDiasEnComboBox();
            cargarHorasYMinutosEnComboBox();
            cargarEspecialidades();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAñadirRango_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBoxDias.Text) || String.IsNullOrEmpty(comboBoxHoraInicio.Text) || String.IsNullOrEmpty(comboBoxMinutosInicio.Text)
              || String.IsNullOrEmpty(comboBoxHoraFin.Text) || String.IsNullOrEmpty(comboBoxMinutosFin.Text))
            {
                MessageBox.Show("No se completaron todos los campos.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                List<String> diasCargados = new List<String>();
                foreach (ListViewItem itemRow in listViewRangos.Items)
                {
                        string dia = itemRow.Text;
                        diasCargados.Add(dia);                    
                }
                ListViewItem lista = new ListViewItem(comboBoxDias.Text);
                if(diasCargados.Contains(comboBoxDias.Text)){
                    MessageBox.Show("Ya se encuentra cargado un rango horario en ese dia.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }else{
                string horaInicio = comboBoxHoraInicio.Text + ":" + comboBoxMinutosInicio.Text;
                string horaFin = comboBoxHoraFin.Text + ":" + comboBoxMinutosFin.Text;
                int horaI = Int32.Parse(comboBoxHoraInicio.Text);
                int horaF = Int32.Parse(comboBoxHoraFin.Text);
                int minutosI = Int32.Parse(comboBoxMinutosInicio.Text);
                int minutosF = Int32.Parse(comboBoxMinutosFin.Text);
                if (horaI > horaF)
                {
                    MessageBox.Show("La hora de inicio es menor que la de fin.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (horaI == horaF && minutosI > minutosF)
                    {
                        MessageBox.Show("La hora de inicio es menor que la de fin.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (horaI == horaF && minutosI == minutosF)
                        {
                            MessageBox.Show("La hora de inicio es igual a la de fin.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            lista.SubItems.Add(horaInicio);
                            lista.SubItems.Add(horaFin);
                            lista.SubItems.Add(comboBoxEspecialidades.Text);
                            listViewRangos.Items.Add(lista);
                        }
                    }
                }                
             }
                   }
         }


        private void RegistrarAgenda_Load(object sender, EventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listViewRangos.SelectedItems) {

                lista.Remove();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {   

            List<Agenda> listaAgenda = new List<Agenda>();
            int filasAgregadasCorrectamente = 0;

            foreach (ListViewItem itemRow in listViewRangos.Items)
            {
                Agenda agenda = new Agenda();
                agenda.dia = itemRow.SubItems[0].Text;
                agenda.idProfesional = idPro;

                string query2 = "SELECT DISTINCT ID_ESPECIALIDAD FROM [3FG].ESPECIALIDADES WHERE DESCRIPCION_ESPECIALIDAD = '" + itemRow.SubItems[3].Text + "'";
                DataTable dt2 = (new ConexionSQL()).cargarTablaSQL(query2);
                string idEsp = dt2.Rows[0][0].ToString();
                Int64 idEspe = Convert.ToInt64(idEsp);
                agenda.idEspecialidad = idEspe;

                agenda.dia = itemRow.SubItems[0].Text;
                agenda.horaInicio = itemRow.SubItems[1].Text;
                agenda.horaFin = itemRow.SubItems[2].Text;

                if (dateTimePickerInicioDisp.Value > dateTimePickerFinDisp.Value || dateTimePickerInicioDisp.Value == dateTimePickerFinDisp.Value)
                {
                    MessageBox.Show("La fecha inicio de disponibilidad es menor o igual que la fecha fin disponibilidad.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (dateTimePickerInicioDisp.Value < DateTime.Today) {
                        MessageBox.Show("La fecha inicio de disponibilidad es menor que la fecha de hoy.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
               
                        AgendaDAL.cargarDisponibilidad(idPro, dateTimePickerInicioDisp.Value.Date, dateTimePickerFinDisp.Value.Date);
                        int resultado = AgendaDAL.agregarAgenda(agenda);
                        filasAgregadasCorrectamente = filasAgregadasCorrectamente + resultado;

                        if (filasAgregadasCorrectamente == listViewRangos.Items.Count)
                        {
                            MessageBox.Show("Se agrego la agenda correctamente.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo guardar la agenda.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
             }

            if (listViewRangos.Items.Count == 0)
            {
                MessageBox.Show("No se agrego ningun rango horario.", this.Text , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
         }

        private void cargarEspecialidades()
        {

            DataTable dt1 = (new ConexionSQL()).cargarTablaSQL("SELECT DESCRIPCION_ESPECIALIDAD FROM [3FG].ESPECIALIDAD_PROFESIONAL EP JOIN [3FG].ESPECIALIDADES E ON (E.ID_ESPECIALIDAD = EP.ID_ESPECIALIDAD) WHERE EP.ID_USUARIO = '" + idPro + "'");
            comboBoxEspecialidades.DataSource = dt1.DefaultView;
            comboBoxEspecialidades.ValueMember = "DESCRIPCION_ESPECIALIDAD";

        }

        private void cargarDiasEnComboBox()
        {
            comboBoxDias.Items.Clear();
            comboBoxDias.Items.Add("LUNES");
            comboBoxDias.Items.Add("MARTES");
            comboBoxDias.Items.Add("MIERCOLES");
            comboBoxDias.Items.Add("JUEVES");
            comboBoxDias.Items.Add("VIERNES");
            comboBoxDias.Items.Add("SABADO");
        }

        private void cargarHorasYMinutosEnComboBox()
        {
            comboBoxHoraInicio.Items.Clear();
            comboBoxHoraInicio.Items.Add("07");
            comboBoxHoraInicio.Items.Add("08");
            comboBoxHoraInicio.Items.Add("09");
            comboBoxHoraInicio.Items.Add("10");
            comboBoxHoraInicio.Items.Add("11");
            comboBoxHoraInicio.Items.Add("12");
            comboBoxHoraInicio.Items.Add("13");
            comboBoxHoraInicio.Items.Add("14");
            comboBoxHoraInicio.Items.Add("15");
            comboBoxHoraInicio.Items.Add("16");
            comboBoxHoraInicio.Items.Add("17");
            comboBoxHoraInicio.Items.Add("18");
            comboBoxHoraInicio.Items.Add("19");
            comboBoxHoraInicio.Items.Add("20");

            comboBoxHoraFin.Items.Clear();
            comboBoxHoraFin.Items.Add("07");
            comboBoxHoraFin.Items.Add("08");
            comboBoxHoraFin.Items.Add("09");
            comboBoxHoraFin.Items.Add("10");
            comboBoxHoraFin.Items.Add("11");
            comboBoxHoraFin.Items.Add("12");
            comboBoxHoraFin.Items.Add("13");
            comboBoxHoraFin.Items.Add("14");
            comboBoxHoraFin.Items.Add("15");
            comboBoxHoraFin.Items.Add("16");
            comboBoxHoraFin.Items.Add("17");
            comboBoxHoraFin.Items.Add("18");
            comboBoxHoraFin.Items.Add("19");
            comboBoxHoraFin.Items.Add("20");

            comboBoxMinutosInicio.Items.Clear();
            comboBoxMinutosInicio.Items.Add("00");
            comboBoxMinutosInicio.Items.Add("30");

            comboBoxMinutosFin.Items.Clear();
            comboBoxMinutosFin.Items.Add("00");
            comboBoxMinutosFin.Items.Add("30");
        }


    }
}
