using BeerBreweryBar.Data;
using BeerBreweryBar.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerBreweryBar.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public GenericRepository(BeerBreweryBarContext context)
        {
            Context = context;
        }
        public BeerBreweryBarContext Context { get; set; }

        public async Task Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Int32 id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.Set<T>().Update(entity);
        }
    }
}