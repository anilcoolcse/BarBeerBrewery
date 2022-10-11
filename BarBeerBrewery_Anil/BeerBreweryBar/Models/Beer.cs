using BeerBreweryBar.Models.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerBreweryBar.Models
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0,100, ErrorMessage = "Alcohol By Volume should be less than 100 percentage")]
        public decimal PercentageAlcoholByVolume { get; set; }

        [Display(Name = "Brewery")]
        public virtual Int32? BreweryId { get; set; }

        [ForeignKey("BreweryId")]
        public virtual Brewery Breweries { get; set; }
        //public virtual ICollection<BarBeer> BarBeers { get; set; }
    }
}
