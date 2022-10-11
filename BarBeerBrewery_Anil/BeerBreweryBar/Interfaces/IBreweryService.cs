using BeerBreweryBar.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerBreweryBar.Interfaces
{
    public interface IBreweryService
    {
        Task<IEnumerable<Brewery>> GetBrewery();
        Task<Brewery> GetBrewery(Int32 id);
        Task<IActionResult> PutBrewery(Brewery brewery);
        Task<IActionResult> PostBrewery(Brewery brewery);
        Task<IActionResult> PostBreweryBeer(BreweryBeerBar brewery);
        Task<BreweryBeer> GetBreweriesByIdWithBeers(Int32 id);
        Task<IEnumerable<BreweryBeer>> GetBreweriesWithBeers();
    }
}
