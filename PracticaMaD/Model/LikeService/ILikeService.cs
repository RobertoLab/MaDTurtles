using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Photogram.Model.LikeDao;

namespace Es.Udc.DotNet.Photogram.Model.LikeService
{
    interface ILikeService
    {
        [Inject]
        ILikeDao LikeDao { set; }

        /// <summary>
        /// Likes an image.
        /// </summary>
        /// <param name="userId">The user that likes the image.</param>
        /// <param name="imgId">The image liked.</param>
        [Transactional]
        void LikeImage(long userId, long imgId);

        /// <summary>
        /// Unlikes an image .
        /// </summary>
        /// <param name="likeId">Id of the like you want to delete.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void Unlike(long likeId);

        /// <summary>
        /// Gets the number of likes on a specific image.
        /// </summary>
        /// <param name="imgId">Id of the image from which you want to get the number of likes.</param>
        /// <returns>The number of likes in that image.</returns>
        [Transactional]
        int GetImageLikes(long imgId);

    }
}
