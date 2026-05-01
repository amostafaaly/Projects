using System;

namespace Projects.Src.Contracts
{
    public interface IFileHandler<T>
    {
        string ToFileLine(T item);
        T FromFileLine(string line);
    }
}
