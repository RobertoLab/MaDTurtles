using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.TagDao;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public interface IImageService
    {
        [Inject]
        IImageDao ImageDao { set; }

        [Inject]
        ITagDao TagDao { set; }

        /// <summary>
        /// Stores the image, saves the image in the DB as bytes.
        /// </summary>
        /// <param name="img">The image.</param>
        [Transactional]
        Image StoreImageAsBlob(ImageDto imageDto);

        /// <summary>
        /// Stores the image, and saves the image in the file system.
        /// </summary>
        /// <param name="img">The image.</param>
        [Transactional]
        Image StoreImageAsFile(ImageDto imageDto);

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <exception cref="InstanceNotFoundException">
        [Transactional]
        void DeleteImage(long imgId, long userId);

        /// <summary>
        /// Searches the image eager fetch, gets also User, Category and Exif info.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <returns>The Image retrieved from the DB</returns>
        [Transactional]
        ImageInfo SearchImageEager(long imgId);

        /// <summary>
        /// Search for an image, lazy fetch.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <exception cref="InstanceNotFoundException">
        /// <returns>The image retrieved from the DB.</returns>
        [Transactional]
        Image SearchImage(long imgId);

        /// <summary>
        /// Searches for images by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns>The list of Images retrieved from the DB.</returns>
        [Transactional]
        Block<ImageInfo> SearchByKeywords(string keywords, int startIndex, int count);

        /// <summary>
        /// Searches for images by keywords and category.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category.</param>
        /// <returns>The list of Images retrieved from the DB.</returns>
        [Transactional]
        Block<ImageInfo> SearchByKeywordsAndCategory(string keywords, string category, int startIndex, int count);

        /// <summary>
        /// Changes the tagging of an image.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <param name="tags">The new tags of the image.</param>
        [Transactional]
        void ModifyImageTags(long imgId, string tags);
    }
}
