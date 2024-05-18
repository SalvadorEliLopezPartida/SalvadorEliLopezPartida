//López Partida Salvador Eli 19211670
// Exámen de girar la rueda y elegir un tema
//Combo 2: Optimización de Recursos. Uso de Flyweight y Singleton
using System;
using System.Collections.Generic;

// Clase principal declarada para representar a la fruta
// y sus atributos 
public class Fruta
{
    public short Id { get; private set; }
    public string Nombre { get; private set; }
    public float Peso { get; private set; }
    public string Color { get; private set; }

    public Fruta(short id, string nombre, float peso, string color)
    {
        Id = id;
        Nombre = nombre;
        Peso = peso;
        Color = color;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Nombre: {Nombre}, Peso: {Peso}kg, Color: {Color}";
    }
}

public class FabricaDeFrutas
{
    private readonly Dictionary<short, Fruta> _frutasPorId;
    private readonly Dictionary<string, List<Fruta>> _frutasPorNombre;
    private static FabricaDeFrutas _instancia;
    private static readonly object _lock = new object();

    private FabricaDeFrutas()
    {
        _frutasPorId = new Dictionary<short, Fruta>();
        _frutasPorNombre = new Dictionary<string, List<Fruta>>();
    }

    public static FabricaDeFrutas Instancia
    {
        get
        {
            if (_instancia == null)
            {
                lock (_lock)
                {
                    if (_instancia == null)
                    {
                        _instancia = new FabricaDeFrutas();
                    }
                }
            }
            return _instancia;
        }
    }

    public Fruta ObtenerFruta(short id, string nombre, float peso, string color)
    {
        if (!_frutasPorId.ContainsKey(id))
        {
            Fruta fruta = new Fruta(id, nombre, peso, color);
            _frutasPorId[id] = fruta;

            if (!_frutasPorNombre.ContainsKey(nombre))
            {
                _frutasPorNombre[nombre] = new List<Fruta>();
            }
            _frutasPorNombre[nombre].Add(fruta);
        }
        return _frutasPorId[id];
    }

    public Fruta ObtenerFrutaPorId(short id)
    {
        _frutasPorId.TryGetValue(id, out Fruta fruta);
        return fruta;
    }

    //Maneja eficientemente las instancias de frutas,
    ///asegurando que las referencias sean eliminadas 
    //correctamente para liberar memoria.

    public bool EliminarFruta(short id)
    {
        if (_frutasPorId.TryGetValue(id, out Fruta fruta))
        {
            _frutasPorId.Remove(id);
            _frutasPorNombre[fruta.Nombre].Remove(fruta);
            if (_frutasPorNombre[fruta.Nombre].Count == 0)
            {
                _frutasPorNombre.Remove(fruta.Nombre);
            }
            return true;
        }
        return false;
    }

    //Garantiza que todas las operaciones sobre frutas se realicen sobre la misma instancia de la fábrica
    public void MostrarFrutasPorNombre(string nombre)
    {
        if (_frutasPorNombre.TryGetValue(nombre, out List<Fruta> frutas))
        {
            foreach (var fruta in frutas)
            {
                Console.WriteLine(fruta);
            }
        }
        else
        {
            Console.WriteLine("No se encontraron frutas con ese nombre.");
        }
    }
}

class Program
{
    //se crea un método estático dentro del programa para crar
    //un menú y mandar a llamar los métodos con los que se trabajará
    static void Main()
    {
        FabricaDeFrutas fabrica = FabricaDeFrutas.Instancia;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Ingresar una fruta");
            Console.WriteLine("2. Mostrar fruta por ID");
            Console.WriteLine("3. Eliminar fruta");
            Console.WriteLine("4. Mostrar frutas por nombre");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Clear();
                    IngresarFruta(fabrica);
                    break;
                case "2":
                    Console.Clear();
                    MostrarFrutaPorId(fabrica);
                    break;
                case "3":
                    Console.Clear();
                    EliminarFruta(fabrica);
                    break;
                case "4":
                    Console.Clear();
                    MostrarFrutasPorNombre(fabrica);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void IngresarFruta(FabricaDeFrutas fabrica)
    {
        try
        {
            Console.Write("Ingrese ID de la fruta: ");
            short id = short.Parse(Console.ReadLine());

            if (fabrica.ObtenerFrutaPorId(id) != null)
            {
                Console.WriteLine("Error: El ID ya existe. Ingrese un ID único.");
                return;
            }

            Console.Write("Ingrese nombre de la fruta: ");
            string nombre = Console.ReadLine();

            if (nombre.Length > 16)
            {
                Console.WriteLine("Error: El nombre no puede tener más de 16 caracteres.");
                return;
            }

            Console.Write("Ingrese peso de la fruta (kg): ");
            float peso = float.Parse(Console.ReadLine());

            Console.Write("Ingrese color de la fruta: ");
            string color = Console.ReadLine();

            if (color.Length > 16)
            {
                Console.WriteLine("Error: El color no puede tener más de 16 caracteres.");
                return;
            }

            Fruta fruta = fabrica.ObtenerFruta(id, nombre, peso, color);
            Console.WriteLine($"Fruta ingresada: {fruta}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Formato de entrada no válido.");
        }
    }
    //Con este método podemos desplegar todas las frutas existentes por id.
    static void MostrarFrutaPorId(FabricaDeFrutas fabrica)
    {
        try
        {
            Console.Write("Ingrese ID de la fruta: ");
            short id = short.Parse(Console.ReadLine()); //se declara una variable id short para leer el id que ingresa el usuario
            //y de esta manera poder leerlo

            Fruta fruta = fabrica.ObtenerFrutaPorId(id);
            if (fruta != null)
            {
                Console.WriteLine($"Fruta encontrada: {fruta}");
                //Si encuentra el id devuelve este mensaje junto con los atributos de la fruta
            }
            else
            {
                Console.WriteLine("Fruta no encontrada."); // Si no encuentra el id devuelve este mensaje
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Formato de entrada no válido.");
        }
    }
    //Con este método se crea la acción de eliminar frutas mediante el id.
    //Se selecciona el id deseado y borra toda la información resguardada
    // acompañda de ese id
    static void EliminarFruta(FabricaDeFrutas fabrica)
    {
        try
        {
            Console.Write("Ingrese ID de la fruta a eliminar: ");
            short id = short.Parse(Console.ReadLine()); //registra la entrada del id para leerse

            if (fabrica.EliminarFruta(id))
            {
                Console.WriteLine("Fruta eliminada.");
            }
            else
            {
                Console.WriteLine("Fruta no encontrada.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Formato de entrada no válido."); 
            //si el valor ingresado no es correcto suelta un msj de error
        }
    }

    //Con este método se crea la acción de desplegar los datos
    //mostrados por elnombre de la fruta ingresada.
    static void MostrarFrutasPorNombre(FabricaDeFrutas fabrica)
    {
        Console.Write("Ingrese nombre de la fruta: ");
        string nombre = Console.ReadLine();

        Console.WriteLine($"Frutas con el nombre '{nombre}':");
        fabrica.MostrarFrutasPorNombre(nombre);
    }
}
