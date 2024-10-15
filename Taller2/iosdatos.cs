using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


   internal class IODatos {

        public const char SEPARADOR_CSV = ','; // Para separar columnas en CSV

            public void GuardarFacturasCSV(Factura[] facturas)
    {
        string filePath = @"../../../../archivos/facturas.csv"; // Cambia la ruta si es necesario
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("FECHA,NOMBRE1,NOMBRE2,PRECIO1,PRECIO2,MEDIO DE PAGO,ESTADO ACTUAL,NÚMERO FACTURA"); // Encabezados

        foreach (Factura factura in facturas)
        {
            if (factura != null)
            {
                // Formatear la línea para cada factura
                sb.Append(factura.Fecha + ",");
                sb.Append(factura.Productos[0] + ","); // Nombre1
                sb.Append(factura.Productos[1] + ","); // Nombre2
                sb.Append(factura.Precios[0] + ","); // Precio1
                sb.Append(factura.Precios[1] + ","); // Precio2
                sb.Append(factura.Medio_pago + ",");
                sb.Append(factura.Estado_actual + ",");
                sb.Append(factura.Numero_factura);
                sb.AppendLine(); // Nueva línea para la siguiente factura
            }
        }

    // Escribir todo el contenido en el archivo
    try
    {
        File.WriteAllText(filePath, sb.ToString());
    }
    catch (Exception e)
    {
        Console.WriteLine("Error al guardar las facturas: " + e.Message);
    }
}

        public Factura[] CargarFacturasCSV()
        {
            Factura[] facturas = null;

            try
            {
                // Leer todas las líneas del archivo CSV
                string[] lineas = File.ReadAllLines(@"../../archivo/facturas.csv");
                Console.WriteLine("Cargando facturas desde CSV...");
                facturas = ProcesarLineas(lineas);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al cargar el archivo de facturas: " + e.Message);
            }

            return facturas;
        }

        public Factura[] ProcesarLineas(string[] lineas)
        {
            Factura[] facturas = new Factura[lineas.Length];

            for (int i = 1; i < lineas.Length; i++) // Saltar el encabezado
            {
                
                string[] temp = lineas[i].Split(SEPARADOR_CSV);
                Factura factura = new();
                factura.Fecha = temp[0];  
                string[] nombres = temp[1].Split('-');
                string[] precios = temp[2].Split('#');
                factura.AgregarProductos(nombres, precios);
                factura.Medio_pago = temp[3];
                factura.Estado_actual = int.Parse(temp[4]);
                factura.Numero_factura = int.Parse(temp[5]);
                facturas[i] = factura;
            }

            return facturas;
        }

        public string FormatearLineasFacturas(Factura[] facturas)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("FECHA,NOMBRE PRODUCTOS,PRECIO,MEDIO DE PAGO,Estado Actual,Numero Factura");

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

            public List<Producto> CargarInventarioCSV()
    {
        string filePath = @"../../../../archivos/inventario.csv"; // Cambia la ruta si es necesario
        if (!File.Exists(filePath))
        {
            Console.WriteLine("El archivo de inventario no existe.");
            return new List<Producto>(); // Devolver una lista vacía si no hay productos
        }

        string[] lineas = File.ReadAllLines(filePath);
        List<Producto> inventario = new List<Producto>();

        for (int i = 1; i < lineas.Length; i++) // Saltar encabezado
        {
            // Llama al método CargarProducto para crear un objeto Producto
            Producto producto = CargarProducto(lineas[i]);
            if (producto != null)
            {
                inventario.Add(producto);
            }
        }

        return inventario; // Devolver la lista de productos
    }

    private Producto CargarProducto(string linea)
    {
        string[] datos = linea.Split(',');
        if (datos.Length < 3) return null; // Asegúrate de que hay suficientes datos

        int precio = int.Parse(datos[0].Trim());
        string nombre = (datos[1].Trim());
        int cantidad = int.Parse(datos[2].Trim());

        return new Producto(precio, nombre, cantidad);
    }

       
    }

