using BeerBreweryBar.Models;
using BeerBreweryBar.Models.POCO;
using BeerBreweryBar.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerBreweryBar.Interfaces
{
    public interface IBarRepository : IGenericRepository<BarBeerData>
    {
        Task<BarBeerData> GetBarsByIdWithBeers(Int32 id);
        Task<IEnumerable<BarBeerData>> GetBarWithBeers();
    }
}
