using BeerBreweryBar.Models.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerBreweryBar.Models
{
    public class BreweryBeer
    {
        [Key]
        public Int32 BreweryId { get; set; }
        public String Name { get; set; }
        public List<BeerData> Beers { get; set; }
        //public List<Beer> Beer { get; set; }
    }
}
