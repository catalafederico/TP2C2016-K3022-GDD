namespace ClinicaFrba.Registrar_Agenda_Medico
{
    partial class RegistrarAgenda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewRangos = new System.Windows.Forms.ListView();
            this.columnDia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHoraInicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHoraFin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEspecialidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnAgregarRango = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerInicioDisp = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFinDisp = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxDias = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxEspecialidades = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxHoraInicio = new System.Windows.Forms.ComboBox();
            this.comboBoxHoraFin = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxMinutosInicio = new System.Windows.Forms.ComboBox();
            this.comboBoxMinutosFin = new System.Windows.Forms.ComboBox();
            this.btnEliminarRango = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewRangos
            // 
            this.listViewRangos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDia,
            this.ColumnHoraInicio,
            this.columnHoraFin,
            this.columnEspecialidad});
            this.listViewRangos.Location = new System.Drawing.Point(54, 161);
            this.listViewRangos.Name = "listViewRangos";
            this.listViewRangos.Size = new System.Drawing.Size(499, 171);
            this.listViewRangos.TabIndex = 0;
            this.listViewRangos.UseCompatibleStateImageBehavior = false;
            this.listViewRangos.View = System.Windows.Forms.View.Details;
            // 
            // columnDia
            // 
            this.columnDia.Text = "Dia";
            this.columnDia.Width = 78;
            // 
            // ColumnHoraInicio
            // 
            this.ColumnHoraInicio.Text = "Hora de inicio";
            this.ColumnHoraInicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnHoraInicio.Width = 125;
            // 
            // columnHoraFin
            // 
            this.columnHoraFin.Text = "Hora de finalizacion";
            this.columnHoraFin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHoraFin.Width = 107;
            // 
            // columnEspecialidad
            // 
            this.columnEspecialidad.Text = "Especialidad";
            this.columnEspecialidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnEspecialidad.Width = 184;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(191, 366);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAgregarRango
            // 
            this.btnAgregarRango.Location = new System.Drawing.Point(523, 109);
            this.btnAgregarRango.Name = "btnAgregarRango";
            this.btnAgregarRango.Size = new System.Drawing.Size(90, 23);
            this.btnAgregarRango.TabIndex = 2;
            this.btnAgregarRango.Text = "Agregar rango";
            this.btnAgregarRango.UseVisualStyleBackColor = true;
            this.btnAgregarRango.Click += new System.EventHandler(this.btnAñadirRango_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(345, 366);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Inicio de disponibilidad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fin de disponibilidad:";
            // 
            // dateTimePickerInicioDisp
            // 
            this.dateTimePickerInicioDisp.Checked = false;
            this.dateTimePickerInicioDisp.Location = new System.Drawing.Point(191, 9);
            this.dateTimePickerInicioDisp.Name = "dateTimePickerInicioDisp";
            this.dateTimePickerInicioDisp.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerInicioDisp.TabIndex = 6;
            this.dateTimePickerInicioDisp.Value = new System.DateTime(2016, 10, 30, 0, 0, 0, 0);
            // 
            // dateTimePickerFinDisp
            // 
            this.dateTimePickerFinDisp.Location = new System.Drawing.Point(191, 49);
            this.dateTimePickerFinDisp.Name = "dateTimePickerFinDisp";
            this.dateTimePickerFinDisp.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFinDisp.TabIndex = 7;
            this.dateTimePickerFinDisp.Value = new System.DateTime(2016, 10, 30, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Dia:";
            // 
            // comboBoxDias
            // 
            this.comboBoxDias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDias.FormattingEnabled = true;
            this.comboBoxDias.Location = new System.Drawing.Point(127, 90);
            this.comboBoxDias.Name = "comboBoxDias";
            this.comboBoxDias.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDias.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Especialidad:";
            // 
            // comboBoxEspecialidades
            // 
            this.comboBoxEspecialidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEspecialidades.FormattingEnabled = true;
            this.comboBoxEspecialidades.Location = new System.Drawing.Point(127, 129);
            this.comboBoxEspecialidades.Name = "comboBoxEspecialidades";
            this.comboBoxEspecialidades.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEspecialidades.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Hora de inicio:";
            // 
            // comboBoxHoraInicio
            // 
            this.comboBoxHoraInicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHoraInicio.FormattingEnabled = true;
            this.comboBoxHoraInicio.Location = new System.Drawing.Point(372, 90);
            this.comboBoxHoraInicio.Name = "comboBoxHoraInicio";
            this.comboBoxHoraInicio.Size = new System.Drawing.Size(48, 21);
            this.comboBoxHoraInicio.TabIndex = 19;
            // 
            // comboBoxHoraFin
            // 
            this.comboBoxHoraFin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHoraFin.FormattingEnabled = true;
            this.comboBoxHoraFin.Location = new System.Drawing.Point(372, 129);
            this.comboBoxHoraFin.Name = "comboBoxHoraFin";
            this.comboBoxHoraFin.Size = new System.Drawing.Size(48, 21);
            this.comboBoxHoraFin.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(285, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Hora de fin:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(426, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = ":";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(426, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = ":";
            // 
            // comboBoxMinutosInicio
            // 
            this.comboBoxMinutosInicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMinutosInicio.FormattingEnabled = true;
            this.comboBoxMinutosInicio.Location = new System.Drawing.Point(442, 90);
            this.comboBoxMinutosInicio.Name = "comboBoxMinutosInicio";
            this.comboBoxMinutosInicio.Size = new System.Drawing.Size(48, 21);
            this.comboBoxMinutosInicio.TabIndex = 24;
            // 
            // comboBoxMinutosFin
            // 
            this.comboBoxMinutosFin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMinutosFin.FormattingEnabled = true;
            this.comboBoxMinutosFin.Location = new System.Drawing.Point(442, 129);
            this.comboBoxMinutosFin.Name = "comboBoxMinutosFin";
            this.comboBoxMinutosFin.Size = new System.Drawing.Size(48, 21);
            this.comboBoxMinutosFin.TabIndex = 25;
            // 
            // btnEliminarRango
            // 
            this.btnEliminarRango.Location = new System.Drawing.Point(591, 241);
            this.btnEliminarRango.Name = "btnEliminarRango";
            this.btnEliminarRango.Size = new System.Drawing.Size(85, 23);
            this.btnEliminarRango.TabIndex = 26;
            this.btnEliminarRango.Text = "Eliminar rango";
            this.btnEliminarRango.UseVisualStyleBackColor = true;
            this.btnEliminarRango.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // RegistrarAgenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 401);
            this.Controls.Add(this.btnEliminarRango);
            this.Controls.Add(this.comboBoxMinutosFin);
            this.Controls.Add(this.comboBoxMinutosInicio);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxHoraFin);
            this.Controls.Add(this.comboBoxHoraInicio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxEspecialidades);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxDias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePickerFinDisp);
            this.Controls.Add(this.dateTimePickerInicioDisp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregarRango);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.listViewRangos);
            this.Name = "RegistrarAgenda";
            this.Text = "Registrar agenda";
            this.Load += new System.EventHandler(this.RegistrarAgenda_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewRangos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnAgregarRango;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerInicioDisp;
        private System.Windows.Forms.DateTimePicker dateTimePickerFinDisp;
        private System.Windows.Forms.ColumnHeader columnDia;
        private System.Windows.Forms.ColumnHeader ColumnHoraInicio;
        private System.Windows.Forms.ColumnHeader columnHoraFin;
        private System.Windows.Forms.ColumnHeader columnEspecialidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxDias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxEspecialidades;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxHoraInicio;
        private System.Windows.Forms.ComboBox comboBoxHoraFin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxMinutosInicio;
        private System.Windows.Forms.ComboBox comboBoxMinutosFin;
        private System.Windows.Forms.Button btnEliminarRango;
    }
}