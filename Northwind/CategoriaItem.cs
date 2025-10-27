using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System;

namespace Northwind
{
    public class CategoriaItem
    {
        public string Nombre { get; set; }

        public CategoriaItem(string nombre)
        {
            Nombre = nombre;
        }

        // Define lo que se muestra en el CheckedListBox
        public override string ToString()
        {
            return Nombre;
        }
    }
}
