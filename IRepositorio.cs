interface IRepositorio<T>
{
    void Agregar(T elemento);
    T? Buscar(int id);
    void Eliminar(int id);
    List<T> Listar();
}