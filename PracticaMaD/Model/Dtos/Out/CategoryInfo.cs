using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class CategoryInfo
    {
        public long categoryId;

        public string category;

        public CategoryInfo(long categoryId, string category)
        {
            this.categoryId = categoryId;
            this.category = category;
        }
    }
}
