using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.ImageDao
{
    public class ImageDaoEntityFramework :
        GenericDaoEntityFramework<Image, Int64>, IImageDao
    {

        public Image FindWithRelatedInfo(long imgId)
        {
            DbSet<Image> images = Context.Set<Image>();

            var result =
                (from img in images.Include("User").Include("Category").Include("Exifs").Include("Tags").Include("UsersLikes").Include("Comments")
                 where img.imgId == imgId
                 select img);
            
            if (result.Count() == 0)
            {
                throw new InstanceNotFoundException(imgId, typeof(Image).FullName);
            }

            return result.Single();
        }

        public long GetMaxImgId()
        {
            DbSet<Image> images = Context.Set<Image>();

            var result =
                (from img in images
                 select img.imgId);

            if(result.Count() == 0)
            {
                return 1;
            }

            return result.Max();
        }

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

        public List<Image> FindByKeywords(string keywordCriteria, long categoryId
            , int startIndex, int count)
        {
            DbSet<Image> images = Context.Set<Image>();
            DbSet<Category> categories = Context.Set<Category>();

            List<string> keywords = keywordCriteria.Split(' ').ToList();
            
            var result =
                 (from img in images.Include("User").Include("Category").Include("Exifs").Include("Tags").Include("UsersLikes").Include("Comments")
                  join cat in categories on img.categoryId equals cat.categoryId
                 where cat.categoryId == categoryId
                  orderby img.imgId
                 select img).Skip(startIndex).Take(count);

            List<Image> foundImages = new List<Image>();

            foreach (string keyword in keywords)
            {
                string keywordLowerCase = keyword.ToLower();
                foundImages.AddRange(result.Where(image => image.title.ToLower().Contains(keywordLowerCase)
                   || image.description.ToLower().Contains(keywordLowerCase)).ToList());
            }

            return foundImages;
        }

        public List<Image> FindByKeywords(string criteria, int startIndex, int count)
        {
            DbSet<Image> images = Context.Set<Image>();

            List<string> keywords = criteria.Split(' ').ToList();

            var result =
                (from img in images.Include("User").Include("Category").Include("Exifs").Include("Tags").Include("UsersLikes").Include("Comments")
                 orderby img.imgId
                 select img).Skip(startIndex).Take(count).ToList<Image>();

            List<Image> foundImages = new List<Image>();

            foreach (string keyword in keywords)
            {
                string keywordLowerCase = keyword.ToLower();
                foundImages.AddRange(result.Where(image => image.title.ToLower().Contains(keywordLowerCase)
                   || image.description.ToLower().Contains(keywordLowerCase)).ToList());
            }

            return foundImages;
        }

        public void UpdateTags(long imgId, List<Tag> tags)
        {
            DbSet<Image> images = Context.Set<Image>();

            Image image = Find(imgId);
            image.Tags = tags;
            Update(image);
        }

        public List<Image> FindByTag(Tag tagCriteria, int startIndex, int count)
        {
            DbSet<Image> images = Context.Set<Image>();
            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from img in images.Include("User").Include("Category").Include("Exifs").Include("Tags").Include("UsersLikes").Include("Comments")
                 orderby img.imgId
                 select img).Skip(startIndex).Take(count).ToList();

            List<Image> foundImages = new List<Image>();

            foreach (Image image in result)
            {
                if (image.Tags.Contains(tagCriteria)) foundImages.Add(image);
            }

            return foundImages;
        }

        public List<Image> FindByCategory(long categoryId, int startIndex, int count)
        {
            DbSet<Image> images = Context.Set<Image>();
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from img in images.Include("User").Include("Category").Include("Exifs").Include("Tags").Include("UsersLikes").Include("Comments")
                 join cat in categories on img.categoryId equals cat.categoryId
                 where cat.categoryId == categoryId
                 orderby img.imgId
                 select img).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<Image> FindByUserId(long userId, int startIndex, int count)
        {
            DbSet<Image> images = Context.Set<Image>();

            var result =
                (from img in images
                 where img.userId == userId
                 orderby img.uploadDate
                 select img).Take(count).ToList();

            return result;
        }
    }
}
