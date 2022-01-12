using Ninject;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.TagDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public interface IImageService
    {
        [Inject]
        IImageDao ImageDao { set; }

        [Inject]
        ITagDao TagDao { set; }

        [Inject]
        ICategoryDao CategoryDao { set; }


        // 
        /// <summary>
        /// Stores the image.
        /// </summary>
        /// <param name="imageDto">The image.</param>
        [Transactional]
        Image StoreImage(ImageDto imageDto);

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
        /// Searches the image details, these do not contain the image itself.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <returns></returns>
        [Transactional]
        ImageBasicInfo SearchImageBasic(long imgId);

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
        /// <param name="startIndex">The start index for the search.</param>
        /// <param name="count">The number of elements to retrieve.</param>
        /// <returns>Block with list and if there exists more elemnets in DB</returns>
        [Transactional]
        Block<ImageBasicInfo> SearchByKeywords(string keywords, int startIndex, int count);

        /// <summary>
        /// Searches for images by keywords and category.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category.</param>
        /// <param name="startIndex">The start index for the search.</param>
        /// <param name="count">The number of elements to retrieve.</param>
        /// <returns>Block with list and if there exists more elemnets in DB</returns>
        [Transactional]
        Block<ImageBasicInfo> SearchByKeywordsAndCategory(string keywords, long categoryId, int startIndex, int count);

        [Transactional]
        Block<ImageBasicInfo> SearchByCategory(long categoryId, int startIndex, int count);

        [Transactional]
        Block<ImageBasicInfo> SearchByCategory(string category, int startIndex, int count);

        /// <summary>
        /// Changes the tagging of an image.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <param name="tags">The new tags of the image.</param>
        [Transactional]
        void ModifyImageTags(long imgId, string tags);

        /// <summary>
        /// Searches for all tags, paginated.
        /// </summary>
        /// <param name="startIndex">The start index for the search.</param>
        /// <param name="count">The number of elements to retrieve.</param>
        /// <returns>Block with list and if there exists more elemnets in DB</returns>
        [Transactional]
        List<TagInfo> SearchAllTags(int startIndex, int count);

        /// <summary>
        /// Searches all categories.
        /// </summary>
        /// <returns>The list of strings with all categories.</returns>
        [Transactional]
        List<CategoryInfo> SearchAllCategories();

        [Transactional]
        byte[] GetImage(long imageId);

        [Transactional]
        byte[] GetThumbnail(long imageId);

        [Transactional]
        Block<ImageBasicInfo> SearchByTag(string tag, int startIndex, int count);
    }
}
