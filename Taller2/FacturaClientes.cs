
using System;
using System.Collections.Generic;
using System.Text;

namespace restaurante
{
    public class FacturaCliente
    {
        public int NumeroFactura { get; set; }            // Número único de la factura.
        public DateTime Fecha { get; set; }               // Fecha de la factura.
        public string Estado { get; set; }                // Estado de la factura: Pagada, Cuenta Abierta, Cuenta Pendiente.
        public List<Producto> Productos { get; set; }     // Lista de productos en la factura.
        public float Descuento { get; set; }              // Descuento aplicado.

        // Constructor para inicializar la factura.
        public FacturaCliente(int numeroFactura, DateTime fecha, string estado)
        {
            NumeroFactura = numeroFactura;
            Fecha = fecha;
            Estado = estado;
            Productos = new List<Producto>();
            Descuento = 0;
        }

        // Método para agregar un producto a la factura.
        public void AgregarProducto(Producto producto, int cantidad = 1)
        {
            var productoExistente = Productos.Find(p => p.Id == producto.Id);
            if (productoExistente != null)
            {
                productoExistente.Cantidad += cantidad;
            }
            else
            {
                Productos.Add(new Producto(producto.Id, producto.nombre, producto.precio, cantidad));
            }
        }

        // Método para calcular el total de la factura.
        public float CalcularTotal()
        {
            float total = 0;
            foreach (var producto in Productos)
            {
                total += producto.precio * producto.Cantidad;
            }
            // Aplicar descuento si existe.
            total -= Descuento;
            return total;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Factura Nº: {NumeroFactura}");
            sb.AppendLine($"Fecha: {Fecha.ToShortDateString()}");
            sb.AppendLine($"Estado: {Estado}");
            sb.AppendLine("--- Productos ---");
            foreach (var producto in Productos)
            {
                sb.AppendLine($"ID: {producto.Id} - {producto.nombre} - ${producto.precio} x {producto.Cantidad}");
            }
            sb.AppendLine($"Descuento: ${Descuento}");
            sb.AppendLine($"Total: ${CalcularTotal():F2}");
            return sb.ToString();
        }
    }
}
