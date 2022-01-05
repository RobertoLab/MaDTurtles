using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Photogram.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int32>
    {
        /// <summary>
        /// Finds tag by the string in the DB.
        /// </summary>
        /// <param name="tag">The tag as string.</param>
        /// <returns>The tag.</returns>
        Tag FindByName(string tag);

        /// <summary>
        /// Existses the by name of tag.
        /// </summary>
        /// <param name="tag">The tag name.</param>
        /// <returns>True if exists, otherwise false.</returns>
        bool ExistsByString(string tag);

        /// <summary>
        /// Gets the number of images tagged by an specific tag.
        /// </summary>
        /// <param name="tags">The tag List to retrieve info.</param>
        /// <returns>The count of images tagged for each tag.</returns>
        IDictionary<string, int> GetNumberOfImagesTagged(List<string> tags);

        /// <summary>
        /// Gets all tags by the number of images they tag, ascending order.
        /// </summary>
        /// <returns>The list of all tags ordered 
        /// by the number of images they tag.</returns>
        List<Tag> GetAllTagsByUse(int startIndex, int count);

        // ------TODO-------
        // PAGINACION DE LAS ETIQUETAS
        // CUANTAS ETIQUETAS HAY PARA DEVOLVER (PARA LA NUBE)
        // GetNumberOfImagesTagged QUE USE UNA LISTA (POR EFICIENCIA)
    }
}
