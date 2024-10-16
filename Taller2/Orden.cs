using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurante
{
    public class Orden
    {
        private List<Producto> productos;

        // Constructor para inicializar la lista
        public Orden()
        {
            productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto, int cantidad = 1)
        {
            // Verificar si el producto ya existe en la orden
            var prodExistente = productos.FirstOrDefault(p => p.Id == producto.Id);
            if (prodExistente != null)
            {
                prodExistente.Cantidad += cantidad;
            }
            else
            {
                // Crear una copia del producto con la cantidad vendida
                productos.Add(new Producto(producto.Id, producto.nombre, producto.precio, cantidad));
            }
        }

        public void EditarProducto(int idProducto, string nuevoNombre, float nuevoPrecio)
        {
            var producto = productos.FirstOrDefault(p => p.Id == idProducto);
            if (producto != null)
            {
                producto.nombre = nuevoNombre;
                producto.precio = nuevoPrecio;
                Console.WriteLine($"Producto '{producto.nombre}' ha sido actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado en la orden.");
            }
        }

        public void EliminarProducto(int idProducto)
        {
            var producto = productos.FirstOrDefault(p => p.Id == idProducto);
            if (producto != null)
            {
                productos.Remove(producto);
                Console.WriteLine($"Producto '{producto.nombre}' eliminado de la orden.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado en la orden.");
            }
        }

        public void MostrarOrden()
        {
            Console.WriteLine("\n--- ORDEN ---");
            foreach (var producto in productos)
            {
                Console.WriteLine($"ID: {producto.Id} - {producto.nombre} - ${producto.precio} - Cantidad: {producto.Cantidad}");
            }
        }

        public float CalcularTotal()
        {
            return productos.Sum(p => p.precio * p.Cantidad);
        }
    }
}

