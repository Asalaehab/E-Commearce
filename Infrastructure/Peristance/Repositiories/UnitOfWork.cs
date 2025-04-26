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
        private readonly Dictionary<string, object> _repositories = [];//to make intialize by new object
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
          //Get Type Name
           var typeName=typeof(TEntity).Name;

            //search if name is found in dictionary before

            if(_repositories.ContainsKey(typeName))
            {
                return (IGenericRepository<TEntity,Tkey>)_repositories[typeName];
            }
            else
            {
                //create object
                var Repo = new GenericRepository<TEntity, Tkey>(_dbContext);//he will need to take _dbcontext to store and make opertions
              
                //store object in repositories
                _repositories.Add(typeName, Repo);

                //return repo
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();


    }
}
