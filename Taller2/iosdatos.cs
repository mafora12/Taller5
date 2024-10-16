/*using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

 namespace restaurante
 {
   internal class IODatos
{
    public const char SEPARADOR_CSV = ','; // Separador para CSV

    // Método para guardar facturas en un archivo CSV
    public void GuardarFacturasCSV(Factura[] facturas)
{
    string filePath = @"../../../../archivos/facturas.csv"; // Cambia la ruta según sea necesario
    StringBuilder sb = new StringBuilder();
    sb.AppendLine("FECHA,NOMBRE1,NOMBRE2,PRECIO1,PRECIO2,MEDIO DE PAGO,ESTADO ACTUAL,NUMERO FACTURA"); // Encabezados

    foreach (Factura factura in facturas)
    {
        if (factura != null)
        {
            sb.Append(factura.Fecha + SEPARADOR_CSV);

            // Verifica si hay suficientes productos antes de acceder
            if (factura.Producto.Count > 0)
            {
                sb.Append(factura.Producto[0].nombre + SEPARADOR_CSV); // Nombre1
                sb.Append(factura.Producto[0].precio + SEPARADOR_CSV); // Precio1
            }
            else
            {
                sb.Append("N/A" + SEPARADOR_CSV); // Si no hay productos, se pone "N/A"
                sb.Append("0" + SEPARADOR_CSV);   // Precio "0"
            }

            if (factura.Productos.Count > 1)
            {
                sb.Append(factura.Productos[1].nombre + SEPARADOR_CSV); // Nombre2
                sb.Append(factura.Productos[1].precio + SEPARADOR_CSV); // Precio2
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
        string filePath = @"../../../../archivos/facturas.csv"; // Asegúrate que la ruta sea correcta

        try
        {
            string[] lineas = File.ReadAllLines(filePath);
            Console.WriteLine("Cargando facturas desde CSV...");
            facturas = ProcesarLineas(lineas);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al cargar el archivo de facturas: " + e.Message);
        }

        return facturas;
    }

    // Procesar las líneas del archivo CSV y convertirlas en objetos Factura
    public Factura[] ProcesarLineas(string[] lineas)
    {
        Factura[] facturas = new Factura[lineas.Length - 1]; // El -1 es para ignorar el encabezado

        for (int i = 1; i < lineas.Length; i++) // Saltar el encabezado
        {
            string[] temp = lineas[i].Split(SEPARADOR_CSV);
            Factura factura = new Factura();
            factura.Fecha = temp[0];
            string[] nombres = new[] { temp[1], temp[2] };
            string[] precios = new[] { temp[3], temp[4] };
            factura.AgregarProductos(nombres, precios);
            factura.Medio_pago = temp[5];
            factura.Estado_actual = int.Parse(temp[6]);
            factura.Numero_factura = int.Parse(temp[7]);
            facturas[i - 1] = factura;
        }

        return facturas;
    }

    // Formatear las facturas en texto para CSV
    public string FormatearLineasFacturas(Factura[] facturas)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("FECHA,NOMBRE PRODUCTOS,PRECIO,MEDIO DE PAGO,ESTADO ACTUAL,NUMERO FACTURA");

        foreach (Factura f in facturas)
        {
            if (f != null)
            {
                sb.Append(f.Fecha + SEPARADOR_CSV);
                sb.Append(f.formatearProductos() + SEPARADOR_CSV);
                sb.Append(f.formatearPrecios() + SEPARADOR_CSV);
                sb.Append(f.Medio_pago.Trim() + SEPARADOR_CSV);
                sb.Append(f.Estado_actual + SEPARADOR_CSV);
                sb.Append(f.Numero_factura);
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    // Método para cargar el inventario desde un archivo CSV
    public List<Producto> CargarInventarioCSV()
    {
        string filePath = @"../../../../archivos/inventario.csv"; // Cambia la ruta si es necesario

        if (!File.Exists(filePath))
        {
            Console.WriteLine("El archivo de inventario no existe.");
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

        return inventario; // Retornar la lista de productos
    }

    // Método para cargar un producto desde una línea del CSV
    private Producto CargarProducto(string linea)
    {
        string[] datos = linea.Split(SEPARADOR_CSV);
        if (datos.Length < 3) return null; // Verificar que haya suficientes datos

        int id = int.Parse(datos[0].Trim());
        string nombre = datos[1].Trim();
        float precio = float.Parse(datos[2].Trim());

        return new Producto(id, nombre, precio);
    }
       
    }
}
*/
