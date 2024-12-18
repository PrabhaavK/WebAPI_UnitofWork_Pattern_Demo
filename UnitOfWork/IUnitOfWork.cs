using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitofWork_Pattern_Demo.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        public Task SaveChangesAsync();
    }
}
