using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.FollowsDao
{
    public interface IFollowsDao : IGenericDao<Follows,Int64>
    {
        bool IsFollowing(long userId, long followedId);

        List<Follows> GetAllFollowed(long userId);

        List<Follows> GetAllFollowers(long followedId);
    }
}
