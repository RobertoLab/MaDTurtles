using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public static class ImageConversor
    {
        private static List<string> TagsToStrings(ICollection<Tag> tags)
        {
            List<string> tagsNames = new List<string>();
            foreach (Tag tag in tags)
            {
                tagsNames.Add(tag.tag);
            }
            return tagsNames;
        }

        public static ImageInfo ToImageInfo(Image image, string imageAsB64)
        { 
            ImageInfo imageInfo = new ImageInfo(image.title, image.description
                , image.uploadDate , image.categoryId, image.Category.category
                , imageAsB64, image.userId, image.User.userName, 
                image.Exifs.ToList(), TagsToStrings(image.Tags), image.UsersLikes.Count());

            return imageInfo;
        }
        
        public static List<ImageInfo> ToImageInfos(List<Image> images, List<string> imagesAsB64)
        {
            List<ImageInfo> imagesInfo = new List<ImageInfo>();

            for(int index = 0; index < images.Count; index++)
            {
                Image image = images.ElementAt(index);
                string imageAsB64 = imagesAsB64.ElementAt(index);

                imagesInfo.Add(ToImageInfo(image, imageAsB64));
            }

            return imagesInfo;
        }

        public static Image ToImage(ImageDto imageDto)
        {
            Image image = new Image();
            image.title = imageDto.title;
            image.description = imageDto.description;
            image.categoryId = imageDto.categoryId;
            image.userId = imageDto.userId;
            List<Exif> exifs = new List<Exif>();
            if (!float.IsNaN(imageDto.diaphragm))
            {
                Exif diaphragm = new Exif
                {
                    infoType = "f",
                    value = (decimal)imageDto.diaphragm
                };
                exifs.Add(diaphragm);
            }
            if (!float.IsNaN(imageDto.exposureTime))
            {
                Exif exposureTime = new Exif
                {
                    infoType = "t",
                    value = (decimal)imageDto.exposureTime
                };
                exifs.Add(exposureTime);
            }
            if (!float.IsNaN(imageDto.iso))
            {
                Exif iso = new Exif
                {
                    infoType = "iso",
                    value = (decimal)imageDto.iso
                };
                exifs.Add(iso);
            }
            if (!float.IsNaN(imageDto.whiteBalance))
            {
                Exif whiteBalance = new Exif
                {
                    infoType = "wb",
                    value = (decimal)imageDto.whiteBalance
                };
                exifs.Add(whiteBalance);
            }
            if (exifs.Count != 0) { image.Exifs = exifs; }
            return image;
        }

    }
}
