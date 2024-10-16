using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace restaurante
{
    public class InventarioManager
    {
        private const char SEPARADOR_CSV = ','; // Separador para CSV
        private string filePath;

        // Constructor que recibe la ruta del archivo de inventario
        public InventarioManager(string rutaArchivo)
        {
            filePath = rutaArchivo;
        }

        // Método para cargar el inventario desde un archivo CSV
        public void CargarInventario(MenuAdmin menuAdmin)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("El archivo de inventario no existe. Se utilizarán los productos predeterminados.");
                return;
            }

            try
            {
                var lineas = File.ReadAllLines(filePath);
                foreach (var linea in lineas.Skip(1)) // Ignorar encabezado
                {
                    var datos = linea.Split(SEPARADOR_CSV);
                    if (datos.Length < 5)
                        continue;

                    if (!int.TryParse(datos[0], out int id))
                        continue;
                    string categoria = datos[1].Trim().ToLower();
                    string nombre = datos[2].Trim();
                    if (!float.TryParse(datos[3], out float precio))
                        continue;
                    if (!int.TryParse(datos[4], out int cantidad))
                        continue;

                    Producto producto = new Producto(id, nombre, precio, cantidad);

                    switch (categoria)
                    {
                        case "almuerzo":
                            menuAdmin.ObtenerAlmuerzos().Add(producto);
                            break;
                        case "desayuno":
                            menuAdmin.ObtenerDesayunos().Add(producto);
                            break;
                        case "bebida":
                            menuAdmin.ObtenerBebidas().Add(producto);
                            break;
                        case "postre":
                            menuAdmin.ObtenerPostres().Add(producto);
                            break;
                        default:
                            Console.WriteLine($"Categoría desconocida: {categoria}");
                            break;
                    }
                }

                Console.WriteLine("Inventario cargado desde CSV exitosamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al cargar el inventario: {e.Message}");
            }
        }

        // Método para guardar el inventario en un archivo CSV
        public void GuardarInventario(MenuAdmin menuAdmin)
        {
            var sb = new StringBuilder();
            sb.AppendLine("ID,Categoria,Nombre,Precio,Cantidad");

            foreach (var producto in menuAdmin.ObtenerAlmuerzos())
            {
                sb.AppendLine($"{producto.Id},Almuerzo,{producto.Nombre},{producto.precio},{producto.Cantidad}");
            }
            foreach (var producto in menuAdmin.ObtenerDesayunos())
            {
                sb.AppendLine($"{producto.Id},Desayuno,{producto.Nombre},{producto.precio},{producto.Cantidad}");
            }
            foreach (var producto in menuAdmin.ObtenerBebidas())
            {
                sb.AppendLine($"{producto.Id},Bebida,{producto.Nombre},{producto.precio},{producto.Cantidad}");
            }
            foreach (var producto in menuAdmin.ObtenerPostres())
            {
                sb.AppendLine($"{producto.Id},Postre,{producto.Nombre},{producto.precio},{producto.Cantidad}");
            }

            try
            {
                File.WriteAllText(filePath, sb.ToString());
                Console.WriteLine("Inventario guardado en CSV exitosamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al guardar el inventario: {e.Message}");
            }
        }
    }
}
