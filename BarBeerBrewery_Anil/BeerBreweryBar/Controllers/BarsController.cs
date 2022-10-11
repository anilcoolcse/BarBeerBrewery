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
    public class BarsController : ControllerBase
    {
        private IBarService _barService;
        public BarsController(IBarService barService)
        {
            _barService = barService;
        }

        //// GET: api/Bars
        //[HttpGet]
        //public async Task<IEnumerable<Bar>> Get()
        //{
        //    return await _barService.GetBar();
        //}

        // GET: api/Bars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bar>> Get(Int32 id)
        {
            var result = await _barService.GetBar(id);
            if (result == null)
                return NoContent();
            return result;
        }

        // PUT: api/Bars/5
        [HttpPut]
        public async Task<IActionResult> Put(Bar bar)
        {
            if (bar == null)
                return BadRequest();
            var result = await _barService.PutBar(bar);
            if (result == null)
                return NotFound();
            return result;
        }

        // POST: api/Bars
        [HttpPost]
        public async Task<IActionResult> Post(Bar bar)
        {
            return await _barService.PostBar(bar);
        }
        // POST: api/Bars/Beer
        [HttpPost("Beer")]
        public async Task<IActionResult> PostBarBeer(BarBeer input)
        {
            return await _barService.PostBarBeer(input);
        }
        //GET : api/Bars/{barId}/beer get a single brewery by id with associated beers
        [HttpGet("{barId}/beer")]
        public async Task<IActionResult> GetBarsByIdWithBeers(Int32 barId)
        {
            var result = await _barService.GetBarsByIdWithBeers(barId);
            if (result == null)
                return NoContent();
            return Ok(result);
        }
        //GET : api/Bars/beer get a single brewery by id with associated beers
        [HttpGet("beer")]
        public async Task<IActionResult> GetBarWithBeers()
        {
            var result = await _barService.GetBarWithBeers();
            if (result == null)
                return NoContent();
            return Ok(result);
        }
    }
}
