﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Registro_Resultado
{
    public partial class RegistroResultado : Form
    {
        private DateTime fechaAtencion;
        private TimeSpan horarioAtencion;
        private string nombreAfiliado;

        public RegistroResultado()
        {
            InitializeComponent();
        }
    }
}
