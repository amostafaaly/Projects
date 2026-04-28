using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.IServices
{
   public interface IManager<T> where T : class
    {
       public void Add(T entity);   
        public void Remove(T entity);
        public void Update(T entity);
        public List<T> GetAll();
        public T GetById(int id);
        public List<T> Search(string keyword);

    }
}
