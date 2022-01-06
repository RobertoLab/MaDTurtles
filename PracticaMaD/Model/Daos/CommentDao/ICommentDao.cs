using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;


namespace Es.Udc.DotNet.Photogram.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Gets the comments from an Image stored in the DB.
        /// </summary>
        /// <param name="ImgId">The image identifier.</param>
        /// <returns>The list of comments that belong to the image.</returns>
        List<Comment> GetCommentsFromImage(long ImgId, int startIndex, int count);

        /// <summary>
        /// Check if the image has any comment asociated.
        /// </summary>
        /// <param name="imgId">The image identifier.</param>
        /// <returns>True if the image has any comment asociated, false otherwise.</returns>
        bool IsCommented(long imgId);
    }
}
