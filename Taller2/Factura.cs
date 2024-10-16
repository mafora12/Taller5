using System;
using System.Collections.Generic;
using System.Text;

namespace restaurante
{
    public class Factura
    {
        public string Fecha { get; set; }
        public List<Producto> Productos { get; set; }
        public string Medio_pago { get; set; }
        public int Estado_actual { get; set; }
        public int Numero_factura { get; set; }

        public Factura()
        {
            Productos = new List<Producto>();
        }

        // Método para agregar productos a la factura
        public void AgregarProductos(string[] nombres, string[] precios)
        {
            for (int i = 0; i < nombres.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(nombres[i]) && float.TryParse(precios[i], out float precio))
                {
                    Productos.Add(new Producto(0, nombres[i], precio, 0)); // ID y Cantidad no son relevantes aquí
                }
            }
        }

        // Método para formatear productos para CSV
        public string formatearProductos()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Productos.Count; i++)
            {
                sb.Append(Productos[i].Nombre);
                if (i < Productos.Count - 1)
                {
                    sb.Append(";");
                }
            }
            return sb.ToString();
        }

        // Método para formatear precios para CSV
        public string formatearPrecios()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Productos.Count; i++)
            {
                sb.Append(Productos[i].Precio.ToString("F2"));
                if (i < Productos.Count - 1)
                {
                    sb.Append(";");
                }
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"Fecha: {Fecha}, Productos: {formatearProductos()}, Precios: {formatearPrecios()}, Medio de Pago: {Medio_pago}, Estado: {Estado_actual}, Número Factura: {Numero_factura}";
        }
    }
}

