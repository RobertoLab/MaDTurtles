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
    }
}
