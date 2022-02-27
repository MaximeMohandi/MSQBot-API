using MSQBot_API.Business.Services;
using MSQBot_API.Core.Interfaces;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Interfaces;
using NUnit.Framework;
using System;
using NSubstitute;
using MSQBot_API.Core.DTOs;

namespace MSQBot_API.Business.Test
{
    public class MovieServicesTest
    {
        private MovieServices _movieServices;
        private IMovieRepository _movieRepository;
        private IImageScrapperService _imageScrapperServices;
        [SetUp]
        public void Setup()
        {
            _movieRepository = Substitute.For<IMovieRepository>();
            _imageScrapperServices = Substitute.For<IImageScrapperService>();
            _movieServices = new MovieServices(_movieRepository, _imageScrapperServices);
        }

        [Test]
        public void GivenNewTitle_WhenTitleIsEmpty_ThenThrowArgumentException()
        {
            var newMovieTitle = new MovieTitleUpdateDto
            {
                MovieId = 1,
                NewTitle = ""
            };
            ;
            Assert.ThrowsAsync<ArgumentException>(async () => await _movieServices.UpdateName(newMovieTitle));
        }
    }
}
