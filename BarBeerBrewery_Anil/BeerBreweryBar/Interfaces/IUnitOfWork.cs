using System;
using System.Threading.Tasks;
using BeerBreweryBar.Models;
using BeerBreweryBar.Repository;
using Microsoft.AspNetCore.Mvc;
namespace BeerBreweryBar.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Beer> BeerRepository { get; }
        IGenericRepository<Brewery> BreweryRepository { get; }
        IGenericRepository<Bar> BarRepository { get; }
        Task<System.Boolean> SaveAsync();
        IBreweryRepository BreweryBeerRepository { get; }
        IGenericRepository<BarBeer> BarBeerRepository { get; }
        IBarRepository BarBeerDataRepository { get; }
    }
}
