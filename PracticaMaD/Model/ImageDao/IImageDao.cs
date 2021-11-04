using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{

    public interface IImageDao : IGenericDao<Image, Int64>
    {
        /// <summary>
        /// Returns an Image from DB by a given identifier. 
        /// If the identifier does not exist, a null value is returned.
        /// </summary>
        /// <param name="imgId">the image identifier</param>
        /// <returns>the image retrieved</returns>
        Image FindByImgId(long imgId);

        /// <summary>
        /// Returns a list of Images from DB that adecuate to a
        /// list of keywords in either title or description of image, 
        /// if a category is also given it will only retrieve images
        /// from that category.
        /// If the category is null or empty it will search indepenently 
        /// of the category.
        /// If none is found returns an empty list.
        /// </summary>
        /// <param name="keywords">the keywords to search separated by spaces.
        /// Ex:"landscape lake sunny"</param>
        /// <param name="category">the category to search only for</param>
        /// <returns></returns>
        List<Image> FindByKeywords(string keywords, string category);
    }
}
