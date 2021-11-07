using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Photogram.Model.CommentService
{
    public class CommentService : ICommentService
    {
        public CommentService() { }

        [Inject]
        public ICommentDao CommentDao { private get; set; }


        [Transactional]
        public void PostComment(string commentText, long userId, long imgId)
        {
            Comment comment = null;
            comment.imgId = imgId;
            comment.uploadDate = DateTime.Now;
            comment.userId = userId;
            comment.comment = commentText;
            CommentDao.Create(comment);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void DeleteComment(long commentId)
        {
            Comment comment = CommentDao.Find(commentId);
           
            CommentDao.Remove(commentId);
        }

        [Transactional]
        public void EditComment(long commentId, string newCommentText)
        {
            Comment comment = CommentDao.Find(commentId);
            comment.comment = newCommentText;
            comment.uploadDate = DateTime.Now;
            CommentDao.Update(comment);
        }

        [Transactional]
        public List<Comment> GetImageComments(long imgId)
        {
            return CommentDao.GetCommentsFromImage(imgId);
        }

    }
}
