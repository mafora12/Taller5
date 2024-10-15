public class Producto
{
    public int Id { get; private set; }
    public string nombre { get; set; }
    public float precio { get; set; }

    public Producto(int id, string nombre, float precio)
    {
        Id = id;
        this.nombre = nombre;
        this.precio = precio;
    }
}