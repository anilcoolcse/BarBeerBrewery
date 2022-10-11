using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerBreweryBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewerysController : ControllerBase
    {
        private IBreweryService _breweryService;
        public BrewerysController(IBreweryService breweryService)
        {
            _breweryService = breweryService;
        }

        //// GET: api/Brewerys
        //[HttpGet]
        //public async Task<IEnumerable<Brewery>> Get()
        //{
        //    return await _breweryService.GetBrewery();
        //}

        // GET: api/Brewerys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brewery>> Get(Int32 id)
        {
            var result = await _breweryService.GetBrewery(id);
            if (result == null)
                return NoContent();
            return result;
        }

        // PUT: api/Brewerys/5
        [HttpPut]
        public async Task<IActionResult> Put(Brewery brewery)
        {
            if (brewery == null)
                return BadRequest();
            var result = await _breweryService.PutBrewery(brewery);
            if (result == null)
                return NotFound();
            return result;
        }

        // POST: api/Brewerys
        [HttpPost]
        public async Task<IActionResult> Post(Brewery brewery)
        {
            return await _breweryService.PostBrewery(brewery);
        }
        // POST: api/Brewerys/Beer
        [HttpPost("Beer")]
        public async Task<IActionResult> PostBreweryBeer(BreweryBeerBar input)
        {
            return await _breweryService.PostBreweryBeer(input);
        }
        //GET : api/Brewerys/{breweryId}/beer get a single brewery by id with associated beers
        [HttpGet("{breweryId}/beer")]
        public async Task<IActionResult> GetBreweriesByIdWithBeers(Int32 breweryId)
        {
            var result = await _breweryService.GetBreweriesByIdWithBeers(breweryId);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        //GET : api/Brewerys/beer - get all brewery with associated beers
        [HttpGet("beer")]
        public async Task<IEnumerable<BreweryBeer>> GetBreweriesWithBeers()
        {
            return await _breweryService.GetBreweriesWithBeers();
        }
    }
}
