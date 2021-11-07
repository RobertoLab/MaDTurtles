using System;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using static Es.Udc.DotNet.Photogram.Model.Dtos.ImageConversor;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public class ImageService : IImageService
    {
        public ImageService() { }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        public string ImagesPathKey = "ImagesPath";

        #region Private helper functions

        /// <summary>
        /// Stores the image in the file system.
        /// </summary>
        /// <param name="imgB64">The imgage in base 64.</param>
        /// <param name="imgId">The img identifier.</param>
        /// <returns>The filename which it was stored by.</returns>
        private string StoreImageFile(string imgB64, long imgId)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string dirPath = appSettings[ImagesPathKey];
            Directory.CreateDirectory(dirPath);
            byte[] imgAsBytes = System.Convert.FromBase64String(imgB64);
            //MemoryStream imgAsStream = new MemoryStream(imgAsBytes);
            
            //System.Drawing.Image image = System.Drawing.Image.FromStream(imgAsStream);

            string imageName = imgId.ToString() + ".jpeg";
            string imageFile = dirPath + "\\" + imageName;

            File.WriteAllBytes(imageFile, imgAsBytes);
            //image.Save(imageFile);
            //imgAsStream.Close();
            return imageName;
        }

        /// <summary>
        /// Deletes the image stored filename.
        /// </summary>
        /// <param name="imgId">The img identifier.</param>
        private void DeleteImageFile(string imageName)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string imagePath = appSettings[ImagesPathKey];
            imagePath = imagePath + "\\" + imageName;

            File.Delete(imagePath);
        }

        private byte[] GetImageFromFile(string imageName)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string imagePath = appSettings[ImagesPathKey];
            imagePath = imagePath + "\\" + imageName;

            FileStream imageAsFileStream = File.Open(imagePath, FileMode.Open);
            int imageAsFileStreamLength = (int) imageAsFileStream.Length;
            byte[] imageAsByte = new byte[imageAsFileStreamLength];
            imageAsFileStream.Read(imageAsByte, 0, imageAsFileStreamLength);
            imageAsFileStream.Close();
            return imageAsByte;
        }

        #endregion

        #region IImageService Members

        [Transactional]
        public Image StoreImageAsBlob(ImageDto imageDto)
        {
            Image image = new Image();
            image.title = imageDto.title;
            image.description = imageDto.description;
            image.categoryId = imageDto.categoryId;
            image.userId = imageDto.userId;
            image.uploadDate = System.DateTime.Now;
            image.path = null;
            image.img = System.Convert.FromBase64String(imageDto.imgB64);
            ImageDao.Create(image);
            return image;
        }

        [Transactional]
        public Image StoreImageAsFile(ImageDto imageDto)
        {
            Image image = new Image();
            image.title = imageDto.title;
            image.description = imageDto.description;
            image.categoryId = imageDto.categoryId;
            image.userId = imageDto.userId;
            image.uploadDate = System.DateTime.Now;
            image.img = null;

            long previousImage = ImageDao.GetMaxImgId();
            string imageName = StoreImageFile(imageDto.imgB64, previousImage+1);

            image.path = imageName;
            ImageDao.Create(image);
            return image;
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
                DeleteImageFile(image.path);
            }
            ImageDao.Remove(imgId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public ImageInfo SearchImageEager(long imgId)
        {
            Image image = new Image();
            image = ImageDao.FindWithChilds(imgId);
            if (image.path == null)
            {
                return ToImageInfo(image, System.Convert.ToBase64String(image.img));
            } else
            {
                return ToImageInfo(image, System.Convert.ToBase64String(GetImageFromFile(image.path)));
            }
            
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Image SearchImage(long imgId)
        {
            Image image = new Image();
            image = ImageDao.Find(imgId);
            return image;
        }

        [Transactional]
        public Block<ImageInfo> SearchByKeywords(string keywords, int startIndex, int count)
        {
            List<Image> imagesFound = new List<Image>();
            imagesFound = ImageDao.FindByKeywords(keywords, startIndex, count + 1);
            bool existMoreImages = (imagesFound.Count == count + 1);

            List<string> imagesAsB64 = new List<string>();
            foreach (Image image in imagesFound)
            {
                if (image.path == null)
                {
                    imagesAsB64.Add(System.Convert.ToBase64String(image.img));
                }
                else
                {
                    imagesAsB64.Add(System.Convert.ToBase64String(GetImageFromFile(image.path)));
                }
            }

            return new Block<ImageInfo>(ToImageInfos(imagesFound, imagesAsB64), existMoreImages);
        }

        [Transactional]
        public Block<ImageInfo> SearchByKeywordsAndCategory(string keywords, string category, int startIndex, int count)
        {
            List<Image> imagesFound = new List<Image>();
            imagesFound = ImageDao.FindByKeywords(keywords, category, startIndex, count + 1);
            bool existMoreImages = (imagesFound.Count == count + 1);

            List<string> imagesAsB64 = new List<string>();
            foreach(Image image in imagesFound)
            {
                if (image.path == null)
                {
                    imagesAsB64.Add(System.Convert.ToBase64String(image.img));
                } else
                {
                    imagesAsB64.Add(System.Convert.ToBase64String(GetImageFromFile(image.path)));
                }
            }

            return new Block<ImageInfo>(ToImageInfos(imagesFound, imagesAsB64), existMoreImages);
        }

        #endregion IImageService Members
    }
}
