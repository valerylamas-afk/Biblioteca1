BibliotecaService biblioteca = new BibliotecaService();

int opcion = 0;

while (opcion != 10)
{
    Console.WriteLine("\n===== SISTEMA DE BIBLIOTECA =====");
    Console.WriteLine("1. Registrar libro");
    Console.WriteLine("2. Registrar usuario");
    Console.WriteLine("3. Listar libros");
    Console.WriteLine("4. Buscar libro por código");
    Console.WriteLine("5. Eliminar libro");
    Console.WriteLine("6. Registrar préstamo");
    Console.WriteLine("7. Registrar devolución");
    Console.WriteLine("8. Consultar libros disponibles");
    Console.WriteLine("9. Consultar préstamos activos");
    Console.WriteLine("10. Salir");
    Console.Write("Seleccione una opción: ");

    try
    {
        opcion = int.Parse(Console.ReadLine()!);

        switch (opcion)
        {
            case 1:
                RegistrarLibro();
                break;

            case 2:
                RegistrarUsuario();
                break;

            case 3:
                biblioteca.ListarLibros();
                break;

            case 4:
                BuscarLibro();
                break;

            case 5:
                EliminarLibro();
                break;

            case 6:
                RegistrarPrestamo();
                break;

            case 7:
                RegistrarDevolucion();
                break;

            case 8:
                biblioteca.ConsultarDisponibles();
                break;

            case 9:
                biblioteca.ConsultarPrestamosActivos();
                break;

            case 10:
                Console.WriteLine("Saliendo del sistema...");
                break;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: debe ingresar un número válido.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void RegistrarLibro()
{
    Console.WriteLine("\n=== REGISTRAR LIBRO ===");

    Console.Write("Código: ");
    int codigo = int.Parse(Console.ReadLine()!);

    Console.Write("Título: ");
    string titulo = Console.ReadLine()!;

    Console.Write("Autor: ");
    string autor = Console.ReadLine()!;

    Console.Write("Categoría: ");
    string categoria = Console.ReadLine()!;

    Libro libro = new Libro(codigo, titulo, autor, categoria);

    biblioteca.RegistrarLibro(libro);
}

void RegistrarUsuario()
{
    Console.WriteLine("\n=== REGISTRAR USUARIO ===");

    Console.Write("Identificador: ");
    int id = int.Parse(Console.ReadLine()!);

    Console.Write("Nombre: ");
    string nombre = Console.ReadLine()!;

    Console.Write("Correo: ");
    string correo = Console.ReadLine()!;

    Usuario usuario = new Usuario(id, nombre, correo);

    biblioteca.RegistrarUsuario(usuario);
}

void BuscarLibro()
{
    Console.WriteLine("\n=== BUSCAR LIBRO ===");

    Console.Write("Código: ");
    int codigo = int.Parse(Console.ReadLine()!);

    Libro? libro = biblioteca.BuscarLibroPorCodigo(codigo);

    if (libro == null)
    {
        Console.WriteLine("No se encontró el libro.");
    }
    else
    {
        Console.WriteLine($"Código: {libro.Codigo}");
        Console.WriteLine($"Título: {libro.Titulo}");
        Console.WriteLine($"Autor: {libro.Autor}");
        Console.WriteLine($"Categoría: {libro.Categoria}");
        Console.WriteLine($"Disponible: {libro.Disponible}");
    }
}

void EliminarLibro()
{
    Console.WriteLine("\n=== ELIMINAR LIBRO ===");

    Console.Write("Código del libro: ");
    int codigo = int.Parse(Console.ReadLine()!);

    biblioteca.EliminarLibro(codigo);
}

void RegistrarPrestamo()
{
    Console.WriteLine("\n=== REGISTRAR PRÉSTAMO ===");

    Console.Write("Código del libro: ");
    int codigoLibro = int.Parse(Console.ReadLine()!);

    Console.Write("ID del usuario: ");
    int idUsuario = int.Parse(Console.ReadLine()!);

    biblioteca.RegistrarPrestamo(codigoLibro, idUsuario);
}

void RegistrarDevolucion()
{
    Console.WriteLine("\n=== REGISTRAR DEVOLUCIÓN ===");

    Console.Write("Código del libro: ");
    int codigoLibro = int.Parse(Console.ReadLine()!);

    Console.Write("ID del usuario: ");
    int idUsuario = int.Parse(Console.ReadLine()!);

    biblioteca.RegistrarDevolucion(codigoLibro, idUsuario);
}