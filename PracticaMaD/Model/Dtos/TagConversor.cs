using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public static class TagConversor
    {
        public static TagInfo ToTagInfo(Tag tag)
        {
            return new TagInfo(tag.tag, tag.imgCount);
        }

        public static List<TagInfo> ToTagInfos(List<Tag> tags)
        {
            List<TagInfo> tagInfos = new List<TagInfo>();
            tags.ForEach(tag => tagInfos.Add(ToTagInfo(tag)));
            return tagInfos;
        }
    }
}
