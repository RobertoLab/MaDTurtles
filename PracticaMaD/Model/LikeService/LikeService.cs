using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.LikeDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Photogram.Model.LikeService
{
    public class LikeService : ILikeService
    {
        public LikeService() { }

        [Inject]
        public ILikeDao LikeDao { private get; set; }


        [Transactional]
        public void LikeImage(long userId, long imgId)
        {
            Like like = null;
            like.imgId = imgId;
            like.userId = userId;
            if (!LikeDao.AlreadyLiked(imgId,userId))
            {
                LikeDao.Create(like);
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void Unlike(long likeId)
        {
            Like like = LikeDao.Find(likeId);

            LikeDao.Remove(likeId);
        }


        [Transactional]
        public int GetImageLikes(long imgId)
        {
            return LikeDao.NumberOfLikes(imgId);
        }

    }
}
