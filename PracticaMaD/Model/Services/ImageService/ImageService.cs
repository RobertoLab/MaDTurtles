using System.IO;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.TagDao;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using static Es.Udc.DotNet.Photogram.Model.Dtos.ImageConversor;
using static Es.Udc.DotNet.Photogram.Model.Dtos.TagConversor;
using static Es.Udc.DotNet.Photogram.Model.Dtos.CategoryConversor;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public class ImageService : IImageService
    {
        public ImageService() { }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        public string ImagesPathKey = "ImagesPath";


        //Creating the cache
        //private ObjectCache cache = MemoryCache.Default;
        //CacheItemPolicy policy = new CacheItemPolicy();
        //private int index = 1;
        
        
        #region Private helper functions

        /// <summary>
        /// Stores the image in the file system.
        /// </summary>
        /// <param name="imgB64">The imgage in base 64.</param>
        /// <param name="imgId">The img identifier.</param>
        /// <returns>The filename which it was stored by.</returns>
        private string StoreImageFile(byte[] content, long imgId)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string dirPath = appSettings[ImagesPathKey];
            Directory.CreateDirectory(dirPath);
            byte[] imgAsBytes = content;

            string imageName = imgId.ToString() + ".jpeg";
            string imageFile = dirPath + "\\" + imageName;

            File.WriteAllBytes(imageFile, imgAsBytes);

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

        private List<Tag> ParseImageTags(string tagCriteria)
        {
            List<string> tags = tagCriteria.Split(' ').ToList();

            List<Tag> ImageTags = new List<Tag>();
            foreach (string tag in tags)
            {
                string tagLowerCase = tag.ToLower();
                Tag tagToAdd = new Tag();
                if (!TagDao.ExistsByString(tagLowerCase))
                {
                    tagToAdd.tag = tagLowerCase;
                    tagToAdd.imgCount = 0;
                    TagDao.Create(tagToAdd);
                } else
                {
                    tagToAdd = TagDao.FindByName(tagLowerCase);
                }
                ImageTags.Add(tagToAdd);
            }

            return ImageTags;
        }

        [Transactional]
        private Image StoreImageAsBlob(Image image, byte[] content)
        {
            image.img = content;
            ImageDao.Create(image);
            return image;
        }

        [Transactional]
        private Image StoreImageAsFile(Image image, byte[] content)
        {
            long previousImage = ImageDao.GetMaxImgId();
            string imageName = StoreImageFile(content, previousImage + 1);

            image.path = imageName;
            ImageDao.Create(image);
            return image;
        }
        #endregion

        #region IImageService Members

        public Image StoreImage(ImageDto imageDto)
        {
            Image image = ToImage(imageDto);
            image.uploadDate = System.DateTime.Now;
            image.Tags = ParseImageTags(imageDto.tags);

            byte[] imageContent = System.Convert.FromBase64String(imageDto.imgB64);
            // Si pesa mas de 200KB se guarda en sistema de ficheros
            if (imageContent.Length > 200000)
            {
                image.img = null;
                return StoreImageAsFile(image, imageContent);
            } else
            {
                image.path = null;
                return StoreImageAsBlob(image, imageContent);
            }

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
            image = ImageDao.FindWithRelatedInfo(imgId);
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
            //if(cache.Contains(imgId)
            //return cache.Get(imgID)
            //else
            Image image = new Image();
            image = ImageDao.Find(imgId);
            //cache.Add(image.imgId, image, policy)
            return image;
        }

        [Transactional]
        public Block<ImageInfo> SearchByKeywords(string keywords, int startIndex, int count)
        {
            List<Image> imagesFound = new List<Image>();
            imagesFound = ImageDao.FindByKeywords(keywords, startIndex, count + 1);
            bool existMoreImages = (imagesFound.Count == count + 1);

            if (existMoreImages) imagesFound.RemoveAt(count);

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

            if (existMoreImages) imagesFound.RemoveAt(count);

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

        public void ModifyImageTags(long imgId ,string tagsCriteria)
        {
            List<string> tags = tagsCriteria.Split(' ').ToList();

            List<Tag> newImageTags = ParseImageTags(tagsCriteria);

            ImageDao.UpdateTags(imgId, newImageTags);
        }

        public List<TagInfo> SearchAllTags(int startIndex, int count)
        {
            List<Tag> tagsFound = TagDao.GetAllTagsByUse(startIndex, count + 1);
            bool existsMoreTags = (tagsFound.Count == count + 1);
            if (existsMoreTags) tagsFound.RemoveAt(count);

            //return new Block<TagInfo>(ToTagInfos(tagsFound), existsMoreTags);
            return ToTagInfos(tagsFound);
        }
        
        public List<CategoryInfo> SearchAllCategories()
        {
            List<CategoryInfo> categories = new List<CategoryInfo>();

            List<Category> storedCategories = CategoryDao.GetAllCategoriesByName();

            categories = ToCategoryInfos(storedCategories);

            return categories;
        }
        #endregion IImageService Members
    }
}
