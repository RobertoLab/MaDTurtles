using Ninject;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.TagDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
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
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DeleteDeniedException"/>
        [Transactional]
        void DeleteImage(long imgId, long userId);

        /// <summary>
        /// Searches the image eager fetch, gets also User, Category and Exif info.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <returns cref="ImageInfo">DTO of the Image retrieved from the DB
        /// </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ImageInfo SearchImageEager(long imgId);

        /// <summary>
        /// Searches the image details, these do not contain the image itself.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <returns cref="ImageBasicInfo">DTO of the Image retrieved from the DB
        /// </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ImageBasicInfo SearchImageBasic(long imgId);

        /// <summary>
        /// Search for an image, lazy fetch.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <returns>The image retrieved from the DB.</returns>
        /// <exception cref="InstanceNotFoundException"/>
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

        /// <summary>
        /// Searches for images uploaded by an user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Block with list and if there exists more elemnets in DB</returns>
        [Transactional]
        Block<ImageBasicInfo> SearchByUserId(long userId, int startIndex, int count);

        /// <summary>
        /// Searches for images stored by a category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Block with list and if there exists more elemnets in DB</returns>
        [Transactional]
        Block<ImageBasicInfo> SearchByCategory(long categoryId, int startIndex, int count);

        /// <summary>
        /// A wrapper for <c>SearchByCategory</c> that allows to search
        /// via the name of the category instead of the id.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Block with list and if there exists more elemnets in DB</returns>
        [Transactional]
        Block<ImageBasicInfo> SearchByCategory(string category, int startIndex, int count);
        
        /// <summary>
        /// Searches images stored by a tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Block with list and if there exists more elemnets in DB</returns>
        [Transactional]
        Block<ImageBasicInfo> SearchByTag(string tag, int startIndex, int count);

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

        /// <summary>
        /// Gets the image content as a byte array.
        /// </summary>
        /// <param name="imageId">The image identifier.</param>
        /// <returns>The image content as byte array.</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        byte[] GetImage(long imageId);

        /// <summary>
        /// Gets a thumbnail for an image as a byte array.
        /// </summary>
        /// <param name="imageId">The image identifier.</param>
        /// <returns>The thumbnail content as byte array.</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        byte[] GetThumbnail(long imageId);

        /// <summary>
        /// Determines whether the specified user identifier is propietary of the image.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="imgId">The img identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified user identifier is propietary; otherwise, <c>false</c>.
        /// </returns>
        bool IsPropietary(long userId, long imgId);
    }
}
