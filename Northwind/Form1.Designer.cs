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
            btnPrevious = new Button();
            btnNext = new Button();
            lblExportFormat = new Label();
            cmbFormatoExportar = new ComboBox();
            btnExport = new Button();
            saveFileDialog1 = new SaveFileDialog();
            progressBar1 = new ProgressBar();
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
            // btnPrevious
            // 
            btnPrevious.Location = new Point(142, 630);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(112, 34);
            btnPrevious.TabIndex = 8;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(12, 630);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(112, 34);
            btnNext.TabIndex = 9;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // lblExportFormat
            // 
            lblExportFormat.AutoSize = true;
            lblExportFormat.Location = new Point(499, 34);
            lblExportFormat.Name = "lblExportFormat";
            lblExportFormat.Size = new Size(69, 25);
            lblExportFormat.TabIndex = 10;
            lblExportFormat.Text = "Format";
            // 
            // cmbFormatoExportar
            // 
            cmbFormatoExportar.FormattingEnabled = true;
            cmbFormatoExportar.Location = new Point(580, 26);
            cmbFormatoExportar.Name = "cmbFormatoExportar";
            cmbFormatoExportar.Size = new Size(182, 33);
            cmbFormatoExportar.TabIndex = 11;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(782, 24);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(112, 34);
            btnExport.TabIndex = 12;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(344, 111);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(150, 34);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 13;
            progressBar1.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1053, 690);
            Controls.Add(progressBar1);
            Controls.Add(btnExport);
            Controls.Add(cmbFormatoExportar);
            Controls.Add(lblExportFormat);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(clbCategorias);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(btnFilter);
            Controls.Add(txtBuscarProducto);
            Controls.Add(dgvReporte);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
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
        private Button btnPrevious;
        private Button btnNext;
        private Label lblExportFormat;
        private ComboBox cmbFormatoExportar;
        private Button btnExport;
        private SaveFileDialog saveFileDialog1;
        private ProgressBar progressBar1;
    }
}
