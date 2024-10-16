using System;
using System.Collections.Generic;
using System.IO;

namespace restaurante
{
    class Program
    {
        static void Main(string[] args)
        {
            // Asegurarse de que la carpeta 'archivos' exista
            string carpetaArchivos = "archivos";
            if (!Directory.Exists(carpetaArchivos))
            {
                Directory.CreateDirectory(carpetaArchivos);
                Console.WriteLine("Carpeta 'archivos' creada.");
            }

            IODatos ioDatos = new IODatos();
            List<Producto> inventario = ioDatos.CargarInventarioCSV();
            Factura[] facturas = ioDatos.CargarFacturasCSV();

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n--- Menú Principal ---");
                Console.WriteLine("1. Realizar una venta");
                Console.WriteLine("2. Guardar Inventario");
                Console.WriteLine("3. Mostrar Inventario");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RealizarVenta(inventario, ref facturas);
                        break;
                    case "2":
                        ioDatos.GuardarInventarioCSV(inventario);
                        ioDatos.GuardarFacturasCSV(facturas);
                        break;
                    case "3":
                        MostrarInventario(inventario);
                        break;
                    case "4":
                        // Guardar antes de salir
                        ioDatos.GuardarInventarioCSV(inventario);
                        ioDatos.GuardarFacturasCSV(facturas);
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }

            Console.WriteLine("Programa finalizado.");
        }

        static void MostrarInventario(List<Producto> inventario)
        {
            Console.WriteLine("\n--- Inventario ---");
            foreach (var producto in inventario)
            {
                Console.WriteLine(producto);
            }
        }

        static void RealizarVenta(List<Producto> inventario, ref Factura[] facturas)
        {
            Console.Write("Ingrese el ID del producto vendido: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Producto producto = inventario.Find(p => p.Id == id);
                if (producto != null)
                {
                    Console.Write("Ingrese la cantidad vendida: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidadVendida))
                    {
                        if (cantidadVendida <= 0)
                        {
                            Console.WriteLine("La cantidad vendida debe ser mayor que cero.");
                            return;
                        }

                        if (producto.Cantidad >= cantidadVendida)
                        {
                            producto.Cantidad -= cantidadVendida;
                            Console.WriteLine($"Venta registrada: {cantidadVendida} unidad(es) de {producto.Nombre}.");

                            // Crear una nueva factura
                            Factura nuevaFactura = new Factura
                            {
                                Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                                Medio_pago = ObtenerMedioPago(),
                                Estado_actual = 1, // Por ejemplo, 1 para "Pagada"
                                Numero_factura = GenerarNumeroFactura(facturas)
                            };

                            nuevaFactura.Productos.Add(new Producto(producto.Id, producto.Nombre, producto.Precio, cantidadVendida));

                            // Agregar la nueva factura al array
                            facturas = AgregarFactura(facturas, nuevaFactura);
                        }
                        else
                        {
                            Console.WriteLine("No hay suficiente inventario para esta venta.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cantidad inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("Producto no encontrado en el inventario.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        static string ObtenerMedioPago()
        {
            Console.WriteLine("Seleccione el medio de pago:");
            Console.WriteLine("1. Efectivo");
            Console.WriteLine("2. Tarjeta");
            Console.WriteLine("3. Otro");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    return "Efectivo";
                case "2":
                    return "Tarjeta";
                case "3":
                    Console.Write("Ingrese el medio de pago: ");
                    return Console.ReadLine();
                default:
                    Console.WriteLine("Opción no válida. Se asignará 'Otro'.");
                    return "Otro";
            }
        }

        static int GenerarNumeroFactura(Factura[] facturas)
        {
            if (facturas.Length == 0)
                return 1001; // Número inicial

            int maxNumero = 1000;
            foreach (var factura in facturas)
            {
                if (factura.Numero_factura > maxNumero)
                    maxNumero = factura.Numero_factura;
            }
            return maxNumero + 1;
        }

        static Factura[] AgregarFactura(Factura[] facturas, Factura nuevaFactura)
        {
            List<Factura> listaFacturas = new List<Factura>(facturas);
            listaFacturas.Add(nuevaFactura);
            return listaFacturas.ToArray();
        }
    }
}
