using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerBreweryBar.Data;
using Microsoft.AspNetCore.Mvc;
using BeerBreweryBar.Common;

namespace BeerBreweryBar.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IUnitOfWork _context;
        public BreweryService(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Brewery>> GetBrewery()
        {
            return await _context.BreweryRepository.GetAll();
        }

        public async Task<Brewery> GetBrewery(Int32 id)
        {
            if (id < 1)
                return null;
            return await _context.BreweryRepository.GetById(id);
        }

        public async Task<IActionResult> PutBrewery(Brewery brewery)
        {
            try
            {
                var breweryDetails = await _context.BreweryRepository.GetById(brewery.Id);
                if (breweryDetails != null)
                {
                    breweryDetails.Id = brewery.Id;
                    breweryDetails.Name = brewery.Name;
                    _context.BreweryRepository.Update(breweryDetails);
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

        public async Task<IActionResult> PostBrewery(Brewery brewery)
        {
            try
            {
                brewery.Id = 0;
                _ = _context.BreweryRepository.Add(brewery);
                await _context.SaveAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
            return new OkResult();
        }

        public async Task<IActionResult> PostBreweryBeer(BreweryBeerBar input)
        {
            try
            {
                var beerDetails = await _context.BeerRepository.GetById(input.BeerId);
                var breweryDetails = await _context.BreweryRepository.GetById(input.BreweryId);
                if (beerDetails != null && breweryDetails != null)
                {
                    beerDetails.BreweryId = input.BreweryId;
                    this._context.BeerRepository.Update(beerDetails);
                    await this._context.SaveAsync();
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
            return new OkResult();
        }

        public async Task<BreweryBeer> GetBreweriesByIdWithBeers(Int32 id)
        {
            if (id < 1)
                return null;
            var result = await this._context.BreweryBeerRepository.GetBreweryBeerById(id);
            return result;
        }
        public async Task<IEnumerable<BreweryBeer>> GetBreweriesWithBeers()
        {
            var result = await this._context.BreweryBeerRepository.GetBreweriesWithBeers();
            return result;
        }

    }
}
