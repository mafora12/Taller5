using System;
using System.Collections.Generic;
using System.Linq;
using restaurante;

namespace restaurante
{


    internal class Programa
{
    // Método principal que inicia la ejecución del programa.
    private static void Main(string[] args)
    {
        // Instancia de la clase IU para manejar la interfaz de usuario.
        IU iU = new IU();
        // Instancia de la clase MenuAdmin que gestiona los productos del menú.
        MenuAdmin menuAdmin = new MenuAdmin();
        // Instancia de la clase Factura que maneja las reservas y facturas de las mesas.
        Factura factura = new Factura();
        
        // Muestra el logo del sistema al iniciar.
        iU.Logo();        

        // Ciclo principal del programa que muestra el menú de opciones.
        while (true)
        {
            // Muestra las opciones del programa.
            iU.OpcionesPrograma();

            // Captura la opción elegida por el usuario.
            int opcion = int.Parse(Console.ReadLine());

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
                    int categoria = int.Parse(Console.ReadLine());
                    // Llama al método para mostrar productos de la categoría seleccionada.
                    menuAdmin.VerProductos(categoria);
                    break;
                case 2:
                    // Llama al método para hacer una reserva.
                    HacerReserva(menuAdmin, factura);
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
                    // Otra opción para hacer una reserva (misma funcionalidad que la opción 2).
                    HacerReserva(menuAdmin, factura);
                    break;
                case 6:
                    // Muestra la factura total de todas las mesas.
                    factura.MostrarFactura();
                    break;
                case 7:
                    // Llama al método para editar un producto en el menú.
                    EditarProductoEnMenu(menuAdmin);
                    break;
                case 0:
                    // Sale del programa.
                    Environment.Exit(0);
                    break;
                default:
                    // Opción inválida.
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    // Método que gestiona la creación de una reserva.
    private static void HacerReserva(MenuAdmin menuAdmin, Factura factura)
    {
        // Solicita el ID del producto que se desea agregar a la reserva.
        Console.Write("Ingrese el ID del producto a reservar: ");
        int idProducto = int.Parse(Console.ReadLine());
        // Busca el producto por su ID.
        Producto producto = menuAdmin.BuscarProductoPorId(idProducto);
        
        // Si el producto existe, solicita el número de mesa y lo agrega a la reserva.
        if (producto != null)
        {
            Console.Write("Ingrese el número de la mesa: ");
            int numeroMesa = int.Parse(Console.ReadLine());
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
        int mesa = int.Parse(Console.ReadLine());
        // Busca la reserva por el número de la mesa.
        Orden orden = factura.BuscarReservaPorMesa(mesa);
        
        // Si la reserva existe, permite editar un producto dentro de la orden.
        if (orden != null)
        {
            Console.Write("Ingrese el ID del producto a editar: ");
            int idProd = int.Parse(Console.ReadLine());
            Console.Write("Ingrese el nuevo nombre del producto: ");
            string nuevoNombre = Console.ReadLine();
            Console.Write("Ingrese el nuevo precio del producto: ");
            float nuevoPrecio = float.Parse(Console.ReadLine());
            // Llama al método para editar el producto.
            orden.EditarProducto(idProd, nuevoNombre, nuevoPrecio);
        }
        else
        {
            // Si la reserva no se encuentra, muestra un mensaje de error.
            Console.WriteLine("Reserva no encontrada.");
        }
    }

    // Método que permite editar un producto del menú.
    private static void EditarProductoEnMenu(MenuAdmin menuAdmin)
    {
        // Solicita el ID del producto que se quiere editar.
        Console.Write("Ingrese el ID del producto a editar: ");
        int idProd = int.Parse(Console.ReadLine());
        // Solicita los nuevos detalles del producto.
        Console.Write("Ingrese el nuevo nombre del producto: ");
        string nuevoNombre = Console.ReadLine();
        Console.Write("Ingrese el nuevo precio del producto: ");
        float nuevoPrecio = float.Parse(Console.ReadLine());
        // Llama al método para editar el producto en el menú.
        menuAdmin.EditarProductoEnMenu(idProd, nuevoNombre, nuevoPrecio);
    }
}
 }