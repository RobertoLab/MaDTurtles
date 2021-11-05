using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    interface IImageService
    {
        /// <summary>
        /// Stores the image.
        /// </summary>
        /// <param name="img">The image.</param>
        void StoreImage(Image image);

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
        Image SearchImage(long imgId);

        /// <summary>
        /// Searches for images by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns>The list of Images retrieved from the DB.</returns>
        List<Image> SearchByKeywords(string keywords);

        /// <summary>
        /// Searches for images by keywords and category.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category.</param>
        /// <returns>The list of Images retrieved from the DB.</returns>
        List<Image> SearchByKeywordsAndCategory(string keywords, string category);
    }
}
