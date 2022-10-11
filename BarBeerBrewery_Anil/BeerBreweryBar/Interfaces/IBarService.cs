using BeerBreweryBar.Models;
using BeerBreweryBar.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerBreweryBar.Interfaces
{
    public interface IBarService
    {
        Task<IEnumerable<Bar>> GetBar();
        Task<Bar> GetBar(Int32 id);
        Task<IActionResult> PutBar(Bar beer);
        Task<IActionResult> PostBar(Bar beer);
        Task<IActionResult> PostBarBeer(BarBeer input);
        Task<BarBeerData> GetBarsByIdWithBeers(Int32 id);
        Task<IEnumerable<BarBeerData>> GetBarWithBeers();
    }
}
