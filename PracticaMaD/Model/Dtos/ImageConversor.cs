using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public static class ImageConversor
    {
        public static ImageInfo ToImageInfo(Image image, string imageAsB64)
        { 
            ImageInfo imageInfo = new ImageInfo(image.title, image.description
                , image.uploadDate , image.categoryId, image.Category.category
                , imageAsB64, image.userId, image.User.userName, image.Exifs.ToList());

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
            return image;
        }

    }
}
