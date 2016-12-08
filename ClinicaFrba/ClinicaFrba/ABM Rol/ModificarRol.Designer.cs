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
            this.txtNombreRol = new System.Windows.Forms.TextBox();
            this.chkListaFuncionalidades = new System.Windows.Forms.CheckedListBox();
            this.button_seleccionarTodo = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "nombre rol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "elegir funcionalidad";
            // 
            // txtNombreRol
            // 
            this.txtNombreRol.Location = new System.Drawing.Point(154, 29);
            this.txtNombreRol.Name = "txtNombreRol";
            this.txtNombreRol.Size = new System.Drawing.Size(216, 20);
            this.txtNombreRol.TabIndex = 2;
            this.txtNombreRol.TextChanged += new System.EventHandler(this.txtNombreRol_TextChanged);
            // 
            // chkListaFuncionalidades
            // 
            this.chkListaFuncionalidades.FormattingEnabled = true;
            this.chkListaFuncionalidades.Location = new System.Drawing.Point(154, 110);
            this.chkListaFuncionalidades.Name = "chkListaFuncionalidades";
            this.chkListaFuncionalidades.Size = new System.Drawing.Size(216, 94);
            this.chkListaFuncionalidades.TabIndex = 3;
            this.chkListaFuncionalidades.SelectedIndexChanged += new System.EventHandler(this.chkListaFuncionalidades_SelectedIndexChanged_1);
            // 
            // button_seleccionarTodo
            // 
            this.button_seleccionarTodo.Location = new System.Drawing.Point(432, 110);
            this.button_seleccionarTodo.Name = "button_seleccionarTodo";
            this.button_seleccionarTodo.Size = new System.Drawing.Size(122, 23);
            this.button_seleccionarTodo.TabIndex = 4;
            this.button_seleccionarTodo.Text = "seleccionar todo";
            this.button_seleccionarTodo.UseVisualStyleBackColor = true;
            this.button_seleccionarTodo.Click += new System.EventHandler(this.button_seleccionarTodo_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(71, 276);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Location = new System.Drawing.Point(454, 275);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(75, 23);
            this.buttonGuardar.TabIndex = 6;
            this.buttonGuardar.Text = "modificar rol";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click_1);
            // 
            // ModificarRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 338);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_seleccionarTodo);
            this.Controls.Add(this.chkListaFuncionalidades);
            this.Controls.Add(this.txtNombreRol);
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
        private System.Windows.Forms.TextBox txtNombreRol;
        private System.Windows.Forms.CheckedListBox chkListaFuncionalidades;
        private System.Windows.Forms.Button button_seleccionarTodo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonGuardar;
    }
}