using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaFrba.Registrar_Agenda_Medico
{
    public class Agenda
    {
        public Int64 idProfesional;

        public Int64 idEspecialidad;

        public String dia;

        public String horaInicio;

        public String horaFin;

        public Agenda() { }

        public Agenda(Int64 pIdProfesional, Int64 pIdEspecialidad, String pDia, String pHoraInicio, String pHoraFin) {

            this.idProfesional = pIdProfesional;
            this.idEspecialidad = pIdEspecialidad;
            this.dia = pDia;
            this.horaInicio = pHoraInicio;
            this.horaFin = pHoraFin;
        
        }




    }
}
