using MSQBot_API.Core.Enums;

namespace MSQBot_API.Business.Helpers
{
    public static class ImageScrapperHelper
    {

        /// <summary>
        /// Determine the image extension
        /// </summary>
        /// <param name="imageUri">url to the image</param>
        /// <returns>The link with the correct image extension</returns>
        public static string ExtractImageUri(string imageUri)
        {
            if (imageUri == string.Empty || UriContainsImage(imageUri) is false)
            {
                return string.Empty;
            }

            string extension = UriContainsJPG(imageUri) ? ImageExtensions.JPG : ImageExtensions.PNG;
            int startIndex = imageUri.IndexOf("http");
            int endIndex = (imageUri.IndexOf(extension) + extension.Length) - imageUri.IndexOf("http");
            return imageUri.Substring(startIndex, endIndex);
        }

        /// <summary>
        /// true if the url contain an image, false otherwise
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool UriContainsImage(string uri)
        {
            return UriContainsJPG(uri) || UriContainsPNG(uri);
        }

        /// <summary>
        /// Check if a given uri contains a PNG image extension
        /// </summary>
        /// <param name="uri">uri to check</param>
        /// <returns>true if the uri contain a PNG format image, false otherwise</returns>
        public static bool UriContainsPNG(string uri)
        {
            return uri.Contains(ImageExtensions.PNG);
        }

        /// <summary>
        /// Check if a given uri contains a JPEG image extension
        /// </summary>
        /// <param name="uri">uri to check</param>
        /// <returns>true if the uri contain a jpeg format image, false otherwise</returns>
        public static bool UriContainsJPG(string uri)
        {
            return uri.Contains(ImageExtensions.JPG);
        }
    }
}
