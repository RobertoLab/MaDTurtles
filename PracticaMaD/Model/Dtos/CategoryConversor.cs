using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public static class CategoryConversor
    {
        public static CategoryInfo ToCategoryInfo(Category category)
        {
            return new CategoryInfo(category.categoryId, category.category);
        }

        public static List<CategoryInfo> ToCategoryInfos(List<Category> categories)
        {
            List<CategoryInfo> categoryInfos = new List<CategoryInfo>();
            categories.ForEach(category => categoryInfos.Add(ToCategoryInfo(category)));
            return categoryInfos;
        }
    }
}
