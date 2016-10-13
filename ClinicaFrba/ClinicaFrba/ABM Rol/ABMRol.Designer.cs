namespace ClinicaFrba.AbmRol
{
    partial class ABMROL
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
            this.label = new System.Windows.Forms.Label();
            this.button_crear = new System.Windows.Forms.Button();
            this.button_modificar = new System.Windows.Forms.Button();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(242, 46);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(49, 13);
            this.label.TabIndex = 0;
            this.label.Text = "ABM Rol";
            // 
            // button_crear
            // 
            this.button_crear.Location = new System.Drawing.Point(83, 175);
            this.button_crear.Name = "button_crear";
            this.button_crear.Size = new System.Drawing.Size(75, 23);
            this.button_crear.TabIndex = 1;
            this.button_crear.Text = "Crear";
            this.button_crear.UseVisualStyleBackColor = true;
            this.button_crear.Click += new System.EventHandler(this.button_crear_Click);
            // 
            // button_modificar
            // 
            this.button_modificar.Location = new System.Drawing.Point(245, 175);
            this.button_modificar.Name = "button_modificar";
            this.button_modificar.Size = new System.Drawing.Size(75, 23);
            this.button_modificar.TabIndex = 2;
            this.button_modificar.Text = "Modificar";
            this.button_modificar.UseVisualStyleBackColor = true;
            this.button_modificar.Click += new System.EventHandler(this.button_modificar_Click);
            // 
            // button_eliminar
            // 
            this.button_eliminar.Location = new System.Drawing.Point(415, 175);
            this.button_eliminar.Name = "button_eliminar";
            this.button_eliminar.Size = new System.Drawing.Size(75, 23);
            this.button_eliminar.TabIndex = 3;
            this.button_eliminar.Text = "Eliminar";
            this.button_eliminar.UseVisualStyleBackColor = true;
            this.button_eliminar.Click += new System.EventHandler(this.button_eliminar_Click);
            // 
            // ABMROL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 262);
            this.Controls.Add(this.button_eliminar);
            this.Controls.Add(this.button_modificar);
            this.Controls.Add(this.button_crear);
            this.Controls.Add(this.label);
            this.Name = "ABMROL";
            this.Text = "abm_rol";
            this.Load += new System.EventHandler(this.ABMROL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button button_crear;
        private System.Windows.Forms.Button button_modificar;
        private System.Windows.Forms.Button button_eliminar;

    }
}