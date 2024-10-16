using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurante
{
    public class MenuAdmin
    {
        // Listas privadas para almacenar los productos de cada categoría.
        private List<Producto> almuerzo;
        private List<Producto> desayuno;
        private List<Producto> bebida;
        private List<Producto> postre;

        // Constructor que inicializa las listas y carga los productos predeterminados.
        public MenuAdmin()
        {
            almuerzo = new List<Producto>();
            desayuno = new List<Producto>();
            bebida = new List<Producto>();
            postre = new List<Producto>();

            // Cargar productos en las listas correspondientes.
            CargarAlmuerzos();
            CargarDesayunos();
            CargarBebidas();
            CargarPostres();
        }

        // Métodos para cargar productos en cada categoría.
        private void CargarAlmuerzos()
        {
            almuerzo.Add(new Producto(1362, "Pizza mediana", 13.5f, 50));
            almuerzo.Add(new Producto(2321, "Hamburguesa con queso", 10f, 30));
            almuerzo.Add(new Producto(3918, "Spaguettis a la boloñesa", 11.3f, 40));
            almuerzo.Add(new Producto(4382, "Perro con papas fritas", 9.5f, 25));
            almuerzo.Add(new Producto(5019, "Burrito", 9.5f, 35));
            almuerzo.Add(new Producto(6372, "Quesadilla", 5.7f, 60));
        }

        private void CargarDesayunos()
        {
            desayuno.Add(new Producto(1291, "Huevos y tocino", 5.9f, 20));
            desayuno.Add(new Producto(2283, "Cereal", 3.5f, 45));
            desayuno.Add(new Producto(3283, "Panqueques y fruta", 5.5f, 15));
            desayuno.Add(new Producto(4109, "Arepa con queso", 5f, 25));
            desayuno.Add(new Producto(5981, "Empanada", 3.8f, 50));
            desayuno.Add(new Producto(6167, "Buñuelos", 1.5f, 100));
        }

        private void CargarBebidas()
        {
            bebida.Add(new Producto(1019, "Jugo de naranja", 3.2f, 70));
            bebida.Add(new Producto(2811, "Café", 3.6f, 80));
            bebida.Add(new Producto(3222, "Cerveza", 4.9f, 60));
            bebida.Add(new Producto(4990, "Gaseosa", 5.9f, 90));
            bebida.Add(new Producto(5181, "Limonada", 6.3f, 40));
            bebida.Add(new Producto(6098, "Jugo del día", 3f, 30));
        }

        private void CargarPostres()
        {
            postre.Add(new Producto(1234, "Pastel de chocolate", 6.8f, 20));
            postre.Add(new Producto(2563, "Helado de vainilla", 5.6f, 25));
            postre.Add(new Producto(3876, "Banana split", 10.2f, 15));
            postre.Add(new Producto(4098, "Gelatina de café", 4.5f, 30));
        }

        // Método para mostrar los productos según la categoría seleccionada.
        public void VerProductos(int categoria)
        {
            switch (categoria) // Switch para manejar las categorías.
            {
                case 1:
                    MostrarProductos(almuerzo, "Almuerzos"); // Muestra los almuerzos.
                    break;
                case 2:
                    MostrarProductos(desayuno, "Desayunos"); // Muestra los desayunos.
                    break;
                case 3:
                    MostrarProductos(bebida, "Bebidas"); // Muestra las bebidas.
                    break;
                case 4:
                    MostrarProductos(postre, "Postres"); // Muestra los postres.
                    break;
                case 5:
                    // Muestra todas las categorías.
                    MostrarProductos(almuerzo, "Almuerzos");
                    MostrarProductos(desayuno, "Desayunos");
                    MostrarProductos(bebida, "Bebidas");
                    MostrarProductos(postre, "Postres");
                    break;
                default:
                    Console.WriteLine("Categoría no válida."); // Mensaje en caso de seleccionar una opción incorrecta.
                    break;
            }
        }

        // Método auxiliar para mostrar los productos de una categoría específica.
        private void MostrarProductos(List<Producto> lista, string categoria)
        {
            Console.WriteLine($"\n--- {categoria} ---"); // Muestra el nombre de la categoría.
            foreach (var producto in lista) // Itera sobre la lista de productos.
            {
                // Muestra la información del producto (ID, nombre, precio, cantidad).
                Console.WriteLine($"ID: {producto.Id} - {producto.Nombre} - ${producto.precio} - Cantidad: {producto.Cantidad}");
            }
        }

        // Métodos públicos para acceder a las listas de productos por categoría.
        public List<Producto> ObtenerAlmuerzos()
        {
            return almuerzo;
        }

        public List<Producto> ObtenerDesayunos()
        {
            return desayuno;
        }

        public List<Producto> ObtenerBebidas()
        {
            return bebida;
        }

        public List<Producto> ObtenerPostres()
        {
            return postre;
        }

        // Método para buscar un producto por su ID en todas las listas de productos.
        public Producto BuscarProductoPorId(int id)
        {
            // Concatena todas las listas y busca el primer producto que coincida con el ID.
            return almuerzo.Concat(desayuno).Concat(bebida).Concat(postre)
                        .FirstOrDefault(p => p.Id == id);
        }

        // Método para editar un producto en el menú.
        public void EditarProductoEnMenu(int idProducto, string nuevoNombre, float nuevoPrecio)
        {
            Producto producto = BuscarProductoPorId(idProducto); // Busca el producto por su ID.
            if (producto != null) // Si el producto es encontrado.
            {
                // Actualiza el nombre y el precio del producto.
                producto.Nombre = nuevoNombre;
                producto.precio = nuevoPrecio;
                Console.WriteLine($"Producto {idProducto} actualizado a '{nuevoNombre}' con precio ${nuevoPrecio}.");
            }
            else
            {
                // Mensaje si no se encuentra el producto.
                Console.WriteLine("Producto no encontrado en el menú.");
            }
        }
    }
}


