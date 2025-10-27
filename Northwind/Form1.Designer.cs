namespace Northwind
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvReporte = new DataGridView();
            txtBuscarProducto = new TextBox();
            btnFilter = new Button();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            clbCategorias = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            SuspendLayout();
            // 
            // dgvReporte
            // 
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporte.Location = new Point(12, 162);
            dgvReporte.Name = "dgvReporte";
            dgvReporte.RowHeadersWidth = 62;
            dgvReporte.Size = new Size(644, 445);
            dgvReporte.TabIndex = 0;
            // 
            // txtBuscarProducto
            // 
            txtBuscarProducto.Location = new Point(173, 31);
            txtBuscarProducto.Name = "txtBuscarProducto";
            txtBuscarProducto.Size = new Size(150, 31);
            txtBuscarProducto.TabIndex = 2;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(344, 31);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(112, 34);
            btnFilter.TabIndex = 3;
            btnFilter.Text = "filtrar";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(23, 77);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(300, 31);
            dtpFechaInicio.TabIndex = 4;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(23, 114);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(300, 31);
            dtpFechaFin.TabIndex = 5;
            // 
            // clbCategorias
            // 
            clbCategorias.FormattingEnabled = true;
            clbCategorias.Location = new Point(685, 162);
            clbCategorias.Name = "clbCategorias";
            clbCategorias.Size = new Size(356, 424);
            clbCategorias.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1053, 672);
            Controls.Add(clbCategorias);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(btnFilter);
            Controls.Add(txtBuscarProducto);
            Controls.Add(dgvReporte);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvReporte;
        private TextBox txtBuscarProducto;
        private Button btnFilter;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private CheckedListBox clbCategorias;
    }
}
