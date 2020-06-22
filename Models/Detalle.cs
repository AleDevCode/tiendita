using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiendita.Models
{
    class Detalle
    {   
        public uint Id { get; set; }
        public uint ProductoId { get; set; }
        public Producto Producto { get; set; }
        public uint Cantidad { get; set; }
        public uint VentaId { get; set; }
        public Venta Venta { get; set; }
        public decimal Subtotal { get; set; }


        public override string ToString()
        {
            return $"ID: {Id} - ProductoId: {ProductoId} -Cantidad {Cantidad} - Subtotal: {Subtotal.ToString("N4")}MXN   ";
        }
    }
}
