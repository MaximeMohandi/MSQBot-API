using MSQBot_API.Business.Helpers;
using NUnit.Framework;

namespace MSQBot_API.Core.Test.Helper
{
    public class ImageScrapperHelperTest
    {
        [TestCase("/imgres?imgurl=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.jpg&amp;", ExpectedResult = "https://fakeSite.doe/pictures/18/12/13/23/29/4823925.jpg")]
        [TestCase("/imgurl=http://otherFakeSite/images/anImage.jpg&loremIpsum;", ExpectedResult = "http://otherFakeSite/images/anImage.jpg")]
        [TestCase("url=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.png&amp;", ExpectedResult = "https://fakeSite.doe/pictures/18/12/13/23/29/4823925.png")]
        [TestCase("href=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.jpg&amp;", ExpectedResult = "https://fakeSite.doe/pictures/18/12/13/23/29/4823925.jpg")]
        public string GivenAnUri_WhenExtractImageUri_ThenImageUri(string uri)
        {
            return ImageScrapperHelper.ExtractImageUri(uri);
        }

        [TestCase("/imgres?imgurl=https://fakeSite.doe/pictures/18/12/13/23/29/4823925&amp;")]
        [TestCase("/imgurl=&loremIpsum;")]
        [TestCase("")]
        [TestCase("href=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.json&amp;")]
        public void GivenAnUriWithNoImage_WhenExtractImageUri_ThenEmpty(string uri)
        {
            Assert.That(ImageScrapperHelper.ExtractImageUri(uri), Is.EqualTo(string.Empty));
        }

        [TestCase("/imgres?imgurl=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.jpg&amp;")]
        [TestCase("/imgurl=&lorem.jpgIpsum;")]
        [TestCase(".jpg")]
        [TestCase("href=https://fakeSite.doe/pictures/18/12/13/23/29/4823925..jpgn&amp;")]
        public void GivenAnUriContainJpg_WhenUriContainsJPG_ThenTrue(string uri)
        {
            Assert.That(ImageScrapperHelper.UriContainsJPG(uri), Is.True);
        }

        [TestCase("/imgres?imgurl=https://fakeSite.doe/pictures/18/12/13/23/29/4823925jpg&amp;")]
        [TestCase("/imgurl=&loremjpegIpsum;")]
        [TestCase("jpg")]
        [TestCase("href=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.png")]
        public void GivenAnUriWithNoJpg_WhenUriContainsJPG_ThenFalse(string uri)
        {
            Assert.That(ImageScrapperHelper.UriContainsJPG(uri), Is.False);
        }

        [TestCase("/imgres?imgurl=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.png&amp;")]
        [TestCase("/imgurl=&lorem.pngIpsum;")]
        [TestCase(".png")]
        [TestCase("href=https://fakeSite.doe/pictures/18/12/13/23/29/4823925..pngn&amp;")]
        public void GivenAnUriContainPng_WhenUriContainsPNG_ThenTrue(string uri)
        {
            Assert.That(ImageScrapperHelper.UriContainsPNG(uri), Is.True);
        }

        [TestCase("/imgres?imgurl=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.pnng&amp;")]
        [TestCase("/imgurl=&lorempngIpsum;")]
        [TestCase("png")]
        [TestCase("href=https://fakeSite.doe/pictures/18/12/13/23/29/4823925.jpgn&amp;")]
        public void GivenAnUriContainPng_WhenUriContainsPNG_ThenFalse(string uri)
        {
            Assert.That(ImageScrapperHelper.UriContainsPNG(uri), Is.False);
        }
    }
}
