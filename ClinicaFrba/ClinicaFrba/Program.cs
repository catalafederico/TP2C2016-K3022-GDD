using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaFrba;

namespace ClinicaFrba
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Registro_Llegada.ElegirAfiliado(56565));
        }

        public static String ip()
        {
            return ConfigurationManager.AppSettings["ip"];
        }

        public static String puerto()
        {
            return ConfigurationManager.AppSettings["puerto"];
        }


        public static String nuevaFechaSistema()
        {
            return ConfigurationManager.AppSettings["FechaGlobal"];
        }
    }
}
