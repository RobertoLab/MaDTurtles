﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public static class ImageConversor
    {
        public static ImageInfo ToImageInfo(Image image)
        { 
            ImageInfo imageInfo = new ImageInfo(image.title, image.description
                , image.uploadDate , image.categoryId, image.Category.category
                , null , image.userId, image.User.userName, image.Exifs.ToList());

            return imageInfo;
        }
        
        public static List<ImageInfo> ToImageInfos(List<Image> images)
        {
            List<ImageInfo> imagesInfo = null;
            images.ForEach(image => imagesInfo.Add(ToImageInfo(image)));

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
