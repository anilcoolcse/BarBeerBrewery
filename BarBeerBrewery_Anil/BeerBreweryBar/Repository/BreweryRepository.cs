using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using AutoMapper;
using BeerBreweryBar.Common;
using BeerBreweryBar.Data;
using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using BeerBreweryBar.Models.POCO;

namespace BeerBreweryBar.Repository
{
    public class BreweryRepository : GenericRepository<BreweryBeer>, IBreweryRepository
    {
        private readonly IMapper _mapper;
        public BreweryRepository(BeerBreweryBarContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<BreweryBeer> GetBreweryBeerById(Int32 id)
        {
            var brewery = Context.Brewery.Where(d => d.Id == id).FirstOrDefault();
            if (brewery == null) return null;

            Context.Entry(brewery).Collection(s => s.Beers).Load();
            var beersData = _mapper.Map<List<BeerData>>(brewery.Beers);

            var result = new BreweryBeer()
            {
                BreweryId = brewery.Id,
                Name = brewery.Name,
                Beers = beersData
            };
            return await new ValueTask<BreweryBeer>(result);
        }
        public async Task<IEnumerable<BreweryBeer>> GetBreweriesWithBeers()
        {
            _ = Context.Beer.Where(d => d.BreweryId != null).ToList();
            var breweries = Context.Brewery.ToList();

            List<BreweryBeer> result = new List<BreweryBeer>();
            foreach (var brewery in breweries)
            {
                var beerData = _mapper.Map<List<BeerData>>(brewery.Beers);

                result.Add(new BreweryBeer()
                {
                    BreweryId = brewery.Id,
                    Name = brewery.Name,
                    Beers = beerData
                });
            }

            return result;
        }
    }
}
