using AutoMapper;
using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using BeerBreweryBar.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using BeerBreweryBar.Models.POCO;

namespace BeerBreweryBarUnitTesting.Tests
{
    public class BarServiceTest
    {
        private readonly Mock<IUnitOfWork> mockUnit;
        private readonly IBarService service;
        private readonly IMapper _mapper;
        private readonly List<Bar> bars = new List<Bar>() { };
        private readonly List<BarBeerData> barBeerData = new List<BarBeerData>() { };
        public BarServiceTest()
        {
            bars.Add(new Bar() { Id = 10, Name = "Bar1" });
            barBeerData.Add(new BarBeerData() { BarId = 1, BarName = "Bar1", BarAddress = "Address" });
            mockUnit = new Mock<IUnitOfWork>();
            this.service = new BarService(this.mockUnit.Object);
            mockUnit.Setup(x => x.BarRepository.GetById(It.IsAny<int>())).ReturnsAsync(
                (int input) => input == 0 ? null : new Bar() { Id = input, Name = "Bar1" }
                );
            mockUnit.Setup(x => x.BeerRepository.GetById(It.IsAny<int>())).ReturnsAsync(
                (int input) => input == 0 ? null : new Beer() { Id = input, Name = "Beer1", PercentageAlcoholByVolume = 1 }
            );
            mockUnit.Setup(x => x.BarRepository.GetAll()).ReturnsAsync(bars);

            mockUnit.Setup(x => x.BarBeerDataRepository.GetBarsByIdWithBeers(It.IsAny<int>())).ReturnsAsync(
                (int input) => input == 0 ? null : new BarBeerData() { BarId = input, BarName = "Brewery1",BarAddress = "BarAddress"}
            );
            mockUnit.Setup(x => x.BarBeerDataRepository.GetBarWithBeers()).ReturnsAsync(barBeerData);
            mockUnit.Setup(x => x.BarBeerRepository.GetAll()).ReturnsAsync(new List<BarBeer>());
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(10, 10)]
        public void GetById_Test(int id, dynamic expectedResult)
        {
            var result = service.GetBar(id);
            Assert.AreEqual(expectedResult, result?.Result?.Id);
        }

        [Test]
        public void GetAllBars_Success_Test()
        {
            var result = service.GetBar();
            Assert.AreEqual(bars, result.Result);
        }

        [Test]
        [TestCase(true, 200)]
        [TestCase(false, 500)]
        public void PostBar_Test(bool flag, int expectedResult)
        {
            Bar bar = flag == true ? new Bar() : null;
            var value = service.PostBar(bar);
            Assert.AreEqual(expectedResult, ((StatusCodeResult)value.Result).StatusCode);
        }

        [Test]
        [TestCase(true, 200)]
        [TestCase(false, 500)]
        public void PutBar_Test(bool flag, int expectedResult)
        {
            Bar bar = flag == true ? new Bar() { Id = 2 } : null;
            var value = service.PutBar(bar);
            Assert.AreEqual(expectedResult, ((StatusCodeResult)value.Result).StatusCode);
        }

        [Test]
        public void PutBar_Validation_Test()
        {
            var value = service.PutBar(new Bar());
            Assert.AreEqual(400, ((ContentResult)value.Result).StatusCode);
            Assert.AreEqual("Record not found!", ((ContentResult)value.Result).Content);
        }
        [Test]
        [TestCase(1, 200)]
        [TestCase(0, 400)]
        [TestCase(null, 500)]
        public void PostBarBeer_Test(dynamic id, int expectedResult)
        {
            BarBeer barBeer = null;
            if (id != null)
                barBeer = new BarBeer() { BeerId = id, BarId = id, Id = id};

            var value = service.PostBarBeer(barBeer);
            Assert.AreEqual(expectedResult, ((StatusCodeResult)value.Result).StatusCode);
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(10, 10)]
        public void GetBarsByIdWithBeers_Test(int id, dynamic expectedResult)
        {
            var result = service.GetBarsByIdWithBeers(id);
            Assert.AreEqual(expectedResult, result?.Result?.BarId);
        }

        [Test]
        public void GetBarWithBeers_Test()
        {
            var result = service.GetBarWithBeers();
            Assert.AreEqual(barBeerData, result.Result);
        }
    }
}