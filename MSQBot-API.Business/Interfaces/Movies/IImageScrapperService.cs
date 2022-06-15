namespace MSQBot_API.Business.Interfaces.Movies
{
    public interface IImageScrapperService
    {
        /// <summary>
        /// Find an image from a search request
        /// </summary>
        /// <param name="searchQuery">the image wanted</param>
        /// <returns>Image uri</returns>
        public string FindImage(string searchQuery);
    }
}