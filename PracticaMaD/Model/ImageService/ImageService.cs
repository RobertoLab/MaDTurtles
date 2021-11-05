using System;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageService : IImageService
    {
        public ImageService() { }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        #region IImageService Members

        [Transactional]
        public void StoreImage(Image image)
        {
            ImageDao.Create(image);
        }

        /// <exception cref="DeleteDeniedException"/>
        [Transactional]
        public void DeleteImage(long imgId, long userId)
        {
            if (!ImageDao.BelongsTo(imgId, userId))
            {
                throw new DeleteDeniedException(imgId, userId);
            }
            ImageDao.Remove(imgId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Image SearchImage(long imgId)
        {
            Image image = null;
            image = ImageDao.Find(imgId);
            return image;
        }

        [Transactional]
        public List<Image> SearchByKeywords(string keywords)
        {
            List<Image> imagesFound = null;
            imagesFound = ImageDao.FindByKeywords(keywords);
            return imagesFound;
        }

        [Transactional]
        public List<Image> SearchByKeywordsAndCategory(string keywords, string category)
        {
            List<Image> imagesFound = null;
            imagesFound = ImageDao.FindByKeywords(keywords, category);
            return imagesFound;
        }

        #endregion IImageService Members
    }
}
