
    public class Cliente{
         
          
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
 
 

    public Cliente(string nombre, string descripcion, string  telefono, string correo)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Telefono = telefono;
        Correo = correo;      
    }
    }