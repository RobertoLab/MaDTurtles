using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Photogram.Model.CategoryDao
{

    public interface ICategoryDao : IGenericDao<Category, Int32>
    {
        /// <summary>
        /// Gets the category as string stored in the DB.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>The category stored as a string.</returns>
        string GetCategoryValue(long categoryId);

        /// <summary>
        /// Gets the category identifier via the category name.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The category identifier. Or null if it does not exist.</returns>
        long GetCategoryId(string category);

        /// <summary>
        /// Gets all categories by name, descending order.
        /// </summary>
        /// <returns>The list with all categories.</returns>
        List<Category> GetAllCategoriesByName();
    }
}
