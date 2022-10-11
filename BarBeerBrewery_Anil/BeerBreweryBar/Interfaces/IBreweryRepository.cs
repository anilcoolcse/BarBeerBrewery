using BeerBreweryBar.Models;
using BeerBreweryBar.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerBreweryBar.Interfaces
{
    public interface IBreweryRepository : IGenericRepository<BreweryBeer>
    {
        Task<BreweryBeer> GetBreweryBeerById(Int32 id);
        Task<IEnumerable<BreweryBeer>> GetBreweriesWithBeers();
    }
}
