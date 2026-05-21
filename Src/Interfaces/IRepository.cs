namespace Projects.Src.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T? GetById(string id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Update(T entity);
        void Remove(T entity);
    }
}
