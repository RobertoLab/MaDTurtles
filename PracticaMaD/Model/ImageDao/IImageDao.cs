using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Photogram.Model.ImageDao
{

    public interface IImageDao : IGenericDao<Image, Int64>
    {

        /// <summary>
        /// Finds the image also with info about User, Exif and Category.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <returns>The image with extra information.</returns>
        Image FindWithChilds(long imgId);

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
        List<Image> FindByKeywords(string keywords, string category, int startIndex, int count);

        /// <summary>
        /// Updates the tag fora an image.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        /// <param name="tags">The tags.</param>
        void UpdateTags(long imgId, List<Tag> tags);


        // ------TODO-------
        // METODO PARA COGER TODAS LAS IMAGENES ETIQUETADAS POR AUNA ETIQUETA
        // METODO PARA SABER SI UNA IMAGEN TIENE COMENTARIOS
        // METODO PARA SABER SI UNA IMAGEN TIENE COMENTARIOS
    }
}
