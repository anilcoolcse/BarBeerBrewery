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

namespace BeerBreweryBar.Controllers.Odata
{
    public class BreweriesOdataController : Controller
    {
        // GET: odata/BreweriesOdata
        private readonly IBreweryService _brewery;
        public BreweriesOdataController(IBreweryService brewery)
        {
            _brewery = brewery;
        }

        [ODataRoute]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public async Task<IEnumerable<Brewery>> Get()
        {
            return await _brewery.GetBrewery();
        }
    }
}
