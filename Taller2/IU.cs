namespace restaurante   
{
public class IU

    
{
    // Método para mostrar el logo del sistema
    public static void Logo()
    {
        Console.WriteLine("    _____                          _______         ");
        Console.WriteLine("   |_   _|                        |_   __ | "       );
        Console.WriteLine("     | | __   _   ,--.   _ .--.     | |__) |,--.   ");
        Console.WriteLine("     | |[  | | | `'_| : [ `.-. |    |  ___/`'_| :"  );
        Console.WriteLine("| |__' | | |_| |,// | |, | | | |   _| |_   // | |, ");
        Console.WriteLine("`.____.' '.__.'_/\'-;__/[___||__] |_____|  \'-;__/" );
    }

    // Método para mostrar las opciones del programa
    public static void OpcionesPrograma()
    {
        Console.WriteLine("   _.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._");
        Console.WriteLine(" ,'_.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._`.");
        Console.WriteLine("  ))                                                      ((");
        Console.WriteLine(" ((    \n1. Ver menú                                       ))");
        Console.WriteLine("  ))     2. Hacer una reserva                             ((");
        Console.WriteLine(" ((      3. Ver reservas                                   ))");
        Console.WriteLine("  ))     4. Editar producto en reserva                    ((");
        Console.WriteLine(" ((      5. Añadir producto a una reserva existente        ))");
        Console.WriteLine("  ))     6. Generar factura                                ((");
        Console.WriteLine(" ((      7. Editar menú                                     ))");
        Console.WriteLine("  ))     0. Salir                                          ((");
        Console.WriteLine(" ((_.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._))");
        Console.WriteLine("  `._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._.-._,'");
        Console.Write("Seleccione una opción: ");
    }

    // Método para imprimir la factura con el total recibido
    public static void ImpresionFactura(float total)
    {
        Console.WriteLine("   ____________________________________");
        Console.WriteLine(" /  |                                  |.");
        Console.WriteLine("|   |                                  |.");
        Console.WriteLine("| _|    ___         _                  |.");
        Console.WriteLine("    |  | __|__ _ __| |_ _  _ _ _ __ _  |.");
        Console.WriteLine("    |  | _|/ _` / _|  _| || | '_/ _` | |.");
        Console.WriteLine("    |  |_| |_,__|_|,_|_| _,_ | ||_,__| | ");
        Console.WriteLine("    |                                  |.");
        Console.WriteLine($"   |      Total general: ${total}     |." );
        Console.WriteLine("    |                                  |.");
        Console.WriteLine("    |   _______________________________|___");
        Console.WriteLine("    |  /                                  /.");
        Console.WriteLine("      /__________________________________/.");
    }

    internal void ImpresionFactura()
    {
        throw new NotImplementedException();
    }
}
}

     
