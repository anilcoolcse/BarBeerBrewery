using BeerBreweryBar.Models;
using BeerBreweryBar.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerBreweryBar.Interfaces
{
    public interface IBeerService
    {
        Task<IEnumerable<Beer>> GetBeer();
        Task<BeerData> GetBeer(Int32 id);
        Task<IActionResult> PutBeer(Beer beer);
        Task<IActionResult> PostBeer(Beer beer);
    }
}
