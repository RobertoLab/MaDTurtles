using System;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using static Es.Udc.DotNet.Photogram.Model.Dtos.CommentConversor;

namespace Es.Udc.DotNet.Photogram.Model.CommentService
{
    public class CommentService : ICommentService
    {
        public CommentService() { }

        [Inject]
        public ICommentDao CommentDao { private get; set; }


        [Transactional]
        public Comment PostComment(string commentText, long userId, long imgId)
        {
            Comment comment = new Comment();
            comment.imgId = imgId;
            comment.uploadDate = DateTime.Now;
            comment.userId = userId;
            comment.comment = commentText;
            CommentDao.Create(comment);
            return comment;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void DeleteComment(long commentId)
        {
            Comment comment = CommentDao.Find(commentId);
           
            CommentDao.Remove(commentId);
        }

        [Transactional]
        public Comment EditComment(long commentId, string newCommentText)
        {
            Comment comment = CommentDao.Find(commentId);
            comment.comment = newCommentText;
            comment.uploadDate = DateTime.Now;
            CommentDao.Update(comment);
            return comment;
        }

        [Transactional]
        public Block<CommentInfo> GetImageComments(long imgId, int startIndex, int count)
        {
            List<Comment> commentsFound = CommentDao.GetCommentsFromImage(imgId, startIndex, count + 1);
            bool existsMoreComments = (commentsFound.Count == count + 1);

            if (existsMoreComments) commentsFound.RemoveAt(count);

            return new Block<CommentInfo>(ToCommentInfos(commentsFound), existsMoreComments);
        }

    }
}
