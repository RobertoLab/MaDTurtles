using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.Dtos;

namespace Es.Udc.DotNet.Photogram.Web.HTTP.Actions
{
    public class ActionsManager
    {
        private static IImageService imageService;

        public IImageService ImageService
        {
            set { imageService = value; }
        }

        static ActionsManager()
        {
            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            imageService = iocManager.Resolve<IImageService>();
        }

        public static List<ListItem> ImageCategories()
        {
            List<CategoryInfo> categoriesInfo = imageService.SearchAllCategories();

            List<ListItem> ddlCategories = new List<ListItem>();
            int itemIndex = 0;

            foreach(CategoryInfo categoryInfo in categoriesInfo)
            {
                string category = categoryInfo.category;
                long categoryId = categoryInfo.categoryId;

                string capitalCategory = char.ToUpper(category[0]) +
                    category.Substring(1);

                ListItem ddlCategoryItem = 
                    new ListItem(capitalCategory, categoryId.ToString());
                ddlCategories.Add(ddlCategoryItem);
            }

            return ddlCategories;
        } 

        public static void UploadImage(ImageDto imageDto)
        {
            imageService.StoreImage(imageDto);
        }
    }
}