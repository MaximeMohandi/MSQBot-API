using HtmlAgilityPack;
using MSQBot_API.Core.Helpers;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Business.Services.ImageScrapper
{
    public class GoogleImageScrapperServices : IImageScrapperService
    {
        private readonly HtmlWeb _web;

        private readonly string _searchHost = "https://www.google.fr/search?q=";

        public GoogleImageScrapperServices()
        {
            _web = new HtmlWeb();
        }

        public string FindImage(string searchQuery)
        {
            try
            {
                string googleImagesHost = _searchHost + searchQuery + "&tbm=isch";
                HtmlDocument doc = _web.Load(googleImagesHost);

                //select first image in google image result
                string imageLink = doc.DocumentNode.SelectNodes("//a") //get all <a></a> tags in html
                    .Where(link => ImageScrapperHelper.UriContainsImage(link.Attributes["href"].Value)) //select only the link containing image extension
                    .Select(link => link.Attributes["href"].Value)
                    .FirstOrDefault();

                return ImageScrapperHelper.ExtractImageUri(imageLink);
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}