using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class ImageBasicInfo
    {
        public long imageId { get; set; }
        public string title { get; set; }
        public long userId { get; set; }
        public string userName { get; set; }
        public int likes { get; set; }
        public bool hasComments { get; set; }

        public ImageBasicInfo(long imageId, string title, long userId,
            string userName, int likes, bool hasComments)
        {
            this.title = title;
            this.userId = userId;
            this.userName = userName;
            this.likes = likes;
            this.hasComments = hasComments;
        }

        public override bool Equals(object obj)
        {
            var info = obj as ImageInfo;
            return info != null &&
                   title == info.title &&
                   userId == info.userId &&
                   userName == info.userName &&
                   hasComments == info.hasComments;
        }

        public override int GetHashCode()
        {
            var hashCode = -234955022;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + userId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(userName);
            return hashCode;
        }
    }

}