using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind
{
    public class CategoriaItem
    {
        // En este caso, el ID y el Nombre son el mismo (el nombre de la categoría)
        public string Nombre { get; set; }

        public CategoriaItem(string nombre)
        {
            Nombre = nombre;
        }

        // CLAVE: Define lo que se muestra en el CheckedListBox
        public override string ToString()
        {
            return Nombre;
        }
    }
}
