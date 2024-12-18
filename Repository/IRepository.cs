using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitofWork_Pattern_Demo.Repository
{
    public interface IRepository<T>
        where T : class
    {
        public Task<ActionResult<IEnumerable<T>>> Get();
        public Task<ActionResult<T>> Create(T entity);
        public Task<ActionResult> Update(int id, T entity);
        public Task<ActionResult> Delete(int id);
        // public Task<ActionResult<T>> GetById(int id);
    }
}
