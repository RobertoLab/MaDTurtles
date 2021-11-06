using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class ImageDto
    {
        public string title;
        public string description;
        public int categoryId;
        public string imgB64;
        public long userId;

        public ImageDto(string title, string description, int categoryId, string imgB64, long userId)
        {
            this.title = title;
            this.description = description;
            this.categoryId = categoryId;
            this.imgB64 = imgB64;
            this.userId = userId;
        }

        public override bool Equals(object obj)
        {
            var dto = obj as ImageDto;
            return dto != null &&
                   title == dto.title &&
                   description == dto.description &&
                   categoryId == dto.categoryId &&
                   imgB64 == dto.imgB64 &&
                   userId == dto.userId;
        }

        public override int GetHashCode()
        {
            var hashCode = -248841099;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            hashCode = hashCode * -1521134295 + categoryId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(imgB64);
            hashCode = hashCode * -1521134295 + userId.GetHashCode();
            return hashCode;
        }
    }
}
