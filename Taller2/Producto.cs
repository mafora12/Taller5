using System;

namespace restaurante
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public int Cantidad { get; set; }

        public Producto(int id, string nombre, float precio, int cantidad)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
        }

        // Constructor sin cantidad para compatibilidad
        public Producto(int id, string nombre, float precio) : this(id, nombre, precio, 0) { }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Precio: {Precio}, Cantidad: {Cantidad}";
        }
    }
}
