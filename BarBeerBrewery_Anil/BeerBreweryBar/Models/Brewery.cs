using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerBreweryBar.Models
{
    public class Brewery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [MaxLength(200, ErrorMessage = "Name should be less than 200 letters")]
        public string Name { get; set; }
        public virtual ICollection<Beer> Beers { get; set; }
    }
}
