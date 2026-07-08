namespace PrestamoEquipos.Formularios
{
    partial class FrmPrestamos
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
            this.lblEquipo = new System.Windows.Forms.Label();
            this.cboEquipo = new System.Windows.Forms.ComboBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cboUsuario = new System.Windows.Forms.ComboBox();
            this.btnPrestar = new System.Windows.Forms.Button();
            this.lblActivos = new System.Windows.Forms.Label();
            this.dgvActivos = new System.Windows.Forms.DataGridView();
            this.colEquipoActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDevolver = new System.Windows.Forms.Button();
            this.lblHistorial = new System.Windows.Forms.Label();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.colEquipoHist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioHist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrestamoHist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDevolucionHist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoHist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            //
            // lblEquipo
            //
            this.lblEquipo.AutoSize = true;
            this.lblEquipo.Location = new System.Drawing.Point(20, 23);
            this.lblEquipo.Name = "lblEquipo";
            this.lblEquipo.Size = new System.Drawing.Size(46, 13);
            this.lblEquipo.TabIndex = 0;
            this.lblEquipo.Text = "Equipo:";
            //
            // cboEquipo
            //
            this.cboEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEquipo.Location = new System.Drawing.Point(75, 20);
            this.cboEquipo.Name = "cboEquipo";
            this.cboEquipo.Size = new System.Drawing.Size(200, 21);
            this.cboEquipo.TabIndex = 1;
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(295, 23);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(49, 13);
            this.lblUsuario.TabIndex = 2;
            this.lblUsuario.Text = "Usuario:";
            //
            // cboUsuario
            //
            this.cboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuario.Location = new System.Drawing.Point(350, 20);
            this.cboUsuario.Name = "cboUsuario";
            this.cboUsuario.Size = new System.Drawing.Size(200, 21);
            this.cboUsuario.TabIndex = 3;
            //
            // btnPrestar
            //
            this.btnPrestar.Location = new System.Drawing.Point(570, 18);
            this.btnPrestar.Name = "btnPrestar";
            this.btnPrestar.Size = new System.Drawing.Size(120, 25);
            this.btnPrestar.TabIndex = 4;
            this.btnPrestar.Text = "Registrar Prestamo";
            this.btnPrestar.UseVisualStyleBackColor = true;
            //
            // lblActivos
            //
            this.lblActivos.AutoSize = true;
            this.lblActivos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblActivos.Location = new System.Drawing.Point(20, 60);
            this.lblActivos.Name = "lblActivos";
            this.lblActivos.Size = new System.Drawing.Size(112, 15);
            this.lblActivos.TabIndex = 5;
            this.lblActivos.Text = "Prestamos activos";
            //
            // dgvActivos
            //
            this.dgvActivos.AllowUserToAddRows = false;
            this.dgvActivos.AllowUserToDeleteRows = false;
            this.dgvActivos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEquipoActivo,
            this.colUsuarioActivo,
            this.colFechaActivo});
            this.dgvActivos.Location = new System.Drawing.Point(20, 80);
            this.dgvActivos.MultiSelect = false;
            this.dgvActivos.Name = "dgvActivos";
            this.dgvActivos.ReadOnly = true;
            this.dgvActivos.RowHeadersVisible = false;
            this.dgvActivos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivos.Size = new System.Drawing.Size(670, 150);
            this.dgvActivos.TabIndex = 6;
            //
            // colEquipoActivo
            //
            this.colEquipoActivo.HeaderText = "Equipo";
            this.colEquipoActivo.Name = "colEquipoActivo";
            this.colEquipoActivo.ReadOnly = true;
            //
            // colUsuarioActivo
            //
            this.colUsuarioActivo.HeaderText = "Usuario";
            this.colUsuarioActivo.Name = "colUsuarioActivo";
            this.colUsuarioActivo.ReadOnly = true;
            //
            // colFechaActivo
            //
            this.colFechaActivo.HeaderText = "Fecha de prestamo";
            this.colFechaActivo.Name = "colFechaActivo";
            this.colFechaActivo.ReadOnly = true;
            //
            // btnDevolver
            //
            this.btnDevolver.Location = new System.Drawing.Point(20, 240);
            this.btnDevolver.Name = "btnDevolver";
            this.btnDevolver.Size = new System.Drawing.Size(260, 28);
            this.btnDevolver.TabIndex = 7;
            this.btnDevolver.Text = "Registrar Devolucion del seleccionado";
            this.btnDevolver.UseVisualStyleBackColor = true;
            //
            // lblHistorial
            //
            this.lblHistorial.AutoSize = true;
            this.lblHistorial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHistorial.Location = new System.Drawing.Point(20, 285);
            this.lblHistorial.Name = "lblHistorial";
            this.lblHistorial.Size = new System.Drawing.Size(133, 15);
            this.lblHistorial.TabIndex = 8;
            this.lblHistorial.Text = "Historial de prestamos";
            //
            // dgvHistorial
            //
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEquipoHist,
            this.colUsuarioHist,
            this.colPrestamoHist,
            this.colDevolucionHist,
            this.colEstadoHist});
            this.dgvHistorial.Location = new System.Drawing.Point(20, 305);
            this.dgvHistorial.MultiSelect = false;
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(670, 200);
            this.dgvHistorial.TabIndex = 9;
            //
            // colEquipoHist
            //
            this.colEquipoHist.HeaderText = "Equipo";
            this.colEquipoHist.Name = "colEquipoHist";
            this.colEquipoHist.ReadOnly = true;
            //
            // colUsuarioHist
            //
            this.colUsuarioHist.HeaderText = "Usuario";
            this.colUsuarioHist.Name = "colUsuarioHist";
            this.colUsuarioHist.ReadOnly = true;
            //
            // colPrestamoHist
            //
            this.colPrestamoHist.HeaderText = "Fecha de prestamo";
            this.colPrestamoHist.Name = "colPrestamoHist";
            this.colPrestamoHist.ReadOnly = true;
            //
            // colDevolucionHist
            //
            this.colDevolucionHist.HeaderText = "Fecha de devolucion";
            this.colDevolucionHist.Name = "colDevolucionHist";
            this.colDevolucionHist.ReadOnly = true;
            //
            // colEstadoHist
            //
            this.colEstadoHist.HeaderText = "Estado";
            this.colEstadoHist.Name = "colEstadoHist";
            this.colEstadoHist.ReadOnly = true;
            //
            // FrmPrestamos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 525);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.lblHistorial);
            this.Controls.Add(this.btnDevolver);
            this.Controls.Add(this.dgvActivos);
            this.Controls.Add(this.lblActivos);
            this.Controls.Add(this.btnPrestar);
            this.Controls.Add(this.cboUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.cboEquipo);
            this.Controls.Add(this.lblEquipo);
            this.MaximizeBox = false;
            this.Name = "FrmPrestamos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prestamos y Devoluciones";
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblEquipo;
        private System.Windows.Forms.ComboBox cboEquipo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cboUsuario;
        private System.Windows.Forms.Button btnPrestar;
        private System.Windows.Forms.Label lblActivos;
        private System.Windows.Forms.DataGridView dgvActivos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEquipoActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaActivo;
        private System.Windows.Forms.Button btnDevolver;
        private System.Windows.Forms.Label lblHistorial;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEquipoHist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioHist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrestamoHist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDevolucionHist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoHist;
    }
}
