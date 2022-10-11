using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BeerBreweryBar.Common;
using BeerBreweryBar.Models.POCO;

namespace BeerBreweryBar.Services
{
    public class BeerService : IBeerService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        public BeerService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<Beer>> GetBeer()
        {
            var beers = await _context.BeerRepository.GetAll();
            //var rr =  _mapper.Map<IEnumerable<BeerData>>(beers);
            return beers;
        }

        public async Task<BeerData> GetBeer(Int32 id)
        {
            if (id < 1)
                return null;
            var beer = await _context.BeerRepository.GetById(id);
            return _mapper.Map<BeerData>(beer);
        }

        public async Task<IActionResult> PutBeer(Beer beer)
        {
            try
            {
                var beerDetails = await _context.BeerRepository.GetById(beer.Id);
                if (beerDetails != null)
                {
                    beerDetails.Id = beer.Id;
                    beerDetails.Name = beer.Name;
                    beerDetails.PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume; 
                    _context.BeerRepository.Update(beerDetails);
                    await _context.SaveAsync();
                }
                else
                {
                    return Helper.RecordNotFound();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
            return new OkResult();
        }

        public async Task<IActionResult> PostBeer(Beer beer)
        {
            try
            {
                beer.Id = 0;
                beer.BreweryId = null;
                _ = _context.BeerRepository.Add(beer);
                await _context.SaveAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
            return new OkResult();
        }
    }
}
