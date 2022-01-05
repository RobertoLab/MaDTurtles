using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class CommentConversor
    {
        public static CommentInfo ToCommentInfo(Comment comment)
        {
            CommentInfo commentInfo = new CommentInfo(comment.comment, 
                comment.uploadDate, comment.User.userName);
            return commentInfo;
        }

        public static List<CommentInfo> ToCommentInfos(List<Comment> comments)
        {
            List<CommentInfo> commentsInfo = new List<CommentInfo>();

            foreach (Comment comment in comments) {
                commentsInfo.Add(ToCommentInfo(comment));
            }

            return commentsInfo;
        }
    }
}
