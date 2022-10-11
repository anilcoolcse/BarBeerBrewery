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
    public class BeerServiceTest
    {
        private readonly Mock<IUnitOfWork> mockUnit;
        private readonly IBeerService service;
        private readonly IMapper _mapper;
        private readonly List<Beer> beers = new List<Beer>() { };
        public BeerServiceTest()
        {
            _mapper = new MapperConfiguration(c => c.CreateMap<Beer, BeerData>()).CreateMapper();
            beers.Add(new Beer() { Id = 10, Name = "Beer1", PercentageAlcoholByVolume = 1 });
            mockUnit = new Mock<IUnitOfWork>();
            this.service = new BeerService(this.mockUnit.Object, this._mapper);
            mockUnit.Setup(x => x.BeerRepository.GetById(It.IsAny<int>())).ReturnsAsync(
                (int input) => input == 0 ? null : new Beer() { Id = input, Name = "Beer1", PercentageAlcoholByVolume = 1 }
                );
            mockUnit.Setup(x => x.BeerRepository.GetAll()).ReturnsAsync(beers);

            //mockUnit.Setup(x => x.BeerRepository.Add(beer)).Returns(
            //    (Beer input) => input.Name == "" ? Task.FromResult(400) : Task.FromResult(200)
            //);
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(10, 10)]
        public void GetById_Test(int id, dynamic expectedResult)
        {
            var result = service.GetBeer(id);
            Assert.AreEqual(expectedResult, result?.Result?.Id);
        }

        [Test]
        public void GetAllBeers_Success_Test()
        {
            var result = service.GetBeer();
            Assert.AreEqual(beers, result.Result);
        }

        [Test]
        [TestCase(true, 200)]
        [TestCase(false, 500)]
        public void PostBeer_Test(bool flag, int expectedResult)
        {
            Beer beer = flag == true ? new Beer() : null;
            var value = service.PostBeer(beer);
            Assert.AreEqual(expectedResult, ((StatusCodeResult)value.Result).StatusCode);
        }

        [Test]
        [TestCase(true, 200)]
        [TestCase(false, 500)]
        public void PutBeer_Test(bool flag, int expectedResult)
        {
            Beer beer = flag == true ? new Beer() { Id = 2 } : null;
            var value = service.PutBeer(beer);
            Assert.AreEqual(expectedResult, ((StatusCodeResult)value.Result).StatusCode);
        }

        [Test]
        public void PutBeer_Validation_Test()
        {
            var value = service.PutBeer(new Beer());
            Assert.AreEqual(400, ((ContentResult)value.Result).StatusCode);
            Assert.AreEqual("Record not found!", ((ContentResult)value.Result).Content);
        }
    }
}