using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.ImageDao
{

    public interface IImageDao : IGenericDao<Image, Int64>
    {

        /// <summary>
        /// Finds the image also with data about User, Exif, Category, Tags and Likes.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <returns cref="Image"><c></c>The image with extra information.</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        Image FindWithRelatedInfo(long imgId);

        /// <summary>
        /// Gets the maximum stored in DB identifier.
        /// For image file naming purposes.
        /// </summary>
        /// <returns>The biggest image identifier.</returns>
        long GetMaxImgId();

        /// <summary>
        /// Check if the image was uploaded by the given user.
        /// </summary>
        /// <param name="imgId">The image identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>True if the image belongs to the user, false otherwise.</returns>
        bool BelongsTo(long imgId, long userId);

        /// <summary>
        /// Returns a list of Images from DB that adecuate to a 
        /// list of keywords in either title or description of image.
        /// If none is found returns an empty list.
        /// </summary>
        /// <param name="keywords">The keywords to search separated by spaces.</param>
        /// <returns>The list of images retrieved from DB.</returns>
        List<Image> FindByKeywords(string keywords, int startIndex, int count);

        /// <summary>
        /// Returns a list of Images from DB that adecuate to a
        /// list of keywords in either title or description of image, 
        /// if a category is also given it will only retrieve images
        /// from that category.
        /// If none is found returns an empty list.
        /// </summary>
        /// <param name="keywords">the keywords to search separated by spaces.
        /// Ex:"landscape lake sunny"</param>
        /// <param name="category">the category to search only for</param>
        /// <returns>The list of images retrieved from DB.</returns>
        List<Image> FindByKeywords(string keywords, long categoryId, int startIndex, int count);


        /// <summary>
        /// Returns a list of Images that belong to the specified userId
        /// If none is found returns an empty list.
        /// </summary>
        /// <param name="userId">Id of the user that owns the images</param>
        /// <param name="startIndex">The point at which be start recovering images</param>
        /// <param name="count">The number of images we recover</param>
        /// <returns>The list of images retrieved from DB.</returns>
        List<Image> FindByUserId(long userId, int startIndex, int count);

        List<Image> FindByCategory(long categoryId, int startIndex, int count);
        /// <summary>
        /// Updates the tags for an image.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <param name="tags">The tags.</param>
        void UpdateTags(long imgId, List<Tag> tags);

        /// <summary>
        /// Searches for images stored in the DB tagged by parameter.
        /// Paginated.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>List of images.</returns>
        List<Image> FindByTag(Tag tag, int startIndex, int count);
    }
}
