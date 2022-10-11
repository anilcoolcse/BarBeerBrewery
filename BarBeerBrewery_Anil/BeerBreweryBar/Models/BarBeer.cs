using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBreweryBar.Models
{
    public class BarBeer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [Display(Name = "Bar")]
        public virtual Int32 BarId { get; set; }

        [ForeignKey("BarId")]
        public virtual Bar Bars { get; set; }

        [Display(Name = "Beer")]
        public virtual Int32 BeerId { get; set; }

        [ForeignKey("BeerId")]
        public virtual Beer Beers { get; set; }
    }
}
