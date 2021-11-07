using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Photogram.Model.LikeDao
{
    class LikeDaoEntityFrameworkEntityFramework :
        GenericDaoEntityFramework<Like, Int64>, ILikeDao
    {

        public bool AlreadyLiked(long ImageId, long userId)
        {
            DbSet<Like> likes = Context.Set<Like>();

            var result =
                (from like in likes
                 where like.imgId == ImageId
                 where like.userId == userId
                 select like).Any();

            return result;
        }

        public int NumberOfLikes(long ImageId)
        {
            DbSet<Like> likes = Context.Set<Like>();

            var result =
                (from like in likes
                 where like.imgId == ImageId
                 select like).Count();

            return result;

        }

    }
}
