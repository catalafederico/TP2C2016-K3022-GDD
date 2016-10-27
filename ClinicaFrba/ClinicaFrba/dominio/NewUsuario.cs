using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaFrba.Dominio
{
    class NewUsuario
    {
        public string username;
        public string password;


        public string mail;
        public Int64 telefono;
        public string DireccionCompleta;
        public string sexo;
        public string afiPlan;
        public string estadoCivil;
        public Int64 cantidadFamiliares;




        public string afiNombre;
        public string afiApellido;
        public string afiTipoDocumento;
        public Int64 afiNumeroDocumento;
        public string afiFechaNac;


        public NewUsuario(String nombre, String contraseña)
        {
            this.username = nombre;
            this.password = contraseña;
        }


    }
}
