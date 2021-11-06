using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using static Es.Udc.DotNet.PracticaMaD.Model.Dtos.ImageConversor;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageService : IImageService
    {
        public ImageService() { }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        public string Dir = Directory.GetCurrentDirectory();

        #region Private helper functions

        private string StoreImageFile(string imgB64, long imgId)
        {
            string dirPath = Directory.GetParent(Directory.GetParent(Dir).FullName).FullName;
            if (!Directory.Exists(dirPath + "\\images"))
            {
                dirPath = dirPath + "\\images";
                Directory.CreateDirectory(dirPath);
            }
            byte[] imgAsBytes = System.Convert.FromBase64String(imgB64);
            MemoryStream imgAsStream = new MemoryStream(imgAsBytes);

            System.Drawing.Image image = System.Drawing.Image.FromStream(imgAsStream);

            string imageName = imgId.ToString();
            string imageFile = dirPath + "\\" + imgId.ToString();

            image.Save(imageFile);

            return imageName;
        }

        private void DeleteImageFile(long imgId)
        {
            string dirPath = Directory.GetParent(Directory.GetParent(Dir).FullName).FullName;
            dirPath = dirPath + "\\images";
            Directory.CreateDirectory(dirPath);

            File.Delete(dirPath);
        }

        #endregion

        #region IImageService Members

        [Transactional]
        public void StoreImageAsBlob(ImageDto imageDto)
        {
            Image image = null;
            image.title = imageDto.title;
            image.description = imageDto.description;
            image.categoryId = imageDto.categoryId;
            image.userId = imageDto.userId;
            image.uploadDate = System.DateTime.Now;
            image.path = null;
            image.img = System.Convert.FromBase64String(imageDto.imgB64);
            ImageDao.Create(image);
        }

        [Transactional]
        public void StoreImageAsFile(ImageDto imageDto)
        {
            Image image = null;
            image.title = imageDto.title;
            image.description = imageDto.description;
            image.categoryId = imageDto.categoryId;
            image.userId = imageDto.userId;
            image.uploadDate = System.DateTime.Now;
            image.path = imageDto.imgB64;
            image.img = null;

            long previousImage = ImageDao.GetMaxImgId();
            string imageName = StoreImageFile(imageDto.imgB64, previousImage+1);

            image.path = imageName;
            ImageDao.Create(image);
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DeleteDeniedException"/>
        [Transactional]
        public void DeleteImage(long imgId, long userId)
        {
            Image image = ImageDao.Find(imgId);
            if (image.userId != userId)
            {
                throw new DeleteDeniedException(userId, image.imgId);
            }
            if (image.path != null)
            {
                DeleteImageFile(image.imgId);
            }
            ImageDao.Remove(imgId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public ImageInfo SearchImage(long imgId)
        {
            Image image = null;
            image = ImageDao.Find(imgId);
            return ToImageInfo(image);
        }

        [Transactional]
        public Block<ImageInfo> SearchByKeywords(string keywords, int startIndex, int count)
        {
            List<Image> imagesFound = null;
            imagesFound = ImageDao.FindByKeywords(keywords, startIndex, count + 1);
            bool existMoreImages = (imagesFound.Count == count + 1);

            return new Block<ImageInfo>(ToImageInfos(imagesFound), existMoreImages);
        }

        [Transactional]
        public Block<ImageInfo> SearchByKeywordsAndCategory(string keywords, string category, int startIndex, int count)
        {
            List<Image> imagesFound = null;
            imagesFound = ImageDao.FindByKeywords(keywords, category, startIndex, count + 1);
            bool existMoreImages = (imagesFound.Count == count + 1);

            return new Block<ImageInfo>(ToImageInfos(imagesFound), existMoreImages);
        }

        #endregion IImageService Members
    }
}
