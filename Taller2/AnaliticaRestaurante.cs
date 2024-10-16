using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurante
{
    public class AnaliticaRestaurante
    {
        private List<Factura> facturas;
        private List<Cliente> clientes;
        private List<Producto> productos;

        // Constructor que recibe la lista de facturas, clientes y productos
        public AnaliticaRestaurante(List<Factura> facturas, List<Cliente> clientes, List<Producto> productos)
        {
            this.facturas = facturas;
            this.clientes = clientes;
            this.productos = productos;
        }

        // 1. Contar facturas pendientes por pagar
        public int ContarFacturasPendientes()
        {
            return facturas.Count(f => !f.esPagada);
        }

        // 2. Obtener mejores compradores
        public List<Cliente> ObtenerMejoresCompradores(int top = 5)
        {
            return clientes.OrderByDescending(c => c.CalcularTotalGastado()).Take(top).ToList();
        }

        // 3. Obtener clientes con más deudas
        public List<Cliente> ObtenerClientesConMasDeudas(int top = 5)
        {
            return clientes.OrderByDescending(c => c.CalcularTotalDeuda()).Take(top).ToList();
        }

        // 4. Obtener productos más vendidos
        public List<Producto> ObtenerProductosMasVendidos(int top = 5)
        {
            return productos.OrderByDescending(p => p.TotalVendidos).Take(top).ToList();
        }

        // 5. Obtener productos menos vendidos
        public List<Producto> ObtenerProductosMenosVendidos(int top = 5)
        {
            return productos.OrderBy(p => p.TotalVendidos).Take(top).ToList();
        }

        // Método para mostrar el análisis de ventas y clientes
        public void MostrarAnalitica()
        {
            Console.WriteLine($"Facturas pendientes: {ContarFacturasPendientes()}");

            Console.WriteLine("\n--- Mejores compradores ---");
            foreach (var cliente in ObtenerMejoresCompradores())
            {
                Console.WriteLine($"Cliente: {cliente.Nombre} - Total gastado: ${cliente.CalcularTotalGastado()}");
            }

            Console.WriteLine("\n--- Clientes con más deudas ---");
            foreach (var cliente in ObtenerClientesConMasDeudas())
            {
                Console.WriteLine($"Cliente: {cliente.Nombre} - Total deuda: ${cliente.CalcularTotalDeuda()}");
            }

            Console.WriteLine("\n--- Productos más vendidos ---");
            foreach (var producto in ObtenerProductosMasVendidos())
            {
                Console.WriteLine($"{producto.nombre} - Vendidos: {producto.TotalVendidos}");
            }

            Console.WriteLine("\n--- Productos menos vendidos ---");
            foreach (var producto in ObtenerProductosMenosVendidos())
            {
                Console.WriteLine($"{producto.nombre} - Vendidos: {producto.TotalVendidos}");
            }
        }
    }
}
