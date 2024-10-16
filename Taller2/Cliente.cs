
    using restaurante;

public class Cliente
{
         
          
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public List<Factura> Facturas { get; set; }
 
 

    public Cliente(string nombre, string descripcion, string  telefono, string correo)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Telefono = telefono;
        Correo = correo;      
    }


    public Cliente(int id, string nombre)
    {
        int Id = id;

        Nombre = nombre;
        Facturas = new List<Factura>();
    }

    public float CalcularTotalGastado()
    {
        return Facturas.Where(f => f.esPagada).Sum(f => f.Orden.CalcularTotal());
    }

    public float CalcularTotalDeuda()
    {
        return Facturas.Where(f => !f.esPagada).Sum(f => f.Orden.CalcularTotal());
    }
}