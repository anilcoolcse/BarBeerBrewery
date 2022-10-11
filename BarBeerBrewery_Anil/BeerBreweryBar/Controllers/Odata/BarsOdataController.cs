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
    public class BarsOdataController : Controller
    {
        // GET: odata/BarsOdata
        private readonly IBarService _barService;
        public BarsOdataController(IBarService barService)
        {
            _barService = barService;
        }

        [ODataRoute]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public async Task<IEnumerable<Bar>> Get()
        {
            return await _barService.GetBar();
        }
    }
}
