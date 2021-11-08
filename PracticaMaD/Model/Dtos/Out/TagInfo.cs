using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class TagInfo
    {
        public string tag { get; set; }
        public int tagCount { get; set; }

        public TagInfo(string tag, int tagCount)
        {
            this.tag = tag;
            this.tagCount = tagCount;
        }
    }
}
