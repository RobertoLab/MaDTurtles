using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.TagDao
{
    public class TagDaoEntityFramework :
        GenericDaoEntityFramework<Tag, Int32>, ITagDao
    {
        public Tag FindByName(string tagName)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from tag in tags
                 where tag.tag == tagName
                 select tag);

            if (result == null)
            {
                throw new InstanceNotFoundException(tagName, typeof(Tag).FullName);
            }

            return result.Single();
        }

        public bool ExistsByString(string tagName)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from tag in tags
                 where tag.tag == tagName
                 select tag).Any();

            return result;
        }

        public List<Tag> GetAllTagsByUse(int startIndex, int count)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from tag in tags
                 orderby tag.tagId
                 select tag).Skip(startIndex).Take(count);

            return result.OrderBy(tag => tag.imgCount).ToList();
        }

        public IDictionary<string, int> GetNumberOfImagesTagged(List<string> tagsToCheck)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            IDictionary<string, int> valuePairs = new Dictionary<string, int>();

            foreach (string tagToCheck in tagsToCheck)
            {
                var result =
                    (from tag in tags
                     where tag.tag == tagToCheck
                     select tag);

                if (result != null)
                {
                    var tagFound = result.Single();
                    valuePairs.Add(tagFound.tag, tagFound.imgCount);
                }
            }

            return valuePairs;
        }

    }
}
