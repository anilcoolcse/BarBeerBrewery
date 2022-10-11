using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerBreweryBar.Data;
using BeerBreweryBar.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using BeerBreweryBar.Common;

namespace BeerBreweryBar.Services
{
    public class BarService : IBarService
    {
        private readonly IUnitOfWork _context;
        public BarService(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Bar>> GetBar()
        {
            return await _context.BarRepository.GetAll();
        }

        public async Task<Bar> GetBar(Int32 id)
        {
            if (id < 1)
                return null;
            return await _context.BarRepository.GetById(id);
        }

        public async Task<IActionResult> PutBar(Bar bar)
        {
            try
            {
                var barDetails = await _context.BarRepository.GetById(bar.Id);
                if (barDetails != null)
                {
                    barDetails.Id = bar.Id;
                    barDetails.Name = bar.Name;
                    barDetails.Address = bar.Address;
                    _context.BarRepository.Update(barDetails);
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

        public async Task<IActionResult> PostBar(Bar bar)
        {
            try
            {
                bar.Id = 0;
                _ = _context.BarRepository.Add(bar);
                await _context.SaveAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
            return new OkResult();
        }

        public async Task<IActionResult> PostBarBeer(BarBeer input)
        {
            try
            {
                var barDetails = await _context.BarRepository.GetById(input.BarId);
                var beerDetails = await _context.BeerRepository.GetById(input.BeerId);
                if (beerDetails != null && barDetails != null)//Check if BarId and BeerId are valid
                {
                    var barBeersExists = _context.BarBeerRepository.GetAll().Result.Where(d => d.BarId == input.BarId && d.BeerId == input.BeerId).Any();
                    if (barBeersExists) return Helper.DuplicateRecord();

                    await this._context.BarBeerRepository.Add(input);
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
        public async Task<BarBeerData> GetBarsByIdWithBeers(Int32 id)
        {
            if (id < 1)
                return null;
            return await _context.BarBeerDataRepository.GetBarsByIdWithBeers(id);
        }
        public async Task<IEnumerable<BarBeerData>> GetBarWithBeers()
        {
            return await _context.BarBeerDataRepository.GetBarWithBeers();
        }
    }
}
