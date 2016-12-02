namespace ClinicaFrba.Eleccion_Funcionalidad
{
    partial class ElegirFuncionalidad
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
            this.comboBox_funcionalidades = new System.Windows.Forms.ComboBox();
            this.button_seleccionar_funcionalidad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Elegir Funcionalidad";
            // 
            // comboBox_funcionalidades
            // 
            this.comboBox_funcionalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_funcionalidades.FormattingEnabled = true;
            this.comboBox_funcionalidades.Location = new System.Drawing.Point(197, 41);
            this.comboBox_funcionalidades.Name = "comboBox_funcionalidades";
            this.comboBox_funcionalidades.Size = new System.Drawing.Size(228, 21);
            this.comboBox_funcionalidades.TabIndex = 1;
            this.comboBox_funcionalidades.SelectedIndexChanged += new System.EventHandler(this.comboBox_funcionalidades_SelectedIndexChanged);
            // 
            // button_seleccionar_funcionalidad
            // 
            this.button_seleccionar_funcionalidad.Location = new System.Drawing.Point(197, 173);
            this.button_seleccionar_funcionalidad.Name = "button_seleccionar_funcionalidad";
            this.button_seleccionar_funcionalidad.Size = new System.Drawing.Size(214, 23);
            this.button_seleccionar_funcionalidad.TabIndex = 2;
            this.button_seleccionar_funcionalidad.Text = "Seleccionar Funcionalidad";
            this.button_seleccionar_funcionalidad.UseVisualStyleBackColor = true;
            this.button_seleccionar_funcionalidad.Click += new System.EventHandler(this.button_seleccionar_funcionalidad_Click);
            // 
            // ElegirFuncionalidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 261);
            this.Controls.Add(this.button_seleccionar_funcionalidad);
            this.Controls.Add(this.comboBox_funcionalidades);
            this.Controls.Add(this.label1);
            this.Name = "ElegirFuncionalidad";
            this.Text = "ElegirFuncionalidad";
            this.Load += new System.EventHandler(this.ElegirFuncionalidad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_funcionalidades;
        private System.Windows.Forms.Button button_seleccionar_funcionalidad;
    }
}