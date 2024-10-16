
public class Producto 
{
    // Propiedades públicas para Id, nombre y precio.
    public int Id { get; private set; }     // Id solo lectura desde fuera.
    public string nombre { get; set; }      // El nombre puede ser modificado.
    public float precio { get; set; }       // El precio puede ser modificado.

    // Constructor para inicializar el producto con su Id, nombre y precio.
    public Producto(int id, string nombre, float precio)
    {
        Id = id;
        this.nombre = nombre;
        this.precio = precio;
    }
}