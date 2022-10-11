using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBreweryBar.Models.POCO
{
    public class BarBeerData
    {
        public int BarId { get; set; }
        public string BarName { get; set; }
        public string BarAddress { get; set; }
        public List<BeerData> Beers { get; set; }

    }
}
