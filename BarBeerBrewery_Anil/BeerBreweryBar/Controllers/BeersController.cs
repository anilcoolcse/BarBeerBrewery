using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Query;
using BeerBreweryBar.Models.POCO;

namespace BeerBreweryBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private IBeerService _beerService;
        public BeersController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        #region Commented
        //// GET: api/Beers
        //[HttpGet]
        //[ODataRoute]
        //[EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        //public async Task<IEnumerable<Beer>> Get()
        //{
        //    return await _beerService.GetBeer();
        //} 
        #endregion

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerData>> Get(Int32 id)
        {
            var result = await _beerService.GetBeer(id);
            if (result == null)
                return NoContent();
            return result;
        }

        // PUT: api/Beers/5
        [HttpPut]
        public async Task<IActionResult> Put(Beer beer)
        {
            if (beer == null)
                return BadRequest();
            var result = await _beerService.PutBeer(beer);
            if (result == null)
                return NotFound();
            return result;
        }

        // POST: api/Beers
        [HttpPost]
        public async Task<IActionResult> Post(Beer beer)
        {
            return await _beerService.PostBeer(beer);
        }
    }
}
