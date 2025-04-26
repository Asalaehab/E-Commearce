using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Peristance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peristance.Repositiories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
       // private readonly StoreDbContext dbContext = _dbContext;

        public async Task<IEnumerable<TEntity>> GetAllAsync()=> await  _dbContext.Set<TEntity>().ToListAsync();
        



        public async Task<TEntity?> GetById(Tkey id) => await _dbContext.Set<TEntity>().FindAsync(id);
        


        public async Task AddAsync(TEntity entity)=>await  _dbContext.AddAsync(entity);


      

        public void Update(TEntity entity)=>_dbContext.Set<TEntity>().Update(entity);
       


        public void Delete(TEntity entity)=> _dbContext.Set<TEntity>().Remove(entity);
       

    }
}
