using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Northwind
{
    public partial class Form1 : Form
    {
        string connectionString = @"Server=localhost\SQLEXPRESS;Database=MiEcommerceDB;Trusted_connection=yes; TrustServerCertificate=true";
        public Form1()
        {
            InitializeComponent();
            // Llama a la nueva función para cargar CATEGORÍAS
            CargarCategorias();

            // Configura el rango de fechas para la nueva DB de E-commerce
            dtpFechaInicio.Value = new DateTime(2020, 1, 1);
            dtpFechaFin.Value = DateTime.Today;
        }

        

      

        private void CargarCategorias()
        {
            // Consulta para obtener todas las categorías únicas de la tabla de productos
            // Usamos la columna real de tu tabla: 'category'
            string sqlCategorias = "SELECT DISTINCT category FROM dbo.products ORDER BY category";

            // Asumo que renombraste el control a clbCategorias
            clbCategorias.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlCategorias, connection))
                {
                    try
                    {
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

        // El método CargarDatos NO se modifica, solo cambia el nombre del objeto de conexión.
        private void CargarDatos(string sqlQuery)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Usamos un SqlCommand dentro del SqlDataAdapter para acceder al CommandTimeout
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Establece el tiempo de espera del comando a 120 segundos (2 minutos)
                    // El valor predeterminado es 30 segundos.
                    command.CommandTimeout = 120;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command)) // Pasa el comando, no la cadena SQL
                    {
                        DataTable dataTable = new DataTable();
                        try
                        {
                            adapter.Fill(dataTable);
                            dgvReporte.DataSource = dataTable;
                            MessageBox.Show($"Reporte cargado. Filas encontradas: {dataTable.Rows.Count}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            // La consulta que falló aparece en el error, lo cual es útil.
                            MessageBox.Show("Error de SQL: " + ex.Message + "\nConsulta: " + sqlQuery, "Error de Conexión o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            string sqlBase = "SELECT TOP 500 * FROM vw_reporte "; // USAMOS EL NOMBRE SIMPLE

            // 2. Inicialización de la Cláusula WHERE
            string sqlWhere = "WHERE 1 = 1 ";

            // 3. A. Filtro por Patrón (LIKE) - Nombre del Producto
            // Usamos el nombre de columna CORREGIDO: nombre_producto
            string productoFiltro = txtBuscarProducto.Text.Trim();
            if (!string.IsNullOrEmpty(productoFiltro))
            {
                sqlWhere += $"AND nombre_producto LIKE '%{productoFiltro}%' ";
            }

            // 3. B. Filtro por Rango de Fechas (BETWEEN)
            // Usamos el nombre de columna CORREGIDO: fecha_pedido
            string fechaInicio = dtpFechaInicio.Value.ToString("yyyy-MM-dd");
            string fechaFin = dtpFechaFin.Value.ToString("yyyy-MM-dd");
            sqlWhere += $"AND fecha_pedido BETWEEN '{fechaInicio}' AND '{fechaFin}' ";

            // 3. C. Filtro por Conjunto (IN) - Categorías Seleccionadas
            if (clbCategorias.CheckedItems.Count > 0)
            {
                var categoriasSeleccionadas = clbCategorias.CheckedItems.Cast<CategoriaItem>()
                                                        .Select(item => $"'{item.Nombre}'")
                                                        .ToArray();

                string listaCategorias = string.Join(",", categoriasSeleccionadas);

                // Usamos el nombre de columna CORREGIDO: categoria_producto
                sqlWhere += $"AND categoria_producto IN ({listaCategorias}) ";
            }

            // 4. Ordenamiento
            // Usamos el nombre de columna CORREGIDO: fecha_pedido
            string sqlOrder = "ORDER BY fecha_pedido DESC;";

            // 5. Construcción y Ejecución
            string sqlFinal = sqlBase + sqlWhere + sqlOrder;

            CargarDatos(sqlFinal);
        }
    }
}
