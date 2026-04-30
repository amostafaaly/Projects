using Projects.Src.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Projects.Src.Models.Instructors;
using Projects.Src.Interfaces;

namespace Projects.Src.Services.TaskManager
{
    public class InstructorManager : IInstructorManager
    {
        private List<Instructor> items;

        public InstructorManager()
        {
            items = new List<Instructor>();
        }

        public void Add(Instructor entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            items.Add(entity);
        }

        public List<Instructor> GetAll()
        {
            return items;
        }

        public Instructor GetById(string id)
        {
            var item = items.Find(x => x.Id == id);

            if (item == null)
                throw new KeyNotFoundException("Instructor with the specified ID not found.");

            return item;
        }

        public void Remove(Instructor entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var item = items.Find(x => x.Id == entity.Id);

            if (item == null)
                throw new KeyNotFoundException("Instructor not found.");

            items.Remove(item);
        }

        public List<Instructor> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentNullException(nameof(keyword));

            var result = items
                .Where(x => x.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (result.Count == 0)
                throw new KeyNotFoundException("No instructors found.");

            return result;
        }

        public void Update(Instructor entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var item = items.Find(x => x.Id == entity.Id);

            if (item == null)
                throw new KeyNotFoundException("Instructor not found.");


        }
    }
}