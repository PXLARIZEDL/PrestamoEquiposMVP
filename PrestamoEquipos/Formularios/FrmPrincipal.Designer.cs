namespace PrestamoEquipos.Formularios
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnEquipos = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnPrestamos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(35, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(320, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Sistema de Prestamo de Equipos";
            //
            // btnEquipos
            //
            this.btnEquipos.Location = new System.Drawing.Point(45, 80);
            this.btnEquipos.Name = "btnEquipos";
            this.btnEquipos.Size = new System.Drawing.Size(300, 45);
            this.btnEquipos.TabIndex = 1;
            this.btnEquipos.Text = "Gestionar Equipos";
            this.btnEquipos.UseVisualStyleBackColor = true;
            //
            // btnUsuarios
            //
            this.btnUsuarios.Location = new System.Drawing.Point(45, 140);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(300, 45);
            this.btnUsuarios.TabIndex = 2;
            this.btnUsuarios.Text = "Gestionar Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            //
            // btnPrestamos
            //
            this.btnPrestamos.Location = new System.Drawing.Point(45, 200);
            this.btnPrestamos.Name = "btnPrestamos";
            this.btnPrestamos.Size = new System.Drawing.Size(300, 45);
            this.btnPrestamos.TabIndex = 3;
            this.btnPrestamos.Text = "Prestamos y Devoluciones";
            this.btnPrestamos.UseVisualStyleBackColor = true;
            //
            // FrmPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 285);
            this.Controls.Add(this.btnPrestamos);
            this.Controls.Add(this.btnUsuarios);
            this.Controls.Add(this.btnEquipos);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Principal";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnEquipos;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnPrestamos;
    }
}
