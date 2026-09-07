using System.Linq;

class BibliotecaService
{
    private List<Libro> libros = new();
    private List<Usuario> usuarios = new();
    private List<Prestamo> prestamos = new();

    public void RegistrarLibro(Libro libro)
    {
        if (libros.Any(l => l.Codigo == libro.Codigo))
            throw new Exception("El código del libro ya existe.");

        libros.Add(libro);
        Console.WriteLine("Libro registrado correctamente.");
    }

    public void RegistrarUsuario(Usuario usuario)
    {
        if (usuarios.Any(u => u.Id == usuario.Id))
            throw new Exception("El identificador del usuario ya existe.");

        usuarios.Add(usuario);
        Console.WriteLine("Usuario registrado correctamente.");
    }

    public void ListarLibros()
    {
        if (!libros.Any())
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        foreach (var libro in libros)
        {
            Console.WriteLine(
                $"Código: {libro.Codigo} | " +
                $"Título: {libro.Titulo} | " +
                $"Autor: {libro.Autor} | " +
                $"Categoría: {libro.Categoria} | " +
                $"Disponible: {libro.Disponible}"
            );
        }
    }

    public Libro? BuscarLibroPorCodigo(int codigo)
    {
        return libros.FirstOrDefault(l => l.Codigo == codigo);
    }

    public void EliminarLibro(int codigo)
    {
        var libro = BuscarLibroPorCodigo(codigo);

        if (libro == null)
            throw new Exception("El libro no existe.");

        if (!libro.Disponible)
            throw new Exception("No se puede eliminar un libro prestado.");

        libros.Remove(libro);
        Console.WriteLine("Libro eliminado correctamente.");
    }

    public void RegistrarPrestamo(int codigoLibro, int idUsuario)
    {
        var libro = BuscarLibroPorCodigo(codigoLibro);

        if (libro == null)
            throw new Exception("El libro no existe.");

        var usuario = usuarios.FirstOrDefault(u => u.Id == idUsuario);

        if (usuario == null)
            throw new Exception("El usuario no existe.");

        if (!libro.Disponible)
            throw new Exception("El libro no está disponible.");

        libro.Disponible = false;

        prestamos.Add(
            new Prestamo(
                codigoLibro,
                idUsuario,
                DateTime.Now,
                null
            )
        );

        Console.WriteLine("Préstamo registrado correctamente.");
    }

    public void RegistrarDevolucion(int codigoLibro, int idUsuario)
    {
        var prestamo = prestamos.FirstOrDefault(
            p => p.CodigoLibro == codigoLibro &&
                 p.IdUsuario == idUsuario &&
                 p.FechaDevolucion == null
        );

        if (prestamo == null)
            throw new Exception("No existe un préstamo activo para ese usuario y libro.");

        var libro = BuscarLibroPorCodigo(codigoLibro);

        if (libro != null)
            libro.Disponible = true;

        prestamos.Remove(prestamo);

        prestamos.Add(
            prestamo with
            {
                FechaDevolucion = DateTime.Now
            }
        );

        Console.WriteLine("Devolución registrada correctamente.");
    }

    public void ConsultarDisponibles()
    {
        var disponibles = libros
            .Where(l => l.Disponible)
            .Select(l => new
            {
                l.Codigo,
                l.Titulo,
                l.Autor
            });

        if (!disponibles.Any())
        {
            Console.WriteLine("No hay libros disponibles.");
            return;
        }

        foreach (var libro in disponibles)
        {
            Console.WriteLine(
                $"Código: {libro.Codigo} | " +
                $"Título: {libro.Titulo} | " +
                $"Autor: {libro.Autor}"
            );
        }
    }

    public void BuscarPorAutorOCategoria(string texto)
    {
        var resultados = libros
            .Where(l =>
                l.Autor.Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                l.Categoria.Contains(texto, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!resultados.Any())
        {
            Console.WriteLine("No se encontraron libros.");
            return;
        }

        foreach (var libro in resultados)
        {
            Console.WriteLine(
                $"{libro.Codigo} - {libro.Titulo} - " +
                $"{libro.Autor} - {libro.Categoria}"
            );
        }
    }

    public void ListarLibrosOrdenados()
    {
        var resultados = libros
            .OrderBy(l => l.Titulo)
            .Select(l => $"{l.Codigo} - {l.Titulo} - {l.Autor}");

        foreach (var libro in resultados)
            Console.WriteLine(libro);
    }

    public void ConsultarPrestamosActivos()
    {
        var activos = prestamos
            .Where(p => p.FechaDevolucion == null)
            .Select(p => new
            {
                p.CodigoLibro,
                p.IdUsuario,
                p.FechaPrestamo
            });

        if (!activos.Any())
        {
            Console.WriteLine("No hay préstamos activos.");
            return;
        }

        foreach (var prestamo in activos)
        {
            Console.WriteLine(
                $"Libro: {prestamo.CodigoLibro} | " +
                $"Usuario: {prestamo.IdUsuario} | " +
                $"Fecha: {prestamo.FechaPrestamo}"
            );
        }
    }
}