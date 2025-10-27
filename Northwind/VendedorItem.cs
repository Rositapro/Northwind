using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind
{
    public class VendedorItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public VendedorItem(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        // ESTO ES CLAVE: Define lo que se muestra en el CheckedListBox
        public override string ToString()
        {
            return Nombre;
        }
    }
}
