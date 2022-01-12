using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.InteractionService;
using Es.Udc.DotNet.Photogram.Model.Dtos;

namespace Es.Udc.DotNet.Photogram.Web.HTTP.Actions
{
    public static class ActionsManager
    {
        private static IImageService imageService;
        private static IInteractionService interactionService;

        public static IImageService ImageService
        {
            set { imageService = value; }
        }

        public static IInteractionService InteractionService
        {
            set { interactionService = value; }
        }

        static ActionsManager()
        {
            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            imageService = iocManager.Resolve<IImageService>();
            interactionService = iocManager.Resolve<IInteractionService>();
        }

        public static List<ListItem> ImageCategories()
        {
            List<CategoryInfo> categoriesInfo = imageService.SearchAllCategories();

            List<ListItem> ddlCategories = new List<ListItem>();

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

        private static List<Tuple<string, int>> CalculateTagSizes(List<TagInfo> tagsInfo, 
            int maxFontSize, int baseFontReduction, int? stepsBeforeReduction)
        {
            List<Tuple<string, int>> tagSizes = new List<Tuple<string, int>>();
            int fontSize = maxFontSize;
            int fontReduce = baseFontReduction;
            int numTags = tagsInfo.Count();
            int tagsInfoIndex = 0;
            int stepsLeft = stepsBeforeReduction ?? 1;
            foreach (TagInfo tagInfo in tagsInfo)
            {
                if (tagsInfoIndex < 3)
                {
                    tagSizes.Add(new Tuple<string, int>(tagInfo.tag, fontSize));
                    fontSize -= fontReduce;
                }
                else
                {
                    tagSizes.Add(new Tuple<string, int>(tagInfo.tag, fontSize));
                    stepsLeft--;
                    if (stepsLeft == 0) fontSize -= 1;
                }
                tagsInfoIndex++;
            }
            return tagSizes;
        }

        private static List<T> Shuffle<T>(this List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public static List<Tuple<string,int>> TagSizes(int startIndex, int tagsToTake)
        {
            List<Tuple<string, int>> tagSizes = new List<Tuple<string, int>>();

            List<TagInfo> tagsInfo = imageService.SearchAllTags(startIndex, tagsToTake);
            int numTags = tagsInfo.Count();

            if (numTags == 10)
            {
                tagSizes = CalculateTagSizes(tagsInfo, 24, 2, 1);
            } else if (numTags == 25)
            {
                tagSizes = CalculateTagSizes(tagsInfo, 24, 2, 2);
            } else
            {
                tagSizes = CalculateTagSizes(tagsInfo, 24, 2, 0);
            }
            
            return Shuffle(tagSizes);
        }

        public static Block<ImageBasicInfo> SearchImageBasic(string keywords, string category, int startIndex, int count)
        {
            Block<ImageBasicInfo> block;

            if (string.IsNullOrEmpty(keywords) && string.IsNullOrEmpty(category))
                return block = new Block<ImageBasicInfo>(new List<ImageBasicInfo>(), false);

            if (string.IsNullOrEmpty(keywords))
                block = imageService.SearchByCategory(long.Parse(category), startIndex, count);
            else if (string.IsNullOrEmpty(category))
                block = imageService.SearchByKeywords(keywords, startIndex, count);
            else
                block = imageService.SearchByKeywordsAndCategory(keywords, long.Parse(category), startIndex, count);

            return block;
        }

        public static Block<ImageBasicInfo> SearchImageBasic(string tag, int startIndex, int count)
        {
            Block<ImageBasicInfo> block = imageService.SearchByTag(tag, startIndex, count);
            
            return block;
        }

        public static byte[] GetThumbnail(long imgId)
        {
            return imageService.GetThumbnail(imgId);
        }

        public static void LikeImage(long userId, long imgId)
        {
            if (!interactionService.AlreadyLiked(userId, imgId))
                interactionService.LikeImage(userId, imgId);
        }

        public static bool AlreadyLiked(long userId, long imgId)
        {
            return interactionService.AlreadyLiked(userId, imgId);
        }
    }
}