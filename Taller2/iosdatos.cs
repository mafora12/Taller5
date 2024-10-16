using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace restaurante
{
    internal class IODatos
    {
        public const char SEPARADOR_CSV = ','; // Separador para CSV

        private string rutaInventario = Path.Combine("archivos", "inventario.csv"); // Ruta relativa para inventario
        // private string rutaFacturas = Path.Combine("archivos", "facturas.csv"); // Ruta relativa para facturas (si es necesario)

        // Método para guardar el inventario en un archivo CSV
        public void GuardarInventarioCSV(List<Producto> inventario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID,NOMBRE,PRECIO,CANTIDAD"); // Encabezados

            foreach (Producto producto in inventario)
            {
                if (producto != null)
                {
                    sb.Append(producto.Id + SEPARADOR_CSV);
                    sb.Append(producto.nombre + SEPARADOR_CSV);
                    sb.Append(producto.precio + SEPARADOR_CSV);
                    sb.Append(producto.Cantidad);
                    sb.AppendLine();
                }
            }

            try
            {
                File.WriteAllText(rutaInventario, sb.ToString());
                Console.WriteLine("Inventario guardado exitosamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al guardar el inventario: " + e.Message);
            }
        }

        // Método para cargar el inventario desde un archivo CSV
        public List<Producto> CargarInventarioCSV()
        {
            if (!File.Exists(rutaInventario))
            {
                Console.WriteLine("El archivo de inventario no existe. Se creará uno nuevo al guardar.");
                return new List<Producto>(); // Retorna una lista vacía si no hay productos
            }

            try
            {
                string[] lineas = File.ReadAllLines(rutaInventario);
                List<Producto> inventario = new List<Producto>();

                for (int i = 1; i < lineas.Length; i++) // Saltar el encabezado
                {
                    Producto producto = CargarProducto(lineas[i]);
                    if (producto != null)
                    {
                        inventario.Add(producto);
                    }
                }

                Console.WriteLine("Inventario cargado correctamente.");
                return inventario; // Retornar la lista de productos
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al cargar el inventario: " + e.Message);
                return new List<Producto>();
            }
        }

        // Método para cargar un producto desde una línea del CSV
        private Producto CargarProducto(string linea)
        {
            string[] datos = linea.Split(SEPARADOR_CSV);
            if (datos.Length < 4) 
            {
                Console.WriteLine("Línea de inventario mal formateada.");
                return null; // Verificar que haya suficientes datos
            }

            if (!int.TryParse(datos[0].Trim(), out int id))
            {
                Console.WriteLine("ID de producto inválido.");
                return null;
            }

            string nombre = datos[1].Trim();

            if (!float.TryParse(datos[2].Trim(), out float precio))
            {
                Console.WriteLine("Precio de producto inválido.");
                return null;
            }

            if (!int.TryParse(datos[3].Trim(), out int cantidad))
            {
                Console.WriteLine("Cantidad de producto inválida.");
                return null;
            }

            return new Producto(id, nombre, precio, cantidad);
        }

        // Métodos para Facturas (si es necesario)
        // Puedes implementar estos métodos según cómo desees gestionar las facturas
    }
}
