using System;
using System.Collections.Generic;
using System.IO;
using restaurante;

namespace restaurante
{
    internal class Programa
    {
        // Método principal que inicia la ejecución del programa.
        private static void Main(string[] args)
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
            Factura factura = new Factura();
            MenuAdmin menuAdmin = new MenuAdmin();
            
            // Crear algunos productos, clientes, facturas y órdenes para pruebas
        Producto p1 = new Producto(1, "Pizza", 8.5f, 10);
        Producto p2 = new Producto(2, "Hamburguesa", 6.0f, 20);
        Producto p3 = new Producto(3, "Soda", 1.5f, 50);

        Cliente cliente1 = new Cliente(1, "Juan");
        Cliente cliente2 = new Cliente(2, "Maria");

        Orden orden1 = new Orden();
        orden1.AgregarProducto(p1, 2); // Juan compra 2 Pizzas
        orden1.AgregarProducto(p3, 5); // Juan compra 5 Sodas

        Orden orden2 = new Orden();
        orden2.AgregarProducto(p2, 3); // Maria compra 3 Hamburguesas
        orden2.AgregarProducto(p3, 2); // Maria compra 2 Sodas

        Factura factura1 = new Factura(1, orden1, cliente1);
        Factura factura2 = new Factura(2, orden2, cliente2);

        cliente1.Facturas.Add(factura1);
        cliente2.Facturas.Add(factura2);

        // Crear listas de facturas, clientes y productos
        List<Factura> facturas = new List<Factura> { factura1, factura2 };
        List<Cliente> clientes = new List<Cliente> { cliente1, cliente2 };
        List<Producto> productos = new List<Producto> { p1, p2, p3 };

        // Crear instancia de AnaliticaRestaurante
        AnaliticaRestaurante analitica = new AnaliticaRestaurante(facturas, clientes, productos);
        
        // Mostrar analítica del restaurante
        analitica.MostrarAnalitica();

            // Muestra el logo del sistema al iniciar.
            Console.WriteLine( "        (O O) ");
            Console.WriteLine(" ---oOo---(_)---oOo---");
            Console.WriteLine(" |     COMIDITA       |");
            Console.WriteLine(" ---------------------");
            Console.WriteLine("       -------");
            Console.WriteLine("        | | |");
            Console.WriteLine("        | | |");
            Console.WriteLine("       oOo oOo");

