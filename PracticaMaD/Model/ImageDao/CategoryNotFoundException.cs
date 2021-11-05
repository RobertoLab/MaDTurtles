using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{
    /// <summary>
    /// This exception is raised if the image category to search for
    /// does not exist.
    /// </summary>
    public class CategoryNotFoundException : Exception
    {
        private readonly long categoryId;
        private readonly string category;

        #region Properties Region

        public long CategoryId
        {
            get { return categoryId; }
        }

        public string Category
        {
            get { return category; }
        }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CategoryNotFoundException"/> class.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="category">The category value.</param>
        public CategoryNotFoundException(long categoryId,
            string category)
            : base("Category not found exception => " +
            "categoryIdentiffier = " + categoryId + " | " +
            "category = " + category)
        {
            this.categoryId = categoryId;
            this.category = category;
        }
    }
}
