using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Photogram.Model.FollowsDao
{
    public class FollowsDaoEntityFramework : GenericDaoEntityFramework<Follows, Int64>, IFollowsDao
    {
        public List<Follows> GetAllFollowed(long userId)
        {
            Context.Set<Follows>();

            List<Follows> Seguidos =
                (from a in Context.Set<Follows>()
                 where a.userId == userId
                 select a).ToList();
            return Seguidos;
        }

        public List<Follows> GetAllFollowers(long followedId)
        {
            List<Follows> Seguidores =
            (from a in Context.Set<Follows>()
             where a.followedId == followedId
             select a).ToList();
            return Seguidores;
        }

        public bool IsFollowing(long userId, long followedId)
        {
            DbSet<Follows> TFollows = Context.Set<Follows>();

            var result=
                (from a in TFollows
                where a.userId==userId
                where a.followedId==followedId
                select a).Any();


            return result;
        }
    }
}
