using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Photogram.Model.LikeDao
{
    public interface ILikeDao : IGenericDao<Like, Int64>
    {

        /// <summary>
        /// Check if the image was liked by the given user.
        /// </summary>
        /// <param name="imgId">The image identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>True if the image has been already liked by the user, false otherwise.</returns>
        bool AlreadyLiked(long imgId, long userId);

        /// <summary>
        /// Returns the number of times an image was given a like.
        /// </summary>
        /// <param name="imgId">The image identifier.</param>
        /// <returns>The number of times an image was given a like.</returns>
        int NumberOfLikes(long imgId);
    }
}
