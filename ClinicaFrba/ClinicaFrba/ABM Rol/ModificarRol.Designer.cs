namespace ClinicaFrba.ABM_Rol
{
    partial class ModificarRol
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.txtNombreRol = new System.Windows.Forms.TextBox();
            this.chkListaFuncionalidades = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.button_seleccionarTodo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Del Rol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Elegir Funcionalidad";
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Location = new System.Drawing.Point(45, 215);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(80, 17);
            this.chkHabilitado.TabIndex = 2;
            this.chkHabilitado.Text = "checkBox1";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            this.chkHabilitado.CheckedChanged += new System.EventHandler(this.chkHabilitado_CheckedChanged);
            // 
            // txtNombreRol
            // 
            this.txtNombreRol.Location = new System.Drawing.Point(136, 31);
            this.txtNombreRol.Name = "txtNombreRol";
            this.txtNombreRol.Size = new System.Drawing.Size(266, 20);
            this.txtNombreRol.TabIndex = 3;
            // 
            // chkListaFuncionalidades
            // 
            this.chkListaFuncionalidades.FormattingEnabled = true;
            this.chkListaFuncionalidades.Location = new System.Drawing.Point(136, 90);
            this.chkListaFuncionalidades.Name = "chkListaFuncionalidades";
            this.chkListaFuncionalidades.Size = new System.Drawing.Size(266, 109);
            this.chkListaFuncionalidades.TabIndex = 4;
            this.chkListaFuncionalidades.SelectedIndexChanged += new System.EventHandler(this.chkListaFuncionalidades_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 43);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Location = new System.Drawing.Point(424, 270);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(100, 42);
            this.buttonGuardar.TabIndex = 6;
            this.buttonGuardar.Text = "ModificarRol";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // button_seleccionarTodo
            // 
            this.button_seleccionarTodo.Location = new System.Drawing.Point(424, 90);
            this.button_seleccionarTodo.Name = "button_seleccionarTodo";
            this.button_seleccionarTodo.Size = new System.Drawing.Size(119, 23);
            this.button_seleccionarTodo.TabIndex = 7;
            this.button_seleccionarTodo.Text = "SeleccionarTodo";
            this.button_seleccionarTodo.UseVisualStyleBackColor = true;
            this.button_seleccionarTodo.Click += new System.EventHandler(this.button_seleccionarTodo_Click);
            // 
            // ModificarRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 340);
            this.Controls.Add(this.button_seleccionarTodo);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkListaFuncionalidades);
            this.Controls.Add(this.txtNombreRol);
            this.Controls.Add(this.chkHabilitado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ModificarRol";
            this.Text = "ModificarRol";
            this.Load += new System.EventHandler(this.ModificarRol_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkHabilitado;
        private System.Windows.Forms.TextBox txtNombreRol;
        private System.Windows.Forms.CheckedListBox chkListaFuncionalidades;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.Button button_seleccionarTodo;
    }
}