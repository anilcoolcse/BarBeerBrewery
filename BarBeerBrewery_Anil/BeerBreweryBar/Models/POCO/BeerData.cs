using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBreweryBar.Models.POCO
{
    public class BeerData
    {
        [Key]
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
    }
}
