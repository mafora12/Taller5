using System;
using System.Diagnostics;

namespace restaurante
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class Producto 
    {
        // Propiedades públicas para Id, nombre, precio y cantidad.
        public int Id { get; private set; }     // Id solo lectura desde fuera.
        public string nombre { get; set; }      // El nombre puede ser modificado.
        public float precio { get; set; }       // El precio puede ser modificado.
        public int Cantidad { get; set; }       // Cantidad disponible en inventario.
        public string Nombre { get; internal set; }
        public int TotalVendidos { get; set; } // Para almacenar el total vendido

        // Constructor para inicializar el producto con su Id, nombre, precio y cantidad.

        public Producto(int id, string nombre, float precio, int cantidad)
        {
            Id = id;
            this.nombre = nombre;
            this.precio = precio;
            Cantidad = cantidad;
            TotalVendidos = 0;
        }

        // Constructor sin cantidad para compatibilidad
        public Producto(int id, string nombre, float precio) : this(id, nombre, precio, 0) { }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {nombre}, Precio: ${precio}, Cantidad: {Cantidad}";
        }
           public void RegistrarVenta(int cantidadVendida) => TotalVendidos += cantidadVendida;
    
      private string GetDebuggerDisplay()
        {
            return ToString();
        }
    
    }

 
    


    
}


