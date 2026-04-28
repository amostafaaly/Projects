using Projects.IServices;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projects.Services.TaskManager
{
    public class GenericManager<T> : IManager<T> where T :  BaseEntity
    {
        private List<T> items;

        public GenericManager()
        {
            items = new List<T>();
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            items.Add(entity);
        }

        public List<T> GetAll()
        {
            return items;
        }

        public T GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Id must be greater than zero.");

            var item = items.Find(x => x.Id == id);

            if (item == null)
                throw new KeyNotFoundException("Entity with the specified ID not found.");

            return item;
        }

        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var item = items.Find(x => x.Id == entity.Id);

            if (item == null)
                throw new KeyNotFoundException("Entity not found.");

            items.Remove(item);
        }

        public List<T> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentNullException(nameof(keyword));

            var result = items
                .Where(x => x.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (result.Count == 0)
                throw new KeyNotFoundException("No results found.");

            return result;
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var item = items.Find(x => x.Id == entity.Id);

            if (item == null)
                throw new KeyNotFoundException("Entity not found.");

            item.Name = entity.Name;
        }
    }
}