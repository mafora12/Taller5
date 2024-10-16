namespace restaurante
{
public class Factura 
{
    // Diccionario para almacenar las reservas de cada mesa, donde la clave es el número de la mesa y el valor es una Orden.
    private Dictionary<int, Orden> reservas;

    // Constructor para inicializar el diccionario de reservas.
    public Factura()
    {
        reservas = new Dictionary<int, Orden>(); // Se inicializa el diccionario en el constructor.
    }

    // Método para agregar una reserva a una mesa específica.
    // Si la mesa no tiene una reserva previa, se crea una nueva Orden.
    // Luego, se agrega el producto a la orden correspondiente a esa mesa.
    public void AgregarReserva(int numeroMesa, Producto producto)
    {
        if (!reservas.ContainsKey(numeroMesa)) // Verifica si la mesa ya tiene una reserva.
        {
            reservas[numeroMesa] = new Orden(); // Si no tiene reserva, se crea una nueva Orden.
        }
        reservas[numeroMesa].AgregarProducto(producto); // Se agrega el producto a la Orden de la mesa.
    }

    // Método para buscar la reserva de una mesa en particular.
    public Orden BuscarReservaPorMesa(int numeroMesa)
    {
        if (reservas.ContainsKey(numeroMesa)) // Verifica si la mesa tiene una reserva.
        {
            return reservas[numeroMesa]; // Devuelve la Orden si la mesa tiene una reserva.
        }
        else
        {
            Console.WriteLine("Mesa no encontrada."); // Muestra un mensaje si la mesa no tiene reserva.
            return null; // Devuelve null si la mesa no fue encontrada.
        }
    }

    // Método para editar un producto en una reserva específica.
    public void EditarProductoEnReserva(int numeroMesa, int idProducto, string nuevoNombre, float nuevoPrecio)
    {
        if (reservas.ContainsKey(numeroMesa)) // Verifica si la mesa tiene una reserva.
        {
            reservas[numeroMesa].EditarProducto(idProducto, nuevoNombre, nuevoPrecio); // Edita el producto en la Orden de la mesa.
        }
        else
        {
            Console.WriteLine("Mesa no encontrada."); // Mensaje si la mesa no tiene reserva.
        }
    }

    // Método para eliminar un producto de una reserva específica.
    public void EliminarProductoDeReserva(int numeroMesa, int idProducto)
    {
        if (reservas.ContainsKey(numeroMesa)) // Verifica si la mesa tiene una reserva.
        {
            reservas[numeroMesa].EliminarProducto(idProducto); // Elimina el producto de la Orden de la mesa.
        }
        else
        {
            Console.WriteLine("Mesa no encontrada."); // Mensaje si la mesa no tiene reserva.
        }
    }

    // Método para mostrar todas las reservas.
    public void MostrarReservas()
    {
        foreach (var reserva in reservas) // Itera sobre cada reserva en el diccionario.
        {
            Console.WriteLine($"\nMesa {reserva.Key}:"); // Muestra el número de la mesa.
            reserva.Value.MostrarOrden(); // Muestra los detalles de la orden asociada a esa mesa.
        }
    }

    // Método para mostrar la factura total de todas las mesas.
    public void MostrarFactura()
    {
        IU iU = new IU(); // Instancia para manejar la interfaz de usuario.
        float total = 0; // Variable para acumular el total de la factura.
        
        foreach (var reserva in reservas) // Itera sobre cada reserva en el diccionario.
        {
            Console.WriteLine($"\nMesa {reserva.Key}:"); // Muestra el número de la mesa.
            reserva.Value.MostrarOrden(); // Muestra la orden de la mesa.
            total += reserva.Value.CalcularTotal(); // Suma el total de la orden al total general.
        }
        
        iU.ImpresionFactura(); // Llama a la interfaz para imprimir la factura.
    }
}
}


