namespace restaurante
{
    internal class IODatos
    {
        public const char SEPARADOR_CSV = ','; // Separador para CSV

        // Método para guardar facturas en un archivo CSV
        public void GuardarFacturasCSV(Factura[] facturas)
        {
            string filePath = Path.Combine("archivos", "facturas.csv"); // Ruta relativa
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("FECHA,NOMBRE1,NOMBRE2,PRECIO1,PRECIO2,MEDIO DE PAGO,ESTADO ACTUAL,NUMERO FACTURA"); // Encabezados

            foreach (Factura factura in facturas)
            {
                if (factura != null)
                {
                    sb.Append(factura.Fecha + SEPARADOR_CSV);

                    // Verifica si hay suficientes productos antes de acceder
                    if (factura.Productos.Count > 0)
                    {
                        sb.Append(factura.Productos[0].Nombre + SEPARADOR_CSV); // Nombre1
                        sb.Append(factura.Productos[0].Precio + SEPARADOR_CSV); // Precio1
                    }
                    else
                    {
                        sb.Append("N/A" + SEPARADOR_CSV); // Si no hay productos, se pone "N/A"
                        sb.Append("0" + SEPARADOR_CSV);   // Precio "0"
                    }

                    if (factura.Productos.Count > 1)
                    {
                        sb.Append(factura.Productos[1].Nombre + SEPARADOR_CSV); // Nombre2
                        sb.Append(factura.Productos[1].Precio + SEPARADOR_CSV); // Precio2
                    }
                    else
                    {
                        sb.Append("N/A" + SEPARADOR_CSV); // Si solo hay un producto, se pone "N/A"
                        sb.Append("0" + SEPARADOR_CSV);   // Precio "0"
                    }

                    sb.Append(factura.Medio_pago + SEPARADOR_CSV);
                    sb.Append(factura.Estado_actual + SEPARADOR_CSV);
                    sb.Append(factura.Numero_factura);
                    sb.AppendLine();
                }
            }

            // Guardar en archivo
            try
            {
                File.WriteAllText(filePath, sb.ToString());
                Console.WriteLine("Facturas guardadas exitosamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al guardar las facturas: " + e.Message);
            }
        }

        // Método para cargar facturas desde un archivo CSV
        public Factura[] CargarFacturasCSV()
        {
            Factura[] facturas = null;
            string filePath = Path.Combine("archivos", "facturas.csv"); // Ruta relativa

            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("El archivo de facturas no existe. Se creará uno nuevo al guardar.");
                    return new Factura[0];
                }

                string[] lineas = File.ReadAllLines(filePath);
                Console.WriteLine("Cargando facturas desde CSV...");
                facturas = ProcesarLineas(lineas);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al cargar el archivo de facturas: " + e.Message);
                facturas = new Factura[0];
            }

            return facturas;
        }

        // Procesar las líneas del archivo CSV y convertirlas en objetos Factura
        public Factura[] ProcesarLineas(string[] lineas)
        {
            if (lineas.Length <= 1)
                return new Factura[0]; // No hay facturas

            Factura[] facturas = new Factura[lineas.Length - 1]; // El -1 es para ignorar el encabezado

            for (int i = 1; i < lineas.Length; i++) // Saltar el encabezado
            {
                string[] temp = lineas[i].Split(SEPARADOR_CSV);
                if (temp.Length < 8)
                {
                    Console.WriteLine($"Línea {i + 1} mal formateada. Ignorada.");
                    continue;
                }

                Factura factura = new Factura();
                factura.Fecha = temp[0];
                string[] nombres = new[] { temp[1], temp[3] }; // Nombres1 y Nombre2
                string[] precios = new[] { temp[3], temp[4] }; // Precios1 y Precio2
                factura.AgregarProductos(nombres, precios);
                factura.Medio_pago = temp[5];
                if (int.TryParse(temp[6], out int estado))
                {
                    factura.Estado_actual = estado;
                }
                else
                {
                    factura.Estado_actual = 0;
                }

                if (int.TryParse(temp[7], out int numero))
                {
                    factura.Numero_factura = numero;
                }
                else
                {
                    factura.Numero_factura = 0;
                }

                facturas[i - 1] = factura;
            }

            return facturas;
        }

        // Método para guardar el inventario en un archivo CSV
        public void GuardarInventarioCSV(List<Producto> inventario)
        {
            string filePath = Path.Combine("archivos", "inventario.csv"); // Ruta relativa
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID,NOMBRE,PRECIO,CANTIDAD"); // Encabezados

            foreach (Producto producto in inventario)
            {
                if (producto != null)
                {
                    sb.Append(producto.Id + SEPARADOR_CSV);
                    sb.Append(producto.Nombre + SEPARADOR_CSV);
                    sb.Append(producto.Precio + SEPARADOR_CSV);
                    sb.Append(producto.Cantidad);
                    sb.AppendLine();
                }
            }

            try
            {
                File.WriteAllText(filePath, sb.ToString());
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
            string filePath = Path.Combine("archivos", "inventario.csv"); // Ruta relativa

            if (!File.Exists(filePath))
            {
                Console.WriteLine("El archivo de inventario no existe. Se creará uno nuevo al guardar.");
                return new List<Producto>(); // Retorna una lista vacía si no hay productos
            }

            string[] lineas = File.ReadAllLines(filePath);
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

        // Método para cargar un producto desde una línea del CSV
        private Producto CargarProducto(string linea)
        {
            string[] datos = linea.Split(SEPARADOR_CSV);
            if (datos.Length < 4) return null; // Verificar que haya suficientes datos

            if (!int.TryParse(datos[0].Trim(), out int id))
                return null;
            string nombre = datos[1].Trim();
            if (!float.TryParse(datos[2].Trim(), out float precio))
                return null;
            if (!int.TryParse(datos[3].Trim(), out int cantidad))
                return null;

            return new Producto(id, nombre, precio, cantidad);
        }
    }
}

