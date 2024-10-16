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
            // Instancia de la clase MenuAdmin que gestiona los productos del menú.
            MenuAdmin menuAdmin = new MenuAdmin();
            // Instancia de la clase Factura que maneja las reservas y facturas de las mesas.
            Factura factura = new Factura();

            // Instancia de InventarioManager con la ruta del archivo de inventario
            string rutaInventario = Path.Combine("archivos", "inventario.csv");
            InventarioManager inventarioManager = new InventarioManager(rutaInventario);

            // Cargar el inventario desde el archivo CSV al iniciar el programa
            inventarioManager.CargarInventario(menuAdmin);

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
                Console.WriteLine("⧣₊˚﹒✦₊ ⧣₊˚ 𓂃★ ⸝⸝ ⧣₊˚﹒✦₊ ⧣₊˚");
                Console.WriteLine("   /) /)");
                Console.WriteLine("  (｡•ㅅ•｡)〝₎₎ Menú de Opciones ✦₊");
                Console.WriteLine(". .╭∪─∪────────── ✦ ⁺.");
                Console.WriteLine(". .┊ 1. Imprimir Menú del Restaurante ◟﹫");
                Console.WriteLine(". .┊ 2. Agregar producto al menú ﹒𐐪 ");
                Console.WriteLine(". .┊ 3. Agregar producto a una mesa ꜝꜝ﹒");
                Console.WriteLine(". .┊ 4. Editar productos de una mesa ⨳゛");
                Console.WriteLine(". .┊ 5. Imprimir cuenta de una mesa ◟ヾ");
                Console.WriteLine(". .┊ 6. Imprimir factura con impuestos y propina ﹒𐐪");
                Console.WriteLine(". .┊ 7. Guardar inventario ◟ヾ");
                Console.WriteLine(". .┊ 8. Cargar facturas ◟ヾ");
                Console.WriteLine(". .┊ 0. Salir ﹒𐐪 ");
                Console.WriteLine("   ╰───────────── ✦ ⁺.");

                Console.Write("⧣₊˚﹒✦₊ ⧣₊˚ Seleccione una opción: ");
                //Console.Write("Seleccione una opción: "); // Eliminado para evitar duplicación

                // Captura la opción elegida por el usuario.
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int opcion))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
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
                        string categoriaInput = Console.ReadLine();
                        if (!int.TryParse(categoriaInput, out int categoria))
                        {
                            Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
                            break;
                        }
                        // Llama al método para mostrar productos de la categoría seleccionada.
                        menuAdmin.VerProductos(categoria);
                        break;
                    case 2:
                        // Llama al método para editar un producto en el menú.
                        EditarProductoEnMenu(menuAdmin);
                        break;
                    case 3:
                        // Llama al método para hacer una reserva.
                        HacerReserva(menuAdmin, factura);
                        break;
                    case 4:
                        // Llama al método para editar un producto dentro de una reserva.
                        EditarProductoEnReserva(factura);
                        break;
                    case 5:
                        // Otra opción para hacer una reserva (misma funcionalidad que la opción 3).
                        MostrarCuentaDeMesa(factura);
                        break;
                    case 6:
                        // Muestra la factura total de todas las mesas.
                        factura.MostrarFactura();
                        break;
                    case 7:
                        // Guardar el inventario
                        inventarioManager.GuardarInventario(menuAdmin);
                        break;
                    case 8:
                        // Opcional: Implementar carga de facturas si es necesario
                        // Por ahora, se puede omitir o implementar según necesidades
                        Console.WriteLine("Funcionalidad de cargar facturas no implementada.");
                        break;
                    case 0:
                        // Guardar inventario antes de salir
                        inventarioManager.GuardarInventario(menuAdmin);
                        // Sale del programa.
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
        private static void HacerReserva(MenuAdmin menuAdmin, Factura factura)
        {
            // Solicita el ID del producto que se desea agregar a la reserva.
            Console.Write("Ingrese el ID del producto a reservar: ");
            string idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int idProducto))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // Busca el producto por su ID.
            Producto producto = menuAdmin.BuscarProductoPorId(idProducto);
            
            // Si el producto existe, solicita el número de mesa y lo agrega a la reserva.
            if (producto != null)
            {
                Console.Write("Ingrese el número de la mesa: ");
                string mesaInput = Console.ReadLine();
                if (!int.TryParse(mesaInput, out int numeroMesa))
                {
                    Console.WriteLine("Número de mesa inválido.");
                    return;
                }

                factura.AgregarReserva(numeroMesa, producto);
                Console.WriteLine("Producto agregado a la reserva.");
            }
            else
            {
                // Si el producto no se encuentra, muestra un mensaje de error.
                Console.WriteLine("Producto no encontrado.");
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

                Console.Write("Ingrese el nuevo precio del producto: ");
                string precioInput = Console.ReadLine();
                if (!float.TryParse(precioInput, out float nuevoPrecio))
                {
                    Console.WriteLine("Precio inválido.");
                    return;
                }

                // Llama al método para editar el producto.
                orden.EditarProducto(idProd, nuevoNombre, nuevoPrecio);
            }
            else
            {
                // Si la reserva no existe, muestra un mensaje de error.
                Console.WriteLine("Reserva no encontrada.");
            }
        }

        // Método que permite editar un producto del menú.
        private static void EditarProductoEnMenu(MenuAdmin menuAdmin)
        {
            // Solicita el ID del producto que se quiere editar.
            Console.Write("Ingrese el ID del producto a editar: ");
            string idProdInput = Console.ReadLine();
            if (!int.TryParse(idProdInput, out int idProd))
            {
                Console.WriteLine("ID de producto inválido.");
                return;
            }

            // Solicita los nuevos detalles del producto.
            Console.Write("Ingrese el nuevo nombre del producto: ");
            string nuevoNombre = Console.ReadLine();

            Console.Write("Ingrese el nuevo precio del producto: ");
            string precioInput = Console.ReadLine();
            if (!float.TryParse(precioInput, out float nuevoPrecio))
            {
                Console.WriteLine("Precio inválido.");
                return;
            }

            // Llama al método para editar el producto en el menú.
            menuAdmin.EditarProductoEnMenu(idProd, nuevoNombre, nuevoPrecio);
        }

        // Método para mostrar la cuenta de una mesa específica.
        private static void MostrarCuentaDeMesa(Factura factura)
        {
            Console.Write("Ingrese el número de la mesa para mostrar la cuenta: ");
            string mesaInput = Console.ReadLine();
            if (!int.TryParse(mesaInput, out int mesa))
            {
                Console.WriteLine("Número de mesa inválido.");
                return;
            }

            Orden orden = factura.BuscarReservaPorMesa(mesa);
            if (orden != null)
            {
                orden.MostrarOrden();
                float total = orden.CalcularTotal();
                Console.WriteLine($"Total de la mesa {mesa}: ${total}");
            }
            else
            {
                Console.WriteLine("Reserva no encontrada para la mesa especificada.");
            }
        }
    }
}
