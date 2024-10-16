using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurante
{
    public class Factura 
    {
        private Dictionary<int, Orden> reservas;
        public float DescuentoCumpleaños { get; set; }
        public float Impuestos { get; set; }
        public float Propina { get; set; }
            public int NumeroFactura { get; set; }
        public Orden Orden { get; set; }
        public Cliente Cliente { get; set; } // Asociar la factura a un cliente
         public bool esPagada { get; set; } // Estado de la factura

        public Factura()
        {
            reservas = new Dictionary<int, Orden>();
            DescuentoCumpleaños = 0;
            Impuestos = 0;
            Propina = 0;
        }

        // Agregar una reserva (orden) a una mesa específica.
        public void AgregarReserva(int numeroMesa, Producto producto, int cantidad = 1)
        {
            if (!reservas.ContainsKey(numeroMesa))
            {
                reservas[numeroMesa] = new Orden(numeroMesa);
            }
            reservas[numeroMesa].AgregarProducto(producto, cantidad);
        }

        // Buscar una reserva por el número de la mesa.
        public Orden BuscarReservaPorMesa(int numeroMesa)
        {
            return reservas.ContainsKey(numeroMesa) ? reservas[numeroMesa] : null;
        }

        // Editar un producto en una reserva específica.
        public void EditarProductoEnReserva(int numeroMesa, int idProducto, string nuevoNombre, float nuevoPrecio)
        {
            if (reservas.ContainsKey(numeroMesa))
            {
                reservas[numeroMesa].EditarProducto(idProducto, nuevoNombre, nuevoPrecio);
            }
            else
            {
                Console.WriteLine("Mesa no encontrada.");
            }
        }

        // Eliminar un producto de una reserva.
        public void EliminarProductoDeReserva(int numeroMesa, int idProducto)
        {
            if (reservas.ContainsKey(numeroMesa))
            {
                reservas[numeroMesa].EliminarProducto(idProducto);
            }
            else
            {
                Console.WriteLine("Mesa no encontrada.");
            }
        }

        // Mostrar todas las reservas.
        public void MostrarReservas()
        {
            foreach (var reserva in reservas)
            {
                Console.WriteLine($"\nMesa {reserva.Key}:");
                reserva.Value.MostrarOrden();
            }
        }

        // Mostrar factura detallada (tirilla) para una mesa específica.
        public void MostrarTirilla(int numeroMesa)
        {
            var orden = BuscarReservaPorMesa(numeroMesa);
            if (orden == null)
            {
                Console.WriteLine("No hay reservas para esta mesa.");
                return;
            }

            Console.WriteLine("\n--- TIRILLA ---");
            Console.WriteLine($"Mesa: {numeroMesa}");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Producto\tCantidad\tPrecio Unitario\tTotal");

            float totalGeneral = 0;


            // Calcular descuentos, impuestos y propina
            float descuentoAplicado = (DescuentoCumpleaños > 0) ? (totalGeneral * DescuentoCumpleaños / 100) : 0;
            float totalConDescuento = totalGeneral - descuentoAplicado;
            float totalConImpuestos = totalConDescuento + Impuestos;

            Console.WriteLine("------------------------------");
            Console.WriteLine($"Subtotal: \t\t\t${totalGeneral:F2}");

            if (DescuentoCumpleaños > 0)
                Console.WriteLine($"Descuento ({DescuentoCumpleaños}%): \t-${descuentoAplicado:F2}");

            Console.WriteLine($"Impuestos: \t\t\t${Impuestos:F2}");

            if (Propina > 0)
                Console.WriteLine($"Propina: \t\t\t${Propina:F2}");

            Console.WriteLine("------------------------------");
            Console.WriteLine($"Total a Pagar: \t\t\t${totalConImpuestos + Propina:F2}");
        }

        // Mostrar factura total para todas las mesas.
        public void MostrarFactura()
        {
            float total = 0;

            foreach (var reserva in reservas)
            {
                Console.WriteLine($"\nMesa {reserva.Key}:");
                reserva.Value.MostrarOrden();
                total += reserva.Value.CalcularTotal();
            }

            Console.WriteLine($"\nTotal general de todas las mesas: ${total:F2}");
             // Arte ASCII de la factura
            Console.WriteLine("                                       ||      |    |    |       | ");
            Console.WriteLine("                                        ||     |    |    |      |");
            Console.WriteLine("                                         ||    |    |    |     |");
            Console.WriteLine("                                          |    |    |    |    |");
            Console.WriteLine("                                           |   |    |    |   |");
            Console.WriteLine("                                            |  |    |    |  |");
            Console.WriteLine("                                          ||                 ||");
            Console.WriteLine("                                         |                     |");
            Console.WriteLine("                                         |    |||       |||     |");
            Console.WriteLine("                                         |   |   |     |   |    |");
            Console.WriteLine("                                         |   ||||      ||||     |");
            Console.WriteLine("                                          ||                 ||");
            Console.WriteLine("                                                ||      ||");
            Console.WriteLine("                             || ||            ||         ||");
            Console.WriteLine("                    |||  ||| |   |            |           |");
            Console.WriteLine("         |||| |||| |  |||   ||   | |||| ||||  |           |  |||| |||| ||||| |||| |||| |||| ||||");
            Console.WriteLine("                   |  |||| ||||  |            ||          |");
            Console.WriteLine("                                               || ||||| ||");
            Console.WriteLine("|                                                (O O)                                           ");
            Console.WriteLine("|                                        ---oOo---(_)---oOo---                                   ");
            Console.WriteLine("|                                        |     COMIDITA       |                                  ");
            Console.WriteLine("|                                        ---------------------");
            Console.WriteLine("|                                               -------");
            Console.WriteLine("|                                                | | |");
            Console.WriteLine("|                                                | | |");
            Console.WriteLine("|                                               oOo oOo");
            Console.WriteLine("|            NOMBRE                     DIRECCIÓN               NIT           NÚMERO                 |");
            Console.WriteLine("|          COMIDITA * Calle 13 #45-67, MEDELLIN, Colombia * 900123456-7 * (+57) 1 555 1234           |");
            Console.WriteLine("|                                                                                                    |");
            Console.WriteLine("|                                                                                                    |");
            Console.WriteLine("|                                                                                                    |");
            Console.WriteLine($"|                                Total general: ${total.ToString("N0")}                             |");
            Console.WriteLine("   |                                                                                            |");
            Console.WriteLine("    |                                                                                          |");
            Console.WriteLine("     |                                                                                        |");
            Console.WriteLine("      |                                                                                      |");
            Console.WriteLine("       |                                                                                    ||");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |               |               |");
            Console.WriteLine("                                    |  |||||||||||  |  |||||||||||  |");
            Console.WriteLine("                                          |    |         |     |");
            Console.WriteLine("                                          |    |         |     |");
            Console.WriteLine("                                          |    |         |     |");
            Console.WriteLine("                                          |    |         |     |");
            Console.WriteLine("                                          |    |         |     |");
            Console.WriteLine("                                          |    |         |     |");
            Console.WriteLine("                                     ||      ||||       ||||      ||");
            Console.WriteLine("                                    |           |       |           |");
            Console.WriteLine("                                   |            |       |            |");
            Console.WriteLine("                                   |            |       |            |");
            Console.WriteLine("                                    |           |       |           ||");
            Console.WriteLine("                                     |          |       |          |");
        }
    
          
           
        
    

    public Factura(int numeroFactura, Orden orden, Cliente cliente)
    {
        NumeroFactura = numeroFactura;
        Orden = orden;
        Cliente = cliente;
        esPagada = false;
    }

    public void MarcarComoPagada()
    {
        esPagada = true;
    }
}

    
}