using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerBreweryBar.Models
{
    public class Bar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [MaxLength(200, ErrorMessage = "Name should be less than 200 letters")]
        public string Name { get; set; }
        [MaxLength(300, ErrorMessage = "Address should be less than 300 letters")]
        public string Address { get; set; }
        //public List<Brewery> Breweries { get; set; }
        //public virtual ICollection<BarBeer> BarBeers { get; set; }
    }
}
