using System;
using System.Linq;
using System.Threading.Tasks;
using BeerBreweryBar.Models;
using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Repository;
using AutoMapper;

namespace BeerBreweryBar.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BeerBreweryBarContext _context;
        private readonly IMapper _mapper;
        private bool _disposed;
        public UnitOfWork(BeerBreweryBarContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public IGenericRepository<Beer> BeerRepository => new GenericRepository<Beer>(_context);
        public IGenericRepository<Brewery> BreweryRepository => new GenericRepository<Brewery>(_context);
        public IGenericRepository<Bar> BarRepository => new GenericRepository<Bar>(this._context);
        public IGenericRepository<BarBeer> BarBeerRepository => new GenericRepository<BarBeer>(this._context);
        public IBreweryRepository BreweryBeerRepository => new BreweryRepository(this._context, _mapper);
        public IBarRepository BarBeerDataRepository => new BarRepository(this._context, _mapper);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(System.Boolean disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }

        public async Task<System.Boolean> SaveAsync()
        {
            return await this._context.SaveChangesAsync() > 0;
        }
    }
}