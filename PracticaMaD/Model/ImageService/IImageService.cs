using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.Dtos;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public interface IImageService
    {
        /// <summary>
        /// Stores the image.
        /// </summary>
        /// <param name="img">The image.</param>
        void StoreImageAsBlob(ImageDto imageDto);

        /// <summary>
        /// Stores the image.
        /// </summary>
        /// <param name="img">The image.</param>
        void StoreImageAsFile(ImageDto imageDto);

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <exception cref="InstanceNotFoundException">
        void DeleteImage(long imgId, long userId);

        /// <summary>
        /// Search for an image.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <exception cref="InstanceNotFoundException">
        /// <returns>The image retrieved from the DB.</returns>
        ImageInfo SearchImage(long imgId);

        /// <summary>
        /// Searches for images by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns>The list of Images retrieved from the DB.</returns>
        Block<ImageInfo> SearchByKeywords(string keywords, int startIndex, int count);

        /// <summary>
        /// Searches for images by keywords and category.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category.</param>
        /// <returns>The list of Images retrieved from the DB.</returns>
        Block<ImageInfo> SearchByKeywordsAndCategory(string keywords, string category, int startIndex, int count);
    }
}
