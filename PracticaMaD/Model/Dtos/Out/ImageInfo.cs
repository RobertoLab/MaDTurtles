using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class ImageInfo
    {
        public string title { get; set; }
        public string description { get; set; }
        public System.DateTime uploadDate { get; set; }
        public int categoryId { get; set; }
        public string category { get; set; }
        public string imgBase64 { get; set; }
        public long userId { get; set; }
        public string userName { get; set; }
        public List<Exif> metadata { get; set; }
        public List<ImageTag> tags { get; set; }
        public int likes { get; set; }

        public ImageInfo(string title, string description, DateTime uploadDate,
            int categoryId, string category, string imgBase64, long userId,
            string userName, List<Exif> metadata, List<ImageTag> tags, int likes)
        {
            this.title = title;
            this.description = description;
            this.uploadDate = uploadDate;
            this.categoryId = categoryId;
            this.category = category;
            this.imgBase64 = imgBase64;
            this.userId = userId;
            this.userName = userName;
            this.metadata = metadata;
            this.tags = tags;
            this.likes = likes;
        }

        public override bool Equals(object obj)
        {
            var info = obj as ImageInfo;
            return info != null &&
                   title == info.title &&
                   description == info.description &&
                   uploadDate == info.uploadDate &&
                   categoryId == info.categoryId &&
                   category == info.category &&
                   imgBase64 == info.imgBase64 &&
                   userId == info.userId &&
                   userName == info.userName &&
                   EqualityComparer<List<Exif>>.Default.Equals(metadata, info.metadata);
        }

        public override int GetHashCode()
        {
            var hashCode = -234955022;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            hashCode = hashCode * -1521134295 + uploadDate.GetHashCode();
            hashCode = hashCode * -1521134295 + categoryId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(category);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(imgBase64);
            hashCode = hashCode * -1521134295 + userId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(userName);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Exif>>.Default.GetHashCode(metadata);
            return hashCode;
        }
    }

}