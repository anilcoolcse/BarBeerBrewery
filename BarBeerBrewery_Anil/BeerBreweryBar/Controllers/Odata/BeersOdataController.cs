using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using BeerBreweryBar.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using BeerBreweryBar.Models.POCO;

namespace BeerBreweryBar.Controllers.Odata
{
    public class BeersOdataController : Controller
    {
        // GET: odata/BeersOdata
        private readonly IBeerService _beerService;
        public BeersOdataController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [ODataRoute]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public async Task<IEnumerable<Beer>> Get()
        {
            var result = await _beerService.GetBeer();
            return result;
        }
    }
}
