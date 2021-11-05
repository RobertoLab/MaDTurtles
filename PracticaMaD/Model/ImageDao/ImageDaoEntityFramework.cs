using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{
    public class ImageDaoEntityFramework :
        GenericDaoEntityFramework<Image, Int64>, IImageDao
    {
        public bool BelongsTo(long imgId, long userId)
        {
            DbSet<Image> images = Context.Set<Image>();

            var result =
                (from img in images
                 where img.imgId == imgId
                 where img.userId == userId
                 select img).Any();

            return result;
        }

        public List<Image> FindByKeywords(string keywordCriteria, string categoryCriteria)
        {
            DbSet<Image> images = Context.Set<Image>();
            DbSet<Category> categories = Context.Set<Category>();

            List<string> keywords = keywordCriteria.Split(' ').ToList();
            
            var result =
                 (from img in images
                 join cat in categories on img.categoryId equals cat.categoryId
                 where cat.category == categoryCriteria
                 select img);

            foreach (string keyword in keywords)
            {
                result.Where(image => image.title.Contains(keyword)
                   || image.description.Contains(keyword));
            }

            return result.ToList<Image>();
        }

        public List<Image> FindByKeywords(string criteria)
        {
            DbSet<Image> images = Context.Set<Image>();

            List<string> keywords = criteria.Split(' ').ToList();

            var result =
                (from img in images
                 select img).ToList<Image>();

            return result;
        }
    }
}
