using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using AutoMapper;
using BeerBreweryBar.Data;
using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using BeerBreweryBar.Models.POCO;

namespace BeerBreweryBar.Repository
{
    public class BarRepository : GenericRepository<BarBeerData>, IBarRepository
    {
        private readonly IMapper _mapper;
        public BarRepository(BeerBreweryBarContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<BarBeerData> GetBarsByIdWithBeers(Int32 id)
        {
            var beerIds = Context.BarBeers.Where(d => d.BarId == id).Select(d => d.BeerId);
            var beers = Context.Beer.Where(d => beerIds.Contains(d.Id));
            var bar = Context.Bar.Where(d => d.Id == id).FirstOrDefault();
            if (bar == null) return null;

            var beerData = _mapper.Map<List<BeerData>>(beers);
            var result = new BarBeerData()
            {
                BarId = bar.Id,
                BarName = bar.Name,
                BarAddress = bar.Address,
                Beers = beerData
            };
            return await Task.FromResult(result);
        }
        public async Task<IEnumerable<BarBeerData>> GetBarWithBeers()
        {
            var barBeers = Context.BarBeers;
            var beers = Context.Beer;
            var bars = Context.Bar.Select(d => d);

            var result = new List<BarBeerData>();
            BarBeerData barBeer = null;

            foreach (var bar in bars)
            {
                var BeerIds = barBeers.Where(d => d.BarId == bar.Id).Select(d => d.BeerId);
                var beerList = new List<Beer>();

                foreach (var beerId in BeerIds)
                {
                    var beer = beers.Where(d => d.Id == beerId).FirstOrDefault();
                    beerList.Add(beer);
                }
                var beerData = _mapper.Map<List<BeerData>>(beerList);
                barBeer = new BarBeerData()
                {
                    BarId = bar.Id,
                    BarName = bar.Name,
                    BarAddress = bar.Address,
                    Beers = beerData
                };
                result.Add(barBeer);
            }

            #region Commented
            //Due to time constraint, Unable to fetch linq query by combining all there tables and getting group by issues

            //var result = from bar in Context.Bar
            //             join barBeer in Context.BarBeers on bar.Id equals barBeer.BarId
            //             join beer in Context.Beer on barBeer.BeerId equals beer.Id
            //             select new BarBeerData()
            //             {
            //                 BarId = bar.Id,
            //                 BarName = bar.Name,
            //                 BarAddress = bar.Address
            //                 //BeerId = beer.Id,
            //                 //BeerName = beer.Name,
            //                 //PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume
            //             }; 
            #endregion

            return await Task.FromResult(result);
        }
    }
}
