using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class Block<T>
    {
        public List<T> items { get; private set; } 
        public bool existMoreItems { get; private set; }

        public Block(List<T> items, bool existMoreItems)
        {
            this.items = items;
            this.existMoreItems = existMoreItems;
        }

        public override bool Equals(object obj)
        {
            var block = obj as Block<T>;
            return block != null &&
                   EqualityComparer<List<T>>.Default.Equals(items, block.items) &&
                   existMoreItems == block.existMoreItems;
        }

        public override int GetHashCode()
        {
            var hashCode = -892010250;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<T>>.Default.GetHashCode(items);
            hashCode = hashCode * -1521134295 + existMoreItems.GetHashCode();
            return hashCode;
        }
    }
}
