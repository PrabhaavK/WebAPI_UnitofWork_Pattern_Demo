using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitofWork_Pattern_Demo.Repository
{
    public abstract class RepositoryBase<T> : ControllerBase, IRepository<T>
        where T : class
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> dbSet;
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            dbSet = _unitOfWork.Context.Set<T>();
        }

        //Get Request

        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var data = await dbSet.ToListAsync();
            return Ok(data);
        }

        // Create Request
        public async Task<ActionResult<T>> Create(T entity)
        {
            dbSet.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<ActionResult> Update(int id, T entity)
        {
            var existingrecord = await dbSet.FindAsync(id);
            if (existingrecord == null)
            {
                return NotFound();
            }
            _unitOfWork.Context.Entry(existingrecord).CurrentValues.SetValues(entity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        // Delete Request
        public async Task<ActionResult> Delete(int id)
        {
            var data = await dbSet.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            dbSet.Remove(data);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
