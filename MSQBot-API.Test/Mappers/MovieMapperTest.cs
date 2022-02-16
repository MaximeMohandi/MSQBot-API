using MSQBot_API.Business.Mappers;
using MSQBot_API.Core.Entities;
using NUnit.Framework;

namespace MSQBot_API.Test.Mappers
{
    [TestFixture]
    public class MovieService_MapMovieShould
    {

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        [TestCase()]
        public void MapMovieToDTO_MovieAsAllProperty_ReturnTrue()
        {
            

        }

        [Test]
        public void MapMovieToDTO_MovieWithoutRates_Match()
        {

        }

        [Test]
        public void MapMovieToDTO_MovieWithoutPropery_Fail(Movie movie)
        {

        }

    }
}
