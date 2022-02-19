using HtmlAgilityPack;
using MSQBot_API.Core.Enums;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Business.Services.ImageScrapper
{
    public class GoogleImageScrapperServices : IImageScrapperService
    {
        //private readonly ILogger _logger;
        private readonly HtmlWeb _web;

        private readonly string _searchHost = "https://www.google.fr/search?q=";

        public GoogleImageScrapperServices(/*ILogger logger*/)
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
                    .Where(link => UriContainsImage(link.Attributes["href"].Value)) //select only the link containing image extension
                    .Select(link => link.Attributes["href"].Value)
                    .FirstOrDefault();

                return ExtractImageUri(imageLink);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Something went wrong when fetching image from Google");
                return String.Empty;
            }
        }

        #region utils

        /// <summary>
        /// Determine the image extension
        /// </summary>
        /// <param name="imageUri">url to the image</param>
        /// <returns>The link with the correct image extension</returns>
        protected string ExtractImageUri(string imageUri)
        {
            string extension = UriContainsJPEG(imageUri) ? ImageExtensions.JPG : ImageExtensions.PNG;
            int startIndex = imageUri.IndexOf("http");
            int endIndex = (imageUri.IndexOf(extension) + extension.Length) - imageUri.IndexOf("http");
            return imageUri.Substring(startIndex, endIndex);
        }

        /// <summary>
        /// true if the url contain an image, false otherwise
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected bool UriContainsImage(string uri)
        {
            return UriContainsJPEG(uri) || UriContainsPNG(uri);
        }

        /// <summary>
        /// true if the uri contain a png format image, false otherwise
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private bool UriContainsPNG(string uri)
        {
            return uri.Contains(ImageExtensions.PNG);
        }

        /// <summary>
        /// true if the uri contain a jpeg format image, false otherwise
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private bool UriContainsJPEG(string uri)
        {
            return uri.Contains(ImageExtensions.JPG);
        }

        #endregion utils
    }
}