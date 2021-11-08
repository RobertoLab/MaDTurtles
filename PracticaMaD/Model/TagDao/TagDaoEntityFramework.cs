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
                 select tag).ToList();

            if (result.Count == 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public List<Tag> GetAllTagsByUse()
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from tag in tags
                 orderby tag.imgCount
                 select tag).ToList<Tag>();

            return result;
        }

        public int GetNumberOfImagesTagged(string tagName)
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

            return result.Single().imgCount;
        }
        
    }
}