            // Ciclo principal del programa que muestra el menú de opciones.
            while (true)
            {
                // Muestra las opciones del programa.
                Console.WriteLine("\n⧣₊˚﹒✦₊ ⧣₊˚ 𓂃★ ⸝⸝ ⧣₊˚﹒✦₊ ⧣₊˚");
                Console.WriteLine("   /) /)");
                Console.WriteLine("  (｡•ㅅ•｡)〝₎₎ Menú de Opciones ✦₊");
                Console.WriteLine(". .╭∪─∪────────── ✦ ⁺.");
                Console.WriteLine(". .┊ 1. Imprimir Menú del Restaurante ◟﹫");
                Console.WriteLine(". .┊ 2. Agregar producto a una mesa ꜝꜝ﹒");
                Console.WriteLine(". .┊ 3. Mostrar Reservas ꜝꜝ﹒");
                Console.WriteLine(". .┊ 4. Editar productos de una mesa ⨳゛");
                Console.WriteLine(". .┊ 5. Mostrar factura total ◟ヾ");
                Console.WriteLine(". .┊ 6. Guardar Inventario y Salir");
                Console.WriteLine(". .┊ 7. Mostrar tirilla");
                Console.WriteLine(". .┊ 0. Salir sin Guardar ﹒𐐪 ");
                Console.WriteLine("   ╰───────────── ✦ ⁺.");

                Console.Write("⧣₊˚﹒✦₊ ⧣₊˚ Seleccione una opción: ");

                // Captura la opción elegida por el usuario.
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int opcion))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                    continue;
                }

                // Estructura switch para manejar la opción seleccionada.
                switch (opcion)
                {
                    case 1:
                        // Mostrar productos según la categoría seleccionada.
                        Console.WriteLine("\nSeleccione una categoría:");
                        Console.WriteLine("1. Almuerzos");
                        Console.WriteLine("2. Desayunos");
                        Console.WriteLine("3. Bebidas");
                        Console.WriteLine("4. Postres");
                        Console.WriteLine("5. Ver todo");
                        Console.Write("Opción: ");
                        string catInput = Console.ReadLine();
                        if (!int.TryParse(catInput, out int categoria))
                        {
                            Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                            break;
                        }
                        // Llama al método para mostrar productos de la categoría seleccionada.
                        menuAdmin.VerProductos(categoria);
                        break;

                    case 2:
                        // Llama al método para hacer una reserva.
                        HacerReserva(menuAdmin, factura, inventario);
                        break;

                    case 3:
                        // Muestra todas las reservas actuales.
                        factura.MostrarReservas();
                        break;

                    case 4:
                        // Llama al método para editar un producto dentro de una reserva.
                        EditarProductoEnReserva(factura);
                        break;

                    case 5:
                        // Muestra la factura total de todas las mesas.
                        factura.MostrarFactura();
                        break;

                    case 6:
                        // Guardar inventario y salir
                        ioDatos.GuardarInventarioCSV(inventario);
                        Console.WriteLine("Guardando inventario y saliendo...");
                        Environment.Exit(0);
                        break;
                        case 7:
                        MostrarTirilla(factura);
                        break;

                    case 0:
                        // Sale del programa sin guardar.
                        Console.WriteLine("Saliendo sin guardar...");
                        Environment.Exit(0);
                        break;

                    default:
                        // Opción inválida.
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }

        // Método que gestiona la creación de una reserva.
        private static void HacerReserva(MenuAdmin menuAdmin, Factura factura, List<Producto> inventario)
        {
            // Solicita el ID del producto que se desea agregar a la reserva.
            Console.Write("Ingrese el ID del producto a reservar: ");
            string idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int idProducto))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // Busca el producto por su ID en el menú.
            Producto producto = menuAdmin.BuscarProductoPorId(idProducto);
            
            // Si el producto existe, verifica si hay suficiente cantidad en inventario.
            if (producto != null)
            {
                Console.Write("Ingrese la cantidad a reservar: ");
                string cantInput = Console.ReadLine();
                if (!int.TryParse(cantInput, out int cantidad) || cantidad <= 0)
                {
                    Console.WriteLine("Cantidad inválida.");
                    return;
                }

                // Buscar el producto en el inventario para verificar la cantidad disponible.
                Producto inventarioProducto = inventario.FirstOrDefault(p => p.Id == idProducto);
                if (inventarioProducto == null)
                {
                    Console.WriteLine("Producto no encontrado en el inventario.");
                    return;
                }

                if (inventarioProducto.Cantidad >= cantidad)
                {
                    // Reducir la cantidad en el inventario.
                    inventarioProducto.Cantidad -= cantidad;

                    // Solicita el número de mesa y agrega la reserva.
                    Console.Write("Ingrese el número de la mesa: ");
                    string mesaInput = Console.ReadLine();
                    if (!int.TryParse(mesaInput, out int numeroMesa))
                    {
                        Console.WriteLine("Número de mesa inválido.");
                        // Revertir la cantidad en el inventario.
                        inventarioProducto.Cantidad += cantidad;
                        return;
                    }

                    factura.AgregarReserva(numeroMesa, producto, cantidad);
                    Console.WriteLine("Producto agregado a la reserva.");
                }
                else
                {
                    Console.WriteLine("No hay suficiente inventario para esta reserva.");
                }
            }
            else
            {
                // Si el producto no se encuentra, muestra un mensaje de error.
                Console.WriteLine("Producto no encontrado en el menú.");
            }
        }

        // Método que permite editar un producto en una reserva existente.
        private static void EditarProductoEnReserva(Factura factura)
        {
            // Solicita el número de la mesa cuya reserva se quiere modificar.
            Console.Write("Ingrese el número de la mesa: ");
            string mesaInput = Console.ReadLine();
            if (!int.TryParse(mesaInput, out int mesa))
            {
                Console.WriteLine("Número de mesa inválido.");
                return;
            }

            // Busca la reserva por el número de la mesa.
            Orden orden = factura.BuscarReservaPorMesa(mesa);
            
            // Si la reserva existe, permite editar un producto dentro de la orden.
            if (orden != null)
            {
                Console.Write("Ingrese el ID del producto a editar: ");
                string idProdInput = Console.ReadLine();
                if (!int.TryParse(idProdInput, out int idProd))
                {
                    Console.WriteLine("ID de producto inválido.");
                    return;
                }

                Console.Write("Ingrese el nuevo nombre del producto: ");
                string nuevoNombre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    Console.WriteLine("Nombre inválido.");
                    return;
                }

                Console.Write("Ingrese el nuevo precio del producto: ");
                string precioInput = Console.ReadLine();
                if (!float.TryParse(precioInput, out float nuevoPrecio) || nuevoPrecio < 0)
                {
                    Console.WriteLine("Precio inválido.");
                    return;
                }

                // Llama al método para editar el producto.
                orden.EditarProducto(idProd, nuevoNombre, nuevoPrecio);
            }
            else
            {
                // Si la reserva no se encuentra, muestra un mensaje de error.
                Console.WriteLine("Reserva no encontrada.");
            }
        }
                private static void MostrarTirilla(Factura factura)
        {
            // Solicita el número de mesa para mostrar la tirilla
            Console.Write("Ingrese el número de la mesa para imprimir la tirilla: ");
            string mesaInput = Console.ReadLine();
            
            if (!int.TryParse(mesaInput, out int numeroMesa))
            {
                Console.WriteLine("Número de mesa inválido.");
                return;
            }

            factura.MostrarTirilla(numeroMesa);
        }
    }
}
   

