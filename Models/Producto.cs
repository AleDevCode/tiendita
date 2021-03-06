﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Models
{
    class Producto
    {
        public uint Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tamano { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public uint Stock { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} - {Nombre} Precio: {Precio.ToString("N4")}MXN";
        }
    }
}
