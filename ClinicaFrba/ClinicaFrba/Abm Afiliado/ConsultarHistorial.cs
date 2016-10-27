﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaFrba.Dominio;

namespace ClinicaFrba.ABM_Afiliado
{
    public partial class ConsultarHistorial : Form
    {
        public ConsultarHistorial()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            inicializar();
        }

        private void ConsultarHistorial_Load(object sender, EventArgs e)
        {

        }
        private void inicializar()
        {
            CompletadorDeTablas.hacerQuery("select ID_USUARIO, MOTIVO_CAMBIO_PLAN,FECHA_MODIFICACION from [3FG].HISTORIAL_CAMBIOS_PLAN", ref dataGridView1);
            /*SELECT [Código Publicación], [Código Compra u Oferta], Tipo, Descripción, [Monto ($)], Cantidad, Envío, [Fecha Operación] FROM (SELECT CO.N_ID_PUBLICACION 'Código Publicación', N_ID_COMPRA 'Código Compra u Oferta', 'Compra Inmediata' Tipo, P.D_DESCRED Descripción, N_CANTIDAD*N_PRECIO 'Monto ($)', N_CANTIDAD Cantidad, C_ENVIO Envío, F_ALTA 'Fecha Operación' FROM GDD_15.CLIENTES CL JOIN GDD_15.COMPRAS CO ON (CL.N_ID_USUARIO = CO.N_ID_CLIENTE) JOIN GDD_15.PUBLICACIONES P ON (CO.N_ID_PUBLICACION = P.N_ID_PUBLICACION) WHERE CL.N_ID_USUARIO = '" + idCli + "' AND N_ID_COMPRA NOT IN (SELECT N_ID_COMPRA FROM GDD_15.CALIFICACIONES WHERE N_ID_CLIENTE = '" + idCli + "' AND N_ID_COMPRA IS NOT NULL) UNION ALL SELECT O.N_ID_PUBLICACION 'Código Publicación', N_ID_OFERTA 'Código Compra u Oferta', 'Subasta' Tipo, P.D_DESCRED Descripción, N_MONTO 'Monto ($)', '1' Cantidad, C_ENVIO Envíor, F_ALTA 'Fecha Operación' FROM GDD_15.CLIENTES CL JOIN GDD_15.OFERTAS O ON (CL.N_ID_USUARIO = O.N_ID_CLIENTE) JOIN GDD_15.PUBLICACIONES P ON (O.N_ID_PUBLICACION = P.N_ID_PUBLICACION) WHERE C_GANADOR = 'SI' AND CL.N_ID_USUARIO = '" + idCli + "' AND N_ID_OFERTA NOT IN (SELECT N_ID_OFERTA FROM GDD_15.CALIFICACIONES WHERE N_ID_CLIENTE = '" + idCli + "' AND N_ID_OFERTA IS NOT NULL)) SQ ORDER BY [Fecha Operación]*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
