public class MenuAdmin
{
    // Listas privadas para almacenar los productos de cada categoría.
    private List<Producto> almuerzo;
    private List<Producto> desayuno;
    private List<Producto> bebida;
    private List<Producto> postre;

    // Constructor que inicializa las listas y llama a los métodos que cargan los productos.
    public MenuAdmin()
    {
        // Inicializar listas en el constructor.
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

    // Método para cargar productos en la lista de almuerzos.
    private void CargarAlmuerzos()
    {
        almuerzo.Add(new Producto(1362, "Pizza mediana", 13.5f));
        almuerzo.Add(new Producto(2321, "Hamburguesa con queso", 10f));
        almuerzo.Add(new Producto(3918, "Spaguettis a la boloñesa", 11.3f));
        almuerzo.Add(new Producto(4382, "Perro con papas fritas", 9.5f));
        almuerzo.Add(new Producto(5019, "Burrito", 9.5f));
        almuerzo.Add(new Producto(6372, "Quesadilla", 5.7f));
    }

    // Método para cargar productos en la lista de desayunos.
    private void CargarDesayunos()
    {
        desayuno.Add(new Producto(1291, "Huevos y tocino", 5.9f));
        desayuno.Add(new Producto(2283, "Cereal", 3.5f));
        desayuno.Add(new Producto(3283, "Panqueques y fruta", 5.5f));
        desayuno.Add(new Producto(4109, "Arepa con queso", 5f));
        desayuno.Add(new Producto(5981, "Empanada", 3.8f));
        desayuno.Add(new Producto(6167, "Buñuelos", 1.5f));
    }

    // Método para cargar productos en la lista de bebidas.
    private void CargarBebidas()
    {
        bebida.Add(new Producto(1019, "Jugo de naranja", 3.2f));
        bebida.Add(new Producto(2811, "Café", 3.6f));
        bebida.Add(new Producto(3222, "Cerveza", 4.9f));
        bebida.Add(new Producto(4990, "Gaseosa", 5.9f));
        bebida.Add(new Producto(5181, "Limonada", 6.3f));
        bebida.Add(new Producto(6098, "Jugo del día", 3f));
    }

    // Método para cargar productos en la lista de postres.
    private void CargarPostres()
    {
        postre.Add(new Producto(1234, "Pastel de chocolate", 6.8f));
        postre.Add(new Producto(2563, "Helado de vainilla", 5.6f));
        postre.Add(new Producto(3876, "Banana split", 10.2f));
        postre.Add(new Producto(4098, "Gelatina de café", 4.5f));
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
            // Muestra la información del producto (ID, nombre, precio).
            Console.WriteLine($"ID: {producto.Id} - {producto.nombre} - ${producto.precio}");
        }
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
            producto.nombre = nuevoNombre;
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
