using AutoMapper;
using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using BeerBreweryBar.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BeerBreweryBar.Models.POCO;

namespace BeerBreweryBarUnitTesting.Tests
{
    public class BreweryServiceTest
    {
        private readonly Mock<IUnitOfWork> mockUnit;
        private readonly IBreweryService service;
        private readonly IMapper _mapper;
        private readonly List<Brewery> brewerys = new List<Brewery>() { };
        private readonly List<BreweryBeer> breweryBeer = new List<BreweryBeer>() { };
        public BreweryServiceTest()
        {
            brewerys.Add(new Brewery() { Id = 10, Name = "Brewery1" });
            breweryBeer.Add(new BreweryBeer() { BreweryId = 10, Name = "BreweryBeer1" });
            mockUnit = new Mock<IUnitOfWork>();
            this.service = new BreweryService(this.mockUnit.Object);
            mockUnit.Setup(x => x.BeerRepository.GetById(It.IsAny<int>())).ReturnsAsync(
                (int input) => input == 0 ? null : new Beer() { Id = input, Name = "Beer1", PercentageAlcoholByVolume = 1 }
            );
            mockUnit.Setup(x => x.BreweryRepository.GetById(It.IsAny<int>())).ReturnsAsync(
                (int input) => input == 0 ? null : new Brewery() { Id = input, Name = "Brewery1" }
                );
            mockUnit.Setup(x => x.BreweryRepository.GetAll()).ReturnsAsync(brewerys);
            mockUnit.Setup(x => x.BreweryBeerRepository.GetBreweryBeerById(It.IsAny<int>())).ReturnsAsync(
                            (int input) => input == 0 ? null : new BreweryBeer() { BreweryId = input, Name = "Brewery1" }
                            );
            mockUnit.Setup(x => x.BreweryBeerRepository.GetBreweriesWithBeers()).ReturnsAsync(breweryBeer);
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(10, 10)]
        public void GetById_Test(int id, dynamic expectedResult)
        {
            var result = service.GetBrewery(id);
            Assert.AreEqual(expectedResult, result?.Result?.Id);
        }

        [Test]
        public void GetAllBrewerys_Success_Test()
        {
            var result = service.GetBrewery();
            Assert.AreEqual(brewerys, result.Result);
        }

        [Test]
        [TestCase(true, 200)]
        [TestCase(false, 500)]
        public void PostBrewery_Test(bool flag, int expectedResult)
        {
            Brewery brewery = flag == true ? new Brewery() : null;
            var value = service.PostBrewery(brewery);
            Assert.AreEqual(expectedResult, ((StatusCodeResult)value.Result).StatusCode);
        }

        [Test]
        [TestCase(true, 200)]
        [TestCase(false, 500)]
        public void PutBrewery_Test(bool flag, int expectedResult)
        {
            Brewery brewery = flag == true ? new Brewery() { Id = 2 } : null;
            var value = service.PutBrewery(brewery);
            Assert.AreEqual(expectedResult, ((StatusCodeResult)value.Result).StatusCode);
        }

        [Test]
        public void PutBrewery_Validation_Test()
        {
            var value = service.PutBrewery(new Brewery());
            Assert.AreEqual(400, ((ContentResult)value.Result).StatusCode);
            Assert.AreEqual("Record not found!", ((ContentResult)value.Result).Content);
        }
        [Test]
        [TestCase(1, 200)]
        [TestCase(0, 400)]
        [TestCase(null, 500)]
        public void PutBreweryBeer_Test(dynamic id, int expectedResult)
        {
            BreweryBeerBar breweryBeerbar = null;
            if (id != null)
                breweryBeerbar = new BreweryBeerBar() { BeerId = id, BreweryId = id };

            var value = service.PostBreweryBeer(breweryBeerbar);
            Assert.AreEqual(expectedResult, ((StatusCodeResult)value.Result).StatusCode);
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(10, 10)]
        public void GetBreweriesByIdWithBeers_Test(int id, dynamic expectedResult)
        {
            var result = service.GetBreweriesByIdWithBeers(id);
            Assert.AreEqual(expectedResult, result?.Result?.BreweryId);
        }

        [Test]
        public void GetBreweriesWithBeers_Test()
        {
            var result = service.GetBreweriesWithBeers();
            Assert.AreEqual(breweryBeer, result.Result);
        }
    }
}