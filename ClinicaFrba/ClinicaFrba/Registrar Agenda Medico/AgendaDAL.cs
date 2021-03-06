﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ClinicaFrba.Registrar_Agenda_Medico
{
    public class AgendaDAL
    {
        public static int agregarAgenda(Agenda pAgenda) {

            int retorno = 0;
            using (SqlConnection conexion = new ConexionSQL().conectar()) {
                SqlCommand comando = new SqlCommand(string.Format("INSERT INTO [3FG].AGENDA(ID_USUARIO,ID_ESPECIALIDAD,DIA_ATENCION,INICIO_ATENCION,FIN_ATENCION) VALUES ('{0}','{1}','{2}','{3}','{4}') ",
                    pAgenda.idProfesional, pAgenda.idEspecialidad, pAgenda.dia, pAgenda.horaInicio, pAgenda.horaFin), conexion);

                retorno = comando.ExecuteNonQuery();
            
            }

            return retorno;
        }

        public static int cargarDisponibilidad(Int64 idProfesional,DateTime inicioDisponibilidad,DateTime finDisponibilidad) {
            
            int retorno = 0;
            using (SqlConnection conexion = new ConexionSQL().conectar())
            {
                SqlCommand comando = new SqlCommand(string.Format("UPDATE [3FG].PROFESIONALES SET INICIO_DISPONIBILIDAD = '{0}', FIN_DISPONIBILIDAD = '{1}' WHERE ID_USUARIO = '{2}'",
                    inicioDisponibilidad,finDisponibilidad,idProfesional), conexion);

                retorno = comando.ExecuteNonQuery();

            }

            return retorno;

        }

        public static DateTime getFinDisponibilidadActual(Int64 idProfesional)
        {
            string query = "SELECT FIN_DISPONIBILIDAD FROM [3FG].PROFESIONALES WHERE ID_USUARIO = '" + idProfesional + "'";
            DataTable dt = (new ConexionSQL()).cargarTablaSQL(query);
            String finDis = dt.Rows[0][0].ToString();
            if (String.IsNullOrEmpty(finDis))
            {
                string flag = "2000-12-30 18:00:00.000";
                return Convert.ToDateTime(flag);
            }
            else
            {
                DateTime finDispo = Convert.ToDateTime(finDis);
                return finDispo;
            }
        }

     }
}
