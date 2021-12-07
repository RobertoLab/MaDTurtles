using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Photogram.Model.ImageDao;

namespace Es.Udc.DotNet.Photogram.Model.LikeService
{
    public class LikeService : ILikeService
    {
        public LikeService() { }

        [Inject]
        public IUserDao UserDao { private get; set; }
        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Transactional]
        public void LikeImage(long userId, long imgId)
        {
            Image imageToLike = ImageDao.Find(imgId);
            User user = UserDao.Find(userId);

            ICollection<Image> imagesLiked = user.ImagesLiked;
            if (!imagesLiked.Contains(imageToLike))
            {
                imagesLiked.Add(imageToLike);
                user.ImagesLiked = imagesLiked;
                UserDao.UpdateImagesLiked(user);
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void Unlike(long userId, long imgId)
        {
            Image imageToLike = ImageDao.Find(imgId);
            User user = UserDao.Find(userId);

            ICollection<Image> imagesLiked = user.ImagesLiked;
            if (imagesLiked.Contains(imageToLike))
            {
                imagesLiked.Remove(imageToLike);
                user.ImagesLiked = imagesLiked;
                UserDao.UpdateImagesLiked(user);
            }
        }


        [Transactional]
        public int GetImageLikes(long imgId)
        {
            // CAMBIAR ESTE LAZY FETCH
            return ImageDao.Find(imgId).UsersLikes.Count;
        }

    }
}
