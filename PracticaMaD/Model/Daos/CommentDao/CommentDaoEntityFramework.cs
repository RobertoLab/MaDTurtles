using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Photogram.Model.CommentDao
{
    /// <summary>
    /// Specific Operations for Comment
    /// </summary>
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> GetCommentsFromImage(long ImageId, int startIndex, int count)
        {
            DbSet<Comment> comments = Context.Set<Comment>();

           var result =
                (from comment in comments.Include("User")
                 orderby comment.commentId
                 where comment.imgId == ImageId
                 select comment).Skip(startIndex).Take(count).ToList<Comment>();

            return result;
        }

        public bool IsCommented(long ImageId)
        {
            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from comment in comments
                 where comment.imgId == ImageId
                 select comment).Any();

            return result;
        }

    }
}
