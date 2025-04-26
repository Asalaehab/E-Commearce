using DomainLayer.Contracts;
using DomainLayer.Models;
using Peristance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peristance.Repositiories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
          //Get Type Name
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();


    }
}
