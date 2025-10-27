using Microsoft.Data.SqlClient; // Conexión SQL Server
using System.Data;             // DataTable, DataRow, etc.
using System.Linq;             // Necesario para LINQ (Select, Cast, etc.)
using System.IO;               // Para archivos (CSV, JSON, XML)
using System.Text.Json;        // Para exportación a JSON
using System.Windows.Forms;    // Componentes del formulario

// --- USINGS NECESARIOS PARA EXPORTACIÓN ---

using ClosedXML.Excel;         // Para exportar a XLSX (ClosedXML)
using iTextSharp.text;         // Para PDF (Document, Phrase, Paragraph)
using iTextSharp.text.pdf;

namespace Northwind
{
    public partial class Form1 : Form
    {
        string connectionString = @"Server=localhost\SQLEXPRESS;Database=MiEcommerceDB;Trusted_connection=yes; TrustServerCertificate=true";

        // 2. Variables para Paginación
        private int currentPage = 1;
        private const int pageSize = 500; // Bloques de 500 filas
        public Form1()
        {
            InitializeComponent();
            CargarCategorias();

            // Configura las fechas por defecto
            dtpFechaInicio.Value = new DateTime(2020, 1, 1);
            dtpFechaFin.Value = DateTime.Today;
            cmbFormatoExportar.Items.Add("CSV");
            cmbFormatoExportar.Items.Add("XLSX (Excel)");
            cmbFormatoExportar.Items.Add("PDF");
            cmbFormatoExportar.Items.Add("JSON");
            cmbFormatoExportar.Items.Add("XML");
            cmbFormatoExportar.SelectedIndex = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void CargarDatos(string sqlQuery, bool usePagination)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.CommandTimeout = 120; // 2 minutos para paginación

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        try
                        {
                            adapter.Fill(dataTable);
                            dgvReporte.DataSource = dataTable;

                            string mensaje = usePagination
                                ? $"Página {currentPage} cargada. Filas: {dataTable.Rows.Count}"
                                : $"Reporte cargado. Filas encontradas: {dataTable.Rows.Count}";

                            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error de SQL: " + ex.Message + "\nConsulta: " + sqlQuery,
                                            "Error de Conexión o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void CargarCategorias()
        {
            string sqlCategorias = "SELECT DISTINCT category FROM dbo.products ORDER BY category";
            clbCategorias.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlCategorias, connection))
                {
                    try
                    {
                        // Uso de 'using' garantiza el open/close
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string categoria = reader["category"].ToString();
                            clbCategorias.Items.Add(new CategoriaItem(categoria));
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar categorías: " + ex.Message);
                    }
                }
            }
            // Marca todas las categorías por defecto
            for (int i = 0; i < clbCategorias.Items.Count; i++)
            {
                clbCategorias.SetItemChecked(i, true);
            }
        }
        private DataTable ObtenerTodosLosDatosFiltrados()
        {
            string sqlBase = "SELECT * FROM vw_reporte ";
            string sqlWhere = "WHERE 1 = 1 ";

            // Filtros Dinámicos
            string productoFiltro = txtBuscarProducto.Text.Trim();
            if (!string.IsNullOrEmpty(productoFiltro))
            {
                sqlWhere += $"AND nombre_producto LIKE '%{productoFiltro}%' ";
            }
            string fechaInicio = dtpFechaInicio.Value.ToString("yyyy-MM-dd");
            string fechaFin = dtpFechaFin.Value.ToString("yyyy-MM-dd");
            sqlWhere += $"AND fecha_pedido BETWEEN '{fechaInicio}' AND '{fechaFin}' ";

            if (clbCategorias.CheckedItems.Count > 0)
            {
                var categoriasSeleccionadas = clbCategorias.CheckedItems.Cast<CategoriaItem>()
                                                        .Select(item => $"'{item.Nombre}'")
                                                        .ToArray();
                string listaCategorias = string.Join(",", categoriasSeleccionadas);
                sqlWhere += $"AND categoria_producto IN ({listaCategorias}) ";
            }

            string sqlFinal = sqlBase + sqlWhere + " ORDER BY fecha_pedido DESC";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlFinal, connection))
                {
                    // AUMENTO DEL TIMEOUT (5 minutos para reportes masivos)
                    command.CommandTimeout = 300;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        try
                        {
                            adapter.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al obtener todos los datos para exportar: " + ex.Message, "Error de Exportación Crítico");
                            return null;
                        }
                    }
                }
            }
            return dt;
        }
        private void LoadPage()
        {
            string sqlBase = "SELECT * FROM vw_reporte ";
            string sqlWhere = "WHERE 1 = 1 ";

            string productoFiltro = txtBuscarProducto.Text.Trim();
            if (!string.IsNullOrEmpty(productoFiltro))
            {
                sqlWhere += $"AND nombre_producto LIKE '%{productoFiltro}%' ";
            }

            string fechaInicio = dtpFechaInicio.Value.ToString("yyyy-MM-dd");
            string fechaFin = dtpFechaFin.Value.ToString("yyyy-MM-dd");
            sqlWhere += $"AND fecha_pedido BETWEEN '{fechaInicio}' AND '{fechaFin}' ";

            if (clbCategorias.CheckedItems.Count > 0)
            {
                var categoriasSeleccionadas = clbCategorias.CheckedItems.Cast<CategoriaItem>()
                                                        .Select(item => $"'{item.Nombre}'")
                                                        .ToArray();
                string listaCategorias = string.Join(",", categoriasSeleccionadas);
                sqlWhere += $"AND categoria_producto IN ({listaCategorias}) ";
            }

            string sqlOrder = "ORDER BY fecha_pedido DESC, order_id DESC "; // Aseguramos el desempate
            int offset = (currentPage - 1) * pageSize;
            string sqlPaging = $"OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;";

            string sqlFinal = sqlBase + sqlWhere + sqlOrder + sqlPaging;
            CargarDatos(sqlFinal, true);
        }


        private void btnFilter_Click(object sender, EventArgs e)
        {
            currentPage = 1; // Reinicia a la primera página al aplicar un nuevo filtro
            LoadPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dgvReporte.RowCount > 0)
            {
                currentPage++;
                LoadPage();
            }
            else
            {
               
                currentPage--;
                MessageBox.Show("No hay más páginas disponibles o esta fue la última.", "Fin del Reporte");
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPage();
            }
            else
            {
                MessageBox.Show("Ya estás en la primera página.", "Límite");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            btnExport.Enabled = false; // Deshabilitar para evitar doble clic
            Application.DoEvents();      // Forzar a la interfaz de usuario a actualizarse y mostrar la barra

            DataTable dtCompleto = null;
            string formatoSeleccionado = cmbFormatoExportar.SelectedItem.ToString();
            string filePath = string.Empty;
            int filasExportadas = 0;

            try
            {
                // 2. OBTENER TODOS LOS DATOS (Parte Lenta con alto Timeout)
                dtCompleto = ObtenerTodosLosDatosFiltrados();

                if (dtCompleto == null || dtCompleto.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar. Aplique un filtro y espere la carga.", "Advertencia");
                    return;
                }

                // 3. CONFIGURAR Y MOSTRAR EL DIÁLOGO DE GUARDADO
                string extension = "";
                string filtro = "";
                switch (formatoSeleccionado)
                {
                    case "CSV": filtro = "CSV (*.csv)|*.csv"; extension = ".csv"; break;
                    case "XLSX (Excel)": filtro = "Excel Workbook (*.xlsx)|*.xlsx"; extension = ".xlsx"; break;
                    case "PDF": filtro = "Portable Document Format (*.pdf)|*.pdf"; extension = ".pdf"; break;
                    case "JSON": filtro = "JSON (*.json)|*.json"; extension = ".json"; break;
                    case "XML": filtro = "XML (*.xml)|*.xml"; extension = ".xml"; break;
                }

                saveFileDialog1.Filter = filtro;
                saveFileDialog1.FileName = $"Reporte_Ventas_{DateTime.Now:yyyyMMdd}{extension}";
                saveFileDialog1.Title = "Guardar Reporte " + formatoSeleccionado;

                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return; // El usuario canceló la operación
                }

                filePath = saveFileDialog1.FileName;

                // 4. EJECUTAR LA EXPORTACIÓN ESPECÍFICA
                switch (formatoSeleccionado)
                {
                    case "CSV": ExportToCsv(dtCompleto, filePath); break;
                    case "XLSX (Excel)": ExportToXlsx(dtCompleto, filePath); break;
                    case "PDF": ExportToPdf(dtCompleto, filePath); break;
                    case "JSON": ExportToJson(dtCompleto, filePath); break;
                    case "XML": ExportToXml(dtCompleto, filePath); break;
                }

                filasExportadas = dtCompleto.Rows.Count;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar: " + ex.Message, "Error Crítico");
            }
            finally
            {
                // 5. GESTIÓN DEL ESTADO DE LA UI (Finalización)
                // Este bloque siempre se ejecuta, garantizando que el usuario pueda usar la app de nuevo.
                progressBar1.Visible = false;
                btnExport.Enabled = true;

                // Mostrar el mensaje de éxito solo si la exportación fue completada
                if (filasExportadas > 0)
                {
                    MessageBox.Show($"Datos exportados ({filasExportadas} filas) con éxito a: {filePath}", "Exportación Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void ExportToCsv(DataTable dt, string filePath)
        {
            var lines = new System.Collections.Generic.List<string>();
            string[] columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
            lines.Add(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                // Manejo de comas y quotes dentro de los datos
                string[] fields = row.ItemArray.Select(field => $"\"{field.ToString().Replace("\"", "\"\"")}\"").ToArray();
                lines.Add(string.Join(",", fields));
            }
            File.WriteAllLines(filePath, lines);
        }

        private void ExportToXlsx(DataTable dt, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dt, "Reporte_ECommerce");
                workbook.SaveAs(filePath);
            }
        }

        private void ExportToJson(DataTable dt, string filePath)
        {
            var rows = dt.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>()
                .Select(c => new { Key = c.ColumnName, Value = r[c] })
                .ToDictionary(x => x.Key, x => x.Value)).ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(rows, options);
            File.WriteAllText(filePath, jsonString);
        }

        private void ExportToXml(DataTable dt, string filePath)
        {
            dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
        }

        private void ExportToPdf(DataTable dt, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                document.Add(new Paragraph("Reporte de Ventas E-commerce\n\n"));

                PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 100;

                foreach (DataColumn column in dt.Columns)
                {
                    pdfTable.AddCell(new Phrase(column.ColumnName));
                }

                foreach (DataRow row in dt.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        pdfTable.AddCell(item.ToString());
                    }
                }
                document.Add(pdfTable);
                document.Close();
            }
        }

    }
}
